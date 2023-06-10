using Microsoft.AspNetCore.Mvc;
using MISA.Web02.CeGov.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB02.EMIS.API.BL.Interfaces;
using WEB02.EMIS.API.Common;
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
            _exerciseDL = exerciseDL;
        }

        /// <summary>
        /// Hàm validate dữ liệu
        /// </summary>
        /// <param name="exercise"></param>
        /// <returns></returns>
        /// VMHieu 07/06/2023
        protected override bool Validate(Exercise exercise)
        {
            errorList.Clear();
            // Xử lý nghiệp vụ
            // Môn bài tập không được để trống
            if (exercise.GradeID == null || exercise.GradeID == Guid.Empty)
            {
                errorList.Add(Resources.ResourceManager.GetString(name: "InvalidGrade"));
            }
            // Khối lớp học không được để trống
            if (exercise.SubjectID == null || exercise.SubjectID == Guid.Empty)
            {
                errorList.Add(Resources.ResourceManager.GetString(name: "InvalidSubject"));
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

        /// <summary>
        /// Phân trang và tìm kiếm
        /// </summary>
        /// <param name="keyword">Từ khóa</param>
        /// <param name="grade">Khối lớp học</param>
        /// <param name="subject">Môn học</param>
        /// <param name="status">Trạng thái bài tập</param>
        /// <param name="skip">Số bản ghi bỏ qua</param>
        /// <param name="take">Số bản ghi lấy</param>
        /// <returns></returns>
        public PagingData<Exercise> GetPaging(string? keyword, Guid? grade, Guid? subject, int? status, int skip, int take)
        {
            return _exerciseDL.GetPaging(keyword, grade, subject, status, skip, take);
        }

        /// <summary>
        /// Thực hiện thêm bài tập, câu hỏi, đáp án và chủ đề liên quan
        /// </summary>
        /// <param name="exercise"></param>
        /// <param name="question"></param>
        /// <param name="answers"></param>
        /// <param name="topicIDs"></param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 06/06/2023
        public Guid InsertMultiple(DataInsertAll dataInsertAll)
        {
            var isValid = ValidateDataAll(dataInsertAll);
            if (isValid)
            {
                return _exerciseDL.InsertMultiple(dataInsertAll);
            }
            else
            {
                throw new ErrorException(devmsg: Resources.ResourceManager.GetString(name: "InvalidData"), listErrors: errorList);
            }
        }

        /// <summary>
        /// Thực hiện thêm câu hỏi, đáp án và chủ đề liên quan trong 1 bài tập đã có
        /// </summary>
        /// <param name="exercise"></param>
        /// <param name="question"></param>
        /// <param name="answers"></param>
        /// <param name="topicIDs"></param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 06/06/2023
        public Boolean UpdateMultiple(DataInsertAll dataInsertAll)
        {
            var isValid = ValidateDataAll(dataInsertAll);
            if (isValid)
            {
                return _exerciseDL.UpdateMultiple(dataInsertAll);
            }
            else
            {
                throw new ErrorException(devmsg: Resources.ResourceManager.GetString(name: "InvalidData"), listErrors: errorList);
            }
        }

        /// <summary>
        /// Validate dữ liệu khi thao tác với nhiều entity
        /// </summary>
        /// <param name="dataInsertAll"></param>
        /// <returns></returns>
        /// VMHieu 07/06/2023
        private bool ValidateDataAll(DataInsertAll dataInsertAll)
        {
            errorList.Clear();
            // Gán các đối tượng
            Exercise exercise = dataInsertAll.exercise;
            Question question = dataInsertAll.question;
            List<Answer> answers = dataInsertAll.answers;
            List<Guid> topicIDs = dataInsertAll.topicIDs;

            // Xử lý nghiệp vụ
            // Validate bài tập (môn , khối không được để trống)
            // Khối lớp học không được để trống
            if (exercise.GradeID == null || exercise.GradeID == Guid.Empty)
            {
                errorList.Add(Resources.ResourceManager.GetString(name: "InvalidGrade"));
            }
            // Môn học không được để trống
            if (exercise.SubjectID == null || exercise.SubjectID == Guid.Empty)
            {
                errorList.Add(Resources.ResourceManager.GetString(name: "InvalidSubject"));
            }
            // Validate câu hỏi (nội dung câu hỏi không được để trống)
            if (string.IsNullOrEmpty(question.QuestionContent) && (question.QuestionImage == null || question.QuestionImage == Guid.Empty))
            {
                errorList.Add(Resources.ResourceManager.GetString(name: "InvalidQuestionContent"));
            }
            // Validate đáp án (đáp án phải có nội dung, câu hỏi chọn và đúng sai phải có đáp án đúng, đặt limit cho đáp án)
            var answerValid = true;
            foreach(var answer in answers)
            {
                // Kiểm tra có nội dung hay không
                if (string.IsNullOrEmpty(answer.AnswerContent) && (answer.AnswerImage == null || answer.AnswerImage == Guid.Empty))
                {
                    answerValid = false;
                    break;
                }
            }

            if (!answerValid)
            {
                errorList.Add(Resources.ResourceManager.GetString(name: "InvalidAnswerContent"));
            }
            // Kiểm tra phải có đáp án đúng trong 2 loại câu hỏi đúng sai và chọn
            if (question.TypeQuestion == Common.Enum.TypeQuestion.Select || question.TypeQuestion == Common.Enum.TypeQuestion.YesOrNo)
            {
                var answerCorrect = answers.Any(x => x.AnswerStatus == Common.Enum.AnswerStatus.True);
                if (!answerCorrect)
                {
                    errorList.Add(Resources.ResourceManager.GetString(name: "NotAnswerCorrect"));
                }
            }

            // Kiểm tra số lượng đáp án
            switch (question.TypeQuestion)
            {
                case (Common.Enum.TypeQuestion.Select):
                    if (answers.Count < 1 || answers.Count > 20)
                    {
                        errorList.Add(Resources.ResourceManager.GetString(name: "LimitAnswer"));
                    }
                    break;
                case (Common.Enum.TypeQuestion.Fill):
                    if (answers.Count < 1)
                    {
                        errorList.Add(Resources.ResourceManager.GetString(name: "LimitAnswer"));
                    }
                    break;
                default:
                    break;
            }

            // Trả về kết quả
            if (errorList.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
