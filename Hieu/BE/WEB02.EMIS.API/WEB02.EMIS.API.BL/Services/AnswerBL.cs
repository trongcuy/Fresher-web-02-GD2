﻿using System;
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
    public class AnswerBL : BaseBL<Answer>, IAnswerBL
    {

        private IAnswerDL _answerDL;
        public AnswerBL(IAnswerDL answerDL) : base(answerDL)
        {
            _answerDL = answerDL;
        }

        /// <summary>
        /// Thêm nhiều bản ghi cùng lúc
        /// </summary>
        /// <returns></returns>
        /// CreatedBy VMHieu 28/05/2023
        public int InsertMultiple(List<Answer> answers)
        {
            return _answerDL.InsertMultiple(answers);
        }

        /// <summary>
        /// Hàm validate dữ liệu
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        /// VMHieu 07/06/2023
        protected override bool Validate(Answer answer)
        {
            errorList.Clear();
            // Xử lý nghiệp vụ
            // Nội dung đáp án không được để trống
            if (string.IsNullOrEmpty(answer.AnswerContent) && string.IsNullOrEmpty(answer.AnswerImage))
            {
                errorList.Add(Resources.ResourceManager.GetString(name: "InvalidAnswerContent"));
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