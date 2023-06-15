using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.CeGov.Common.Entity.DTO
{
    /// <summary>
    /// Đại diện cho 1 ô trong file excel
    /// </summary>
    /// CreatedBy VMHieu 11/06/2023
    public class ErrorCell
    {
        /// <summary>
        /// Tọa độ hàng
        /// </summary>
        public int Row { get; set; }
        /// <summary>
        /// Tọa độ cột
        /// </summary>
        public int Col { get; set; }
        /// <summary>
        /// Msg gắn vào comment
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Nội dung ô
        /// </summary>
        public string? Context { get; set; }
    }
}
