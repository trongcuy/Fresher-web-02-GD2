using EMIS.BL.BaseBL;
using EMIS.Common;
using EMIS.Common.DTO;
using EMIS.Common.Entity;
using EMIS.Common.ExceptionEntity;
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

        #region Constructor
        public ExerciseBL(IExerciseDL exerciseDL) : base(exerciseDL)
        {
            _exerciseDL = exerciseDL;
        }
        #endregion

        #region Methods
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
        public IEnumerable<Exercise> GetFilter(int? state, string? subjectID, string? gradeID, string? search, int pageSize, int pageIndex)
        {
            return _exerciseDL.GetFilter(state, subjectID, gradeID, search, pageSize, pageIndex);
        }

        /// <summary>
        /// lấy ra tất cả câu hỏi theo id bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (29-5-2023)
        /// </summary>
        /// <param name="exerciseID"></param>
        /// <returns></returns>
        public IEnumerable<Question> GetAllQuestion(string exerciseID)
        {
            return _exerciseDL.GetAllQuestion(exerciseID);
        }

        /// <summary>
        /// lấy ra tất cả chủ đề theo id bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (1-6-2023)
        /// </summary>
        /// <param name="exerciseID"></param>
        /// <returns></returns>
        public IEnumerable<Topic> GetAllTopic(string exerciseID)
        {
            return _exerciseDL.GetAllTopic(exerciseID);
        }

        /// <summary>
        /// lấy ra tất cả chủ đề theo id bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (1-6-2023)
        /// </summary>
        /// <param name="exerciseID"></param>
        /// <returns></returns>
        public int InsertTopic(string exerciseID, string? topicIDs)
        {
            return _exerciseDL.InsertTopic(exerciseID, topicIDs);
        }

        /// <summary>
        /// thêm mới một đáp án, câu hỏi, bài tập
        /// CreatedBy: Trịnh Huỳnh Đức (6-6-2023)
        /// </summary>
        /// <returns></returns>
        public string InsertAll(DataAll dataAll)
        {
            if (ValidateAll(dataAll))
            {
                return _exerciseDL.InsertAll(dataAll);
            }
            else
            {
                throw new ErrorException(devmsg: Resource.ResourceManager.GetString(name: "InvalidData"), errors: errorList);
            }

        }

        /// <summary>
        /// hàm validate
        /// CreatedBy: Trịnh Huỳnh Đức (7-6-2023)
        /// </summary>
        /// <param name="exercise"></param>
        /// <returns></returns>
        protected override bool Validate(Exercise exercise)
        {
            errorList.Clear();
            // Check tên bài tập đã nhập chưa
            if (string.IsNullOrEmpty(exercise.ExerciseName))
            {
                errorList.Add(Resource.ResourceManager.GetString(name: "InvalidExerciseName"));
            }

            // Check trạng thái bài tập đã nhập đúng chưa
            if (exercise.ExerciseState!=1 && exercise.ExerciseState!=2)
            {
                errorList.Add(Resource.ResourceManager.GetString(name: "InvalidExerciseState"));
            }

            if (errorList.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// hàm validate câu hỏi
        /// CreatedBy: Trịnh Huỳnh Đức (7-6-2023)
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        private void ValidateQuestion(Question question)
        {
            // Check tên câu hỏi đã nhập chưa
            if (string.IsNullOrEmpty(question.QuestionContent))
            {
                errorList.Add(Resource.ResourceManager.GetString(name: "InvalidQuestionName"));
            }

            // Check loại câu hỏi đã nhập đúng chưa
            if (question.QuestionType>4 || question.QuestionType<1)
            {
                errorList.Add(Resource.ResourceManager.GetString(name: "InvalidQuestionType"));
            }

            // Check số thứ tự câu hỏi đã nhập chưa
            if (string.IsNullOrEmpty(question.QuestionNumber))
            {
                errorList.Add(Resource.ResourceManager.GetString(name: "InvalidQuestionNumber"));
            }
        }

        /// <summary>
        /// hàm validate answer
        /// CreatedBy: Trịnh Huỳnh Đức (7-6-2023)
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        private void ValidateAnswer(List<Answer> answers)
        {
            foreach (var answer in answers)
            {
                //kiểm tra trạng thái đáp án có hợp lệ không
                if ((answer.AnswerState != 1) && (answer.AnswerState != 2))
                {
                    //nếu chưa có lỗi này thì mới add vào
                    if (!errorList.Contains(Resource.ResourceManager.GetString(name: "InvalidAnswerState")))
                        errorList.Add(Resource.ResourceManager.GetString(name: "InvalidAnswerState"));
                }

                //kiểm tra nội dung đáp án đã có chưa
                if (string.IsNullOrEmpty(answer.AnswerContent) && (answer.AnswerImage == Guid.Empty))
                {
                    //nếu chưa có lỗi này thì mới add vào
                    if (!errorList.Contains(Resource.ResourceManager.GetString(name: "InvalidAnswerContent")))
                        errorList.Add(Resource.ResourceManager.GetString(name: "InvalidAnswerContent"));
                }
            }
        }

        /// <summary>
        /// hàm validate insert tất cả
        /// CreatedBy: Trịnh Huỳnh Đức (7-6-2023)
        /// </summary>
        /// <param name="exercise"></param>
        /// <returns></returns>
        private bool ValidateAll(DataAll dataAll)
        {
            errorList.Clear();

            //validate bài tập 
            Validate(dataAll.exercise);

            //validate câu hỏi
            ValidateQuestion(dataAll.question);

            //validate đáp án
            ValidateAnswer(dataAll.answer);

            if (errorList.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        } 
        #endregion
    }
}
