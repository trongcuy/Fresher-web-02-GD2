using Microsoft.AspNetCore.Mvc;
using WEB02.EMIS.API.BL.Interfaces;
using WEB02.EMIS.API.Common.Entity;

namespace WEB02.EMIS.API.Controllers
{
    public class ExerciseTopicController : BaseController<ExerciseTopic>
    {
        private IExerciseTopicBL _exerciseTopicBL;
        public ExerciseTopicController(IExerciseTopicBL exerciseTopicBL) : base(exerciseTopicBL)
        {
            _exerciseTopicBL = exerciseTopicBL;
        }

        /// <summary>
        /// Thực hiện lấy thông tin chủ đề của 1 bài tập theo ID bài tập
        /// </summary>
        /// <param name="ExerciseID">ID bản ghi bài tập</param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 28/05/2023
        [HttpGet("id")]
        public IActionResult GetTopicByID(Guid ExerciseID)
        {
            try
            {
                var result = _exerciseTopicBL.GetTopicByID(ExerciseID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thực hiện thêm nhiều bản ghi chủ đề theo bài tập
        /// </summary>
        /// <param name="ExerciseID"></param>
        /// <param name="topicIDs"></param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 06/01/2023
        [HttpPost("multiple")]
        public IActionResult InsertMultiple(Guid ExerciseID, List<Guid> topicIDs)
        {
            try
            {
                var result = _exerciseTopicBL.InsertMultiple(ExerciseID, topicIDs);
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
