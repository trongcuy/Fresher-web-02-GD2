<template>
    <div class="div-create">
        <!--Thanh header  -->
        <div class="div-header-main">
            <div class="div-header">
                <!-- Thanh chứa tên -->
                <div class="div-name">
                    <img class="icon-back" src="../../assets/img/icon_back.svg" @click="onClickBack" />
                    <img class="image" src="../../assets/img/default.png" />
                    <input type="text" placeholder="Nhập tên bài tập..." />
                </div>
                <!-- Thanh chứa các menu -->
                <div class="div-menu">
                    <div class="menu-left">
                        <MSCombobox class="cb-subject" defaultValue="Toán"
                            :listSelect="['Toán', 'Tiếng Việt', 'Tiếng Anh', 'Tự nhiên xã hội', 'Lịch sử', 'Địa lý']" />
                        <MSCombobox class="cb-grade" defaultValue="Khối 1"
                            :listSelect="['Khối 1', 'Khối 2', 'Khối 3', 'Khối 4', 'Khối 5']" />
                        <MSButton title="Bổ sung thông tin" @click="onClickAddInfor" />
                        <MSButton title="Đổi kiểu soạn" />
                    </div>

                    <div class="menu-right">
                        <div class="div-help"><img src="../../assets/img/ic_help.svg" /></div>
                        <MSButton title="Làm thử" />
                        <MSButton title="Hoàn thành" class="btn-complete" />
                    </div>
                </div>
            </div>
        </div>

        <!-- phần body khi chưa có câu hỏi-->
        <div class="div-body" v-if="numQuestion==0">
            <div class="div-import">
                <p class="title">Tách câu hỏi tự động sử dụng công nghệ AI</p>
                <div class="import">
                    <img src="../../assets/img/import.svg" />
                    <div class="text">Nhấn để tải lên file bài tập hoặc kéo thả file vào đây.
                        <br />File có định dạng xls, xlsx, doc, docx, pdf
                    </div>
                    <div class="btn-import">
                        <MSButton title="Tải lên file bài tập" class="btn-dowload-file" />
                        <MSButton title="Tải file Excel mẫu" class="btn-dowload-excel" />
                    </div>
                </div>
            </div>
            <div class="div-question">
                <div class="title">hoặc tự tạo câu hỏi mới</div>
                <div class="list-btn">
                    <MSButtonQuestion title="Thư viện học tập"
                        src="https://sisapapp.misacdn.net/lms/img/library.fc851823.svg" />
                    <img src="../../assets/img/line.svg" />
                    <MSButtonQuestion @click="onClickNewQuesion('select')" title="Chọn đáp án"
                        src="https://sisapapp.misacdn.net/lms/img/select.c15dfe74.svg" />
                    <MSButtonQuestion @click="onClickNewQuesion('rightwrong')" title="Đúng sai"
                        src="https://sisapapp.misacdn.net/lms/img/yesorno.7f4f0b5a.svg" />
                    <MSButtonQuestion @click="onClickNewQuesion('fill')" title="Điền vào chỗ trống"
                        src="https://sisapapp.misacdn.net/lms/img/fill.af676902.svg" />
                    <MSButtonQuestion @click="onClickNewQuesion('match')" title="Ghép nối"
                        src="https://sisapapp.misacdn.net/lms/img/pairing.4e1fb31f.svg" />
                    <MSButtonQuestion @click="onClickNewQuesion('group')" title="Câu hỏi nhóm"
                        src="	https://sisapapp.misacdn.net/lms/img/group.ca2a19ef.svg" />
                    <MSButtonQuestion @click="onClickNewQuesion('essay')" title="Tự luận"
                        src="https://sisapapp.misacdn.net/lms/img/essay.e07e1e68.svg" />
                </div>
            </div>
        </div>

        <!-- phần body khi số câu hỏi > 0-->
        <div class="div-body div-list-question" v-if="numQuestion>0">
            <div class="list-question div-flex-column">
                <MSQuestion num="1" title="Câu hỏi 1" type="essay"/>
                <MSQuestion num="2" title="Câu hỏi 2" type="select"/>
                <MSQuestion num="3" title="Câu hỏi 3" type=""/>
            </div>
            <div class="list-btn list-btn-column">
                <MSButtonQuestion @click="onClickNewQuesion('select')" dimention="small"
                    src="https://sisapapp.misacdn.net/lms/img/select.c15dfe74.svg" />
                <MSButtonQuestion @click="onClickNewQuesion('rightwrong')" dimention="small"
                    src="https://sisapapp.misacdn.net/lms/img/yesorno.7f4f0b5a.svg" />
                <MSButtonQuestion @click="onClickNewQuesion('fill')" dimention="small"
                    src="https://sisapapp.misacdn.net/lms/img/fill.af676902.svg" />
                <MSButtonQuestion @click="onClickNewQuesion('match')" dimention="small"
                    src="https://sisapapp.misacdn.net/lms/img/pairing.4e1fb31f.svg" />
                <MSButtonQuestion @click="onClickNewQuesion('group')" dimention="small"
                    src="	https://sisapapp.misacdn.net/lms/img/group.ca2a19ef.svg" />
                <MSButtonQuestion @click="onClickNewQuesion('essay')" dimention="small"
                    src="https://sisapapp.misacdn.net/lms/img/essay.e07e1e68.svg" />
                <div class="line"></div>
                <MSButtonQuestion @click="onClickNewQuesion('essay')" dimention="small"
                    src="https://sisapapp.misacdn.net/lms/img/sort.220d1765.svg" />
            </div>
        </div>

        <!-- popup thêm câu hỏi -->
        <MSPopupAdd v-if="showPopupAdd" @onClosePopup="onClosePopup" @onClickSaveQuestion="onSaveQuestion"/>
        <!-- popup bổ sung thông tin -->
        <MSPopupAddInfor v-if="showPopupAddInfor" name="Test bt" subject="Toán" grade="Khối 1"
            @onClosePopup="onCloseAddInfor" />
    </div>
