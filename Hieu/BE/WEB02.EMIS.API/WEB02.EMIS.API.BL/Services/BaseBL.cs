using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB02.EMIS.API.DL.Interfaces;

namespace WEB02.EMIS.API.BL.Services
{
    public class BaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;
        protected List<string> errorList;


        #endregion

        #region Constructor
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// CreatedBy VMHieu 20/03/2023
        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
            errorList = new List<string>();
        }

        #endregion
        #region Method
        /// <summary>
        /// Lấy toàn bộ bản ghi
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        /// CreatedBy VMHieu 27/05/2023 
        public IEnumerable<T> GetAll()
        {
            return _baseDL.GetAll();
        }
        /// <summary>
        /// Lấy bản ghi thông qua ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần lấy</param>
        /// <returns>Bản ghi có id đã tìm</returns>
        /// CreatedBy VMHieu 27/05/2023 
        public T GetByID(Guid id)
        {
            return _baseDL.GetByID(id);
        }
        /// <summary>
        /// Thêm 1 bản ghi 
        /// </summary>
        /// <param name="entity">bản ghi cần thêm</param>
        /// <returns>Số lượng bản ghi được thêm vào</returns>
        /// CreatedBy VMHieu 27/05/2023
        public int Insert(T entity)
        {
            return _baseDL.Insert(entity);
        }
        /// <summary>
        /// Sửa 1 bản ghi
        /// </summary>
        /// <param name="entity">bản ghi cần sửa</param>
        /// <param name="id">ID của bản ghi cần sửa</param>
        /// <returns>Số lượng bản ghi đã sửa</returns>
        /// CreatedBy VMHieu 27/05/2023
        public int Update(T entity, Guid id)
        {
            return _baseDL.Update(entity, id);
        }
        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="id">ID của bản ghi cần xóa</param>
        /// <returns>Số lượng bản ghi đã xóa</returns>
        /// CreatedBy VMHieu 27/05/2023
        public int Delete(Guid id)
        {
            return _baseDL.Delete(id);
        }
        #endregion
    }
}
