using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB02.EMIS.API.Common.Entity
{
    /// <summary>
    /// Đối tượng chủ đề
    /// VMHieu 25/05/2023
    /// </summary>
    public class Topic : BaseEntity
    {
        /// <summary>
        /// ID chủ đề
        /// </summary>
        public Guid TopicID { get; set; }
        /// <summary>
        /// Tên chủ đề
        /// </summary>
        public string TopicName { get; set; }
        /// <summary>
        /// ID khối
        /// </summary>
        public Guid GradeID { get; set; }
        /// <summary>
        /// ID môn
        /// </summary>
        public Guid SubjectID { get; set; }
    }
}
