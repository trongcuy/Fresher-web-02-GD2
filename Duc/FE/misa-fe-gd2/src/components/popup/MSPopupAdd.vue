<template>
    <div class="div-popup-add" :class="{ 'popup-essay': typePopupAdd == 'essay' || typePopupAdd == 'more-explane' }">
        <div class="popup-content" v-if="typePopupAdd != 'more-explane'">
            <!-- khối header trái -->
            <div class="header-left">
                <MSCombobox customClass="combobox-popup"
                    :listSelect="['Chọn đáp án', 'Đúng sai', 'Điền vào chỗ trống', 'Ghép nối', 'Câu hỏi nhóm', 'Tự luận']"
                    :defaultValue="this.resource.TitleQuestion[typePopupAdd]" @setDefaultValue="setTypeQuestion" />
                <div class="num-question">Câu 1 - </div>
            </div>
            <div class="div-center" style="width: 44px;height: 39px;position: absolute;top: 0px;right: 0px;z-index: 3;"><img
                    src="../../assets/img/ic_help.svg" /></div>
            <ckeditor :editor="editor"
                v-model="dataEditor" :config="editorConfig"></ckeditor>
            <!-- <textarea class="input-question" v-show="typePopupAdd == 'more-explane'"
                placeholder="Nhập lời giải tại đây..."></textarea> -->
            <p v-if="typePopupAdd == 'fill' && !dataEditor" class="placeholder-fill">(Sau khi nhập câu hỏi và nhấn ra ngoài,
                phần mềm sẽ tự động nhận diện dấu cách, dấu 3 chấm, dấu gạch chân để tạo ô trống.)</p>
        </div>
        <!-- popup thêm lời giải -->
        <MSPopupAddExplane v-if="typePopupAdd == 'more-explane'"/>
        <!-- phần nội dung -->
        <!-- câu hỏi lựa chọn -->
        <div class="popup-body" v-if="typePopupAdd == 'select'">
            <MSCardAnswer title="Nhập đáp án..." />
            <MSCardAnswer title="Nhập đáp án..." />
            <MSCardAnswer title="Nhập đáp án..." />
            <MSCardAnswer title="Nhập đáp án..." />
            <MSCardAnswer title="Nhập đáp án..." />
        </div>
        <!-- câu hỏi đúng sai -->
        <div class="popup-body popup-body-rightwrong" v-if="typePopupAdd == 'rightwrong'">
            <MSCardAnswer title="Đúng" />
            <MSCardAnswer title="Sai" />
        </div>
        <!-- câu hỏi điền chỗ trống -->
        <div class="div-flex-column popup-body-fill" v-if="typePopupAdd == 'fill'">
            <MSInputTag />
            <div class="div-button div-flex-row">
                <div class="div-flex-row">
                    <img src="../../assets/img/icon_plus.svg" />
                    <div>Thêm đáp án</div>
                </div>
            </div>
        </div>
    </div>
    <!-- phần button cuối popup -->
    <div class="popup-button popup-button-left" v-if="typePopupAdd == 'select'">
        <MSButton title="Thêm đáp án" />
    </div>
    <div class="popup-button" v-if="typePopupAdd != 'more-explane'">
        <MSButton title="Hủy" @click="onClickClosePopup" />
        <MSButton title="Lưu và đóng" @click="onClickSave" />
        <MSButton id="btn-save-new" title="Lưu và thêm câu" />
    </div>
    <div class="popup-button" v-if="typePopupAdd == 'more-explane'">
        <MSButton title="Hủy" @click="onClickReturnQuestion" />
        <MSButton title="Lưu" id="btn-save-new" />
    </div>

    <!-- nuts thêm lời giải -->
    <div class="more-explane" @click="onClickMoreExplane" v-if="showMoreExplane">
        <div>
            <img src="../../assets/img/icon_answer.svg" />
            <div>Thêm lời giải</div>
        </div>
    </div>
    <!-- nut quay lại câu hỏi -->
    <div class="more-explane return-question" @click="onClickReturnQuestion" v-if="!showMoreExplane">
        <div>
            <img src="../../assets/img/icon_back2.svg" />
            <div>Quay lại câu hỏi</div>
        </div>
    </div>
</template>

