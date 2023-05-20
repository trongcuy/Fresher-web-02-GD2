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
}

export default Resource;