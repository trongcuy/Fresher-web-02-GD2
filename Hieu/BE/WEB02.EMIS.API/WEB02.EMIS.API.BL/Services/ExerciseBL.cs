using Aspose.Cells;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MISA.Web02.CeGov.Common;
using MISA.Web02.CeGov.Common.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB02.EMIS.API.BL.Interfaces;
using WEB02.EMIS.API.Common;
using WEB02.EMIS.API.Common.Entity;
using WEB02.EMIS.API.Common.Entity.DTO;
using WEB02.EMIS.API.Common.Enum;
using WEB02.EMIS.API.DL.Interfaces;

namespace WEB02.EMIS.API.BL.Services
{
    public class ExerciseBL : BaseBL<Exercise>, IExerciseBL
    {
        private IExerciseDL _exerciseDL;
        public IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public ExerciseBL(IExerciseDL exerciseDL, IMemoryCache cache) : base(exerciseDL)
        {
            _exerciseDL = exerciseDL;
            _cache = cache;
        }

        /// <summary>
        /// Hàm validate dữ liệu
        /// </summary>
        /// <param name="exercise"></param>
        /// <returns></returns>
        /// VMHieu 07/06/2023
        protected override bool Validate(Exercise exercise)
        {
            errorList.Clear();
            // Xử lý nghiệp vụ
            // Môn bài tập không được để trống
            if (exercise.GradeID == null || exercise.GradeID == Guid.Empty)
            {
                errorList.Add(Resources.ResourceManager.GetString(name: "InvalidGrade"));
            }
            // Khối lớp học không được để trống
            if (exercise.SubjectID == null || exercise.SubjectID == Guid.Empty)
            {
                errorList.Add(Resources.ResourceManager.GetString(name: "InvalidSubject"));
            }

            if (errorList.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Thực hiện lấy thông tin của 1 bản ghi bài tập theo id và các bản ghi phụ thuộc
        /// </summary>
        /// <param name="ExerciseID">ID bản ghi bài tập</param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 28/05/2023
        public ExerciseOverview GetAllByID(Guid ExerciseID)
        {
            return _exerciseDL.GetAllByID(ExerciseID);
        }

        /// <summary>
        /// Phân trang và tìm kiếm
        /// </summary>
        /// <param name="keyword">Từ khóa</param>
        /// <param name="grade">Khối lớp học</param>
        /// <param name="subject">Môn học</param>
        /// <param name="status">Trạng thái bài tập</param>
        /// <param name="skip">Số bản ghi bỏ qua</param>
        /// <param name="take">Số bản ghi lấy</param>
        /// <returns></returns>
        public PagingData<Exercise> GetPaging(string? keyword, Guid? grade, Guid? subject, int? status, int skip, int take)
        {
            return _exerciseDL.GetPaging(keyword, grade, subject, status, skip, take);
        }

        /// <summary>
        /// Thực hiện thêm bài tập, câu hỏi, đáp án và chủ đề liên quan
        /// </summary>
        /// <param name="exercise"></param>
        /// <param name="question"></param>
        /// <param name="answers"></param>
        /// <param name="topicIDs"></param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 06/06/2023
        public Guid InsertMultiple(DataInsertAll dataInsertAll)
        {
            var isValid = ValidateDataAll(dataInsertAll);
            if (isValid)
            {
                return _exerciseDL.InsertMultiple(dataInsertAll);
            }
            else
            {
                throw new ErrorException(devmsg: Resources.ResourceManager.GetString(name: "InvalidData"), listErrors: errorList);
            }
        }

        /// <summary>
        /// Thực hiện thêm câu hỏi, đáp án và chủ đề liên quan trong 1 bài tập đã có
        /// </summary>
        /// <param name="exercise"></param>
        /// <param name="question"></param>
        /// <param name="answers"></param>
        /// <param name="topicIDs"></param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 06/06/2023
        public Boolean UpdateMultiple(DataInsertAll dataInsertAll)
        {
            var isValid = ValidateDataAll(dataInsertAll);
            if (isValid)
            {
                return _exerciseDL.UpdateMultiple(dataInsertAll);
            }
            else
            {
                throw new ErrorException(devmsg: Resources.ResourceManager.GetString(name: "InvalidData"), listErrors: errorList);
            }
        }

        /// <summary>
        /// Validate dữ liệu khi thao tác với nhiều entity
        /// </summary>
        /// <param name="dataInsertAll"></param>
        /// <returns></returns>
        /// VMHieu 07/06/2023
        private bool ValidateDataAll(DataInsertAll dataInsertAll)
        {
            errorList.Clear();
            // Gán các đối tượng
            Exercise exercise = dataInsertAll.exercise;
            Question question = dataInsertAll.question;
            List<Answer> answers = dataInsertAll.answers;
            List<Guid> topicIDs = dataInsertAll.topicIDs;

            // Xử lý nghiệp vụ
            // Validate bài tập (môn , khối không được để trống)
            // Khối lớp học không được để trống
            if (exercise.GradeID == null || exercise.GradeID == Guid.Empty)
            {
                errorList.Add(Resources.ResourceManager.GetString(name: "InvalidGrade"));
            }
            // Môn học không được để trống
            if (exercise.SubjectID == null || exercise.SubjectID == Guid.Empty)
            {
                errorList.Add(Resources.ResourceManager.GetString(name: "InvalidSubject"));
            }
            // Validate câu hỏi (nội dung câu hỏi không được để trống)
            if (string.IsNullOrEmpty(question.QuestionContent) && (question.QuestionImage == null || question.QuestionImage == Guid.Empty))
            {
                errorList.Add(Resources.ResourceManager.GetString(name: "InvalidQuestionContent"));
            }
            // Validate đáp án (đáp án phải có nội dung, câu hỏi chọn và đúng sai phải có đáp án đúng, đặt limit cho đáp án)
            var answerValid = true;
            foreach(var answer in answers)
            {
                // Kiểm tra có nội dung hay không
                if (string.IsNullOrEmpty(answer.AnswerContent) && (answer.AnswerImage == null || answer.AnswerImage == Guid.Empty))
                {
                    answerValid = false;
                    break;
                }
                if (!Enum.IsDefined(typeof(AnswerStatus), answer.AnswerStatus))
                {
                    errorList.Add(Resources.ResourceManager.GetString(name: "AnswerStatus"));
                }
            }

            if (!answerValid)
            {
                errorList.Add(Resources.ResourceManager.GetString(name: "InvalidAnswerContent"));
            }
            // Kiểm tra phải có đáp án đúng trong 2 loại câu hỏi đúng sai và chọn
            if (question.TypeQuestion == TypeQuestion.Select || question.TypeQuestion == TypeQuestion.YesOrNo)
            {
                var answerCorrect = answers.Any(x => x.AnswerStatus == AnswerStatus.True);
                if (!answerCorrect)
                {
                    errorList.Add(Resources.ResourceManager.GetString(name: "NotAnswerCorrect"));
                }
            }

            // Kiểm tra số lượng đáp án
            switch (question.TypeQuestion)
            {
                case (TypeQuestion.Select):
                    if (answers.Count < 1 || answers.Count > 20)
                    {
                        errorList.Add(Resources.ResourceManager.GetString(name: "LimitAnswer"));
                    }
                    break;
                case (TypeQuestion.Fill):
                    if (answers.Count < 1)
                    {
                        errorList.Add(Resources.ResourceManager.GetString(name: "LimitAnswer"));
                    }
                    break;
                default:
                    break;
            }

            // Validate các trường enum
            // Trạng thái bài tập, loại câu hỏi, loại đáp án
            //if (!Enum.IsDefined(typeof(ExerciseStatus), exercise.ExerciseStatus))
            //{
            //    errorList.Add(Resources.ResourceManager.GetString(name: "ExerciseStatus"));
            //}
            //if (!Enum.IsDefined(typeof(TypeQuestion), question.TypeQuestion))
            //{
            //    errorList.Add(Resources.ResourceManager.GetString(name: "TypeQuestion"));
            //}


            // Trả về kết quả
            if (errorList.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Kiểm tra các bản ghi trong file import excel
        /// </summary>
        /// <param name="excelFile"></param>
        /// <returns>Số bản ghi hợp lệ, không hợp lệ và tổng số bản ghi</returns>
        /// CreatedBY VMHieu 19/04/2023
        public ExcelCheckTotal ExcelCheck(IFormFile excelFile)
        {
            // Kiểm tra định dạng của tệp (.xls, .xlsx):
            if (!Path.GetExtension(excelFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                errorList.Add(Resources.ResourceManager.GetString("ErrorImportFile"));
            }

            // Kiểm tra dung lượng của tệp:
            if (excelFile == null || excelFile.Length <= 0)
            {
                errorList.Add(Resources.ResourceManager.GetString("ErrorImportSize"));
            }

            // Tạo Dictionary cho loại câu hỏi
            var questionDictionary = new Dictionary<string, TypeQuestion>
            {
                { Resources.ResourceManager.GetString("TypeQuestion_Select").Trim().ToLower(), TypeQuestion.Select},
                { Resources.ResourceManager.GetString("TypeQuestion_YesOrNo").Trim().ToLower(), TypeQuestion.YesOrNo},
                { Resources.ResourceManager.GetString("TypeQuestion_Fill").Trim().ToLower(), TypeQuestion.Fill },
                { Resources.ResourceManager.GetString("TypeQuestion_Essay").Trim().ToLower(), TypeQuestion.Essay },
                { Resources.ResourceManager.GetString("TypeQuestion_Group").Trim().ToLower(), TypeQuestion.Group }
            };

            // Khởi tạo kết quả trả về
            var result = new ExcelCheckTotal()
            {
                Total = 0,
                TotalSuccess = 0,
                TotalFail = 0,
            };

            List<Question> questions = new List<Question>();
            
            List<ErrorCell> errorCells = new List<ErrorCell>();
            List<int> rowSuccess = new List<int>();
            List<DataExcel> dataResult = new List<DataExcel>();

            using (var stream = new MemoryStream())
            {
                excelFile.CopyToAsync(stream);

                Workbook workbook = new Workbook(stream);
                Worksheet worksheet = workbook.Worksheets[0];
                // Lấy dữ liệu trong file excel, bắt đầu từ dòng thứ 3
                for (int row = 4; row < worksheet.Cells.Rows.Count; row++)
                {
                    // Kiểm tra xem hàng có giá trị không
                    var checkValid = true;
                    for (var col = 1; col < worksheet.Cells.Columns.Count - 1; col++)
                    {
                        if (worksheet.Cells[row, col].Value != null)
                        {
                            checkValid = false;
                            break;
                        }
                    }
                    if (checkValid)
                    {
                        break;
                    }
                    // Tạo ID của question
                    Guid questionID = Guid.NewGuid();
                    // Tạo list đáp án
                    List<Answer> answers = new List<Answer>();
                    // Check giá trị của các ô input trong excel
                    var checkEqual = true;

                    // Khởi tạo các trường dữ liệu
                    var sortOder = "";
                    var typeQuestion = "";
                    var questionContent = "";
                    var questionNote = "";
                    string[] answerCorrect = new string[0];
                    // Kiểm tra require trong bản ghi
                    for (int col = 0; col < 3; col++)
                    {
                        if (worksheet.Cells[row, col].Value == null)
                        {
                            checkEqual = false;
                            var errorCell = new ErrorCell()
                            {
                                Row = row,
                                Col = col,
                                Message = Resources.ResourceManager.GetString(name: "ErrorCellRequire")
                            };
                            errorCells.Add(errorCell);
                        }
                        else
                        {
                            switch (col)
                            {
                                case 0:
                                    // SortOder của câu hỏi
                                    sortOder = worksheet.Cells[row, 0].Value.ToString();
                                    break;
                                case 1:
                                    // Loại câu hỏi
                                    var typeQuestionRow = worksheet.Cells[row, 1].Value.ToString().Trim().ToLower();
                                    if (questionDictionary.ContainsKey(typeQuestionRow))
                                    {
                                        typeQuestion = questionDictionary[typeQuestionRow].ToString();
                                    }
                                    else
                                    {
                                        checkEqual = false;
                                        var errorCell = new ErrorCell()
                                        {
                                            Row = row,
                                            Col = 3,
                                            Message = Resources.ResourceManager.GetString(name: "ErrorCellNotValid")
                                        };
                                        errorCells.Add(errorCell);
                                    }
                                    break;
                                case 2:
                                    questionContent = worksheet.Cells[row, 2].Value.ToString();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    // Kiểm tra 3 cột cuối trong bản ghi
                    for (int col = worksheet.Cells.Columns.Count - 2; col >= worksheet.Cells.Columns.Count - 4; col--)
                    {
                        if (worksheet.Cells[row, col].Value != null)
                        {
                            switch (col)
                            {
                                // Câu hỏi nhóm 
                                case var _ when col == worksheet.Cells.Columns.Count - 2:
                                    break;
                                // Lời giải thêm cho câu hỏi
                                case var _ when col == worksheet.Cells.Columns.Count - 3:
                                    questionNote = worksheet.Cells[row, col].Value.ToString();
                                    break;
                                // Đáp án đúng
                                case var _ when col == worksheet.Cells.Columns.Count - 4:
                                    answerCorrect = worksheet.Cells[row, col].Value.ToString().Split("||");
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    // Kiểm tra các cột đáp án ở giữa
                    for (int col = 3; col < worksheet.Cells.Columns.Count - 4; col++)
                    {
                        if (worksheet.Cells[row, col].Value != null)
                        {
                            var answerContent = worksheet.Cells[row, col].Value.ToString();
                            var sortOderAnswer = (col - 2);
                            var answer = new Answer()
                            {
                                AnswerContent = answerContent,
                                SortOder = sortOderAnswer.ToString()
                            };
                            // Tọa độ đáp án theo ô đáp án đúng
                            var index = (col - 2).ToString();
                            if (answerCorrect.Contains(index))
                            {
                                answer.AnswerStatus = AnswerStatus.True;
                            } else
                            {
                                answer.AnswerStatus = AnswerStatus.False;
                            }

                            answers.Add(answer);
                        }
                    }

                    // Kiểm tra giá trị đầu vào
                    // Nếu phù hợp thì lưu vào cache
                    if (checkEqual)
                    {
                        var question = new Question()
                        {
                            QuestionContent = questionContent,
                            QuestionNote = questionNote,
                            TypeQuestion = (TypeQuestion)Enum.Parse(typeof(TypeQuestion), typeQuestion),
                            SortOder = sortOder
                        };

                        var dataExcel = new DataExcel()
                        {
                            question = question,
                            answers = answers
                        };
                        var errorList = ValidateExcel(dataExcel);
                        if (errorList.Count == 0)
                        {
                            dataResult.Add(dataExcel);
                            result.TotalSuccess++;
                            rowSuccess.Add(row);
                        }
                        else
                        {
                            result.TotalFail++;
                            var errorCell = new ErrorCell()
                            {
                                Row = row,
                                Col = worksheet.Cells.Columns.Count - 1,
                                Message = string.Join(";", errorList),
                                Context = "Lỗi"
                            };
                            errorCells.Add(errorCell);
                        }

                    }
                    else
                    {
                        result.TotalFail++;
                    }

                    result.Total = result.TotalFail + result.TotalSuccess;
                }
                _cache.Set("excelFile", stream.ToArray(), TimeSpan.FromMinutes(60));
            }
            _cache.Set("data", dataResult, TimeSpan.FromDays(1));
            _cache.Set("errorCell", errorCells, TimeSpan.FromMinutes(60));
            _cache.Set("rowSuccess", rowSuccess, TimeSpan.FromMinutes(60));
            return result;
        }

        /// <summary>
        /// Lấy dữ liệu file excel sau khi check
        /// </summary>
        /// <param name="statusCheck">true - lấy tất cả, false - lấy các bản ghi lỗi</param>
        /// <returns></returns>
        /// CreatedBy VMHieu 11/06/2023
        public MemoryStream GetExcelFileCheck(Boolean statusCheck = false)
        {
            var excel = _cache.Get<byte[]>("excelFile");
            List<ErrorCell> errorCells = (List<ErrorCell>)_cache.Get("errorCell");
            List<int> rowSuccess = (List<int>)_cache.Get("rowSuccess");

            using (MemoryStream stream = new MemoryStream(excel))
            {
                // Load the Excel file
                Workbook workbook = new Workbook(stream);

                // Get the first worksheet
                Worksheet worksheet = workbook.Worksheets[0];

                // Remove các ô lỗi trước đó
                for (int row = 4; row < worksheet.Cells.Rows.Count; row++)
                {
                    for (int col = 0; col < worksheet.Cells.Columns.Count; col++)
                    {
                        RemoveError(worksheet, row, col);
                    }
                }
                // Thêm các ô lỗi mới
                foreach (var cell in errorCells)
                {
                    AddError(worksheet, cell.Row, cell.Col, cell.Message, cell.Context);
                }

                if (!statusCheck)
                {
                    int index = 0;
                    foreach (var row in rowSuccess)
                    {
                        worksheet.Cells.DeleteRow(row - index);
                        index++;
                    }
                }

                // Create a new MemoryStream object
                MemoryStream ms = new MemoryStream();
                // Save the workbook to the MemoryStream
                workbook.Save(ms, SaveFormat.Xlsx);

                // Set the position of the MemoryStream to the beginning
                ms.Seek(0, SeekOrigin.Begin);

                return ms;
            }
        }

        /// <summary>
        /// Thưc hiện import từ dữ liệu file
        /// </summary>
        /// <returns></returns>
        /// CreatedBy VMHieu 11/06/2023
        public Guid Import(ExerciseData exerciseData)
        {
            List<DataExcel> dataExcels = (List<DataExcel>)(IEnumerable<DataExcel>)_cache.Get("data");

            return _exerciseDL.Import(exerciseData, dataExcels);
        }

        /// <summary>
        /// Check dữ liệu excel
        /// </summary>
        /// <param name="dataExcel"></param>
        /// <returns></returns>
        /// VMHieu 11/06/2023
        public List<string> ValidateExcel(DataExcel dataExcel)
        {
            // Chuẩn bị danh sách lỗi
            List<string> errorList = new List<string>();
            // Chuẩn bị các dữ liệu cần check
            Question question = dataExcel.question;
            List<Answer> answers = dataExcel.answers;
            // Kiểm tra phải có đáp án đúng trong 2 loại câu hỏi đúng sai và chọn
            if (question.TypeQuestion == TypeQuestion.Select || question.TypeQuestion == TypeQuestion.YesOrNo)
            {
                var answerCorrect = answers.Any(x => x.AnswerStatus == AnswerStatus.True);
                if (!answerCorrect)
                {
                    errorList.Add(Resources.ResourceManager.GetString(name: "NotAnswerCorrect"));
                }
            }

            // Kiểm tra số lượng đáp án
            switch (question.TypeQuestion)
            {
                case (TypeQuestion.Select):
                    if (answers.Count < 1 || answers.Count > 20)
                    {
                        errorList.Add(Resources.ResourceManager.GetString(name: "LimitAnswer"));
                    }
                    break;
                case (TypeQuestion.YesOrNo):
                    if (answers.Count != 2)
                    {
                        errorList.Add(Resources.ResourceManager.GetString(name: "LimitAnswer"));
                    }
                    break;
                case (TypeQuestion.Fill):
                    if (answers.Count < 1)
                    {
                        errorList.Add(Resources.ResourceManager.GetString(name: "LimitAnswer"));
                    }
                    break;
                default:
                    break;
            }
            return errorList;
        }

        /// <summary>
        /// Tạo một ô cảnh báo với comment msg
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="msg"></param>
        /// CreatedBy VMHieu 11/06/2023
        public void AddError(Worksheet worksheet, int row, int col, string msg, string? context)
        {
            // Get the cell
            Cell cell = worksheet.Cells[row, col];
            if (!string.IsNullOrEmpty(context))
            {
                cell.Value = context;
            }
            int commentIndex = worksheet.Comments.Add(row, col);

            Comment comment = worksheet.Comments[commentIndex];

            comment.Note = msg;
            // Create a style object
            Style style = cell.GetStyle();

            // Set the color of the left border
            style.SetBorder(BorderType.LeftBorder, CellBorderType.Thin, Color.Red);

            // Set the color of the right border
            style.SetBorder(BorderType.RightBorder, CellBorderType.Thin, Color.Red);

            // Set the color of the top border
            style.SetBorder(BorderType.TopBorder, CellBorderType.Thin, Color.Red);

            // Set the color of the bottom border
            style.SetBorder(BorderType.BottomBorder, CellBorderType.Thin, Color.Red);

            // Apply the style to the cell
            cell.SetStyle(style);
        }

        /// <summary>
        /// Hủy ô cảnh báo với comment msg
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// CreatedBy VMHieu 11/06/2023
        public void RemoveError(Worksheet worksheet, int row, int col)
        {
            // Get the cell
            Cell cell = worksheet.Cells[row, col];
            worksheet.Comments.RemoveAt(row, col);

            // Create a style object
            Style style = cell.GetStyle();

            // Set the color of the left border
            style.SetBorder(BorderType.LeftBorder, CellBorderType.Thin, Color.Black);

            // Set the color of the right border
            style.SetBorder(BorderType.RightBorder, CellBorderType.Thin, Color.Black);

            // Set the color of the top border
            style.SetBorder(BorderType.TopBorder, CellBorderType.Thin, Color.Black);

            // Set the color of the bottom border
            style.SetBorder(BorderType.BottomBorder, CellBorderType.Thin, Color.Black);

            // Apply the style to the cell
            cell.SetStyle(style);
        }
    }
}
