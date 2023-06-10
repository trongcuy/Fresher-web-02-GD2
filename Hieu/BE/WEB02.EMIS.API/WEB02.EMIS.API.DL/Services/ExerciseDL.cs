using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Web02.CeGov.Common;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB02.EMIS.API.Common;
using WEB02.EMIS.API.Common.Entity;
using WEB02.EMIS.API.Common.Entity.DTO;
using WEB02.EMIS.API.DL.Interfaces;

namespace WEB02.EMIS.API.DL.Services
{
    public class ExerciseDL : BaseDL<Exercise>, IExerciseDL
    {
        public ExerciseDL(IConfiguration configuration) : base(configuration)
        {
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
            using (mySqlConnection = new MySqlConnection(ConnectionString))
            {
                // Chuẩn bị câu lệnh Proc
                var sqlCommand = "Proc_Exercise_GetAllByID";

                // Chuẩn bị tham số đầu vào cho câu lệnh sql
                var parameters = new DynamicParameters();
                parameters.Add($"ExerciseID", ExerciseID);

                // Thực hiện gọi vào db để chạy câu lệnh với tham số đầu vào ở trên
                var result = mySqlConnection.QueryMultiple(sqlCommand, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về ở db
                ExerciseOverview exercise = new ExerciseOverview()
                {
                    Exercise = (List<Exercise>)result.Read<Exercise>(),
                    Question = (List<Question>)result.Read<Question>(),
                    Answer = (List<Answer>)result.Read<Answer>(),
                };
                if (exercise == null)
                {
                    throw new ErrorException(devmsg: Resources.ResourceManager.GetString(name: "NullData"));
                }
                return exercise;
            }
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
        public PagingData<Exercise> GetPaging(string? keyword, Guid? grade, Guid? subject, int? status, 
            int skip = 0 , int take = 9) 
        {
            using (mySqlConnection = new MySqlConnection(ConnectionString))
            {
                // Chuẩn bị tên Stored procedure
                var pagingCommand = "Proc_Exercise_Paging";

                // Chuẩn bị tham số đầu vào cho stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@$Skip", skip);
                parameters.Add("@$Take", take);
                parameters.Add("@$Sort", "");

                var filterString = new List<string>();
                var filterOption = new List<string>();
                string stringAND = "";
                string stringOR = "";
                string whereClause = "";
                // Kiểm tra keyword
                if (keyword != null)
                {
                    filterString.Add($"ExerciseName LIKE '%{keyword}%'");
                }
                // Kiểm tra khối lớp
                if (grade != null)
                {
                    filterOption.Add($"GradeID = '{grade}'");
                }
                // Kiểm tra môn học
                if (subject != null)
                {
                    filterOption.Add($"SubjectID = '{subject}'");
                }
                // Kiểm tra trạng thái
                if (status != null)
                {
                    filterOption.Add($"ExerciseStatus = {status}");
                }

                // Nếu bộ lọc có giá trị
                if (filterOption.Count > 0)
                {
                    stringAND = $"{string.Join(" AND ", filterOption)}";
                }
                // Nếu ô tìm kiếm có giá trị
                if (filterString.Count > 0)
                {
                    stringOR = $"({string.Join(" OR ", filterString)})";
                }
                // Nếu cả 2 có giá trị thì thực hiện nối
                if (stringAND != "" && stringOR != "")
                {
                    whereClause = stringOR + " AND " + stringAND;
                }
                else if (stringAND == "")
                {
                    whereClause = stringOR;
                }
                else if (stringOR == "")
                {
                    whereClause = stringAND;
                }
                // Nếu câu lệnh where có giá trị
                if (whereClause != "")
                {
                    whereClause = "WHERE " + whereClause;
                }

                parameters.Add("@$Where", whereClause);

                // Thực hiện gọi vào DB để chạy stored procedure với tham số đầu vào ở trên
                var multipleResults = mySqlConnection.QueryMultiple(pagingCommand, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về từ DB
                PagingData<Exercise> pagingData = new PagingData<Exercise>()
                {
                    Data = (List<Exercise>)multipleResults.Read<Exercise>(),
                    TotalCount = multipleResults.Read<int>().Single(),
                };
                return pagingData;
            }
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
            using (mySqlConnection = new MySqlConnection(ConnectionString))
            {
                if (mySqlConnection.State != ConnectionState.Open)
                {
                    mySqlConnection.Open();
                }
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        Guid idExercise = Guid.NewGuid();
                        // Khởi tạo đối tượng param
                        var parameters = new DynamicParameters();
                        // Gán các đối tượng
                        Exercise exercise = dataInsertAll.exercise;
                        Question question = dataInsertAll.question;
                        List<Answer> answers = dataInsertAll.answers;
                        List<Guid> topicIDs = dataInsertAll.topicIDs;
                        if (exercise.ExerciseID != null && exercise.ExerciseID != Guid.Empty)
                        {
                            idExercise = exercise.ExerciseID;
                            // Cập nhật bản ghi bài tập
                            var updateCommand = $"Proc_Exercise_Update";

                            // Chuẩn bị các tham số đầu vào
                            parameters = new DynamicParameters(exercise);
                            parameters.Add($"ExerciseID", exercise.ExerciseID);

                            // Thực hiện gọi vào db để chạy câu lệnh 
                            var resultExercise = mySqlConnection.Execute(updateCommand, param: parameters,
                                transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                            if (resultExercise == null)
                            {
                                transaction.Rollback();
                                return Guid.Empty;
                            }
                        } else
                        {
                            // Thêm một bản ghi bài tập
                            // Chuẩn bị câu lệnh 
                            var insertCommand = $"Proc_Exercise_Insert";

                            // Chuẩn bị các tham số đầu vào
                            parameters = new DynamicParameters(exercise);

                            // Thực hiện gọi vào db để chạy câu lệnh 
                            string resultExercise = mySqlConnection.QueryFirstOrDefault<string>(insertCommand, param: parameters,
                                transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                            if (idExercise == null)
                            {
                                transaction.Rollback();
                                return Guid.Empty;
                            } else
                            {
                                idExercise = Guid.Parse(resultExercise);
                            }
                        }
                        

                        // Thêm một bản ghi câu hỏi
                        question.ExerciseID = idExercise;

                        // Chuẩn bị câu lệnh 
                        var insertQuestion = $"Proc_Question_Insert";

                        // Chuẩn bị các tham số đầu vào
                        parameters = new DynamicParameters(question);

                        // Thực hiện gọi vào db để chạy câu lệnh 
                        string idQuestion = mySqlConnection.QueryFirstOrDefault<string>(insertQuestion, param: parameters,
                           transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                        if (idQuestion == null)
                        {
                            transaction.Rollback();
                            return Guid.Empty;
                        }
                        if (answers.Count > 0)
                        {
                            // Thêm id câu hỏi vào các đáp án
                            foreach (var answer in answers)
                            {
                                answer.QuestionID = Guid.Parse(idQuestion);
                            }
                            // Thực hiện thêm các câu hỏi
                            var insertAnswerRow = 0;
                            var answerCommand = $"Proc_Answer_Insert";
                            foreach (var answer in answers)
                            {
                                insertAnswerRow += mySqlConnection.Execute(answerCommand, param: answer, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                            }
                            if (insertAnswerRow != answers.Count)
                            {
                                transaction.Rollback();
                                return Guid.Empty;
                            }
                        }
                        
                        if (topicIDs.Count > 0)
                        {
                            //Thực hiện thêm chủ đề

                            // Chuẩn bị câu lệnh Proc
                            var sqlCommand = "Proc_ExerciseTopic_InsertMultiple";

                            // Biến đổi tham số đầu vào
                            string topicStr = "";
                            foreach (var topicID in topicIDs)
                            {
                                if (topicID.Equals(topicIDs.Last()))
                                {
                                    topicStr += $"{topicID}";
                                }
                                else
                                {
                                    topicStr += $"{topicID},";
                                }
                            }

                            // Chuẩn bị tham số đầu vào cho câu lệnh sql
                            parameters = new DynamicParameters();
                            parameters.Add($"$ExerciseID", idExercise);
                            parameters.Add($"topicIDs", topicStr);

                            // Thực hiện gọi vào db để chạy câu lệnh với tham số đầu vào ở trên
                            var resultTopics = mySqlConnection.Execute(sqlCommand, parameters, transaction: transaction,
                                commandType: System.Data.CommandType.StoredProcedure);

                            if (resultTopics == 0)
                            {
                                transaction.Rollback();
                                return Guid.Empty;
                            }
                        }

                        transaction.Commit();
                        return idExercise;

                    } catch (Exception e)
                    {
                        transaction.Rollback();
                        throw new ErrorException(devmsg: e.Message);
                    } finally
                    {
                        if (mySqlConnection.State != ConnectionState.Closed)
                        {
                            mySqlConnection.Close();
                        }
                    }
                }
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
            using (mySqlConnection = new MySqlConnection(ConnectionString))
            {
                if (mySqlConnection.State != ConnectionState.Open)
                {
                    mySqlConnection.Open();
                }
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        // Gán các đối tượng
                        Exercise exercise = dataInsertAll.exercise;
                        Question question = dataInsertAll.question;
                        List<Answer> answers = dataInsertAll.answers;
                        List<Guid> topicIDs = dataInsertAll.topicIDs;

                        // Sửa một bản ghi bài tập
                        // Chuẩn bị câu lệnh 
                        var updateExercise = $"Proc_Exercise_Update";

                        // Chuẩn bị các tham số đầu vào
                        var parameters = new DynamicParameters(exercise);
                        parameters.Add($"ExerciseID", exercise.ExerciseID);

                        // Thực hiện gọi vào db để chạy câu lệnh 
                        var rowExercise = mySqlConnection.Execute(updateExercise, param: parameters,
                            transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                        if (rowExercise == null)
                        {
                            transaction.Rollback();
                            return false;
                        }

                        // Thêm một bản ghi câu hỏi
                        question.ExerciseID = exercise.ExerciseID;

                        // Chuẩn bị câu lệnh 
                        var updateQuestion = $"Proc_Question_Update";

                        // Chuẩn bị các tham số đầu vào
                        parameters = new DynamicParameters(question);
                        parameters.Add($"QuestionID", question.QuestionID);

                        // Thực hiện gọi vào db để chạy câu lệnh 
                        var rowQuestion = mySqlConnection.Execute(updateQuestion, param: parameters,
                           transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                        if (rowQuestion == null)
                        {
                            transaction.Rollback();
                            return false;
                        }

                        if (answers.Count > 0)
                        {
                            // Thêm id câu hỏi vào các đáp án
                            foreach (var answer in answers)
                            {
                                answer.QuestionID = question.QuestionID;
                            }
                            // Thực hiện xóa các đáp án trước đó của câu hỏi 

                            var deleteAnswer = $"Proc_Answer_DeleteByQuestionID";
                            parameters = new DynamicParameters();
                            parameters.Add($"$QuestionID", question.QuestionID);

                            var deleteAnswerRow = mySqlConnection.Execute(deleteAnswer, param: parameters, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                            
                            // Thực hiện thêm các câu hỏi
                            var insertAnswerRow = 0;
                            var answerCommand = $"Proc_Answer_Insert";
                            foreach (var answer in answers)
                            {
                                insertAnswerRow += mySqlConnection.Execute(answerCommand, param: answer, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                            }
                            if (insertAnswerRow != answers.Count)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }

                        if (topicIDs.Count > 0)
                        {
                            //Thực hiện thêm chủ đề

                            // Chuẩn bị câu lệnh Proc
                            var sqlCommand = "Proc_ExerciseTopic_InsertMultiple";

                            // Biến đổi tham số đầu vào
                            string topicStr = "";
                            foreach (var topicID in topicIDs)
                            {
                                if (topicID.Equals(topicIDs.Last()))
                                {
                                    topicStr += $"{topicID}";
                                }
                                else
                                {
                                    topicStr += $"{topicID},";
                                }
                            }

                            // Chuẩn bị tham số đầu vào cho câu lệnh sql
                            parameters = new DynamicParameters();
                            parameters.Add($"$ExerciseID", exercise.ExerciseID);
                            parameters.Add($"topicIDs", topicStr);

                            // Thực hiện gọi vào db để chạy câu lệnh với tham số đầu vào ở trên
                            var resultTopics = mySqlConnection.Execute(sqlCommand, parameters, transaction: transaction,
                                commandType: System.Data.CommandType.StoredProcedure);

                            if (resultTopics == 0)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }

                        transaction.Commit();
                        return true;

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw new ErrorException(devmsg: e.Message);
                    }
                    finally
                    {
                        if (mySqlConnection.State != ConnectionState.Closed)
                        {
                            mySqlConnection.Close();
                        }
                    }
                }
            }
        }
    }
}
