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
    public class AnswerDL : BaseDL<Answer>, IAnswerDL
    {
        public AnswerDL(IConfiguration configuration) : base(configuration)
        {

        }

        /// <summary>
        /// Xóa nhiều bản ghi cùng lúc
        /// </summary>
        /// <param name="ids">Chuỗi chứa các id của đáp án cần xóa</param>
        /// <returns></returns>
        /// CreatedBy VMHieu 28/05/2023
        public int DeleteMultiple(string ids)
        {
            using (mySqlConnection = new MySqlConnection(ConnectionString))
            {
                // Tách dữ liệu id từ chuỗi ids:
                List<string> selectedIds = ids.Split('/').ToList();

                // Chuẩn bị câu lệnh Proc
                var sqlCommand = "Proc_Answer_DeleteMultiple";

                // Chuẩn bị tham số đầu vào cho câu lệnh sql
                var parameters = new DynamicParameters();

                var whereClause = "";
                foreach (var id in selectedIds)
                {
                    if (id.Equals(selectedIds.Last()))
                    {
                        whereClause += $"'{id}'";
                    }
                    else
                    {
                        whereClause += $"'{id}', ";
                    }
                }
                whereClause = '"' + whereClause + '"';

                parameters.Add("$AnswerID", whereClause);

                // Thực hiện gọi vào db để chạy câu lệnh với tham số đầu vào ở trên
                var result = mySqlConnection.Execute(sqlCommand, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về ở db
                if (result == null)
                {
                    throw new ErrorException(devmsg: Resources.ResourceManager.GetString(name: "NullData"));
                }
                return result;
            }
        }

        /// <summary>
        /// Thêm nhiều bản ghi cùng lúc
        /// </summary>
        /// <returns></returns>
        /// CreatedBy VMHieu 28/05/2023
        public int InsertMultiple(List<Answer> answers)
        {
            using (mySqlConnection = new MySqlConnection(ConnectionString))
            {
                var insertedRow = 0;
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        var sqlCommand = $"Proc_Answer_Insert";
                        foreach (var answer in answers)
                        {
                            insertedRow += mySqlConnection.Execute(sqlCommand, param: answer, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                        }
                        if (insertedRow == answers.Count)
                        {
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                        }
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                    }
                }
                // Xử lý kết quả trả về ở db
                if (insertedRow == 0)
                {
                    throw new ErrorException(devmsg: Resources.ResourceManager.GetString(name: "NullData"));
                }
                return insertedRow;
            }
        }
    }
}
