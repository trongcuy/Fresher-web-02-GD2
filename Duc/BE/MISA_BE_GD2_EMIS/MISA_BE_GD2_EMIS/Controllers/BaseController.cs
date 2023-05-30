using EMIS.BL.BaseBL;
using Microsoft.AspNetCore.Mvc;

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
                //gọi đến BaseBL lấy tất cả 
                var result = _baseBL.GetAll();
                //Xử lý kết quả trả về ở db
                if(result == null)
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
                //gọi đến BaseBL 
                var result = _baseBL.GetById(id);
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
                //gọi đến BaseBL 
                var result = _baseBL.Insert(value);
                //Xử lý kết quả trả về ở db
                if (result == 0)
                {
                    return BadRequest();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
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
                //gọi đến BaseBL 
                var result = _baseBL.Update(value);
                //Xử lý kết quả trả về ở db
                if (result == 0)
                {
                    return BadRequest();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
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
                //gọi đến BaseBL
                var result = _baseBL.DeleteById(id);
                //Xử lý kết quả trả về ở db
                if (result == 0)
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
