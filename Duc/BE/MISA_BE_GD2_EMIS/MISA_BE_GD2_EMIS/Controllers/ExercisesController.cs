using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using EMIS.Common.Entity;
using EMIS.BL.BaseBL;
using EMIS.BL.ExerciseBL;
using EMIS.Common.DTO;
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
        [HttpGet("Paging")]
        public IActionResult GetPage(int pageSize, int pageIndex)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _exerciseBL.GetPage(pageSize, pageIndex));             
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// <summary>
        /// lay bai tap theo bo loc
        /// CreatedBy: Trịnh Huỳnh Đức (30-5-2023)
        /// </summary>
        /// <param name="state"></param>
        /// <param name="subjectID"></param>
        /// <param name="gradeID"></param>
        /// <param name="search"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet("Filter")]
        public IActionResult GetFilter(int? state, string? subjectID, string? gradeID, string? search, int pageSize, int pageIndex)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _exerciseBL.GetFilter(state, subjectID, gradeID, search, pageSize, pageIndex));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// <summary>
        /// lấy tất cả câu hỏi theo id bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="exerciseID"></param>
        /// <returns></returns>
        [HttpGet("{exerciseID}/Question")]
        public IActionResult GetAllQuestion(string exerciseID)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _exerciseBL.GetAllQuestion(exerciseID));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }
        /// <summary>
        /// lấy tất cả chủ đề theo id bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (1-6-2023)
        /// </summary>
        /// <param name="exerciseID"></param>
        /// <returns></returns>
        [HttpGet("{exerciseID}/Topic")]
        public IActionResult GetAllTopic(string exerciseID)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _exerciseBL.GetAllTopic(exerciseID));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// <summary>
        /// thêm nhiều chủ đề theo id bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (1-6-2023)
        /// </summary>
        /// <param name="exerciseID"></param>
        /// <returns></returns>
        [HttpPost("{exerciseID}/Topic")]
        public IActionResult InsertTopic(string exerciseID, string? topicIDs)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _exerciseBL.InsertTopic(exerciseID, topicIDs));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// <summary>
        /// thêm mới đáp án, câu hỏi, bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (6-6-2023)
        /// </summary>
        /// <returns></returns>
        [HttpPost("All")]
        public IActionResult InsertAll(DataAll dataAll)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _exerciseBL.InsertAll(dataAll));
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
