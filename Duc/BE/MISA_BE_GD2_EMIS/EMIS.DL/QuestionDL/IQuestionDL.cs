using EMIS.Common.Entity;
using EMIS.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.DL.QuestionDL
{
    public interface IQuestionDL : IBaseDL<Question>
    {
        /// <summary>
        /// lấy câu hỏi theo id bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="exerciseID"></param>
        /// <returns></returns>
        public IEnumerable<Question> GetAllQuestion(string exerciseID);
    }
}
