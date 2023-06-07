using EMIS.BL.BaseBL;
using EMIS.Common;
using EMIS.Common.ExceptionEntity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA_BE_GD2_EMIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Fields 
        private IBaseBL<T> _baseBL;
        #endregion

        #region Constructor
        public BaseController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }
        #endregion

        #region Methods
        /// GET: api/<BaseController>
        /// <summary>
        /// Hàm lấy tất cả 
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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

        /// GET api/<BaseController>/5
        /// <summary>
        /// Hàm lấy giá trị theo id
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _baseBL.GetById(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// POST api/<BaseController>
        /// <summary>
        /// thêm mới đối tượng
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Insert([FromBody] T value)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, _baseBL.Insert(value));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// PUT api/<BaseController>/5
        /// <summary>
        /// Cập nhật đối tượng
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update([FromBody] T value)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _baseBL.Update(value));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }
        
        /// DELETE api/<BaseController>/5
        /// <summary>
        /// Xóa một đối tượng theo 
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteById(string id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _baseBL.DeleteById(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Hàm xử lý ngoại lệ
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
