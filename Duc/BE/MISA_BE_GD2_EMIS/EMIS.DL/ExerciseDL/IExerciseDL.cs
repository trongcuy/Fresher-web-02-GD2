using EMIS.Common.DTO;
using EMIS.Common.Entity;
using EMIS.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.DL.ExerciseDL
{
    public interface IExerciseDL:IBaseDL<Exercise>
    {
        /// <summary>
        /// lấy theo trang bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IEnumerable<Exercise> GetPage(int pageSize, int pageIndex);

        /// <summary>
        /// lay bai tap theo bo loc
        /// CreatedBy: Trịnh Huỳnh Đức (30-5-2023)
        /// </summary>
        /// <param name="state"></param>
        /// <param name="subjectID"></param>
        /// <param name="gradeID"></param>
        /// <param name="search"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IEnumerable<Exercise> GetFilter(int? state, string? subjectID, string? gradeID, string? search, int pageSize, int pageIndex);

        /// <summary>
        /// lấy câu hỏi theo id bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="exerciseID"></param>
        /// <returns></returns>
        public IEnumerable<Question> GetAllQuestion(string exerciseID);

        /// <summary>
        /// lấy chủ đề theo id bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (1-6-2023)
        /// </summary>
        /// <param name="exerciseID"></param>
        /// <returns></returns>
        public IEnumerable<Topic> GetAllTopic(string exerciseID);

        /// <summary>
        /// thêm chủ đề theo id bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (1-6-2023)
        /// </summary>
        /// <param name="exerciseID"></param>
        /// <param name="topicIDs"></param>
        /// <returns></returns>
        public int InsertTopic(string exerciseID, string? topicIDs);

        /// <summary>
        /// thêm mới một đáp án, câu hỏi, bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (6-6-2023)
        /// </summary>
        /// <param name="dataAll"></param>
        /// <returns></returns>
        public string InsertAll(DataAll dataAll);
    }
}
