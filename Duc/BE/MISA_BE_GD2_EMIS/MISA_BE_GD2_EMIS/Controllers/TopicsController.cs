using EMIS.BL.BaseBL;
using EMIS.BL.ExerciseBL;
using EMIS.BL.QuestionBL;
using EMIS.BL.TopicBL;
using EMIS.Common.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MISA_BE_GD2_EMIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : BaseController<Topic>
    {
        #region Fields 
        private ITopicBL _topicBL;
        #endregion

        #region Constructor
        public TopicsController(ITopicBL topicBL) : base(topicBL)
        {
            _topicBL = topicBL;
        }
        #endregion

        #region Methods
        /// <summary>
        /// lấy ra tất cả chủ đề theo id môn và khối
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="subjectID"></param>
        /// <param name="gradeID"></param>
        /// <returns></returns>
        [HttpGet("SubjectGrade")]
        public IActionResult GetAllTopic(string subjectID, string gradeID)
        {
            try
            {
                //gọi đến BaseBL lấy theo trang
                var result = _topicBL.GetAllTopic(subjectID, gradeID);
                //Xử lý kết quả trả về ở db
                if (result == null)
                {
                    return NoContent();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
