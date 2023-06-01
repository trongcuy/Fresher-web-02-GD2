using Microsoft.AspNetCore.Mvc;
using WEB02.EMIS.API.BL.Interfaces;
using WEB02.EMIS.API.Common.Entity;

namespace WEB02.EMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AnswerController : BaseController<Answer>
    {
        private IAnswerBL _answerBL;
        public AnswerController(IAnswerBL answerBL) : base(answerBL)
        {
            _answerBL = answerBL;
        }

        /// <summary>
        /// Xóa nhiều bản ghi cùng lúc
        /// </summary>
        /// <param name="ids">Chuỗi chứa các id của đáp án cần xóa</param>
        /// <returns></returns>
        /// CreatedBy VMHieu 28/05/2023
        [HttpDelete("multiple")]
        public IActionResult DeleteMultiple(string ids)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _answerBL.DeleteMultiple(ids));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thêm nhiều bản ghi cùng lúc
        /// </summary>
        /// <returns></returns>
        /// CreatedBy VMHieu 28/05/2023
        [HttpPost("multiple")]
        public IActionResult InsertMultiple(List<Answer> answers)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _answerBL.InsertMultiple(answers));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }
    }
}
