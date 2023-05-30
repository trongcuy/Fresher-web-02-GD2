using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.DL.BaseDL
{
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Lây tất cả 
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll();

        /// <summary>
        /// lấy theo id
        /// CreatedBy: Trịnh Huỳnh Đức-(29-5-23)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(string id);

        /// <summary>
        /// Thêm mới mộ đối tượng
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Insert(T value);

        /// <summary>
        /// Cập nhật một đối tượng
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Update(T value);

        /// <summary> 
        /// xoa bawng id
        /// CreatedBy: Trịnh Huỳnh Đức-(29-5-23)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteById(string id);
    }
}
