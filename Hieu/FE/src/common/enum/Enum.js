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
    Fail: 2, // Thao tác thất bại
}

export default Enum;