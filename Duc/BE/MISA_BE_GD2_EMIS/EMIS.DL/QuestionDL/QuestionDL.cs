using Dapper;
using EMIS.Common.Entity;
using EMIS.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.DL.QuestionDL
{
    public class QuestionDL : BaseDL<Question>, IQuestionDL
    {
        /// <summary>
        /// lấy đáp án theo id câu hỏi
        /// CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public IEnumerable<Answer> GetAllAnswer(string questionID)
        {
            //tạo kết nối
            var conn = OpenConnection();
            // Chuẩn bị câu lệnh
            var getAllCommand = "Proc_Answer_GetAll";
            // Chuẩn bị các tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("questionID", questionID);
            // Thực hiện gọi vào db để chạy câu lệnh 
            var result = conn.Query<Answer>(getAllCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            conn.Close();
            return result;
        }

        /// <summary>
        /// xoas đáp án theo id câu hỏi
        /// CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public int DeleteAnswer(string questionID)
        {
            //tạo kết nối
            var conn = OpenConnection();
            // Chuẩn bị câu lệnh
            var getAllCommand = "Proc_Answer_DeleteAll";
            // Chuẩn bị các tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("questionID", questionID);
            // Thực hiện gọi vào db để chạy câu lệnh 
            var result = conn.Execute(getAllCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            conn.Close();
            return result;
        }

        /// <summary>
        /// theem đáp án theo id câu hỏi
        /// CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public int InsertAnswer(string questionID, List<Answer> answers)
        {
            //xóa hết đáp án trước khi thêm
            DeleteAnswer(questionID);
            var res = 0;
            //tạo kết nối
            var conn = OpenConnection();
            // Chuẩn bị câu lệnh
            var getAllCommand = "Proc_Answer_Insert";
            for (var i=0; i<answers.Count; i++)
            {            
                // Chuẩn bị các tham số đầu vào
                var parameters = new DynamicParameters(answers[i]);
                parameters.Add("questionID", questionID);
                // Thực hiện gọi vào db để chạy câu lệnh 
                var result = conn.Execute(getAllCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);               
                res += result;
            }
            conn.Close();         
            return res;
        }
    }
}
