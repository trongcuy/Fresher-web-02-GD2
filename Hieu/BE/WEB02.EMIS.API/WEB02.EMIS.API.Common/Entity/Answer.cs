using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB02.EMIS.API.Common.Enum;

namespace WEB02.EMIS.API.Common.Entity 
{
    /// <summary>
    /// Đối tượng bài tập
    /// VMHieu 25/05/2023
    /// </summary>
    public class Answer : BaseEntity
    {
        #region Property
        /// <summary>
        /// ID đáp án
        /// </summary>
        public Guid AnswerID { get; set; }
        /// <summary>
        /// Nội dung đáp án
        /// </summary>
        public string? AnswerContent { get; set; }
        /// <summary>
        /// Đường dẫn ảnh
        /// </summary>
        public Guid? AnswerImage { get; set; }
        /// <summary>
        /// Trạng thai của đáp án
        /// </summary>
        public AnswerStatus AnswerStatus { get; set; }
        /// <summary>
        /// Thứ tự sắp xếp
        /// </summary>
        public string SortOder { get; set; }
        /// <summary>
        /// ID câu hỏi
        /// </summary>
        public Guid QuestionID { get; set; } 
        #endregion
    }
}
