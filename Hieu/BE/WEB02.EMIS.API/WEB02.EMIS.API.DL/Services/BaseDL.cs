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
using WEB02.EMIS.API.DL.Interfaces;

namespace WEB02.EMIS.API.DL.Services
{
    public class BaseDL<T> : IBaseDL<T>
    {

        #region Field
        /// <summary>
        /// Khai báo biến
        /// </summary>
        protected string ConnectionString;
        protected MySqlConnection mySqlConnection;
        protected string className; 
        #endregion

        #region Constructor
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="configuration"></param>
        public BaseDL(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("dataBase");
            className = typeof(T).Name;
        } 
        #endregion
        #region Method
        /// <summary>
        /// Lấy toàn bộ bản ghi
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        /// CreatedBy VMHieu 27/05/2023 
        public virtual IEnumerable<T> GetAll()
        {
            using (mySqlConnection = new MySqlConnection(ConnectionString))
            {
                // Chuẩn bị câu lệnh 
                var getAllCommand = $"Proc_{className}_GetAll";

                // Thực hiện gọi vào db để chạy câu lệnh 
                var result = mySqlConnection.Query<T>(getAllCommand, commandType: System.Data.CommandType.StoredProcedure);
                // Xử lý kết quả trả về ở db
                if (result == null)
                {
                    throw new ErrorException(devmsg: Resources.ResourceManager.GetString(name: "NullData"));
                }
                return result;
            }
        }
        /// <summary>
        /// Lấy bản ghi thông qua ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần lấy</param>
        /// <returns>Bản ghi có id đã tìm</returns>
        /// CreatedBy VMHieu 27/05/2023 
        public virtual T GetByID(Guid id)
        {
            using (mySqlConnection = new MySqlConnection(ConnectionString))
            {
                // Chuẩn bị câu lệnh 
                var getByIDCommand = $"Proc_{className}_GetByID";

                // Chuẩn bị các tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add($"{className}ID", id);

                // Thực hiện gọi vào db để chạy câu lệnh 
                var result = mySqlConnection.QueryFirstOrDefault<T>(getByIDCommand, parameters, commandType: System.Data.CommandType.StoredProcedure);
                // Xử lý kết quả trả về ở db
                if (result == null)
                {
                    throw new ErrorException(devmsg: Resources.ResourceManager.GetString(name: "NullData"));
                }
                return result;
            }
        }
        /// <summary>
        /// Thêm 1 bản ghi 
        /// </summary>
        /// <param name="entity">bản ghi cần thêm</param>
        /// <returns>Số lượng bản ghi được thêm vào</returns>
        /// CreatedBy VMHieu 27/05/2023
        public virtual int Insert(T entity)
        {
            using (mySqlConnection = new MySqlConnection(ConnectionString))
            {
                // Chuẩn bị câu lệnh 
                var insertCommand = $"Proc_{className}_Insert";

                // Chuẩn bị các tham số đầu vào
                var parameters = new DynamicParameters(entity);

                // Thực hiện gọi vào db để chạy câu lệnh 
                var result = mySqlConnection.Execute(insertCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                // Xử lý kết quả trả về ở db
                if (result == 0)
                {
                    throw new ErrorException(devmsg: Resources.ResourceManager.GetString(name: "ExceptionInsert"));
                }

                return result;
            }
        }
        /// <summary>
        /// Sửa 1 bản ghi
        /// </summary>
        /// <param name="entity">bản ghi cần sửa</param>
        /// <param name="id">ID của bản ghi cần sửa</param>
        /// <returns>Số lượng bản ghi đã sửa</returns>
        /// CreatedBy VMHieu 27/05/2023
        public int Update(T entity, Guid id)
        {
            using (mySqlConnection = new MySqlConnection(ConnectionString))
            {
                // Chuẩn bị câu lệnh 
                var updateCommand = $"Proc_{className}_Update";

                // Chuẩn bị các tham số đầu vào
                var parameters = new DynamicParameters(entity);
                parameters.Add($"{className}ID", id);

                // Thực hiện gọi vào db để chạy câu lệnh 
                var result = mySqlConnection.Execute(updateCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                // Xử lý kết quả trả về ở db
                if (result == 0)
                {
                    throw new ErrorException(devmsg: Resources.ResourceManager.GetString(name: "ExceptionUpdate"));
                }

                return result;
            }
        }
        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="id">ID của bản ghi cần xóa</param>
        /// <returns>Số lượng bản ghi đã xóa</returns>
        /// CreatedBy VMHieu 27/05/2023
        public int Delete(Guid id)
        {
            using (mySqlConnection = new MySqlConnection(ConnectionString))
            {
                // Chuẩn bị câu lệnh 
                var deleteCommand = $"Proc_{className}_Delete";

                // Chuẩn bị các tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add($"${className}ID", id);

                // Thực hiện gọi vào db để chạy câu lệnh 
                var result = mySqlConnection.Execute(deleteCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                // Xử lý kết quả trả về ở db
                if (result == 0)
                {
                    throw new ErrorException(devmsg: Resources.ResourceManager.GetString(name: "ExceptionDelete"));
                }

                return result;
            }
        } 
        #endregion
    }
}
