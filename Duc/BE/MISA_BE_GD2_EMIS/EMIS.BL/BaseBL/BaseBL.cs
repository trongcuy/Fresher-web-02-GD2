using EMIS.Common;
using EMIS.Common.ExceptionEntity;
using EMIS.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.BL.BaseBL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Fields 
        private IBaseDL<T> _baseDL;
        protected List<string> errorList;
        #endregion

        #region Constructor 
        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
            errorList = new List<string>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lây tất cả 
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return _baseDL.GetAll();
        }

        /// <summary>
        /// lấy theo id
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(string id)
        {
            return _baseDL.GetById(id);
        }

        /// <summary>
        /// thêm mới một đối tượng
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Insert(T value)
        {
            if (Validate(value))
            {
                return _baseDL.Insert(value);
            }
            else
            {
                throw new ErrorException(devmsg: Resource.ResourceManager.GetString(name: "InvalidData"), errors: errorList);
            }
        }

        /// <summary>
        /// Cập nhật một đối tượng
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Update(T value)
        {
            if (Validate(value))
            {
                return _baseDL.Update(value);
            }
            else
            {
                throw new ErrorException(devmsg: Resource.ResourceManager.GetString(name: "InvalidData"), errors: errorList);
            }
               
        }

        /// <summary> 
        /// xoa một đối tượng theo id
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteById(string id)
        {
            return _baseDL.DeleteById(id);
        }

        /// <summary>
        /// hàm validate
        /// CreatedBy: Trịnh Huỳnh Đức (7-6-2023)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual bool Validate(T value)
        {
            return true;
        }
        #endregion
    }
}
