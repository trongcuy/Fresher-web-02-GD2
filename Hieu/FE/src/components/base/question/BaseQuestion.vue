<template>
    <div class="question-item">
        <div class="question-decore"></div>
        <div class="question-main">
            <div class="question-content flex">
                <div class="question-number">
                    {{ props.index + 1 }}.
                </div>
                <div class="question-text" v-html="data.QuestionContent"></div>
            </div>
            <div class="question-image" v-if="arrayUrlImage.length > 0">
                <div v-for="(item, index) in arrayUrlImage" :key="index">
                    <img :src="`${constants.API_URL}/${item}`" alt="" @click=viewImage(item)>
                </div>
            </div>
            <div class="question-line" v-if="props.data.TypeQuestion == Enum.FormQuestion.Essay"></div>
            <div class="question-answer">
                <div class="question-answer__list">
                    <div v-for="(answer, index) in props.data.Answer" :key="index">
                        <div class="answer-item">
                            <div class="answer-char" 
                                :class="{
                                'answer-true': answer.AnswerStatus == Enum.AnswerStatus.True || props.data.TypeQuestion == Enum.FormQuestion.Fill,
                                'answer-false': answer.AnswerStatus == Enum.AnswerStatus.False && props.data.TypeQuestion != Enum.FormQuestion.Fill
                                }"
                            >
                                {{ String.fromCharCode(65 + index) }}
                            </div>
                            <div class="answer-text flex" v-html="answer.AnswerContent" v-if="!answer.AnswerImage">
                            </div>
                            <div class="answer-text flex popup-image" v-if="answer.AnswerImage" @click="viewImage(answer.AnswerImage)">
                                <img :src="`${constants.API_URL}/${answer.AnswerImage}`" alt="">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="question-note flex" v-if="props.data.QuestionNote">
                <strong>Lời giải: </strong> <span v-html="data.QuestionNote"></span>
            </div>
            <div class="question-toolbar flex">
                <BaseButton class="ms-button btn-white btn-active" text="Chỉnh sửa" @click="openFormEdit"></BaseButton>
                <BaseButton class="ms-button btn-white btn-active btn-onlyicon" @click="openFormClone" v-tooltip="'Nhân bản'">
                    <template v-slot:icon>
                        <div class="center">
                            <img :src="duplicateImg" alt="">
                        </div>
                    </template>
                </BaseButton>
                <BaseButton class="ms-button btn-white btn-active btn-onlyicon" v-tooltip="'Xóa'"
                 @click="handleOpenPopup(props.data.QuestionID)">
                    <template v-slot:icon>
                        <div class="center">
                            <img :src="trashImg" alt="">
                        </div>
                    </template>
                </BaseButton>
            </div>
        </div>
    </div>
    <BasePopupImage :url="urlImage" v-if="showPopupImage" @closePopup="closePopup"></BasePopupImage>
</template>

<script setup>
import BaseButton from '../button/BaseButton.vue';
import { defineProps, onMounted, computed, watch, ref } from 'vue';
import * as Enum from '@/common/enum/Enum';
import * as Resource from '@/common/resource/Resource';
import { useStore } from 'vuex';
import DOMPurify from 'dompurify';
import { constants } from '@/config/config'
import BasePopupImage from '../popup/BasePopupImage.vue';


const props = defineProps({
    data: null, //Data câu hỏi
    // Vị trí câu hỏi
    index: {
        type: Number,
        default: null
    }
})

const store = useStore();

const arrayUrlImage = ref([]); // Mảng đường dẫn ảnh
const showPopupImage = ref(false); // Hiện popup ảnh
const urlImage = ref(""); // Url ảnh

const sanitizedContent = computed(() => {
    return DOMPurify.sanitize(props.data);
})

// Hiện popup ảnh
const viewImage = (url) => {
    urlImage.value = `${constants.API_URL}/${url}`;
    showPopupImage.value = true;
}

// Đóng popup ảnh
const closePopup = () => {
    showPopupImage.value = false;
}

