using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB02.EMIS.API.Common.Entity.DTO
{
    /// <summary>
    /// Đối tượng khi thêm bài tập, câu hỏi, đáp án và chủ đề
    /// VMHieu 06/06/2023
    /// </summary>
    public class DataExcel
    {
        /// <summary>
        /// Câu hỏi
        /// </summary>
        public Question question { get; set; }
        /// <summary>
        /// List đáp án
        /// </summary>
        public List<Answer> answers { get; set; } = new List<Answer>();
    }
}
