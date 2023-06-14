<template>
    <div class="create-main__form" v-show="!showListQuestion">
        <div class="form-import center">
            <div class="import-title">
                Tách câu hỏi tự động sử dụng công nghệ AI
            </div>
            <div class="import-box flex"
                @dragover="dragover"
                @dragleave="dragleave"
                @drop="drop"
                @click="chooseFile"
                ref="imp"
            >
                <div class="import-box__avatar">
                    <img :src="importImg" alt="">
                </div>
                <div class="import-box__content">
                    Nhấn để tải lên file bài tập hoặc kéo thả file vào đây. <br />
                    File có định dạng xls, xlsx, doc, docx, pdf
                </div>
                <div class="import-box__btn flex">
                    <BaseButton class="ms-button btn-active btn-upload" text="Tải lên file bài tập" @click.stop="" @click="chooseFile"></BaseButton>
                    <BaseButton class="ms-button btn-active btn-green" text="Tải file Excel mẫu" @click="downloadFileExample"></BaseButton>
                </div>
                <input type="file" id="upload-file" ref="upload" @change="handleUploadFile" accept=".xls, .xlsx"/>
            </div>
        </div>
        <div class="form-create">
            <div class="create-title">
                hoặc tự tạo câu hỏi mới
            </div>
            <div class="create-option">
                <div class="create-toolbar flex">
                    <div class="question-library">
                        <div class="question-avatar question-hover">
                            <img :src="libraryImg" alt="">
                        </div>
                        <div class="question-text">Thư viện học liệu</div>
                    </div>
                    <div class="question-line">
                        <div class="question-avatar">
                            <img :src="lineImg" alt="">
                        </div>
                    </div>
                    <div class="question">
                        <div class="question-select" @click="openFormQuestion(Enum.FormQuestion.Select)">
                            <div class="question-avatar question-hover">
                                <img :src="selectImg" alt="">
                            </div>
                            <div class="question-text">Chọn đáp án</div>
                        </div>
                        <div class="question-yesno" @click="openFormQuestion(Enum.FormQuestion.YesOrNo)">
                            <div class="question-avatar question-hover">
                                <img :src="yesnoImg" alt="">
                            </div>
                            <div class="question-text">Đúng sai</div>
                        </div>
                        <div class="question-fill" @click="openFormQuestion(Enum.FormQuestion.Fill)">
                            <div class="question-avatar question-hover">
                                <img :src="fillImg" alt="">
                            </div>
                            <div class="question-text">Điền vào chỗ trống</div>
                        </div>
                        <div class="question-essay" @click="openFormQuestion(Enum.FormQuestion.Essay)">
                            <div class="question-avatar question-hover">
                                <img :src="essayImg" alt="">
                            </div>
                            <div class="question-text">Tự luận</div>
                        </div>
                        <div class="question-group" @click="openFormQuestion(Enum.FormQuestion.Group)">
                            <div class="question-avatar question-hover">
                                <img :src="groupImg" alt="">
                            </div>
                            <div class="question-text">Câu hỏi nhóm</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import BaseButton from '@/components/base/button/BaseButton.vue';
import { useRouter, useRoute } from 'vue-router';
import { useStore } from 'vuex';
import { computed, reactive, onBeforeMount, watch, ref, defineProps, onMounted } from 'vue'; 
import * as Enum from '@/common/enum/Enum.js';
import * as Resource from '@/common/resource/Resource.js';

// Các biến lưu đường dẫn
const importImg = require("@/assets/img/import-file.svg");
const libraryImg = require("@/assets/img/library.svg");
const lineImg = require("@/assets/img/line.svg");
const selectImg = require("@/assets/img/select.svg");
const yesnoImg = require("@/assets/img/yesorno.svg");
const fillImg = require("@/assets/img/fill.svg");
const essayImg = require("@/assets/img/essay.svg");
const groupImg = require("@/assets/img/group.svg");

const store = useStore();
const router = useRouter();
const route = useRoute();
// Show danh sách câu hỏi
const showListQuestion = computed(() => store.state.question.showListQuestion);

const upload = ref("upload");

/**
 * Mở form question theo status truyền vào
 * @param {*} status 
 * CreatedBy VMHieu 21/05/2023
 */
const openFormQuestion = (status) => {
    store.dispatch("showFormQuestion", status);
    store.dispatch("updateFormModeQuestion", Enum.FormModeQuestion.Add);
}
/**
 * Thực hiện chọn file
 * VMHieu 07/06/2023
 */
const handleUploadFile = async () => {
    const files = upload.value.files[0];

    if (files) {
        var formData = new FormData();
        formData.append("excelFile", files);

        store.dispatch("checkFile", formData);
        //store.dispatch("showImport", true);
        upload.value.value = "";
    }
}
/**
 * Thực hiện tải file nhập khẩu mẫu
 * VMHieu 07/06/2023
 */
const downloadFileExample = () => {
    store.dispatch("downloadFileExample");
}
/**
 * Chọn file 
 * VMHieu 07/06/2023
 */
const chooseFile = () => {
    upload.value.click();
}
/**
 * Sự kiện drag file
 * CreatedBy VMHieu 07/06/2023
 */
 const dragover = (e) => {
    e.preventDefault();
}
/**
 * Sự kiện dropfile
 * @param {*} e
 * CreatedBy VMHieu 07/06/2023 
 */
const drop = (e) => {
    e.preventDefault();
    upload.value.files = e.dataTransfer.files;
    handleUploadFile();
}

</script>

<style scoped>
.create-main__form{
    color: #4e5b6a;
    height: 100%;
    padding-bottom: 20px;
}

.form-import{
    flex-direction: column;
    padding-top: 1.5rem;
}

.import-title, .create-title{
    margin-bottom: 1.5rem;
    font-weight: 700;
    font-size: 18px;
    line-height: 26px;
}

.import-box{
    width: 840px;
    height: 224px;
    justify-content: center;
    flex-direction: column;
    position: relative;
    border: 2px dashed #b6b9ce;
    padding: 24px 0;
    border-radius: 10px;
    text-align: center;
    box-sizing: border-box;
    cursor: pointer;
}

.import-box__avatar{
    margin-bottom: 16px;
    height: 64px;
}

.import-box__content{
    font-size: 16px;
}

.import-box__btn{
    margin-top: 12px;
}

.form-create{
    display: flex;
    flex-direction: column;
    align-items: center;
    margin-top: 24px;
}

.create-option{
    display: flex;
}

.question-library{
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
    margin-right: 8px;
    width: 120px;
}

.question-line{
    margin-right: 56px;
    display: block;
    margin-left: 1.25rem;
}

.question-text{
    width: max-content;
    margin-top: 12px;
    font-size: 16px;
}

.question-list{
    display: grid;
    grid-template-columns: 1fr;
    grid-row-gap: 20px;
}
.question-library>.question-avatar>img{
    width: 64px;
    height: 64px;
}

.question{
    display: flex;
    padding-left: .5rem;
    width: 100%;
}
.question>div{
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
    margin-right: 56px;
    width: 64px;
}

.question-hover:hover{
    cursor: pointer;
    transform: scale(1.1);
    transition: .3s;
    outline: none;
}

.option-list{
    position: absolute;
    right: -88px;
    top: 0;
    flex-direction: column;
}

.option-list>div{
    height: 40px;
    margin-bottom: 8px;
    margin-right: 0;
}
.option-list>.question-library{
    width: auto;
}

.option-list img{
    width: 40px !important;
    height: 40px !important;
}

#upload-file{
   opacity: 0;
   position: absolute;
   z-index: -1;
}
</style>