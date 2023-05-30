using Dapper;
using EMIS.Common.Entity;
using EMIS.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.DL.ExerciseDL
{
    public class ExerciseDL : BaseDL<Exercise>, IExerciseDL
    {
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
            var getAllCommand = $"Proc_Exercise_GetPage";
            // Chuẩn bị các tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("pageSize", pageSize);
            parameters.Add("pageIndex", pageIndex);
            // Thực hiện gọi vào db để chạy câu lệnh 
            var result = conn.Query<Exercise>(getAllCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            conn.Close();
            return result;
        }
    }
}
