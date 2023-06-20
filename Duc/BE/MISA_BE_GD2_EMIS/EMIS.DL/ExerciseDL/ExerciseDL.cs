using Dapper;
using EMIS.Common;
using EMIS.Common.DTO;
using EMIS.Common.Entity;
using EMIS.Common.ExceptionEntity;
using EMIS.DL.BaseDL;
using EMIS.DL.QuestionDL;
using EMIS.DL.TopicDL;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EMIS.DL.ExerciseDL
{
    public class ExerciseDL : BaseDL<Exercise>, IExerciseDL
    {
        #region Constructor
        public ExerciseDL(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// lấy theo trang bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IEnumerable<Exercise> GetPage(int pageSize, int pageIndex)
        {
            //tạo kết nối
            var conn = OpenConnection();
            // Chuẩn bị câu lệnh
            var getAllCommand = "Proc_Exercise_GetPage";
            // Chuẩn bị các tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("pageSize", pageSize);
            parameters.Add("pageIndex", pageIndex);
            // Thực hiện gọi vào db để chạy câu lệnh 
            var result = conn.Query<Exercise>(getAllCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            // Xử lý kết quả trả về ở db
            if (result == null)
            {
                throw new ErrorException(devmsg: Resource.ResourceManager.GetString(name: "ResultNull"));
            }
            conn.Close();
            return result;
        }

        /// <summary>
        /// lay bai tap theo bo loc
        /// CreatedBy: Trịnh Huỳnh Đức (30-5-2023)
        /// </summary>
        /// <param name="state"></param>
        /// <param name="subjectID"></param>
        /// <param name="gradeID"></param>
        /// <param name="search"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IEnumerable<Exercise> GetFilter(int? state, string? subjectID, string? gradeID, string? search, int pageSize, int pageIndex)
        {
            //tạo kết nối
            var conn = OpenConnection();
            // Chuẩn bị câu lệnh
            var getAllCommand = "Proc_Exercise_Filter";
            // Chuẩn bị các tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("state", state);
            parameters.Add("subjectID", subjectID);
            parameters.Add("gradeID", gradeID);
            parameters.Add("search", search);
            parameters.Add("pageSize", pageSize);
            parameters.Add("pageIndex", pageIndex);
            // Thực hiện gọi vào db để chạy câu lệnh 
            var result = conn.Query<Exercise>(getAllCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            // Xử lý kết quả trả về ở db
            if (result == null)
            {
                throw new ErrorException(devmsg: Resource.ResourceManager.GetString(name: "ResultNull"));
            }
            conn.Close();
            return result;
        }

        /// <summary>
        /// lấy câu hỏi theo id bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="exerciseID"></param>
        /// <returns></returns>
        public IEnumerable<Question> GetAllQuestion(string exerciseID)
        {
            //tạo kết nối
            var conn = OpenConnection();
            // Chuẩn bị câu lệnh
            var getAllCommand = "Proc_Question_GetAll";
            // Chuẩn bị các tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("exerciseID", exerciseID);
            // Thực hiện gọi vào db để chạy câu lệnh 
            var result = conn.Query<Question>(getAllCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            // Xử lý kết quả trả về ở db
            if (result == null)
            {
                throw new ErrorException(devmsg: Resource.ResourceManager.GetString(name: "ResultNull"));
            }
            conn.Close();
            return result;
        }

        /// <summary>
        /// lấy chủ đề theo id bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (1-6-2023)
        /// </summary>
        /// <param name="exerciseID"></param>
        /// <returns></returns>
        public IEnumerable<Topic> GetAllTopic(string exerciseID)
        {
            //tạo kết nối
            var conn = OpenConnection();
            // Chuẩn bị câu lệnh
            var getAllCommand = "Proc_TopicExercise_GetAll";
            // Chuẩn bị các tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("exerciseID", exerciseID);
            // Thực hiện gọi vào db để chạy câu lệnh 
            var result = conn.Query<Topic>(getAllCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            // Xử lý kết quả trả về ở db
            if (result == null)
            {
                throw new ErrorException(devmsg: Resource.ResourceManager.GetString(name: "ResultNull"));
            }
            conn.Close();
            return result;
        }

        /// <summary>
        /// thêm chủ đề theo id bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (1-6-2023)
        /// </summary>
        /// <param name="exerciseID"></param>
        /// <param name="topicIDs"></param>
        /// <returns></returns>
        public int InsertTopic(string exerciseID, string? topicIDs)
        {
            //tạo kết nối
            var conn = OpenConnection();
            // Chuẩn bị câu lệnh
            var getAllCommand = "Proc_TopicExercise_Insert";
            // Chuẩn bị các tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("exerciseID", exerciseID);
            parameters.Add("topicIDs", topicIDs);
            // Thực hiện gọi vào db để chạy câu lệnh 
            var result = conn.Execute(getAllCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            conn.Close();
            return result;
        }

        /// <summary>
        /// thêm chủ đề theo id bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (1-6-2023)
        /// </summary>
        /// <param name="dataAll"></param>
        /// <returns></returns>
        public string InsertAll(DataAll dataAll)
        {
            //tạo kết nối
            var conn = OpenConnection();
            // Bat dau transaction
            var transaction = conn.BeginTransaction();
            try
            {
                // thêm bài tập
                // Chuẩn bị câu lệnh 
                var insertCommand = "Proc_Exercise_Insert";
                // Chuẩn bị các tham số đầu vào
                var parameters = new DynamicParameters(dataAll.exercise);
                // Thực hiện gọi vào db để chạy câu lệnh
                var resultExercise = conn.ExecuteScalar<string>(insertCommand, param: parameters, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                // thêm câu hỏi
                // Chuẩn bị câu lệnh 
                insertCommand = "Proc_Question_Insert";
                // Chuẩn bị các tham số đầu vào
                dataAll.question.ExerciseID = Guid.Parse(resultExercise);
                parameters = new DynamicParameters(dataAll.question);
                // Thực hiện gọi vào db để chạy câu lệnh
                var resultQuestion = conn.ExecuteScalar<string>(insertCommand, param: parameters, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);              

                //thêm câu hỏi thành công
                // thêm đáp án
                if (dataAll.answer.Count > 0)
                {
                    // Chuẩn bị câu lệnh
                    insertCommand = "Proc_Answer_Insert";
                    var answers = dataAll.answer;
                    var resultAnswer = 0;
                    for (var i = 0; i<answers.Count; i++)
                    {
                        // Chuẩn bị các tham số đầu vào
                        parameters = new DynamicParameters(answers[i]);
                        parameters.Add("questionID", Guid.Parse(resultQuestion));
                        // Thực hiện gọi vào db để chạy câu lệnh 
                        var result = conn.Execute(insertCommand, param: parameters, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                        resultAnswer += result;
                    }
                }

                // thêm chủ đề          
                if (dataAll.topicIDs != "")
                {
                    var resultTopic = 0;
                    // Chuẩn bị câu lệnh
                    var getAllCommand = "Proc_TopicExercise_Insert";
                    // Chuẩn bị các tham số đầu vào
                    parameters = new DynamicParameters();
                    parameters.Add("exerciseID", Guid.Parse(resultExercise));
                    parameters.Add("topicIDs", dataAll.topicIDs);
                    // Thực hiện gọi vào db để chạy câu lệnh 
                    resultTopic = conn.Execute(getAllCommand, param: parameters, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                    
                }

                // Xác nhận thay đổi
                transaction.Commit();
                return resultExercise;
            }
            catch (Exception)
            {
                // rollback
                transaction.Rollback();
                throw;
            }
            finally
            {
                conn.Close();
            }
        } 
        #endregion
    }
}
