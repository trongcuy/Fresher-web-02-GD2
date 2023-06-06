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
    public class ExerciseTopicDL : BaseDL<ExerciseTopic>, IExerciseTopicDL
    {
        public ExerciseTopicDL(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Thực hiện lấy thông tin chủ đề của 1 bài tập theo ID bài tập
        /// </summary>
        /// <param name="ExerciseID">ID bản ghi bài tập</param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 28/05/2023
        public IEnumerable<Guid> GetTopicByID(Guid ExerciseID)
        {
            using (mySqlConnection = new MySqlConnection(ConnectionString))
            {
                // Chuẩn bị câu lệnh Proc
                var sqlCommand = "Proc_ExerciseTopic_GetByExerciseID";

                // Chuẩn bị tham số đầu vào cho câu lệnh sql
                var parameters = new DynamicParameters();
                parameters.Add($"ExerciseID", ExerciseID);

                // Thực hiện gọi vào db để chạy câu lệnh với tham số đầu vào ở trên
                var result = mySqlConnection.Query<Guid>(sqlCommand, parameters, commandType: System.Data.CommandType.StoredProcedure);

                if (result == null)
                {
                    throw new ErrorException(devmsg: Resources.ResourceManager.GetString(name: "NullData"));
                }
                return result;
            }
        }
        /// <summary>
        /// Thực hiện thêm nhiều bản ghi chủ đề theo bài tập
        /// </summary>
        /// <param name="ExerciseID"></param>
        /// <param name="topicIDs"></param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 06/01/2023
        public int InsertMultiple(Guid ExerciseID, List<Guid> topicIDs)
        {
            using (mySqlConnection = new MySqlConnection(ConnectionString))
            {
                // Chuẩn bị câu lệnh Proc
                var sqlCommand = "Proc_ExerciseTopic_InsertMultiple";

                // Biến đổi tham số đầu vào
                string topicStr = "";
                foreach(var topicID in topicIDs)
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
                var parameters = new DynamicParameters();
                parameters.Add($"$ExerciseID", ExerciseID);
                parameters.Add($"topicIDs", topicStr);

                // Thực hiện gọi vào db để chạy câu lệnh với tham số đầu vào ở trên
                var result = mySqlConnection.Execute(sqlCommand, parameters, commandType: System.Data.CommandType.StoredProcedure);

                if (result == null)
                {
                    throw new ErrorException(devmsg: Resources.ResourceManager.GetString(name: "NullData"));
                }
                return result;
            }
        }
    }
}
