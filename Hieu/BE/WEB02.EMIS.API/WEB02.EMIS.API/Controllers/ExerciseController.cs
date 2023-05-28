using Microsoft.AspNetCore.Mvc;
using WEB02.EMIS.API.BL.Interfaces;
using WEB02.EMIS.API.Common.Entity;
using WEB02.EMIS.API.Common.Entity.DTO;

namespace WEB02.EMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ExerciseController : BaseController<Exercise>
    {
        private IExerciseBL _exerciseBL;
        public ExerciseController(IExerciseBL exerciseBL) : base(exerciseBL)
        {
        }

        /// <summary>
        /// Thực hiện lấy thông tin của 1 bản ghi bài tập theo id và các bản ghi phụ thuộc
        /// </summary>
        /// <param name="ExerciseID">ID bản ghi bài tập</param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 28/05/2023
        [HttpGet("overview")]
        public IActionResult GetAllByID(Guid ExerciseID)
        {
            try
            {
                var result = _exerciseBL.GetAllByID(ExerciseID);
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
