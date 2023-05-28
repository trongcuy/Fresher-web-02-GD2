using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB02.EMIS.API.BL.Interfaces;
using WEB02.EMIS.API.Common.Entity;
using WEB02.EMIS.API.Common.Entity.DTO;
using WEB02.EMIS.API.DL.Interfaces;

namespace WEB02.EMIS.API.BL.Services
{
    public class ExerciseBL : BaseBL<Exercise>, IExerciseBL
    {
        private IExerciseDL _exerciseDL;
        public ExerciseBL(IExerciseDL exerciseDL) : base(exerciseDL)
        {
        }

        /// <summary>
        /// Thực hiện lấy thông tin của 1 bản ghi bài tập theo id và các bản ghi phụ thuộc
        /// </summary>
        /// <param name="ExerciseID">ID bản ghi bài tập</param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 28/05/2023
        public ExerciseOverview GetAllByID(Guid ExerciseID)
        {
            return _exerciseDL.GetAllByID(ExerciseID);
        }
    }
}
