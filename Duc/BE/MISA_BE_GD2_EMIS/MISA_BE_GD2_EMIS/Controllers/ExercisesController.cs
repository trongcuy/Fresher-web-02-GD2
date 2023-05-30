using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using EMIS.Common.Entity;
using EMIS.BL.BaseBL;
using EMIS.BL.ExerciseBL;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA_BE_GD2_EMIS.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ExercisesController : BaseController<Exercise>
    {
        #region Fields 
        private IExerciseBL _exerciseBL;
        #endregion

        #region Constructor
        public ExercisesController(IExerciseBL exerciseBL) : base(exerciseBL)
        {
            _exerciseBL = exerciseBL;
        }
        #endregion

        #region Methods
        /// <summary>
        /// lấy bài tập theo trang
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet("{pageSize}/{pageIndex}")]
        public IActionResult GetPage(int pageSize, int pageIndex)
        {
            try
            {
                //gọi đến BaseBL lấy theo trang
                var result = _exerciseBL.GetPage(pageSize, pageIndex);
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
