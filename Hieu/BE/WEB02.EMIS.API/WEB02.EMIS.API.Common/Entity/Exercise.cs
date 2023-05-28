using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB02.EMIS.API.Common.Enum;

namespace WEB02.EMIS.API.Common.Entity
{
    /// <summary>
    /// Đối tượng Bài tập
    /// VMHieu 25/05/2023
    /// </summary>
    public class Exercise : BaseEntity
    {
        #region Property
        /// <summary>
        /// ID bài tập
        /// </summary>
        public Guid ExerciseID { get; set; }
        /// <summary>
        /// Tên bài tập
        /// </summary>
        public string? ExerciseName { get; set; }
        /// <summary>
        /// ID khối lớp học
        /// </summary>
        public Guid GradeID { get; set; }
        /// <summary>
        /// ID môn học
        /// </summary>
        public Guid SubjectID { get; set; }
        /// <summary>
        /// Đường dẫn ảnh
        /// </summary>
        public Guid? ExerciseImage { get; set; }
        /// <summary>
        /// Trạng thái bài tập
        /// </summary>
        public ExerciseStatus ExerciseStatus { get; set; } 

        #endregion
    }
}
