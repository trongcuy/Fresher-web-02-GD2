using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB02.EMIS.API.Common.Enum;

namespace WEB02.EMIS.API.Common.Entity
{
    /// <summary>
    /// Đối tượng câu hỏi
    /// VMHieu 25/05/2023
    /// </summary>
    public class Question : BaseEntity
    {
        #region Property
        /// <summary>
        /// ID câu hỏi
        /// </summary>
        public Guid QuestionID { get; set; }
        /// <summary>
        /// Nội dung câu hỏi
        /// </summary>
        public string QuestionContent { get; set; }
        /// <summary>
        /// Nội dung chú thích
        /// </summary>
        public string? QuestionNote { get; set; }
        /// <summary>
        /// Ảnh câu hỏi
        /// </summary>
        public Guid? QuestionImage { get; set; }
        /// <summary>
        /// ID bài tập
        /// </summary>
        public Guid ExerciseID { get; set; }
        /// <summary>
        /// Thứ tự câu hỏi
        /// </summary>
        public string SortOder { get; set; }
        /// <summary>
        /// Dạng câu hỏi
        /// </summary>
        public TypeQuestion TypeQuestion { get; set; }
        /// <summary>
        /// ID câu hỏi nhóm
        /// </summary>
        public Guid? QuestionSecondaryID { get; set; } 
        #endregion
    }
}
