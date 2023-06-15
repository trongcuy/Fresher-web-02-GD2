using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB02.EMIS.API.Common.Entity
{
    /// <summary>
    /// Đối tượng môn học 
    /// VMHieu 25/05/2023
    /// </summary>
    public class Subject : BaseEntity
    {
        #region Property
        /// <summary>
        /// ID môn học
        /// </summary>
        public Guid SubjectID { get; set; }
        /// <summary>
        /// Tên môn học
        /// </summary>
        public string SubjectName { get; set; }
        /// <summary>
        /// Đường dẫn ảnh
        /// </summary>
        public string? SubjectImage { get; set; } 
        #endregion
    }
}