<script>
import MSCombobox from '../input/MSCombobox.vue'
import MSButton from '../button/MSButton.vue'
import MSCardAnswer from '../card/MSCardAnswer.vue'
import MSInputTag from '../input/MSInputTag.vue'
import MSPopupAddExplane from './MSPopupAddExplane.vue'
import { mapGetters } from 'vuex'
import { mapMutations } from 'vuex'
export default {
    name: 'MSPopupAdd',
    components: {
        MSCombobox,
        MSButton,
        MSCardAnswer,
        MSInputTag,
        MSPopupAddExplane
    },
    props: {
        title: {
            type: String,
            default: ''
        },
    },
    data() {
        const resource = window.Resource
        return {
            resource,//resource
            showPopupAdd: false,//biến show popup thêm câu hỏi
            showMoreExplane: true,//biến hiện form thêm lời giải
            typeQuestionBefore: '',//lưu loại câu hỏi lúc thêm lời giải xong còn quay lại
            editor: ClassicEditor,
            editorConfig: {
                toolbar: ['bold', 'italic', 'underline', 'uploadImage', 'undo', 'redo', '|', '|'],
                placeholder: "Nhập câu hỏi tại đây..."
            },
            editorConfigExplane: {
                toolbar: ['bold', 'italic', 'underline', 'uploadImage', 'undo', 'redo', '|', '|'],
                placeholder: "Nhập lời giải tại đây..."
            },
            dataEditor: '',//giá trị của ckeditor
        }
    },
    computed: {
        ...mapGetters(['typePopupAdd']),
    },
    methods: {
        ...mapMutations(['setTypePopupAdd']),
        /**
         * hàm bắt sự kiện đóng popup
         * CreatedBy: Trịnh Huỳnh Đức (21-5-2023)
         */
        onClickClosePopup() {
            this.$emit('onClosePopup')
        },

        /**
         * hàm bắt sự kiện thay đổi loại câu hỏi bằng combobox
         * CreatedBy: Trịnh Huỳnh Đức (21-5-2023)
         */
        setTypeQuestion(typeQuestion) {
            switch (typeQuestion) {
                case this.resource.TitleQuestion.select:
                    this.setTypePopupAdd('select')
                    break;
                case this.resource.TitleQuestion.rightwrong:
                    this.setTypePopupAdd('rightwrong')
                    break;
                case this.resource.TitleQuestion.fill:
                    this.setTypePopupAdd('fill')
                    break;
                case this.resource.TitleQuestion.match:
                    this.setTypePopupAdd('match')
                    break;
                case this.resource.TitleQuestion.group:
                    this.setTypePopupAdd('group')
                    break;
                case this.resource.TitleQuestion.essay:
                    this.setTypePopupAdd('essay')
                    break;
                default:
                    break;
            }
        },
        /**
         * sự kiện click nút thêm lời giải
         * CreatedBy: Trịnh Huỳnh Đức (21-5-2023)
         */
        onClickMoreExplane() {
            this.typeQuestionBefore = this.typePopupAdd
            this.setTypePopupAdd('more-explane')
            this.showMoreExplane = false
        },
        /**
         * sự kiện click nút quay lại câu hỏi
         * CreatedBy: Trịnh Huỳnh Đức (21-5-2023)
         */
        onClickReturnQuestion() {
            this.setTypePopupAdd(this.typeQuestionBefore)
            this.showMoreExplane = true
        },
        /**
         * sự kiện click nút lưu câu hỏi
         * CreatedBy: Trịnh Huỳnh Đức (21-5-2023)
         */
        onClickSave() {
            this.$emit('onClickSaveQuestion')
        }
    }
}
</script>

<style>
/* tùy chỉnh ckeditor */
.popup-content .ck.ck-toolbar>.ck-toolbar__items {
    justify-content: end;
    padding-right: 35px;
    background-color: #fff2ab;
}

.popup-content .ck.ck-editor__main>.ck-editor__editable:not(.ck-focused) {
    border: none;
    background-color: #fff2ab;
}

.popup-content .ck.ck-toolbar {
    background: #fff2ab !important;
    border: none;
    padding: 0 var(--ck-spacing-small);
}

.popup-content .ck.ck-editor__main>.ck-editor__editable {
    background: #fff2ab;
    border: none;
    padding: 0px 24px;
    margin-top: -4px;
}

.popup-content .ck.ck-editor__main>.ck-editor__editable:focus {
    background-color: #fff2ab;
    outline: none;
    border: none;
    padding: 0px 24px;
    margin-top: -4px;
    box-shadow: 0 0px 0px rgba(90, 125, 141, .16);
}

.popup-content .ck.ck-editor__main>.ck-editor__editable.ck-focused {
    outline: none;
    border: none;
}

.div-popup-add {
    background-color: #ffffff;
    border-radius: 10px;
    width: calc(72vw);
    height: calc(83vh);
    overflow: auto;
    left: calc(14vw);
    top: 24px;
    position: absolute;
    z-index: 3;
}

