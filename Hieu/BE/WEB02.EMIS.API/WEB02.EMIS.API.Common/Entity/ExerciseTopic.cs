using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB02.EMIS.API.Common.Entity
{
    /// <summary>
    /// Đối tượng chủ đề bài tập
    /// VMHieu 25/05/2023
    /// </summary>
    public class ExerciseTopic
    {
        #region Property
        /// <summary>
        /// ID chủ đề bài tập
        /// </summary>
        public Guid ExerciseTopicID { get; set; }
        /// <summary>
        /// ID bài tập
        /// </summary>
        public Guid ExerciseID { get; set; } 
        #endregion
    }
}
