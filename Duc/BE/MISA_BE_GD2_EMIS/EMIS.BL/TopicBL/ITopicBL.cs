using EMIS.BL.BaseBL;
using EMIS.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.BL.TopicBL
{
    public interface ITopicBL : IBaseBL<Topic>
    {
        /// <summary>
        /// lấy ra tất cả chủ đề theo id môn và khối
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="subjectID"></param>
        /// <param name="gradeID"></param>
        /// <returns></returns>
        public IEnumerable<Topic> GetAllTopic(string subjectID, string gradeID);
    }
}