.more-explane {
    width: 96px;
    height: 160px;
    padding: 0 20px;
    right: 0px;
    top: calc(50vh - 80px);
    position: absolute;
    z-index: 3;
    cursor: pointer;
    background-color: #fff2ab;
    border-radius: 10px 0px 0px 10px;
}

.more-explane>div {
    text-align: center;
    display: flex;
    flex-direction: column;
    align-items: center;
    color: #4e5b6a;
}

.more-explane img {
    width: 48px;
    height: 42px;
    margin-top: 28px;
    margin-bottom: 12px;
}

.return-question {
    left: 0px;
    border-radius: 10px;
}

.popup-content {
    background-color: #fff2ab;
    width: 100%;
    min-height: 48%;
    height: auto;
    box-sizing: border-box;
    position: relative;
}

.popup-content .header-left {
    position: absolute;
    top: 0px;
    left: 0px;
    z-index: 3;
}

.popup-header {
    display: flex;
    align-items: center;
    justify-content: space-between;
    height: 40px;
    position: relative;
}

.combobox-popup {
    width: 312px;
}

.combobox-popup>input {
    border-radius: 0px 0px 10px 0px;
    width: 100%;
    padding-left: 70px;
    font-weight: 700;
    font-size: 16px;
    background-color: #f8e373;
    border: none;
}

.combobox-popup>input:hover {
    border: none;
}

.num-question {
    position: absolute;
    top: 10px;
    left: 12px;
    width: fit-content;
    font-weight: 700;
    font-size: 16px;
    color: #606266;
    background-color: #f8e373;
}

.list-edit {
    display: flex;
    align-items: center;
}

.list-edit>div {
    width: 30px;
    height: 30px;
    display: flex;
    align-items: center;
    justify-content: center;
}

.list-edit div:not(:last-child) img {
    width: 16px;
    height: 16px;
}

#list-edit__line {
    margin: 0px;
    width: min-content;
}

.input-question {
    font-size: 16px;
    color: #4e5b6a;
    border: none;
    outline: none;
    margin: 10px 0px 0px 0px;
    padding-left: 24px;
    padding-right: 24px;
    background-color: inherit;
    resize: none;
    width: 100%;
    height: calc(100% - 40px - 12px);
    box-sizing: border-box;
    white-space: pre-wrap;
    /* Xuống dòng khi gặp ký tự xuống dòng (\n) */
}

.popup-button {
    display: flex;
    align-items: center;
    justify-content: end;
    gap: 12px;
    height: 40px;
    position: absolute;
    right: calc(14vw);
    top: calc(83vh + 48px);
    z-index: 3;
}

.popup-button-left {
    width: min-content;
    left: calc(14vw);
}

#btn-save-new {
    border: none;
    background-color: #8a6bf6;
    color: #fff;
}

/* xử lý css riêng từng loại kiểu câu hỏi khác nhau */
/* câu tự luận */
.popup-essay {
    background-color: #fff2ab;
}

.popup-essay .popup-content {
    height: 89%;
}

.popup-essay .input-question {
    height: 100%;
}

.popup-essay .popup-body {
    display: none;
}

.popup-body {
    padding: 1rem 24px;
    height: 52%;
    width: 100%;
    overflow-y: scroll;
    display: grid;
    grid-template-columns: repeat(4, 1fr);
    /* 4 cột có tỷ lệ bằng nhau */
    justify-content: center;
    justify-items: center;
    gap: 1rem;
}

.popup-body-rightwrong {
    padding: 1rem 24px;
    height: 52%;
    width: 100%;
    display: flex;
    justify-content: center;
    gap: 1rem;
}

.popup-body-fill {
    padding: 1rem 24px;
    height: 52%;
    width: 100%;
    gap: 1rem;
    overflow-y: scroll;
}

.div-img {
    position: absolute;
    top: 0px;
    right: 0px;
    width: 46px;
    height: 100%;
    border-left: 1px solid rgb(182, 185, 206);
    cursor: pointer;
}

.div-img img {
    width: 16px;
    height: 16px;
}

.popup-body-fill .div-button {
    justify-content: center;
    height: 40px;
    width: calc(100% - 88px);
    margin-left: 88px;
    border: 1px dashed rgb(182, 185, 206);
    border-radius: 10px;
    background-color: rgb(241, 242, 247);
    cursor: pointer;
    color: rgba(78, 91, 106, 0.7);
}

.div-button img {
    margin-right: 12px;
    width: 12px;
    height: 12px;
}
.placeholder-fill {
    padding: 0px 24px;
    margin-top: -4px;
    margin-top: -12px;
    position: relative;
    z-index: 1;
}</style>
