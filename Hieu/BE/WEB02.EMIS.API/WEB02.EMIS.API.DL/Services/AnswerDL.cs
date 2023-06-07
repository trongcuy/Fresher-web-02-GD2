using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Web02.CeGov.Common;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
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
        /// Thêm nhiều bản ghi cùng lúc
        /// </summary>
        /// <returns></returns>
        /// CreatedBy VMHieu 28/05/2023
        public int InsertMultiple(List<Answer> answers)
        {
            using (mySqlConnection = new MySqlConnection(ConnectionString))
            {
                if (mySqlConnection.State != ConnectionState.Open)
                {
                    mySqlConnection.Open();
                }

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
