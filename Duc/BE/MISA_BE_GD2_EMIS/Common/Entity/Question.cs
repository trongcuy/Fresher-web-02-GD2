using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.Common.Entity
{
    public class Question : BaseEntity
    {
        public Guid QuestionID { get; set; }
        public string QuestionContent { get; set; }
        public int QuestionType { get; set; }
        public string? QuestionExplane { get; set; }
        public string QuestionNumber { get; set; }
        public Guid ExerciseID { get; set; }
        public Question()
        {
            ExerciseID = Guid.NewGuid();
        }
    }
}
