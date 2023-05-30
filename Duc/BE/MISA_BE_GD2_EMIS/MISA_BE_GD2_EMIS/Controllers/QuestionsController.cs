using EMIS.BL.BaseBL;
using EMIS.BL.ExerciseBL;
using EMIS.BL.QuestionBL;
using EMIS.Common.Entity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA_BE_GD2_EMIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : BaseController<Question>
    {
        #region Fields 
        private IQuestionBL _questionBL;
        #endregion

        #region Constructor
        public QuestionsController(IQuestionBL questionBL) : base(questionBL)
        {
            _questionBL = questionBL;
        }
        #endregion

        #region Methods
        /// <summary>
        /// lấy tất cả câu hỏi theo id bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="exerciseID"></param>
        /// <returns></returns>
        [HttpGet("ExerciseID/{exerciseID}")]
        public IActionResult GetAllQuestion(string exerciseID)
        {
            try
            {
                //gọi đến BaseBL lấy theo trang
                var result = _questionBL.GetAllQuestion(exerciseID);
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
