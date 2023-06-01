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
            _exerciseBL = exerciseBL;
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

        /// <summary>
        /// Phân trang và tìm kiếm
        /// </summary>
        /// <param name="keyword">Từ khóa</param>
        /// <param name="grade">Khối lớp học</param>
        /// <param name="subject">Môn học</param>
        /// <param name="status">Trạng thái bài tập</param>
        /// <param name="skip">Số bản ghi bỏ qua</param>
        /// <param name="take">Số bản ghi lấy</param>
        /// <returns></returns>
        /// VMHieu 29/05/2023
        [HttpGet("paging")]
        public IActionResult GetPaging(string? keyword, Guid? grade, Guid? subject, int? status, int skip, int take)
        {
            try
            {
                var result = _exerciseBL.GetPaging(keyword, grade, subject, status, skip, take);
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
