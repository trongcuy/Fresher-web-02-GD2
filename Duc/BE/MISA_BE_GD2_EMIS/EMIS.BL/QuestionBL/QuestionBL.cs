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
using EMIS.BL.ExerciseBL;
using EMIS.Common;
using EMIS.Common.ExceptionEntity;

namespace EMIS.BL.QuestionBL
{
    public class QuestionBL : BaseBL<Question>, IQuestionBL
    {
        #region Field
        private IQuestionDL _questionDL;
        #endregion

        #region Constructor
        public QuestionBL(IQuestionDL questionDL) : base(questionDL)
        {
            _questionDL = questionDL;
        }
        #endregion

        #region Methods
        /// <summary>
        /// lấy ra tất cả đáp án theo id câu hỏi
        /// CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public IEnumerable<Answer> GetAllAnswer(string questionID)
        {
            return _questionDL.GetAllAnswer(questionID);
        }

        /// <summary>
        /// thêm đáp án theo id câu hỏi
        /// CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public int InsertAnswer(string questionID, List<Answer> answers)
        {
            if (ValidateAnswer(answers))
            {
                return _questionDL.InsertAnswer(questionID, answers);
            }
            else
            {
                throw new ErrorException(devmsg: Resource.ResourceManager.GetString(name: "InvalidData"), errors: errorList);
            }
        }

        /// <summary>
        /// hàm validate
        /// CreatedBy: Trịnh Huỳnh Đức (7-6-2023)
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        protected override bool Validate(Question question)
        {
            errorList.Clear();

            // Check tên câu hỏi đã nhập chưa
            if (string.IsNullOrEmpty(question.QuestionContent))
            {
                errorList.Add(Resource.ResourceManager.GetString(name: "InvalidQuestionName"));
            }

            // Check loại câu hỏi đã nhập đúng chưa
            if (question.QuestionType>4 || question.QuestionType<1)
            {
                errorList.Add(Resource.ResourceManager.GetString(name: "InvalidQuestionType"));
            }

            // Check số thứ tự câu hỏi đã nhập chưa
            if (string.IsNullOrEmpty(question.QuestionNumber))
            {
                errorList.Add(Resource.ResourceManager.GetString(name: "InvalidQuestionNumber"));
            }

            if (errorList.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// hàm validate answer
        /// CreatedBy: Trịnh Huỳnh Đức (7-6-2023)
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        private bool ValidateAnswer(List<Answer> answers)
        {
            errorList.Clear();

            foreach (var answer in answers)
            {
                //kiểm tra trạng thái đáp án có hợp lệ không
                if ((answer.AnswerState != 1) && (answer.AnswerState != 2))
                {
                    //nếu chưa có lỗi này thì mới add vào
                    if (!errorList.Contains(Resource.ResourceManager.GetString(name: "InvalidAnswerState")))
                        errorList.Add(Resource.ResourceManager.GetString(name: "InvalidAnswerState"));
                }

                //kiểm tra nội dung đáp án đã có chưa
                if (string.IsNullOrEmpty(answer.AnswerContent) && (answer.AnswerImage == Guid.Empty))
                {
                    //nếu chưa có lỗi này thì mới add vào
                    if (!errorList.Contains(Resource.ResourceManager.GetString(name: "InvalidAnswerContent")))
                        errorList.Add(Resource.ResourceManager.GetString(name: "InvalidAnswerContent"));
                }
            }

            if (errorList.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        } 
        #endregion
    }
}
