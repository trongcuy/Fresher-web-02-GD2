using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Web02.CeGov.Common;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB02.EMIS.API.Common;
using WEB02.EMIS.API.Common.Entity;
using WEB02.EMIS.API.DL.Interfaces;

namespace WEB02.EMIS.API.DL.Services
{
    public class TopicDL : BaseDL<Topic>, ITopicDL
    {
        public TopicDL(IConfiguration configuration) : base(configuration)
        {
        }
        /// <summary>
        /// Lấy chủ đề theo ID môn và khối
        /// </summary>
        /// <param name="gradeID"></param>
        /// <param name="subjectID"></param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 31/05/2023
        public IEnumerable<Topic> GetByGradeSubject (Guid gradeID, Guid subjectID)
        {
            using (mySqlConnection = new MySqlConnection(ConnectionString))
            {
                // Chuẩn bị câu lệnh 
                var getByIDCommand = $"Proc_Topic_GetByGradeSubject";

                // Chuẩn bị các tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add($"GradeID", gradeID);
                parameters.Add($"SubjectID", subjectID);

                // Thực hiện gọi vào db để chạy câu lệnh 
                var result = mySqlConnection.Query<Topic>(getByIDCommand, parameters, commandType: System.Data.CommandType.StoredProcedure);
                // Xử lý kết quả trả về ở db
                if (result == null)
                {
                    throw new ErrorException(devmsg: Resources.ResourceManager.GetString(name: "NullData"));
                }
                return result;
            }
        }
    }
}
