using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB02.EMIS.API.Common.Entity;

namespace WEB02.EMIS.API.DL.Interfaces
{
    public interface IExerciseTopicDL : IBaseDL<ExerciseTopic>
    {
        /// <summary>
        /// Thực hiện lấy thông tin chủ đề của 1 bài tập theo ID bài tập
        /// </summary>
        /// <param name="ExerciseID">ID bản ghi bài tập</param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 28/05/2023
        public IEnumerable<Guid> GetTopicByID(Guid ExerciseID);

        /// <summary>
        /// Thực hiện thêm nhiều bản ghi chủ đề theo bài tập
        /// </summary>
        /// <param name="ExerciseID"></param>
        /// <param name="topicIDs"></param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 06/01/2023
        public int InsertMultiple(Guid ExerciseID, List<Guid> topicIDs);
    }
}
