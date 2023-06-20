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
        /// lấy đáp án theo id câu hỏi
        /// CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public IEnumerable<Answer> GetAllAnswer(string questionID);

        /// <summary>
        /// theem đáp án theo id câu hỏi
        /// CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
        /// </summary>
        /// <param name="questionID"></param>
        /// <param name="answers"></param>
        /// <returns></returns>
        public int InsertAnswer(string questionID, List<Answer> answers);

        /// <summary>
        /// xoas đáp án theo id câu hỏi
        /// CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public int DeleteAnswer(string questionID);
    }
}
