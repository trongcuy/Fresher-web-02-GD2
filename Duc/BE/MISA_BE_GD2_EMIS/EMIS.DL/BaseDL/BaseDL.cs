using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.DL.BaseDL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        #region fields
        private readonly string _connectionString = "Server=127.0.0.1; User ID=root; Password=huynhduc; Database=emis.freshergd2_duc;";
        //lấy tên đối tượng
        private readonly string nameEntity = typeof(T).Name;
        #endregion

        #region Methods
        /// <summary>
        /// Hàm tạo kết nối
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <returns></returns>
        public IDbConnection OpenConnection()
        {
            var cnn = new MySqlConnection(_connectionString);
            cnn.Open();
            return cnn;
        }

        /// <summary>
        /// Lây tất cả 
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            //tạo kết nối
            var conn = OpenConnection();
            // Chuẩn bị câu lệnh
            var getAllCommand = $"Proc_{nameEntity}_GetAll";
            // Chuẩn bị các tham số đầu vào
            var parameters = new DynamicParameters();
            // Thực hiện gọi vào db để chạy câu lệnh 
            var result = conn.Query<T>(getAllCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            conn.Close();
            return result;
        }

        /// <summary>
        /// lấy theo id
        /// CreatedBy: Trịnh Huỳnh Đức-(29-5-23)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(string id)
        {
            //tạo kết nối
            var conn = OpenConnection();
            // Chuẩn bị câu lệnh
            var getAllCommand = $"Proc_{nameEntity}_GetById";
            // Chuẩn bị các tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            // Thực hiện gọi vào db để chạy câu lệnh 
            var result = conn.QueryFirstOrDefault<T>(getAllCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            conn.Close();
            return result;
        }

        /// <summary>
        /// Thêm mới mộ đối tượng
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Insert(T value)
        {
            //tạo kết nối
            var conn = OpenConnection();
            // Chuẩn bị câu lệnh
            var getAllCommand = $"Proc_{nameEntity}_Insert";
            // Chuẩn bị các tham số đầu vào
            var parameters = new DynamicParameters(value);
            //parameters.Add("value", value);
            // Thực hiện gọi vào db để chạy câu lệnh 
            var result = conn.Execute(getAllCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            conn.Close();
            return result;
        }

        /// <summary>
        /// Cập nhật một đối tượng
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Update(T value)
        {
            //tạo kết nối
            var conn = OpenConnection();
            // Chuẩn bị câu lệnh
            var getAllCommand = $"Proc_{nameEntity}_Update";
            // Chuẩn bị các tham số đầu vào
            var parameters = new DynamicParameters(value);
            // Thực hiện gọi vào db để chạy câu lệnh 
            var result = conn.Execute(getAllCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            conn.Close();
            return result;
        }

        /// <summary> 
        /// xoa bawng id
        /// CreatedBy: Trịnh Huỳnh Đức-(29-5-23)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteById(string id)
        {
            //tạo kết nối
            var conn = OpenConnection();
            // Chuẩn bị câu lệnh
            var getAllCommand = $"Proc_{nameEntity}_DeleteById";
            // Chuẩn bị các tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            // Thực hiện gọi vào db để chạy câu lệnh 
            var result = conn.Execute(getAllCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            conn.Close();
            return result;
        } 
        #endregion

    }
}
