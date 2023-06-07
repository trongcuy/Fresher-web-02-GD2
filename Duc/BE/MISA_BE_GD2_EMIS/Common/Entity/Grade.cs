using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.Common.Entity
{
    public class Grade:BaseEntity
    {
        public Guid GradeID { get; set; }
        public string GradeName { get; set; }
    }
}
