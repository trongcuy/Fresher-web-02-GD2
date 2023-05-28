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
                    Exercise = (Exercise)result.Read<Exercise>(),
                    QuestionData = (List<Question>)result.Read<Question>(),
                    AnswerData = (List<Answer>)result.Read<Answer>(),
                };
                if (exercise == null)
                {
                    throw new ErrorException(devmsg: Resources.ResourceManager.GetString(name: "NullData"));
                }
                return exercise;
            }
        }
    }
}
