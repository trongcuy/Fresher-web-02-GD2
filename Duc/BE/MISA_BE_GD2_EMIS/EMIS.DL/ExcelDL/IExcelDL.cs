using EMIS.Common.DTO;
using EMIS.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.DL.ExcelDL
{
    public interface IExcelDL
    {

        /// <summary>
        /// Lưu các bản ghi hợp lệ vào database
        /// CreatedBy: Trịnh Huỳnh Đức (14-6-2023)
        /// </summary>
        /// <param name="exercise"></param>
        /// <param name="excelRows"></param>
        /// <returns></returns>
        public string InsertExcelToDB(Exercise exercise, List<ExcelRow> excelRows);
    }
}
