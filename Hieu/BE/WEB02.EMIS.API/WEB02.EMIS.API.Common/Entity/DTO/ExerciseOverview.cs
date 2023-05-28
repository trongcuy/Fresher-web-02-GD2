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
        public Exercise Exercise { get; set; }
        /// <summary>
        /// List câu hỏi phụ thuộc
        /// </summary>
        public List<Question> QuestionData { get; set; } = new List<Question>();
        /// <summary>
        /// List đáp án phụ thuộc
        /// </summary>
        public List<Answer> AnswerData { get; set; } = new List<Answer>();
    }
}
