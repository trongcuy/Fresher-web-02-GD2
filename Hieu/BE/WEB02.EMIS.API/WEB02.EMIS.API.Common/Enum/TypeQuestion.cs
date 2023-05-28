using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB02.EMIS.API.Common.Enum
{
    /// <summary>
    /// Kiểu câu hỏi 
    /// VMHieu 25/05/2023
    /// </summary>
    public enum TypeQuestion
    {
        /// <summary>
        /// Chọn đáp án
        /// </summary>
        Select = 1,
        /// <summary>
        /// Đúng sai
        /// </summary>
        YesOrNo = 2,
        /// <summary>
        /// Điền vào chỗ trống
        /// </summary>
        Fill = 3,
        /// <summary>
        /// Tự luận
        /// </summary>
        Essay = 4,
        /// <summary>
        /// Câu hỏi nhóm
        /// </summary>
        Group = 5
    }
}
