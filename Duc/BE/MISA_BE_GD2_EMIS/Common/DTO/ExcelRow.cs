using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.Common.DTO
{
    public class ExcelRow
    {
        public string QuestionContent { get; set; }
        public string QuestionType { get; set; }
        public string? QuestionExplane { get; set; }
        public string QuestionNumber { get; set; }
        public string AnswerTrue { get; set; }
        public List<string> Answers { get; set; }

    }
}
