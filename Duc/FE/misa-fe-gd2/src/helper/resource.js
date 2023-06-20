const Resource = {
    VI: {
        //loại câu hỏi
        titleQuestion: {
            select: "Chọn đáp án",
            truefalse: "Đúng sai",
            fill: "Điền vào chỗ trống",
            match: "Ghép nối",
            group: "Câu hỏi nhóm",
            essay: "Tự luận"
        },
        //trạng thái
        stateExercise: {
            1: "Đang soạn",
            2: "Đã soạn"
        },
        typeQuestion: {
            1: "select",
            2: "truefalse",
            3: "fill",
            4: "essay"
        },
        //thông báo
        notifyContent: {
            exception: "Đã có lỗi xảy ra vui lòng liên hệ MISA!",
            errorRemove: "Xóa không thành công!",
            error: "Thêm không thành công!",
            success: "Bạn đã xóa thành công!",
            successImport: "Bạn đã nhập khẩu thành công!",
            successAddExercise: "Soạn bài thành công!",
            warnning: "Câu hỏi phải có ít nhất một đáp án",
            errorFile: "Định dạng file không hợp lệ",
            errorSizeExcel: "Kích thước file không được vượt quá 5Mb",
        },
        //hộp thoại
        dialogContent: {
            removeQuestion: "Bạn có chắc muốn xóa câu hỏi không?",
            removeExercise: {
                firtContent: "Bạn có chắc muốn xóa ",
                lastContent: " không?"
            },
            questionNull: "Bạn vui lòng nhập nội dung câu hỏi.",
            answerNull: "Bạn vui lòng nhập nội dung đáp án, chọn đáp án đúng.",
            exerciseNameTooLong: "Tên bài tập không vượt quá 255 ký tự.",
            noContent: "Không tìm thấy học liệu"
        },
        //các nút
        button: {
            share: "Chia sẻ",
            play: "Tạo trò chơi",
            createExercise: "Soạn bài",
            addInfor: "Bổ sung thông tin",
            changeType: "Đổi kiểu soạn",
            tryDo: "Làm thử",
            complete: "Hoàn thành",
            uploadFile: "Tải lên file bài tập",
            dowloadExcelSample: "Tải file Excel mẫu",
            close: "Đóng",
            save: "Lưu",
            cancel: "Hủy",
            add: "Thêm",
            more: "Xem thêm",
            saveAndNew: "Lưu và thêm câu",
            saveAndClose: "Lưu và đóng",
            addAnswer: "Thêm đáp án",
            addExplane: "Thêm lời giải",
            backToQuestion: "Quay lại câu hỏi",
        },
        //danh sách lựa chọn của combobox
        combobox: {
            typeQuestion: ['Chọn đáp án', 'Đúng sai', 'Điền vào chỗ trống', 'Tự luận', 'Ghép nối', 'Câu hỏi nhóm'],
            stateExercise: ['Đang soạn', 'Đã soạn']
        },
        //input
        input: {
            search: "Nhập tên học liệu để tìm kiếm",
            nameExercise: "Nhập tên bài tập..."
        }
    },
    EN: {
    }
}

export default Resource