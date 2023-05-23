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

export default Resource;