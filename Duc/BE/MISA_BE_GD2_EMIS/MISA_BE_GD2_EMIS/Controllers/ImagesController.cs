using EMIS.Common.ExceptionEntity;
using EMIS.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MISA_BE_GD2_EMIS.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private string pathImage;
        public ImagesController(IConfiguration configuration)
        {
            pathImage = configuration.GetConnectionString("pathImage");
        }
        #region Methods
        /// <summary>
        /// lưu một file ảnh
        /// CreatedBy: Trịnh Huỳnh Đức (8-6-2023)
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            try
            {
                if (image != null && image.Length > 0)
                {
                    string imageName = Guid.NewGuid().ToString();
                    string fileName = imageName + "." + image.ContentType.Split('/')[1];
                    string filePath = Path.Combine(pathImage, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    // Xử lý thành công
                    return Ok(imageName);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// <summary>
        /// lấy ảnh theo tên ảnh
        /// CreatedBy: Trịnh Huỳnh Đức (8-6-2023)
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetImage(string imageName)
        {
            try
            {
                // Đường dẫn đến thư mục chứa ảnh
                string imageJpegPath = Path.Combine(pathImage, imageName+".jpeg");
                string imagePngPath = Path.Combine(pathImage, imageName+".png");
                string imageJpgPath = Path.Combine(pathImage, imageName+".jpg");

                // Đọc dữ liệu ảnh
                byte[] imageData;

                // Kiểm tra xem tệp ảnh có tồn tại không
                if (System.IO.File.Exists(imageJpegPath))
                {
                    // Đọc dữ liệu ảnh
                    imageData = System.IO.File.ReadAllBytes(imageJpegPath);
                    // Trả về phản hồi HTTP chứa dữ liệu ảnh
                    return File(imageData, "image/jpeg"); 
                }

                // Kiểm tra xem tệp ảnh có tồn tại không
                if (System.IO.File.Exists(imagePngPath))
                {
                    // Đọc dữ liệu ảnh
                    imageData = System.IO.File.ReadAllBytes(imagePngPath);
                    // Trả về phản hồi HTTP chứa dữ liệu ảnh
                    return File(imageData, "image/png"); 
                }

                // Kiểm tra xem tệp ảnh có tồn tại không
                if (System.IO.File.Exists(imageJpgPath))
                {
                    // Đọc dữ liệu ảnh
                    imageData = System.IO.File.ReadAllBytes(imageJpgPath);
                    // Trả về phản hồi HTTP chứa dữ liệu ảnh
                    return File(imageData, "image/jpg"); 
                }
                return NoContent();
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
        private IActionResult HandleException(Exception ex)
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
