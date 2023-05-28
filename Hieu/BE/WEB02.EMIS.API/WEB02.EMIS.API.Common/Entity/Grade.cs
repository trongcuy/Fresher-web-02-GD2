using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB02.EMIS.API.Common.Entity
{
    /// <summary>
    /// Đối tượng khối lớp học
    /// VMHieu 25/05/2023
    /// </summary>
    public class Grade : BaseEntity
    {
        #region Property
        /// <summary>
        /// ID khối
        /// </summary>
        public Guid GradeID { get; set; }
        /// <summary>
        /// Tên khối
        /// </summary>
        public string GradeName { get; set; } 
        #endregion
    }
}
