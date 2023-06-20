using Aspose.Cells;
using EMIS.BL.ExcelBL;
using EMIS.BL.QuestionBL;
using EMIS.Common.ExceptionEntity;
using EMIS.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using EMIS.Common.Entity;

namespace MISA_BE_GD2_EMIS.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ExcelsController : ControllerBase
    {
        #region Fields 
        private IExcelBL _excelBL;
        #endregion

        #region Constructor
        public ExcelsController(IExcelBL excelBL)
        {
            _excelBL = excelBL;
        }
        #endregion

        #region Methods
        /// <summary>
        /// dowload fiel excel mẫu
        /// CreatedBy: Trịnh Huỳnh Đức (8-6-2023)
        /// </summary>
        /// <returns></returns>
        [HttpGet("Sample")]
        public IActionResult DownloadExcelSample()
        {
            try
            {
                byte[] fileBytes = _excelBL.DownloadExcelSample();

                if (fileBytes != null)
                {
                    return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "file.xlsx");
                }

                return NotFound("File không tồn tại");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }         
        }

        /// <summary>
        /// up load file excel rồi validate và trả về số bản ghi hợp lệ, không hợp lệ
        /// CreatedBy: Trịnh Huỳnh Đức (13-6-2023)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CheckExcel(IFormFile file)
        {

            try
            {
                // Trả về kết quả thành công
                return Ok(_excelBL.CheckExcel(file));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// <summary>
        /// insert số bản ghi hợp lệ vào db
        /// CreatedBy: Trịnh Huỳnh Đức (13-6-2023)
        /// </summary>
        /// <param name="exercise"></param>
        /// <returns></returns>
        [HttpPost("FileValid")]
        public IActionResult InsertExcelToDB(Exercise exercise)
        {
            try
            {
                return Ok(_excelBL.InsertExcelToDB(exercise));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// <summary>
        /// dowload fiel excel lỗi
        /// CreatedBy: Trịnh Huỳnh Đức (13-6-2023)
        /// </summary>
        /// <returns></returns>
        [HttpGet("FileInvalid")]
        public IActionResult DownloadExcelInvalid()
        {
            try
            {
                byte[] fileBytes = _excelBL.DownloadExcelInvalid();

                if (fileBytes != null)
                {
                    return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "file.xlsx");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }         
        }

        /// <summary>
        /// Hàm xử lý ngoại lệ
        /// CreatedBy: Trịnh Huỳnh Đức (10-6-2023)
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected IActionResult HandleException(Exception ex)
        {
            var error = new
            {
                devMsg = ex.Message,
                userMsg = Resource.ResourceManager.GetString(name: "ErrorMessage"),
                errorMsg = ex.Data["Error"]
            };

            if (ex is ErrorException)
            {
                return BadRequest(error);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, error);
        }
        #endregion
    }
}
