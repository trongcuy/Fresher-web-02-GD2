using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB02.EMIS.API.BL.Interfaces;
using WEB02.EMIS.API.Common.Entity;
using WEB02.EMIS.API.DL.Interfaces;

namespace WEB02.EMIS.API.BL.Services
{
    public class AnswerBL : BaseBL<Answer>, IAnswerBL
    {

        private IAnswerDL _answerDL;
        public AnswerBL(IAnswerDL answerDL) : base(answerDL)
        {
        }

        /// <summary>
        /// Xóa nhiều bản ghi cùng lúc
        /// </summary>
        /// <param name="ids">Chuỗi chứa các id của đáp án cần xóa</param>
        /// <returns></returns>
        /// CreatedBy VMHieu 28/05/2023
        public int DeleteMultiple(string ids)
        {
            return _answerDL.DeleteMultiple(ids);
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
    }
}
