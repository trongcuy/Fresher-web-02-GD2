<template>
    <div class="answer-container">
        <div class="answer-header flex">
            <div class="answer-char">
                {{ String.fromCharCode(65 + props.index) }}
            </div>
            <div class="answer-toolbar flex">
                <div class="icon-trash" v-show="showFormQuestion == Enum.FormQuestion.Select" @click="removeAnswer">
                    <img :src="trashImg" alt="" />
                </div>
                <div class="icon-img" v-show="showFormQuestion == Enum.FormQuestion.Select">
                    <img :src="imgImg" alt="" />
                </div>
                <div class="icon-tick" @click="answerTrue" tick="false" ref="btnTrue">
                    <img :src="tickImg" alt="" ref="iconTrue"/>
                </div>
            </div>
        </div>
        <div class="answer-main focus" v-clickOutside="closeCKEditor" ref="answerMain" @click="openCKEditor">
            <div class="default-content" v-if="!showCKEditor" >
                <div>Nhập đáp án...</div>
            </div>
            <CKEditorAnswer 
                v-model="dataAnswer.AnswerContent" 
                :dataEditor="dataAnswer.AnswerContent" 
                ref="ckedit"
            ></CKEditorAnswer>
        </div>
    </div>
</template>

<script setup>
import { computed, ref, watch, defineProps, defineEmits, toRef, reactive, onMounted } from 'vue';
import { useStore } from 'vuex';
import CKEditorAnswer from '@/components/base/ckeditor/CKEditorAnswer.vue';
import * as Enum from '@/common/enum/Enum';

const props = defineProps({
    // Data truyền từ cha
    data: {},
    // index của đáp án
    index: {
        type: Number,
        default: null
    },
    // index của đáp án remove tick
    indexRemove: {
        type: Number,
        default: null
    }
})
const removeTick = toRef(props, 'indexRemove');
const propData = toRef(props, 'data');

const emit = defineEmits(['removeAnswer', 'saveAnswer', 'removeTick']);

const store = useStore();
// Biến xét đóng mở form question
const showFormQuestion = computed(() => store.state.question.showFormQuestion);

const showCKEditor = ref(false);    // show ô input ckeditor
const ckedit = ref("ckedit");    // show ô input ckeditor
const iconTrue = ref("icontrue"); // icon tick giá trị đúng
const btnTrue = ref("btnTrue"); // button tick giá trị đúng
const answerMain = ref("answerMain"); // ô input nhập đáp án
// Data đáp án
const dataAnswer = reactive({
    AnswerContent: "",
    AnswerImage: "",
    AnswerStatus: "",
    SortOder: ""
})


// Các biến lưu đường dẫn 
const trashImg = require("@/assets/img/trash.svg");
const imgImg = require("@/assets/img/icon-img.svg");
const tickImg = require("@/assets/img/icon-tick.svg");
const icontrueImg = require("@/assets/img/icon-true.svg");

/**
 * Ẩn ô input ckeditor
 * CreatedBy VMHieu 24/05/2023
 */
const closeCKEditor = () => {
    answerMain.value.classList.add("not-focus");
    if (!dataAnswer.AnswerContent){
        showCKEditor.value = false;
    }
}; 

/**
 * Hiện ô input ckeditor
 * CreatedBy VMHieu 24/05/2023
 */
 const openCKEditor = () => {
    answerMain.value.classList.remove("not-focus");
    showCKEditor.value = true;
};

/**
 * Xóa thùng rác ở vị trí index
 * VMHieu 02/06/2023
 */
const removeAnswer = () => {
    emit('removeAnswer');
}

/**
 * Ấn dấu tick đổi màu button
 * CreatedBy VMHieu 24/05/2023
 */
