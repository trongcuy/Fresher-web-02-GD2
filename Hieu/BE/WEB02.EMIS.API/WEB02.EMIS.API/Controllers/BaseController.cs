using Microsoft.AspNetCore.Mvc;
using MISA.Web02.CeGov.Common;
using System.Net;
using WEB02.EMIS.API.BL.Interfaces;
using WEB02.EMIS.API.Common;

namespace WEB02.EMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Filed

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constructor

        public BaseController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        #region Method

        [HttpGet]
        /// <summary>
        /// Lấy toàn bộ bản ghi
        /// </summary>
        /// <returns></returns>
        /// CreatedBy VMHieu 27/05/2023 
        public IActionResult GetAll()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _baseBL.GetAll());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }
        [HttpGet("{id}")]
        /// <summary>
        /// Lấy thông tin bản ghi qua ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// CreatedBy VMHieu 27/05/2023
        public IActionResult GetByID(Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _baseBL.GetByID(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thêm 1 bản ghi 
        /// </summary>
        /// <returns>Số dòng thay đổi sau câu lệnh</returns>
        /// CreatedBy VMHieu 27/05/2023
        [HttpPost]
        public IActionResult Insert(T entity)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, _baseBL.Insert(entity));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Sửa 1 bản ghi 
        /// </summary>
        /// <returns>Số dòng thay đổi sau câu lệnh</returns>
        /// CreatedBy VMHieu 27/05/2023
        [HttpPut("{id}")]
        public IActionResult Update(T entity, Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _baseBL.Update(entity, id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xóa 1 bản ghi 
        /// </summary>
        /// <returns>Số dòng thay đổi sau câu lệnh</returns>
        /// CreatedBy VMHieu 27/05/2023
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _baseBL.Delete(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Exception xử lý ngoại lệ
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        /// CreatedBy VMHieu 27/05/2023
        protected IActionResult HandleException(Exception ex)
        {
            var error = new
            {
                devMsg = ex.Message,
                userMsg = Resources.ResourceManager.GetString(name: "ErrorException"),
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
    
