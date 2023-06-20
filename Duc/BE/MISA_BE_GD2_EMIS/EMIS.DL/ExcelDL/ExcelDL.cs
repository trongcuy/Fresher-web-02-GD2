using Dapper;
using EMIS.Common.DTO;
using EMIS.Common.Entity;
using EMIS.DL.ExerciseDL;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.DL.ExcelDL
{
    public class ExcelDL:IExcelDL
    {
        #region Fields
        private readonly string? _connectionString;
        #endregion

        #region Constructor
        public ExcelDL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("dataBase");
        }
        #endregion

        #region Methods
        /// <summary>
        /// Tạo kết nối đến db
        /// CreatedBy: Trịnh Huỳnh Đức (14-6-2023)
        /// </summary>
        /// <returns></returns>
        private IDbConnection OpenConnection()
        {
            var cnn = new MySqlConnection(_connectionString);
            cnn.Open();
            return cnn;
        }

        /// <summary>
        /// Lưu các bản ghi hợp lệ vào database
        /// CreatedBy: Trịnh Huỳnh Đức (14-6-2023)
        /// </summary>
        /// <param name="exercise"></param>
        /// <param name="excelRows"></param>
        /// <returns></returns>
        public string InsertExcelToDB(Exercise exercise, List<ExcelRow> excelRows)
        {
            //tạo kết nối
            var conn = OpenConnection();
            // Bat dau transaction
            var transaction = conn.BeginTransaction();
            try
            {
                // Insert bài tập
                // Chuẩn bị câu lệnh 
                var insertCommand = "Proc_Exercise_Insert";
                // Chuẩn bị các tham số đầu vào
                var parameters = new DynamicParameters(exercise);
                // Thực hiện gọi vào db để chạy câu lệnh
                var resultExercise = conn.ExecuteScalar<string>(insertCommand, param: parameters, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                // Insert từng câu hỏi
                var indexQuestion = 1;
                foreach (ExcelRow row in excelRows)
                {
                    Question question = new Question()
                    {
                        QuestionContent = row.QuestionContent,
                        QuestionType = ChangeTypeQuestion(row.QuestionType),
                        QuestionExplane = row.QuestionExplane,
                        QuestionNumber = indexQuestion.ToString(),
                        ExerciseID = Guid.Parse(resultExercise)
                    };
                    // Chuẩn bị câu lệnh 
                    insertCommand = "Proc_Question_Insert";
                    // Chuẩn bị các tham số đầu vào
                    parameters = new DynamicParameters(question);
                    // Thực hiện gọi vào db để chạy câu lệnh
                    var resultQuestion = conn.ExecuteScalar<string>(insertCommand, param: parameters, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                    //thêm đáp án
                    if (question.QuestionContent != "Tự luận")
                    {
                        // Chuẩn bị câu lệnh
                        insertCommand = "Proc_Answer_Insert";
                        for (int i = 0; i<row.Answers.Count; i++)
                        {
                            Answer answer = new Answer()
                            {
                                AnswerContent = row.Answers[i],
                                AnswerState = GetStateAnswer(i+1, row.AnswerTrue),
                                QuestionID = Guid.Parse(resultQuestion)
                            };
                            // Chuẩn bị các tham số đầu vào
                            parameters = new DynamicParameters(answer);
                            // Thực hiện gọi vào db để chạy câu lệnh 
                            var result = conn.Execute(insertCommand, param: parameters, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                        }
                    }
                    indexQuestion++;
                }

                // Xác nhận thay đổi
                transaction.Commit();
                return resultExercise;
            }
            catch (Exception)
            {
                // Rollback
                transaction.Rollback();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// đổi loại câu hỏi sang int
        /// CreatedBy: Trịnh Huỳnh Đức (14-6-2023)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private int ChangeTypeQuestion(string type)
        {
            switch (type)
            {
                case "Chọn đáp án":
                    return 1;
                case "Đúng sai":
                    return 2;
                case "Điền vào chỗ trống":
                    return 3;
                case "Tự luận":
                    return 4;
                default: return 0;
            }
        }

        /// <summary>
        /// lấy trạng thái đáp án
        /// CreatedBy: Trịnh Huỳnh Đức (14-6-2023)
        /// </summary>
        /// <param name="index"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        private int GetStateAnswer(int index, string answer)
        {
            if (answer.Contains(index.ToString()))
            {
                return 1;
            }
            return 2;
        } 
        #endregion
    }
}
