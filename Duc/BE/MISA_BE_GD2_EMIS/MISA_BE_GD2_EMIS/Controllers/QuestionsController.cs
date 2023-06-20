using EMIS.BL.BaseBL;
using EMIS.BL.ExerciseBL;
using EMIS.BL.QuestionBL;
using EMIS.Common.Entity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA_BE_GD2_EMIS.Controllers
{
    [Route("api/v1/[controller]")]
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
        /// lấy tất cả đáp án theo id câu hỏi
        /// CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        [HttpGet("{questionID}/Answer")]
        public IActionResult GetAllAnswer(string questionID)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _questionBL.GetAllAnswer(questionID));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// <summary>
        /// thêm đáp án theo id câu hỏi
        /// CreatedBy: Trịnh Huỳnh Đức (4-6-2023)
        /// </summary>
        /// <param name="questionID"></param>
        /// <param name="answers"></param>
        /// <returns></returns>
        [HttpPost("{questionID}/Answer")]
        public IActionResult InsertAnswer(string questionID, List<Answer> answers)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, _questionBL.InsertAnswer(questionID, answers));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        #endregion
    }
}
