using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.Common.Entity
{
    public class BaseEntity
    {
        /// <summary>
        /// tạo bởi
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// truy cập bởi
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// ngày truy cập
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        public BaseEntity()
        {
            CreatedBy = "admin";
            ModifiedBy = "admin";
        }
    }
}
