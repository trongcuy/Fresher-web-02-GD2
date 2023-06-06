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
    public class DataInsertAll
    {
        /// <summary>
        /// Bài tập
        /// </summary>
        public Exercise exercise { get; set; }
        /// <summary>
        /// Câu hỏi
        /// </summary>
        public Question question { get; set; }
        /// <summary>
        /// List đáp án
        /// </summary>
        public List<Answer> answers { get; set; } = new List<Answer>();
        /// <summary>
        /// List chủ đề
        /// </summary>
        public List<Guid> topicIDs { get; set; } = new List<Guid>();
    }
}
