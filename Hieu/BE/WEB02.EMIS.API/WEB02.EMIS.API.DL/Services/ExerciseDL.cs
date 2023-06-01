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
    }
}