</template>

<script>
import MSButton from '../button/MSButton.vue'
import MSCombobox from '../input/MSCombobox.vue'
import MSButtonQuestion from '../button/MSButtonQuestion.vue'
import MSPopupAdd from '../../components/popup/MSPopupAdd.vue'
import MSPopupAddInfor from '../../components/popup/MSPopupAddInfor.vue'
import MSQuestion from '../card/MSQuestion.vue'
import { mapGetters } from 'vuex'
import { mapMutations } from 'vuex'
export default {
    name: "CreatePage",
    props: [],
    components: {
        MSButton,
        MSCombobox,
        MSButtonQuestion,
        MSPopupAdd,
        MSPopupAddInfor,
        MSQuestion
    },
    data() {
        return {
            showPopupAdd: false,//biến show popup thêm câu hỏi
            showPopupAddInfor: false,//biến show popup thêm thông tin cho bài tập
            numQuestion: 1,//số câu hoir của bài tập
        }
    },
    computed: {
        ...mapGetters(['typePopupAdd'])
    },
    methods: {
        ...mapMutations(['setShowOverlay', 'setTypePopupAdd']),
        /**
         * click back về trang chủ
         * CreatedBy: Trịnh Huỳnh Đức (20-5-2023)
         */
        onClickBack() {
            this.$router.push("/course/personal")
        },
        /**
         * click tạo câu hỏi mới -> hiện popup tương ứng
         * CreatedBy: Trịnh Huỳnh Đức (20-5-2023)
         */
        onClickNewQuesion(typeQuestion) {
            this.setTypePopupAdd(typeQuestion)//set loại popup là loại tự luận
            this.setShowOverlay(true)
            this.showPopupAdd = true
        },
        /**
         * bắt sự kiện emit đóng popup
         * CreatedBy: Trịnh Huỳnh Đức (21-5-2023)
         */
        onClosePopup() {
            this.showPopupAdd = false
            this.setShowOverlay(false)
        },

        /**
         * bắt sự kiện click nút bổ sung thêm thông tin
         * CreatedBy: Trịnh Huỳnh Đức (22-5-2023)
         */
        onClickAddInfor() {
            this.showPopupAddInfor = true
            this.setShowOverlay(true)
        },
        /**
         * bắt sự kiện click đóng popup thêm thông tin
         * CreatedBy: Trịnh Huỳnh Đức (22-5-2023)
         */
        onCloseAddInfor() {
            this.showPopupAddInfor = false
            this.setShowOverlay(false)
        },
        /**
         * sự kiện lưu câu hỏi
         * CreatedBy: Trịnh Huỳnh Đức (21-5-2023)
         */
        onSaveQuestion() {
            this.numQuestion++
            this.showPopupAdd = false
            this.setShowOverlay(false)
        }
    }
}
</script>
<style scoped>
.div-create {
    height: 100%;
    position: relative;
}

