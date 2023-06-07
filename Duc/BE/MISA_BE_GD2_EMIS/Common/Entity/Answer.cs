using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.Common.Entity
{
    public class Answer:BaseEntity
    {
        public Guid AnswerID { get; set; }
        public string? AnswerContent { get; set; }
        public int AnswerState { get; set; }
        public Guid? AnswerImage { get; set; }
        public Guid QuestionID { get; set; }

        public Answer()
        {
            AnswerImage = new Guid();
            QuestionID = new Guid();
        }
    }
}
