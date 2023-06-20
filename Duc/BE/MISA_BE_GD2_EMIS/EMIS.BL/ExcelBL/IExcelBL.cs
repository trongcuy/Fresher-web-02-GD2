using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMIS.Common.DTO;
using EMIS.Common.Entity;
using Microsoft.AspNetCore.Http;

namespace EMIS.BL.ExcelBL
{
    public interface IExcelBL
    {
        /// <summary>
        /// dowload fiel excel mẫu
        /// CreatedBy: Trịnh Huỳnh Đức (8-6-2023)
        /// </summary>
        /// <returns></returns>
        public byte[] DownloadExcelSample();

        /// <summary>
        /// up load file excel rồi validate và trả về số bản ghi hợp lệ, không hợp lệ
        /// CreatedBy: Trịnh Huỳnh Đức (13-6-2023)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public Object CheckExcel(IFormFile file);

        /// <summary>
        /// lưu số bản ghi hợp lệ vào db
        /// CreatedBy: Trịnh Huỳnh Đức (13-6-2023)
        /// </summary>
        /// <param name="exercise"></param>
        /// <returns></returns>
        public string InsertExcelToDB(Exercise exercise);


        /// <summary>
        /// lấy file không hợp lệ để người dùng sửa lại
        /// CreatedBy: Trịnh Huỳnh Đức (13-6-2023)
        /// </summary>
        /// <returns></returns>
        public byte[] DownloadExcelInvalid();
    }
}
