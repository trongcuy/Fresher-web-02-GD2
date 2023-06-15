using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB02.EMIS.API.BL.Interfaces;
using WEB02.EMIS.API.Common.Entity;
using WEB02.EMIS.API.DL.Interfaces;

namespace WEB02.EMIS.API.BL.Services
{
    public class ExerciseTopicBL : BaseBL<ExerciseTopic>, IExerciseTopicBL
    {
        private IExerciseTopicDL _exerciseTopicDL;
        public ExerciseTopicBL(IExerciseTopicDL exerciseTopicDL) : base(exerciseTopicDL)
        {
            _exerciseTopicDL = exerciseTopicDL;
        }

        /// <summary>
        /// Thực hiện lấy thông tin chủ đề của 1 bài tập theo ID bài tập
        /// </summary>
        /// <param name="ExerciseID">ID bản ghi bài tập</param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 28/05/2023
        public IEnumerable<Guid> GetTopicByID(Guid ExerciseID)
        {
            return _exerciseTopicDL.GetTopicByID(ExerciseID);
        }

        /// <summary>
        /// Thực hiện thêm nhiều bản ghi chủ đề theo bài tập
        /// </summary>
        /// <param name="ExerciseID"></param>
        /// <param name="topicIDs"></param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 06/01/2023
        public int InsertMultiple(Guid ExerciseID, List<Guid> topicIDs)
        {
            return _exerciseTopicDL.InsertMultiple(ExerciseID, topicIDs);
        }
    }
}
