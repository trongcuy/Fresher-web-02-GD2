using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.Common.Entity
{
    public class Topic:BaseEntity
    {
        public Guid TopicID { get; set; }
        public string TopicName { get; set; }
        public Guid SubjectID { get; set; }
        public Guid GradeID { get; set; }
    }
}
