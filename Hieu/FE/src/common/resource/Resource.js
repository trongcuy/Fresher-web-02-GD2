// Các resource dùng chung toàn chương trình
var Resource = Resource || {};

export const DataRewardLevel = [
    {
        Data: "Đã soạn",
        RewardLevel: ""
    },
    {
        Data: "Đang soạn",
        RewardLevel: "0001"
    },
]

export const PropName = {
    All: "Tất cả",
    RewardObject: "RewardObject",   // Đối tượng khen thưởng
    Status: "Status",               // Trạng thái sử dụng
    TypeMovement: "TypeMovement", // Loại phong trào
    RewardLevel: "RewardLevel", // Cấp khen thưởng
    FormQuestion: "FormQuestion", //
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
    Class: "Tất cả các khối"
}
/**
 * Các text trong trang web
 * CreatedBy VMHieu 24/05/2023
 */
export const TextWeb = {
    Header: {
        Overview: "Tổng quan",
        Class: "Lớp học",
        Courseware: "Học liệu",
        Traning: "Đào tạo MISA EMIS",
        Greeting: "Xin chào, "
    }
}

export default Resource;