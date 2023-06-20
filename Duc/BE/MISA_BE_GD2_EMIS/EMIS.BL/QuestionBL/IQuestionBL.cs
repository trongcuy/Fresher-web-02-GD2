using EMIS.BL.BaseBL;
using EMIS.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.BL.QuestionBL
{
    public interface IQuestionBL:IBaseBL<Question>
    {
        /// <summary>
        /// lấy ra tất cả đáp án theo id câu hỏi
        /// CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public IEnumerable<Answer> GetAllAnswer(string questionID);

        /// <summary>
        /// thêm đáp án theo id câu hỏi
        /// CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
        /// </summary>
        /// <param name="questionID"></param>
        /// <param name="answers"></param>
        /// <returns></returns>
        public int InsertAnswer(string questionID, List<Answer> answers);
    }
}