.div-header-main {
    width: 100%;
    height: 128px;
    padding: 16px;
    box-sizing: border-box;
    box-shadow: 0 3px 20px rgba(90, 125, 141, .16);
}

.div-name {
    width: 100%;
    height: 40px;
    display: flex;
    align-items: center;
}

.image {
    width: 40px;
    height: 40px;
    border-radius: 50px;
    margin-right: 16px;
    object-fit: cover;
}

.icon-back {
    margin-left: 8px;
    margin-right: 16px;
}

.div-header {
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    ;
}

.div-name>input {
    border: none;
    outline: none;
    color: #4e5b6a;
    font-size: 24px;
    line-height: 32px;
    font-weight: 700;
    width: 100%;
    height: 32px;
}

.div-menu {
    padding-left: 48px;
    display: flex;
    width: auto;
    height: 40px;
    justify-content: space-between;
}

.menu-left {
    display: flex;
    gap: 12px;
}

.cb-subject {
    width: 160px;
}

.cb-grade {
    width: 100px;
}

.menu-right {
    display: flex;
    align-items: center;
    gap: 12px;
    padding-right: 6px;
}

.btn-complete {
    background-color: #8a6bf6;
    color: #fff;
}

.div-help {
    background-color: #f1f2f7;
    border: 1px solid #b6b9ce;
    height: 40px;
    border-radius: 10px;
    width: 40px;
    height: 40px;
    display: flex;
    justify-content: center;
    align-items: center;
}

/* phần của body */
.div-body {
    height: calc(100% - 128px);
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    padding-bottom: 20px;
    box-sizing: border-box;
}

.div-import {
    padding-top: 24px;
    display: flex;
    flex-direction: column;
    align-items: center;
}

.text {
    line-height: 26px;
    text-align: center;
    color: #4e5b6a;
}

.title {
    font-weight: 700;
    margin-bottom: 24px;
    line-height: 26px;
    font-size: 18px;
    line-height: 26px;
    color: #4e5b6a;
    text-align: center;
}

.import {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-between;
    width: 840px;
    height: 224px;
    border: 2px dashed #b6b9ce;
    border-radius: 10px;
    padding: 24px 0;
    box-sizing: border-box;
}

.import>img {
    width: 64px;
    height: 64px;
}

.btn-import {
    display: flex;
    align-items: center;
}

.btn-dowload-file {
    margin-right: 12px;
    background-color: #ece7fe;
    color: #8a6bf6;
    border: none;
}

.btn-dowload-excel {
    color: #00c542;
    background-color: rgba(0, 197, 66, .2);
    border: none;
}

.div-question {
    display: flex;
    flex-direction: column;
    align-items: center;
}

.list-btn {
    display: flex;
    align-items: center;
    gap: 56px;
}
.list-btn-column {
    flex-direction: column;
    gap: 12px;
    position: absolute;
    top: 164px;
    right: 88px;
}
.list-btn>img {
    width: 2px;
    height: 98px;
}
.line {
    width: 40px;
    height: 2px;
    background-color: #b6b9ce;
    border-radius: 4px;
    margin: 0;
}
.list-question {
    margin-top: 36px;
    width: 100%;
    gap: 20px;
    padding: 0px 32px 0px 32px;
}
.div-list-question {
    margin: 0px 140px 0px 140px;
    overflow: scroll;
}
</style>