/**
 * Mở popup xóa
 * CreatedBy VMHieu 23/05/2023
 */
 const handleOpenPopup = (id) => {
    store.dispatch("updatePopupMsg", Resource.PopupMessage.DeleteQuestion);
    store.dispatch("updatePopupStatus", Enum.PopupStatus.Delete);
    store.dispatch("showPopup", true);
    store.dispatch("updateStatusDelete", Enum.StatusDelete.Question);

    store.dispatch("updateIdQuestionDelete", id);
}
/**
 * Mở form edit câu hỏi
 * VMHieu 06/06/2023
 */
const openFormEdit = () => {    
    store.dispatch("showFormQuestion" , props.data.TypeQuestion);
    store.dispatch("updateFormModeQuestion", Enum.FormModeQuestion.Edit);
    store.dispatch("updateDataQuestion", props.data);
}
/**
 * Mở form clone câu hỏi
 * VMHieu 06/06/2023
 */
const openFormClone = () => {
    store.dispatch("showFormQuestion" , props.data.TypeQuestion);
    store.dispatch("updateFormModeQuestion", Enum.FormModeQuestion.Clone);
    store.dispatch("updateDataQuestion", props.data);
}
/**
 * Xem sự thay đổi của data để gán lại giá trị cho sanitizedContent
 * VMHieu 12/06/2023
 */
watch(() => props.data, () => {
    sanitizedContent.value = DOMPurify.sanitize(props.data);
        // Lấy đường dẫn ảnh
        if (props.data.QuestionImage) {
        arrayUrlImage.value = props.data.QuestionImage.split(';');
    }
});

onMounted(() => {
    // Lấy đường dẫn ảnh
    if (props.data.QuestionImage) {
        arrayUrlImage.value = props.data.QuestionImage.split(';');
    }
})

// Các biến lưu đường dẫn
const duplicateImg = require("@/assets/img/duplicate.svg");
const trashImg = require("@/assets/img/trash.svg");
</script>

<style scoped>

.question-item{
    box-shadow: 0 3px 20px rgba(90,125,141,.16);
    border-radius: 0 0 6px 6px;
    min-width: 480px;
    overflow: auto;
}
.question-decore{
    height: 8px;
    border-radius: 6px 6px 0 0;
    background-color: rgb(255, 88, 140);
}

.question-main{
    padding: 1.25rem;
    font-size: 16px;
}

.question-content{
    margin-bottom: 20px;

}

.question-number{
    font-weight: 700;
    margin-right: 0.25rem;
}

.question-text{
    display: inline-block;
    position: relative;
}

.question-answer{
    padding-bottom: 20px;
    margin-bottom: 20px;
    border-bottom: 1px solid #eaebf5;
}

.question-answer__list{
    display: grid;
    grid-row-gap: 20px;
    grid-template-columns: 1fr 1fr 1fr 1fr;
    gap: 20px;
}

.answer-item{
    display: flex;
    align-items: center;
}

.answer-char{
    color: #fff;
    min-width: 24px;
    min-height: 24px;
    width: 24px;
    height: 24px;
    border-radius: 50%;
    text-align: center;
    line-height: 24px;
    background-color: #b6b9ce;
    margin-right: 8px;
    font-weight: 700;
    display: flex;
    align-items: center;
    justify-content: center;
}

.answer-text{
    word-wrap: break-word;
    width: 100%;
    height: 100%;
}

.answer-text img{
    width: 80%;
    height: 80%;
}

.answer-correct{
    background-color: #00c542;
}

.question-note{
    border-bottom: 1px solid #eaebf5;
    padding-bottom: 20px;
    margin-bottom: 1.25rem;
}
.question-toolbar{
    justify-content: flex-end;
}

.question-line{
    width: 480px;
    height: 20px;
    border-bottom: 1px dashed #4e5b6a;
    border-top: 1px dashed #4e5b6a;
}

.answer-true {
    background-color: #00c542;
}

.answer-false {
    background-color: #b6b9ce;
}

.question-image{
    margin-top: 16px;
    margin-bottom: 16px;
    gap: 1.5rem;
    flex-wrap: wrap;
    display: flex;
}

.question-image img{
    width: 300px;
    height: 100%;
    -o-object-fit: contain;
    object-fit: contain;
    cursor: pointer;
}
</style>