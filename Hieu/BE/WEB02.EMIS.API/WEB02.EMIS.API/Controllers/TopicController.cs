using Microsoft.AspNetCore.Mvc;
using WEB02.EMIS.API.BL.Interfaces;
using WEB02.EMIS.API.Common.Entity;

namespace WEB02.EMIS.API.Controllers
{
    public class TopicController : BaseController<Topic>
    {
        private ITopicBL _topicBL;
        public TopicController(ITopicBL topicBL) : base(topicBL)
        {
            _topicBL = topicBL;
        }

        /// <summary>
        /// Lấy chủ đề theo ID môn và khối
        /// </summary>
        /// <param name="gradeID"></param>
        /// <param name="subjectID"></param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 31/05/2023
        [HttpGet("gradesubject")]
        public IActionResult GetByGradeSubject(Guid gradeID, Guid subjectID)
        {
            try
            {
                var result = _topicBL.GetByGradeSubject(gradeID, subjectID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }
    }
}
