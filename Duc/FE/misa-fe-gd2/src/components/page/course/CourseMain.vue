<template>
    <div class="div-main">
        <img src="../../../assets/img/emis.jpg" />
        <MSInput defaultValue="Nhập tên môn học để tìm kiếm" />
        <div class="div-combobox">
            <MSCombobox class="cb-document"
                :listSelect="['Đã soạn', 'Đang soạn', 'Đã chia sẻ', 'Chưa chia sẻ', 'Lấy từ thư viện']"
                defaultValue="Tất cả học liệu" src="https://sisapapp.misacdn.net/lms/img/ic_multi.7d8b269c.svg">
            </MSCombobox>
            <MSCombobox class="cb-subject"
                :listSelect="['Toán', 'Tiếng Việt', 'Tiếng Anh', 'Tự nhiên xã hội', 'Lịch sử', 'Địa lý']"
                defaultValue="Tất cả các môn" src="https://sisapapp.misacdn.net/lms/img/ic_multi.7d8b269c.svg"></MSCombobox>
            <MSCombobox class="cb-grade" :listSelect="['Khối 1', 'Khối 2', 'Khối 3', 'Khối 4', 'Khối 5']"
                defaultValue="Tất cả các khối" src="https://sisapapp.misacdn.net/lms/img/ic_multi.7d8b269c.svg">
            </MSCombobox>
        </div>

        <!-- div chứa các card -->
        <div class="div-card">
            <MSCard grade="Khối 1" subject="Giáo dục thể chất" title="Tập thể dục" state="Đang soạn" numQuestion="3"/>
            <MSCard grade="Khối 2" subject="Toán" title="7 hằng đẳng thức đáng nhớ" state="Chia sẻ" numQuestion="1"/>
            <MSCard grade="Khối 3" subject="Tiếng Việt" title="Luyện đọc" state="Đang soạn"/>
            <MSCard grade="Khối 4" subject="Lịch sử" title="Chiến tranh biên giới" state="Đang soạn"/>
            <MSCard grade="Khối 5" subject="Địa lý" title="Diện tích Việt Nam" state="Đang soạn" numQuestion="5"/>
            <MSCard grade="Khối 1" subject="Tiếng Anh" title="Bài tập về nhà" state="Đang soạn"/>
        </div>
        <!-- button xem thêm -->
        <MSButton title="Xem thêm" class="btn-more"/>
    </div>
</template>

<script>
import MSInput from '../../input/MSInput.vue'
import MSCombobox from '../../input/MSCombobox.vue'
import MSCard from '../../card/MSCard.vue'
import MSButton from '../../button/MSButton.vue'
export default {
    name: "CourseMain",
    props: [],
    components: {
        MSInput,
        MSCombobox,
        MSCard,
        MSButton
    },
    data() {
        return {

        }
    }
}
</script>
<style scoped>
.div-main>img {
    width: 100%;
    height: auto;
    border-radius: 10px;
    margin-bottom: 16px;
}
.div-combobox {
    display: flex;
    margin-top: 12px;
}
.cb-document {
    width: 150px;
    margin-right: 16px;
}

.cb-subject {
    width: 172px;
    margin-right: 16px;
}
.cb-grade {
    width: 172px;
}
.div-card {
    margin-top: 20px;
    display: flex;
    justify-content: space-between;
    width: 100%;
    flex-wrap: wrap;
}
.btn-more {
    margin-right: 16px;
    min-width: 113px;
    border: none;
    background-color: #8a6bf6;
    color: #fff;
    margin:auto;
    display:block;
    margin-top: -14px;
    margin-bottom: 20px;
}
.btn-more:hover {
    opacity: .8;
    background-color: #8a6bf6;
}
</style>
