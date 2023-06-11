using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.CeGov.Common.Entity.DTO
{
    public class ExcelCheckTotal
    {
        /// <summary>
        /// Số bản ghi hợp lệ
        /// </summary>
        public int TotalSuccess { get; set; }
        /// <summary>
        /// Số bản ghi không hợp lệ
        /// </summary>
        public int TotalFail { get; set; }
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int Total { get; set; }
    }
}
