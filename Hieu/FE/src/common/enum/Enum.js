// Các Enum dùng chung toàn chương trình
var Enum = Enum || {};

/**
 * Form tạo câu hỏi 
 * CreatedBy VMHieu 21/05/2023
 */
export const FormQuestion = {
    Select: 1, // Form chọn đáp án
    YesOrNo: 2, // Form đúng sai
    Fill: 3, // Form điền vào chỗ trống
    Essay: 4, // Form tự luận
    Group: 5, // Form câu hỏi nhóm
}

/**
 * Các giá trị của trạng thái popup
 * CreatedBy VMHieu 23/05/2023
 */ 
export const PopupStatus = {
    Delete: 1,   // Cảnh báo trước khi xóa
    Error: 2, // Cảnh báo lỗi
    ErrorServer: 3, // Lỗi server
}

/**
 * Các giá trị của trạng thái toast
 * CreatedBy VMHieu 31/03/2023
 */ 
export const ToastStatus = {
    Success: 1,     // Thao tác thành công
    Fail: 2, // Thao tác thất bại,
    Warning: 3 // Cảnh báo
}

/**
 * Các giá trị của trạng thái bài tập
 * CreatedBy VMHieu 30/05/2023
 */ 
export const ExerciseStatus = {
    All: "",
    Composing: 1, // Đang soạn
    Composed: 2, // Đã soạn
}
/**
 * Trạng thái đáp án
 */
export const AnswerStatus = {
    True: 1,
    False: 0,
}
/**
 * Số bản ghi cần lấy mỗi lần mở rộng
 * VMHieu 30/05/2023
 */
export const TakeExercise = {
    Take: 9
}
/**
 * Mode của form bài tập
 */
export const FormModeExercise = {
    Add: 1,
    Edit: 2
}

/**
 * Mode của form câu hỏi
 */
export const FormModeQuestion = {
    Add: 1,
    Edit: 2,
    Clone: 3
}

/**
 * Trạng thái xóa đối tượng
 */
export const StatusDelete = {
    Exercise: 1,
    Question: 2
}

/**
 * Mode của form upload image
 */
export const FormModeUpload = {
    Answer: 1,
    Question: 2
}
export default Enum;