using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB02.EMIS.API.Common.Entity;

namespace WEB02.EMIS.API.DL.Interfaces
{
    public interface IAnswerDL : IBaseDL<Answer>
    {
        /// <summary>
        /// Thêm nhiều bản ghi cùng lúc
        /// </summary>
        /// <returns></returns>
        /// CreatedBy VMHieu 28/05/2023
        public int InsertMultiple(List<Answer> answers);
    }
}
