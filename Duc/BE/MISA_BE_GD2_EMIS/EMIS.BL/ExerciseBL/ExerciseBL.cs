using EMIS.BL.BaseBL;
using EMIS.Common.Entity;
using EMIS.DL.BaseDL;
using EMIS.DL.ExerciseDL;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.BL.ExerciseBL
{
    public class ExerciseBL : BaseBL<Exercise>, IExerciseBL
    {
        #region Field
        private IExerciseDL _exerciseDL;
        #endregion

        public ExerciseBL (IExerciseDL exerciseDL) : base(exerciseDL)
        {
            _exerciseDL = exerciseDL;
        }

        /// <summary>
        /// lấy theo trang bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IEnumerable<Exercise> GetPage(int pageSize, int pageIndex)
        {
            return _exerciseDL.GetPage(pageSize, pageIndex);
        }
    }
}
