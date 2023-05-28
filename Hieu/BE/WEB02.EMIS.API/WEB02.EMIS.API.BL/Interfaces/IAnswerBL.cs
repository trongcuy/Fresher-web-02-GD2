using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB02.EMIS.API.Common.Entity;

namespace WEB02.EMIS.API.BL.Interfaces
{
    public interface IAnswerBL : IBaseBL<Answer> 
    {

        /// <summary>
        /// Xóa nhiều bản ghi cùng lúc
        /// </summary>
        /// <param name="ids">Chuỗi chứa các id của đáp án cần xóa</param>
        /// <returns></returns>
        /// CreatedBy VMHieu 28/05/2023
        public int DeleteMultiple(string ids);

        /// <summary>
        /// Thêm nhiều bản ghi cùng lúc
        /// </summary>
        /// <returns></returns>
        /// CreatedBy VMHieu 28/05/2023
        public int InsertMultiple(List<Answer> answers);
    }
}
