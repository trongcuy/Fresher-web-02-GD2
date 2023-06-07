using EMIS.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.Common.DTO
{
    public class DataAll : BaseEntity
    {
        public Exercise exercise { get; set; }
        public Question question { get; set; }
        public List<Answer>? answer { get; set; }
        public string  topicIDs { get; set; }
    }
}
