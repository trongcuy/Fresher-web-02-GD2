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

export default Enum;