using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells;
using Aspose.Cells.Charts;
using EMIS.Common;
using EMIS.Common.DTO;
using EMIS.Common.Entity;
using EMIS.Common.ExceptionEntity;
using EMIS.DL.BaseDL;
using EMIS.DL.ExcelDL;
using EMIS.DL.QuestionDL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace EMIS.BL.ExcelBL
{
    public class ExcelBL:IExcelBL
    {
        #region Fields 
        private List<string> errorList;
        private IMemoryCache _cache;
        private IExcelDL _excelDL;
        #endregion

        #region Constructor
        public ExcelBL(IMemoryCache cache, IExcelDL excelDL)
        {
            _cache = cache;
            errorList = new List<string>();
            _excelDL = excelDL;
        }
        #endregion

        #region 

        /// <summary>
        /// dowload fiel excel mẫu
        /// CreatedBy: Trịnh Huỳnh Đức (8-6-2023)
        /// </summary>
        /// <returns></returns>
        public byte[] DownloadExcelSample()
        {
            // Đường dẫn đến file XLSX trong thư mục Backend
            string filePath = "C:\\Users\\DELL\\OneDrive\\Tài liệu\\TaiLieuCodeVue\\Fresher-web-02-GD2\\Duc\\BE\\MISA_BE_GD2_EMIS\\Common\\Excel\\Mau_nhap_khau_cau_hoi_loai_1.xlsx";

            // Kiểm tra sự tồn tại của file
            if (System.IO.File.Exists(filePath))
            {
                byte[]  fileByte = System.IO.File.ReadAllBytes(filePath);
            }
            return null;
        }

        /// <summary>
        /// up load file excel rồi validate và trả về số bản ghi hợp lệ, không hợp lệ
        /// CreatedBy: Trịnh Huỳnh Đức (13-6-2023)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public Object CheckExcel(IFormFile file)
        {
            // Đường dẫn tới file Excel tạm thời
            var filePath = Path.GetTempFileName();

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // Mở tệp Excel sử dụng Aspose.Cells
            var workbook = new Workbook(filePath);

            // Xử lý nội dung của tệp Excel
            var worksheet = workbook.Worksheets[0];
            var cells = worksheet.Cells;

            //khởi tạo các giá trị ban đầu
            int valid = 0;
            int invalid = 0;
            //lưu các dòng lỗi
            List<ExcelError> excelErrors = new List<ExcelError>();

            for (int row = 4; row < cells.MaxDataRow; row++)
            {
                //kiểm tra xem hàng ko có giá trị thì bỏ đi
                bool rowNull = true;
                for (int col = 1; col < cells.MaxDataColumn; col++)
                {
                    if (cells[row, col].StringValue != "")
                    {
                        rowNull = false;
                        break;
                    }
                }
                //nếu hàng không có giá trị thì bỏ qua
                if (rowNull)
                {
                    continue;
                }

                //tạo một đối tượng ExcelRow
                List<string> answers = new List<string>();
                for (int col = 3; col < cells.MaxDataColumn - 3; col++)
                {
                    if (cells[row, col].StringValue != "")
                        answers.Add(cells[row, col].StringValue);
                }
                ExcelRow excelRow = new ExcelRow()
                {
                    QuestionContent = cells[row, 2].StringValue,
                    QuestionType = cells[row, 1].StringValue,
                    QuestionExplane = cells[row, cells.MaxDataColumn - 2].StringValue,
                    QuestionNumber = cells[row, 0].StringValue,
                    AnswerTrue = cells[row, cells.MaxDataColumn - 3].StringValue,
                    Answers = answers
                };

                
                //validate đối tượng excelrow
                if (ValidateExcelRow(excelRow))
                {
                    valid++;
                }
                else
                {
                    //tạo list mới tránh bị tham chiếu
                    List<string> newErrorList = new List<string>();
                    newErrorList.AddRange(errorList);

                    //thêm hàng vào danh sách không hợp lệ
                    excelErrors.Add(new ExcelError()
                    {
                        RowIndex = row,
                        Errors = newErrorList
                    });
                    invalid++;
                }
            }

            // Xóa file tạm thời
            System.IO.File.Delete(filePath);

            //lưu dữ liệu vào bộ nhớ tạm
            _cache.Set("InvalidRecords", excelErrors, DateTimeOffset.Now.AddMinutes(60));
            _cache.Set("FileExcel", worksheet, DateTimeOffset.Now.AddMinutes(60));

            var result = new
            {
                validCount = valid,
                invalidCount = invalid
            };
            return result;
        }

        

        /// <summary>
        /// validate từng hàng trong bảng
        /// CreatedBy: Trịnh Huỳnh Đức (13-6-2023)
        /// </summary>
        /// <returns></returns>
        private bool ValidateExcelRow(ExcelRow excelRow)
        {
            errorList.Clear();
            bool result = true;
            //kiểm tra nội dung câu hỏi, loại câu hỏi, đáp án không được để trống
            if (excelRow.QuestionContent == "")
            {
                result = false;
                errorList.Add(Resource.ResourceManager.GetString(name: "ExcelContenNull"));
            }
            if (excelRow.QuestionType == "")
            {
                result = false;
                errorList.Add(Resource.ResourceManager.GetString(name: "ExcelTypeNull"));
            }
            if (excelRow.AnswerTrue == "" && (excelRow.QuestionType != "Tự luận" && excelRow.QuestionType != "Điền vào chỗ trống"))
            {
                result = false;
                errorList.Add(Resource.ResourceManager.GetString(name: "ExcelAnswerTrueNull"));
            }

            //Kiểm tra số đáp án phải đủ và đúng số lượng >0, và =2 với câu đúng sai
            switch (excelRow.QuestionType)
            {
                case "Đúng sai":
                    if (excelRow.Answers.Count != 2)
                    {
                        result = false;
                        errorList.Add(Resource.ResourceManager.GetString(name: "ExcelNumAnswerInvalid"));
                    }
                    break;
                case "Điền vào chỗ trống":
                    if (excelRow.Answers.Count == 0)
                    {
                        result = false;
                        errorList.Add(Resource.ResourceManager.GetString(name: "ExcelNumAnswerInvalid"));
                    }
                    break;
                case "Chọn đáp án":
                    if (excelRow.Answers.Count == 0)
                    {
                        result = false;
                        errorList.Add(Resource.ResourceManager.GetString(name: "ExcelNumAnswerInvalid"));
                    }
                    break;
                default:
                    break;
            }

            //kiểm tra đáp án đúng có hợp lệ không có 1 đ/a hoặc nhiều đ/a cách bởi dấu ||
            if (excelRow.AnswerTrue != "" && excelRow.QuestionType!="Tự luận")
            {
                string[] answerTrue = excelRow.AnswerTrue.Split(new string[] { "||" }, StringSplitOptions.None);
                for (int i = 0; i<answerTrue.Length; i++)
                {
                    int num;
                    if (!int.TryParse(answerTrue[i], out num))
                    {
                        result = false;
                        errorList.Add(Resource.ResourceManager.GetString(name: "ExcelAnswerTrueInvalid"));
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Lưu các bản ghi hợp lệ vào database
        /// CreatedBy: Trịnh Huỳnh Đức (14-6-2023)
        /// </summary>
        /// <returns></returns>
        public string InsertExcelToDB(Exercise exercise)
        {
            // Lấy danh sách bản ghi hợp lệ từ bộ nhớ cache
            if (_cache.TryGetValue("InvalidRecords", out List<ExcelError> invalidRecords) && _cache.TryGetValue("FileExcel", out Worksheet worksheet))
            {
                // Tạo một Workbook mới
                var validWorkbook = new Workbook();

                // Lấy Sheet đầu tiên từ Workbook mới
                var validWorksheet = validWorkbook.Worksheets[0];

                // Sao chép nội dung từ tệp tin Excel ban đầu vào Workbook mới
                validWorksheet.Copy(worksheet);
                var cells = validWorksheet.Cells;

                // tạo list các dòng là đối tượng ExcelRow để truyền sang DL lưu vào db
                List<ExcelRow> excelRows = new List<ExcelRow>();

                for (int row = 4; row < cells.MaxDataRow; row++)
                {
                    //nếu nó là bản ghi hợp lệ và ko phải dòng trống (kiểm tra nội dung khác null là được)
                    if((!invalidRecords.Any(error => error.RowIndex == row)) && (cells[row, 2].StringValue != ""))
                    {
                        //tạo một đối tượng ExcelRow
                        //lấy đáp án
                        List<string> answers = new List<string>();
                        for (int col = 3; col < cells.MaxDataColumn - 3; col++)
                        {
                            if (cells[row, col].StringValue != "")
                                answers.Add(cells[row, col].StringValue);
                        }

                        ExcelRow excelRow = new ExcelRow()
                        {
                            QuestionContent = cells[row, 2].StringValue,
                            QuestionType = cells[row, 1].StringValue,
                            QuestionExplane = cells[row, cells.MaxDataColumn - 2].StringValue,
                            QuestionNumber = cells[row, 0].StringValue,
                            AnswerTrue = cells[row, cells.MaxDataColumn - 3].StringValue,
                            Answers = answers
                        };
                        excelRows.Add(excelRow);
                    }                   
                }

                // Lưu danh sách bản ghi hợp lệ vào cơ sở dữ liệu
                return _excelDL.InsertExcelToDB(exercise, excelRows);
            }
            return "";
        }

        /// <summary>
        /// Set giá trị và màu đỏ cho ô cảnh báo lỗi
        /// CreatedBy: Trịnh Huỳnh Đức (14-6-2023)
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="cellValue"></param>
        private void SetValueCell(Worksheet worksheet, int rowIndex, int columnIndex, string cellValue)
        {
            // Đặt giá trị cho ô
            worksheet.Cells[rowIndex, columnIndex].PutValue(cellValue);

            // Lấy đối tượng Style của ô
            Style style = worksheet.Cells[rowIndex, columnIndex].GetStyle();

            // Lấy đối tượng Font của ô
            Font font = style.Font;

            // Đặt màu chữ thành màu đỏ
            font.Color = Color.Red;

            // Đặt kiểu chữ in đậm
            if (rowIndex == 3)
                font.IsBold = true;

            // Áp dụng định dạng cho ô
            worksheet.Cells[rowIndex, columnIndex].SetStyle(style);
        }

        /// <summary>
        /// tạo file excel lỗi và gán thông báo lỗi vào 
        /// CreatedBy: Trịnh Huỳnh Đức (14-6-2023)
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="excelErrors"></param>
        private byte[] GenerateInvalidFile(Worksheet worksheet, List<ExcelError> excelErrors)
        {
            // Tạo một Workbook mới
            var invalidWorkbook = new Workbook();

            // Lấy Sheet đầu tiên từ Workbook mới
            var invalidWorksheet = invalidWorkbook.Worksheets[0];

            // Sao chép nội dung từ tệp tin Excel ban đầu vào Workbook mới
            invalidWorksheet.Copy(worksheet);
            var invalidCells = invalidWorksheet.Cells;

            //insert cột thông báo lỗi vào cuối
            invalidCells.InsertColumn(invalidCells.MaxDataColumn);
            invalidCells.SetColumnWidth(invalidCells.MaxDataColumn -1, 100);
            SetValueCell(invalidWorksheet, 3, invalidCells.MaxDataColumn -1, "Lỗi soạn câu");

            //đọc qua từng dòng lỗi và gán thông báo vào
            foreach (ExcelError item in excelErrors)
            {
                string valueCell = "";
                foreach (string error in item.Errors)
                {
                    valueCell = valueCell + error + ". ";
                }
                SetValueCell(invalidWorksheet, item.RowIndex, invalidCells.MaxDataColumn -1, valueCell);
            }

            // Đọc dữ liệu của tệp tin lỗi thành một mảng byte
            using (var memoryStream = new MemoryStream())
            {
                // Lưu Workbook mới vào một MemoryStream
                invalidWorkbook.Save(memoryStream, SaveFormat.Xlsx);
                // Chuyển MemoryStream thành mảng byte
                byte[] fileBytes = memoryStream.ToArray();
                return fileBytes;
            }
        }

        /// <summary>
        /// lấy file không hợp lệ để người dùng sửa lại
        /// CreatedBy: Trịnh Huỳnh Đức (13-6-2023)
        /// </summary>
        /// <returns></returns>
        public byte[] DownloadExcelInvalid()
        {
            // Lấy danh sách bản ghi ban đầu và các dòng lỗi từ bộ nhớ cache
            if (_cache.TryGetValue("FileExcel", out Worksheet worksheet) && _cache.TryGetValue("InvalidRecords", out List<ExcelError> excelErrors))
            {
                // tạo và trả về file lỗi
                return GenerateInvalidFile(worksheet, excelErrors);
            }

            return null;
        }
        #endregion
    }
}
