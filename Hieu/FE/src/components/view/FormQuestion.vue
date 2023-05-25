<template>
    <div class="form-question" v-show="showFormQuestion != 0">
        <div class="form-container" v-show="showNoteQuestion">
            <div class="form-body">
                <div class="form-body__header flex">
                    <div class="choose-question-type flex">
                        <div class="form-question__number">Câu 1 - </div>
                        <BaseCombobox 
                            cbbClass="combobox-question"
                            :data=Resource.FormQuestionType
                            :propValue=Resource.PropName.FormQuestion
                            propText="Data"
                            v-model="typeQuestion"
                            :autoFocus=showFormQuestion
                        ></BaseCombobox>
                    </div>
                    <div class="toolbar-item">

                    </div>
                </div>
                <div class="form-body__main" ref="main">
                    <CKEditor class="question-ckeditor" v-model="editorData"></CKEditor>
                </div>
                <div class="form-body__answer" v-show="showFormQuestion == Enum.FormQuestion.Select">
                    <div class="list-answer">
                        <BaseAnswer></BaseAnswer>
                        <BaseAnswer></BaseAnswer>
                        <BaseAnswer></BaseAnswer>
                        <BaseAnswer></BaseAnswer>
                        <BaseAnswer></BaseAnswer>
                        <BaseAnswer></BaseAnswer>
                        <BaseAnswer></BaseAnswer>
                    </div>
                </div>
                <div class="form-body__answer" v-show="showFormQuestion == 2">
                    <div class="list-answer">
                        <div></div>
                        <BaseAnswer></BaseAnswer>
                        <BaseAnswer></BaseAnswer>
                        <div></div>
                    </div>
                </div>
                <div class="form-body__answer" v-show="showFormQuestion == 3">
                    <div class="answer-fill">
                        <BaseAnswerFill></BaseAnswerFill>
                    </div>
                </div>
            </div>
            <div class="form-footer flex">
                <div class="footer-left">
                    <BaseButton class="ms-button btn-active btn-white" text="Thêm đáp án"></BaseButton>
                </div>
                <div class="footer-right">
                    <BaseButton class="ms-button btn-active btn-white" text="Hủy" @click="closeForm"></BaseButton>
                    <BaseButton class="ms-button btn-active btn-white" text="Lưu và đóng"></BaseButton>
                    <BaseButton class="ms-button btn-active btn-blue" text="Lưu và thêm câu"></BaseButton>
                </div>
            </div>
        </div>
        <div class="form-container form-note" v-show="!showNoteQuestion">
            <div class="form-body">
                <div class="form-body__header flex">
                    <div class="choose-question-type flex">
                        <div class="form-question__number">Câu 1</div>
                    </div>
                    <div class="toolbar-item">

                    </div>
                </div>
                <div class="form-body__main fullscreen">
                    <CKEditor class="question-ckeditor" v-model="editorData"></CKEditor>
                </div>
            </div>
            <div class="form-footer flex">
                <div class="footer-right">
                    <BaseButton class="ms-button btn-active btn-white" text="Hủy" @click="closeForm"></BaseButton>
                    <BaseButton class="ms-button btn-active btn-blue" text="Lưu"></BaseButton>
                </div>
            </div>
        </div>
        <div class="add-note flex" v-show="showNoteQuestion" @click="showNoteQuestion = false">
            <div class="note-img">
                <img :src="noteaddImg" alt="">
            </div>
            <div class="note-text">
                Thêm lời giải
            </div>
        </div>
        <div class="close-note flex" v-show="!showNoteQuestion" @click="showNoteQuestion = true">
            <div class="note-img">
                <img :src="notebackImg" alt="">
            </div>
            <div class="note-text">
                Quay lại câu hỏi
            </div>
        </div>
    </div>
</template>

<script setup>
import BaseCombobox from '../base/combobox/BaseCombobox.vue';
import BaseButton from '../base/button/BaseButton.vue';
import BaseAnswer from '../base/answer/BaseAnswer.vue';
import CKEditor from '@/components/base/ckeditor/CKEditor.vue';
import { useStore } from 'vuex';
import { computed, ref, watch } from 'vue'; 
import * as Resource from '@/common/resource/Resource';
import * as Enum from '@/common/enum/Enum';
import BaseAnswerFill from '../base/answer/BaseAnswerFill.vue';

// Các biến lưu đường dẫn
const noteaddImg = require("@/assets/img/icon-noteadd.svg");
const notebackImg = require("@/assets/img/icon-noteback.svg");

const store = useStore();

const showFormQuestion = computed(() => store.state.question.showFormQuestion);

const typeQuestion = ref(); // Kiểu câu hỏi
const showNoteQuestion = ref(true); // Show form chú thích
const editorData = ref();   // Giá trị ô input CKEditor
const main = ref("main");
/**
 * Đóng form
 * CreatedBy VMHieu 21/05/2023
 */
