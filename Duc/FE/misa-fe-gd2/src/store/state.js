export default {
    username: "Trịnh Huỳnh Đức",
    showOverlay: false,//lớp phủ
    typePopupAdd: "select",//loại popup thêm câu hỏi
    showNotify: 'no',//notify
    showLoading: false,//loading
    pageSize: 9,//số bản ghi
    pageIndex: 1,//số trang
    exerciseList: [],//danh sách bài tập
    dataFilter: {
        state: 0,
        subjectID: "",
        gradeID: "",
        search: ""
    },//thông tin dùng để lọc
    subjectList: [],//danh sách môn học
    gradeList: [],//danh sachs lớp
    topicList: [],//danh sách chủ đề
    subjectOptions: [],//danh sách môn học chỉ chưas tên cho combobox
    gradeOptions: [],//danh sách khối chỉ chưas tên cho combobox
    exerciseIDSelected: "",//lưu id bài tập đang thao tác
    exerciseSelected: {},//lưu bài tập đang thao tác
    questionList: [],//lưu danh sách câu hỏi của bai tap
    numQuestion: 0,//lưu số câu hỏi của bài tập
    topicExercise: [],//lưu danh sách chủ đề của bài tập đang sửa
    imageIdExercise: '',//id ảnh bài tập
    urlImageExercise: 'https://sisapapp.misacdn.net/lms/img/tiengviet1.edd81b92.png',//url ảnh bài tập gọi từ api
}