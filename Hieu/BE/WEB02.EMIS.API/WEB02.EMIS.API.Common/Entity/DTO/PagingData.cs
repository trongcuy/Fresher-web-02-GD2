using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB02.EMIS.API.Common.Entity.DTO
{
    /// <summary>
    /// Dữ liệu trả về khi lọc và phân trang
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu của đối tượng trong mảng trả về</typeparam>
    /// CreatedBy VMHieu 29/05/2023
    public class PagingData<T>
    {
        /// <summary>
        /// Mảng đối tượng thỏa mãn điều kiện lọc và phân trang 
        /// </summary>
        public List<T> Data { get; set; } = new List<T>();

        /// <summary>
        /// Tổng số bản ghi thỏa mãn điều kiện
        /// </summary>
        public long TotalCount { get; set; }
    }
}
