using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MISA_BE_GD2_EMIS.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        #region Methods
        /// <summary>
        /// lưu một file ảnh
        /// CreatedBy: Trịnh Huỳnh Đức (8-6-2023)
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                string imageName = Guid.NewGuid().ToString();
                string fileName = imageName + "." + image.ContentType.Split('/')[1];
                string filePath = Path.Combine("C:\\Users\\DELL\\OneDrive\\Tài liệu\\TaiLieuCodeVue\\Fresher-web-02-GD2\\Duc\\BE\\MISA_BE_GD2_EMIS\\Common\\Img\\", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // Xử lý thành công
                return Ok(imageName);
            }
            // Xử lý lỗi
            return BadRequest();
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
            // Đường dẫn đến thư mục chứa ảnh
            string imageJpegPath = Path.Combine("C:\\Users\\DELL\\OneDrive\\Tài liệu\\TaiLieuCodeVue\\Fresher-web-02-GD2\\Duc\\BE\\MISA_BE_GD2_EMIS\\Common\\Img\\", imageName+".jpeg");
            string imagePngPath = Path.Combine("C:\\Users\\DELL\\OneDrive\\Tài liệu\\TaiLieuCodeVue\\Fresher-web-02-GD2\\Duc\\BE\\MISA_BE_GD2_EMIS\\Common\\Img\\", imageName+".png");
            string imageJpgPath = Path.Combine("C:\\Users\\DELL\\OneDrive\\Tài liệu\\TaiLieuCodeVue\\Fresher-web-02-GD2\\Duc\\BE\\MISA_BE_GD2_EMIS\\Common\\Img\\", imageName+".jpg");

            // Đọc dữ liệu ảnh
            byte[] imageData;

            // Kiểm tra xem tệp ảnh có tồn tại không
            if (System.IO.File.Exists(imageJpegPath))
            {
                // Đọc dữ liệu ảnh
                imageData = System.IO.File.ReadAllBytes(imageJpegPath);
                // Trả về phản hồi HTTP chứa dữ liệu ảnh
                return File(imageData, "image/jpeg"); // hoặc "image/png" tùy vào định dạng ảnh
            }

            // Kiểm tra xem tệp ảnh có tồn tại không
            if (System.IO.File.Exists(imagePngPath))
            {
                // Đọc dữ liệu ảnh
                imageData = System.IO.File.ReadAllBytes(imagePngPath);
                // Trả về phản hồi HTTP chứa dữ liệu ảnh
                return File(imageData, "image/png"); // hoặc "image/png" tùy vào định dạng ảnh
            }

            // Kiểm tra xem tệp ảnh có tồn tại không
            if (System.IO.File.Exists(imageJpgPath))
            {
                // Đọc dữ liệu ảnh
                imageData = System.IO.File.ReadAllBytes(imageJpgPath);
                // Trả về phản hồi HTTP chứa dữ liệu ảnh
                return File(imageData, "image/jpg"); // hoặc "image/png" tùy vào định dạng ảnh
            }
            return NoContent();
        } 
        #endregion
    }
}
