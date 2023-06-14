using Microsoft.AspNetCore.Http;
using MISA.Web02.CeGov.Common.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB02.EMIS.API.Common.Entity;
using WEB02.EMIS.API.Common.Entity.DTO;

namespace WEB02.EMIS.API.BL.Interfaces
{
    public interface IExerciseBL : IBaseBL<Exercise>
    {
        /// <summary>
        /// Thực hiện lấy thông tin của 1 bản ghi bài tập theo id và các bản ghi phụ thuộc
        /// </summary>
        /// <param name="ExerciseID">ID bản ghi bài tập</param>
        /// <returns></returns>
        /// <exception cref="ErrorException"></exception>
        /// VMHieu 28/05/2023
        public ExerciseOverview GetAllByID(Guid ExerciseID);

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
        public PagingData<Exercise> GetPaging(string? keyword, Guid? grade, Guid? subject, int? status, int skip, int take);

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
        public Guid InsertMultiple(DataInsertAll dataInsertAll);

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
        public Boolean UpdateMultiple(DataInsertAll dataInsertAll);

        /// <summary>
        /// Kiểm tra các bản ghi trong file import excel
        /// </summary>
        /// <param name="excelFile"></param>
        /// <returns>Số bản ghi hợp lệ, không hợp lệ và tổng số bản ghi</returns>
        /// CreatedBY VMHieu 19/04/2023
        public ExcelCheckTotal ExcelCheck(IFormFile excelFile);

        /// <summary>
        /// Lấy dữ liệu file excel sau khi check
        /// </summary>
        /// <param name="statusCheck">true - lấy tất cả, false - lấy các bản ghi lỗi</param>
        /// <returns></returns>
        /// CreatedBy VMHieu 11/06/2023
        public MemoryStream GetExcelFileCheck(Boolean statusCheck = false);

        /// <summary>
        /// Thưc hiện import từ dữ liệu file
        /// </summary>
        /// <returns></returns>
        /// CreatedBy VMHieu 11/06/2023
        public Guid Import(ExerciseData exerciseData);

        /// <summary>
        /// Upload ảnh
        /// </summary>
        /// <param name="file"></param>
        /// <param name="idImage"></param>
        /// <returns></returns>
        /// VMHieu 12/06/2023
        public string UploadImage(IFormFile file, Guid idImage);
    }
}
