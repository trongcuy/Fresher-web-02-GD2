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
    public class TopicBL : BaseBL<Topic>, ITopicBL
    {
        private ITopicDL _topicDL;
        public TopicBL(ITopicDL topicDL) : base(topicDL)
        {
            _topicDL = topicDL;
        }

        /// <summary>
        /// Lấy chủ đề theo ID môn và khối
        /// </summary>
        /// <param name="gradeID"></param>
        /// <param name="subjectID"></param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 31/05/2023
        public IEnumerable<Topic> GetByGradeSubject(Guid gradeID, Guid subjectID)
        {
            return _topicDL.GetByGradeSubject(gradeID, subjectID);
        }
    }
}
