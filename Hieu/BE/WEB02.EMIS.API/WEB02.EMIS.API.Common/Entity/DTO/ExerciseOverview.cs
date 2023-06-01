using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB02.EMIS.API.Common.Entity.DTO
{
    /// <summary>
    /// Đối tượng bài tập và các câu hỏi, đáp án phụ thuộc
    /// VMHieu 28.05.2023
    /// </summary>
    public class ExerciseOverview
    {
        /// <summary>
        /// Thông tin bài tập
        /// </summary>
        public List<Exercise> Exercise { get; set; } = new List<Exercise>();
        /// <summary>
        /// List câu hỏi phụ thuộc
        /// </summary>
        public List<Question> Question { get; set; } = new List<Question>();
        /// <summary>
        /// List đáp án phụ thuộc
        /// </summary>
        public List<Answer> Answer { get; set; } = new List<Answer>();
    }
}
