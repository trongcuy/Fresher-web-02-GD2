using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB02.EMIS.API.Common.Entity;

namespace WEB02.EMIS.API.DL.Interfaces
{
    public interface ITopicDL : IBaseDL<Topic>
    {
        /// <summary>
        /// Lấy chủ đề theo ID môn và khối
        /// </summary>
        /// <param name="gradeID"></param>
        /// <param name="subjectID"></param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 31/05/2023
        public IEnumerable<Topic> GetByGradeSubject(Guid gradeID, Guid subjectID);
    }
}
