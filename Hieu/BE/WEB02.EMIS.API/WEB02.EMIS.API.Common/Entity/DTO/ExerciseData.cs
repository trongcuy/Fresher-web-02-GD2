using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB02.EMIS.API.Common.Entity.DTO
{
    /// <summary>
    /// Dữ liệu bài tập khi không có câu hỏi
    /// </summary>
    public class ExerciseData
    {
        /// <summary>
        /// Bài tập
        /// </summary>
        public Exercise exercise { get; set; }

        /// <summary>
        /// List chủ đề
        /// </summary>
        public List<Guid>? topicIDs { get; set; } = new List<Guid>();
    }
}