const closeForm = () => {
    store.dispatch("showFormQuestion", false);
    showNoteQuestion.value = true;
}

/** 
 * Xem sự thay đổi của trạng thái form
 * CreatedBy VMHieu 22/05/2023
 */
watch((typeQuestion), () => {
    if (typeQuestion.value == Enum.FormQuestion.Essay) {
        main.value.classList.add("fullscreen");
    } else {
        main.value.classList.remove("fullscreen");
    }
    store.dispatch("showFormQuestion", typeQuestion.value);
})
/** 
 * Xem sự thay đổi đóng mở form
 * CreatedBy VMHieu 22/05/2023
 */
watch((showFormQuestion), () => {
    if (showFormQuestion.value == Enum.FormQuestion.Essay) {
        main.value.classList.add("fullscreen");
    } else {
        main.value.classList.remove("fullscreen");
    }
})

// watch((editorData), () => {
//     console.log(editorData.value);
// })
</script>

<style scoped>
.form-question{
    position: fixed;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    z-index: 2000;
    height: 100%;
    background-color: rgba(0,0,0,.5);
    overflow: auto;
}

.form-container{
    margin: 24px auto 0 auto;
    width: 80vw;
    min-width: 800px;
    border-radius: 10px;
    box-shadow: 0 0 20px rgba(0,0,0,.16);
    height: calc(100vh - 104px);
    background-color: #fff2ab;
    position: relative;
    background: #fff;
    border-radius: 10px;
    box-sizing: border-box;
}

.form-body{
    height: 100%;
    overflow: auto;
    border-radius: 10px;
}

.form-body__header{
    height: 40px;
    background-color: #fff2ab;
    border-radius: 10px 10px 0 0;
    justify-content: space-between;
    position: fixed;
    z-index: 5;
}

.form-body__main{
    height: auto;
    background-color: #fff2ab;
    border-radius: 10px 10px 0 0;
}

.form-body__main>div{
    height: 100%;
}

.question-content{
    margin: 50px 0 10px 0;
    background-color: #fff2ab;
    width: 100%;
    height: 100%;
    border: none;
    outline: none;
    padding: 0 24px;
    font-size: 16px;
    color: #4e5b6a;
    box-sizing: border-box;
    resize: none;
    font-family: GoogleSans;
}

.question-content::placeholder{
    font-size: 16px;
    opacity: 0.4;
}

.question-content:focus, .question-content:focus-visible{
    border: none;
    outline: none;
}

.form-footer{
    position: absolute;
    bottom: -60px;
    right: 0;
    width: 100%;
    justify-content: space-between;
}

.footer-right>button:last-child{
    margin-right: 0;
}

.choose-question-type{
    background-color: #f8e373;
    padding-left: 12px;
    border-radius: 10px 0 10px 0;
    font-weight: 700;
    font-size: 16px;
    line-height: 24px;
}

.form-body__answer{
    padding: 0 1.5rem 1.5rem 1.5rem;
    margin-top: 66px;
}

.list-answer{
    display: grid;
    grid-template-columns: repeat(4,minmax(0,1fr));
    gap: 1rem;
}

.answer-fill{
    width: 100%;
}

.fullscreen{
    height: 100%;
}

.fullscreen>.question-content{
    height: calc(100% - 64px);
}

.add-note{
    position: fixed;
    height: 160px;
    top: calc(50% - 80px);
    overflow: hidden;
    right: 0;
    border-radius: 10px 0 0 10px;
    padding-left: 1.25rem;
    padding-right: 1.25rem;
    flex-direction: column;
    width: 6rem;
    box-sizing: border-box;
    background: transparent url(@/assets/img/bg-noteadd.svg) no-repeat;
}

.close-note{
    position: fixed;
    background-color: blue;
    height: 160px;
    top: calc(50% - 80px);
    overflow: hidden;
    left: 0;
    border-radius: 0 10px 10px 0 ;
    padding-left: 1.25rem;
    padding-right: 1.25rem;
    flex-direction: column;
    width: 6rem;
    box-sizing: border-box;
        background: transparent url(@/assets/img/bg-noteadd.svg) no-repeat;
}

.add-note:hover, .close-note:hover{
    cursor: pointer;
}

.note-img{
    margin: 1.75rem 0 0.75rem 0;
}

.note-text{
    font-size: 14px;
    line-height: 20px;
    text-align: center;
}

.form-note .form-body__header{
    background-color: #f8e373;
    border-radius: 10px 0 10px 0;
}

.form-note .choose-question-type{
    padding: 0 12px;
}

.form-note .form-footer{
    justify-content: flex-end;
}


</style>