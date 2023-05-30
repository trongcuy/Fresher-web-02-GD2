using Dapper;
using EMIS.Common.Entity;
using EMIS.DL.BaseDL;
using EMIS.DL.QuestionDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.DL.TopicDL
{
    public class TopicDL : BaseDL<Topic>, ITopicDL
    {
        /// <summary>
        /// lấy ra tất cả chủ đề theo id môn và khối
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="subjectID"></param>
        /// <param name="gradeID"></param>
        /// <returns></returns>
        public IEnumerable<Topic> GetAllTopic(string subjectID, string gradeID)
        {
            //tạo kết nối
            var conn = OpenConnection();
            // Chuẩn bị câu lệnh
            var getAllCommand = $"Proc_Topic_GetAll";
            // Chuẩn bị các tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("subjectID", subjectID);
            parameters.Add("gradeID", gradeID);
            // Thực hiện gọi vào db để chạy câu lệnh 
            var result = conn.Query<Topic>(getAllCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            conn.Close();
            return result;
        }
    }
}
