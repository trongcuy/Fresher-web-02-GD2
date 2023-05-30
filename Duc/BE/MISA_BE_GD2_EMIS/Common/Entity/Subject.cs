using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.Common.Entity
{
    public class Subject:BaseEntity
    {
        public Guid SubjectID { get; set; }
        public string SubjectName{ get; set; }
        public Guid SubjectImage { get; set; }
    }
}
