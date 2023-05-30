using EMIS.Common.Entity;
using EMIS.BL.BaseBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMIS.DL.BaseDL;
using EMIS.DL.ExerciseDL;
using EMIS.DL.QuestionDL;

namespace EMIS.BL.QuestionBL
{
    public class QuestionBL : BaseBL<Question>, IQuestionBL
    {
        #region Field
        private IQuestionDL _questionDL;
        #endregion

        public QuestionBL(IQuestionDL questionDL) : base(questionDL)
        {
            _questionDL = questionDL;
        }

        /// <summary>
        /// lấy ra tất cả câu hỏi theo id bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="exerciseID"></param>
        /// <returns></returns>
        public IEnumerable<Question> GetAllQuestion(string exerciseID)
        {
            return _questionDL.GetAllQuestion(exerciseID);
        }
    }
}
