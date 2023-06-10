using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB02.EMIS.API.BL.Interfaces;
using WEB02.EMIS.API.Common;
using WEB02.EMIS.API.Common.Entity;
using WEB02.EMIS.API.DL.Interfaces;

namespace WEB02.EMIS.API.BL.Services
{
    public class QuestionBL : BaseBL<Question>, IQuestionBL
    {
        public QuestionBL(IQuestionDL questionDL) : base(questionDL)
        {
        }

        /// <summary>
        /// Hàm validate dữ liệu
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        /// VMHieu 07/06/2023
        protected override bool Validate(Question question)
        {
            errorList.Clear();
            // Xử lý nghiệp vụ
            // Câu hỏi phải có nội dung hoặc hình ảnh
            if (string.IsNullOrEmpty(question.QuestionContent) && (question.QuestionImage == null || question.QuestionImage == Guid.Empty))
            {
                errorList.Add(Resources.ResourceManager.GetString(name: "InvalidQuestionContent"));
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
    }
}