const answerTrue = (event) => {
    if (showFormQuestion.value == Enum.FormQuestion.Select) {
        if (!btnTrue.value.tick){
            btnTrue.value.tick = true;
            iconTrue.value.src = icontrueImg;
            btnTrue.value.classList.add("btn-true");

            dataAnswer.AnswerStatus = Enum.AnswerStatus.True;
        } else {
            btnTrue.value.tick = false;
            iconTrue.value.src = tickImg;
            btnTrue.value.classList.remove("btn-true");

            dataAnswer.AnswerStatus = Enum.AnswerStatus.False;
        }
    } else if ( showFormQuestion.value == Enum.FormQuestion.YesOrNo ) {
        btnTrue.value.tick = true;
        iconTrue.value.src = icontrueImg;
        btnTrue.value.classList.add("btn-true");

        dataAnswer.AnswerStatus = Enum.AnswerStatus.True;

        emit('removeTick');
    }

}
/**
 * Bắt sự kiện đóng form câu hỏi để reset đáp án
 * CreatedBy VMHieu 25/05/2023
 */
watch((showFormQuestion), () => {
    if (!showFormQuestion.value){
        // Remove các ô tick
        btnTrue.value.classList.remove("btn-true");
        btnTrue.value.tick = false;
        iconTrue.value.src = tickImg;
    } else {
        if (props.data) {
            for (let i in props.data) {
                dataAnswer[i] = props.data[i];
            }
            dataAnswer.AnswerStatus = Enum.AnswerStatus.False;
            showCKEditor.value = true;
        }
    }
})
/**
 * Xem sự thay đổi của dataAnswer để đẩy lên cha
 * VMHieu 05/06/2023
 */
watch((dataAnswer), () => {
    dataAnswer.SortOder = dataAnswer.SortOder.toString();
    emit("update:modelValue", dataAnswer);
})

/**
 * Xem sự thay đổi của index Remove để thay đổi tick
 * VMHieu 05/06/2023
 */
 watch((removeTick), () => {
    if (props.index != props.indexRemove) {
        btnTrue.value.classList.remove("btn-true");
        btnTrue.value.tick = false;
        iconTrue.value.src = tickImg;

        dataAnswer.AnswerStatus = Enum.AnswerStatus.False;
    }
})

/**
 * Xem sự thay đổi của prop data để render lại đáp án
 * VMHieu 05/06/2023
 */
watch((propData), () => {
    if (props.data) {
        for (let i in props.data) {
            dataAnswer[i] = props.data[i];
        }
        dataAnswer.AnswerStatus = Enum.AnswerStatus.False;
        showCKEditor.value = true;
    }
})

</script>

<style scoped>
.answer-container {
    aspect-ratio : 1 / 1;
    overflow: hidden;
    border-radius: 10px;
    background-color: #ffaec7;
    width: 100%;
    font-size: 20px;
    font-weight: 700;
}

.answer-header{
    justify-content: space-between;
    padding: 8px;
}

.answer-char{
    margin: 0.25rem 0 0 0.25rem;
}

.answer-toolbar>div{
    background-color: #fff;
    width: 32px;
    height: 32px;
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: 10px;
    cursor: pointer;
    margin-left: 8px;
    outline: none;
}

.answer-toolbar>div:hover{
    transform: scale(1.1);
}

.answer-main{
    height: calc(100% - 48px);
    position: relative;
    font-weight: 500;
}

.answer-main div{
    height: 100%;
}

.default-content{
    position: absolute;
    top: 0;
    width: 150px;
    text-align: center;
    left: 50%;
    cursor: text;
    color: rgba(78,91,106,.7);
    font-size: 18px;
    font-weight: 700;
    cursor: text;
    transform: translateX(-50%);
    z-index: 10;
}

.default-content>div{
    height: auto;
    position: absolute;
    top: 40%;
    left: 50%;
    text-align: center;
    transform: translate(-50%, -50%);
    width: 150px;
}

.btn-true{
    background-color: #00c542 !important;
}

.btn-true>img{
    background-color: #00c542;
}
</style>