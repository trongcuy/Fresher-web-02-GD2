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
            byte[] fileBytes = _excelBL.DownloadExcelSample();

            if (fileBytes != null)
            {
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "file.xlsx");
            }

            return NotFound();
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
            if (file == null || file.Length <= 0)
            {
                return BadRequest("Invalid file");
            }
            try
            {
                // Trả về kết quả thành công
                return Ok(_excelBL.CheckExcel(file));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// insert số bản ghi hợp lệ vào db
        /// CreatedBy: Trịnh Huỳnh Đức (13-6-2023)
        /// </summary>
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
                return BadRequest(ex);
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
            byte[] fileBytes = _excelBL.DownloadExcelInvalid();

            if (fileBytes != null)
            {
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "file.xlsx");
            }

            return NotFound();
        } 
        #endregion
    }
}
