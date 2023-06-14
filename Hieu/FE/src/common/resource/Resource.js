// Các resource dùng chung toàn chương trình
var Resource = Resource || {};

export const DataExerciseStatus = [
    {
        Data: "Đã soạn",
        ExerciseStatus: "2"
    },
    {
        Data: "Đang soạn",
        ExerciseStatus: "1"
    },
]

export const PropName = {
    All: "Tất cả",
    ExerciseStatus: "ExerciseStatus",
    FormQuestion: "FormQuestion",
}
/**
 * Data combobox cho các loại câu hỏi
 * CreatedBy VMHieu 24/05/2023
 */
export const FormQuestionType = [
    {
        Data: "Chọn đáp án",
        FormQuestion: "1"
    },
    {
        Data: "Đúng sai",
        FormQuestion: "2"
    },
    {
        Data: "Điền vào chỗ trống",
        FormQuestion: "3"
    },
    {
        Data: "Tự luận",
        FormQuestion: "4"
    },
    {
        Data: "Bài tập nhóm",
        FormQuestion: "5"
    },
]
/**
 * Các dữ liệu cho CKEditor
 * CreatedBy VMHieu 24/05/2023
 */
export const DataCKEditor = {
    placeholderQuestion: "Nhập câu hỏi tại đây...",
    placeholderAnswer: "Nhập đáp án..."
}

/**
 * Các dữ liệu mặc định ban đầu của combobox
 * CreatedBy VMHieu 24/05/2023
 */
export const ComboboxDefault = {
    Courseware: "Tất cả học liệu",
    Subject: "Tất cả các môn",
    Grade: "Tất cả các khối",
    Topic: "Chọn chủ đề"
}
/**
 * Các text trong trang web
 * CreatedBy VMHieu 24/05/2023
 */
export const TextWeb = {
    Header: {
        Overview: "Tổng quan",
        Grade: "Lớp học",
        Courseware: "Học liệu",
        Traning: "Đào tạo MISA EMIS",
        Greeting: "Xin chào, "
    }
}

/**
 * Nội dung thông báo toast message
 * CreatedBy VMHieu 30/05/2023
 */
export const ToastSuccess = {
    AddSuccess: "Thêm dữ liệu thành công",
    EditSuccess: "Sửa dữ liệu thành công",
    DeleteSuccess: "Xóa dữ liệu thành công",
    LoadSuccess: "Tải dữ liệu thành công",
    ImportSuccess: "Nhập khẩu dữ liệu thành công",
}

/**
 * Nội dung thông báo toast message
 * CreatedBy VMHieu 30/05/2023
 */
export const ToastWarning = {
    LeastAnswer: "Câu hỏi phải có ít nhất một đáp án!",
    LimitAnswer: "Số đáp án đạt giới hạn!"
}
/**
 * Nội dung thông báo toast message
 * CreatedBy VMHieu 30/05/2023
 */
export const ToastFail = {
    AddFail: "Thêm thất bại",
    EditFail: "Sửa thất bại",
    DeleteFail: "Xóa thất bại",
    LoadFail: "Tải dữ liệu thất bại",
    ExportFail: "Xuất khẩu thất bại",
    ImportFail: "Nhập khẩu thất bại",
    CheckFail: "Check file thất bại",
    DownloadFileFail: "Tải file thất bại",
    InvalidDataResponse: "Không nhận được dữ liệu trả về"
}

/**
 * Các giá trị của trạng thái bài tập
 * CreatedBy VMHieu 30/05/2023
 */ 
export const ExerciseStatus = {
    All: "",
    Composing: "Đang soạn", // Đang soạn
    Composed: "Đã soạn", // Đã soạn
}

/**
 * Nội dung xóa danh hiệu thi đua
 * CreatedBy VMHieu 01/06/2023
 */
export const PopupMessage = {
    DeleteExercise: "Bạn có chắc chắn muốn xóa bài tập * này không?",
    DeleteQuestion: "Bạn có chắc chắn muốn xóa câu hỏi không?",
    DeleteMultiple: "Xóa * danh hiệu đã chọn?",
    DuplicateCode: "Mã danh hiệu * đã tồn tại trong hệ thống. Xin vui lòng kiểm tra lại.",
    ErrorFilter: "Dữ liệu lọc không hợp lệ. Vui lòng chọn dữ liệu lọc trong danh sách.",
    NotValidFileSuccess: "Không có bản ghi nào hợp lệ để nhập khẩu.",
    NotValidFileFali: "Không có bản ghi lỗi nào để tải về.",
    NotQuestion: "Bạn vui lòng nhập nội dung câu hỏi",
    NotAnswerCheck: "Bạn vui lòng nhập nội dung đáp án, chọn đáp án đúng",
    NotAnswerFill: "Bạn vui lòng nhập nội dung đáp án",
    Breakpoint: "*", // Kí tự ngắt dòng để thay thế 
}
/**
 * Nội dung thông báo input
 */
export const InputMessage = {
    InvalidError: "Không được bỏ trống"
}
/**
 * Đáp án "Đúng" trong câu Yes No
 */
export const AnswerYes = {
    AnswerContent: "Đúng",
    AnswerImage: "",
    AnswerStatus: "",
    SortOder: 0,
}
/**
 * Đáp án "Sai" trong câu Yes No
 */
export const AnswerNo = {
    AnswerContent: "Sai",
    AnswerImage: "",
    AnswerStatus: "",
    SortOder: 1,
}


export default Resource;