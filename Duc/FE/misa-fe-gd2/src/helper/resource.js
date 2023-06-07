const Resource = {
    VI: {     
        //loại câu hỏi
        TitleQuestion: {
            select: "Chọn đáp án",
            truefalse: "Đúng sai",
            fill: "Điền vào chỗ trống",
            match: "Ghép nối",
            group: "Câu hỏi nhóm",
            essay: "Tự luận"
        },
        //trạng thái
        StateExercise: {
            1: "Đang soạn",
            2: "Đã soạn"
        },
        TypeQuestion: {
            1: "select",
            2: "truefalse",
            3: "fill",
            4: "essay"
        },
        NotifyContent: {
            exception: "Đã có lỗi xảy ra vui lòng liên hệ MISA!",
            errorRemove: "Xóa không thành công!",
            error: "Thêm không thành công!",
            success: "Bạn đã xóa thành công!",           
        },
        DialogContent: {
            removeQuestion: "Bạn có chắc muốn xóa câu hỏi không?",
            removeExercise: {
                firtContent: "Bạn có chắc muốn xóa ",
                lastContent: " không?"
            },
            questionNull: "Bạn vui lòng nhập nội dung câu hỏi.",
            answerNull: "Bạn vui lòng nhập nội dung đáp án, chọn đáp án đúng."      
        }
    },
    EN: {
    }
}

export default Resource