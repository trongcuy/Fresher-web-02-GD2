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
                <div class="icon-img" v-show="showFormQuestion == Enum.FormQuestion.Select" @click="openFormImage">
                    <img :src="imgImg" alt="" />
                </div>
                <div class="icon-tick" @click="answerTrue" tick="false" ref="btnTrue">
                    <img :src="tickImg" alt="" ref="iconTrue"/>
                </div>
            </div>
        </div>
        <div class="answer-main focus" v-clickOutside="closeCKEditor" ref="answerMain" @click="openCKEditor">
            <div class="default-content" v-if="!dataAnswer.AnswerImage && !dataAnswer.AnswerContent">
                <div>Nhập đáp án...</div>
            </div>
            <div ref="ckeditBox" v-if="!dataAnswer.AnswerImage">
                <CKEditorAnswer 
                    v-model="dataAnswer.AnswerContent" 
                    :dataEditor="dataAnswer.AnswerContent" 
                    ref="ckedit"
                    :focusEditor="focusEditor"
                ></CKEditorAnswer>
            </div>
            <div class="answer-image" v-if="dataAnswer.AnswerImage">
                <img :src="`${constants.API_URL}/${dataAnswer.AnswerImage}`" alt="">
                <div class="remove-img" @click="removeImg" v-tooltip="'Xóa'">
                    <icon class="icon icon-exit"></icon>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { computed, ref, watch, defineProps, defineEmits, toRef, reactive, isReactive, nextTick, onMounted } from 'vue';
import { useStore } from 'vuex';
import CKEditorAnswer from '@/components/base/ckeditor/CKEditorAnswer.vue';
import FormImage from '@/components/view/FormImage.vue';
import * as Enum from '@/common/enum/Enum';
import { constants } from '@/config/config';

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

const emit = defineEmits(['removeAnswer', 'saveAnswer', 'removeTick', 'update:modelValue', 'openFormImage']);

const store = useStore();
// Biến xét đóng mở form question
const showFormQuestion = computed(() => store.state.question.showFormQuestion);

const showCKEditor = ref(false);    // show ô input ckeditor
const ckedit = ref("ckedit");    // show ô input ckeditor
const ckeditBox = ref("ckeditBox");    // show ô input ckeditor
const iconTrue = ref("icontrue"); // icon tick giá trị đúng
const btnTrue = ref("btnTrue"); // button tick giá trị đúng
const answerMain = ref("answerMain"); // ô input nhập đáp án
const focusEditor = ref(false);
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
        focusEditor.value = false;
    }
}; 

/**
 * Hiện ô input ckeditor
 * CreatedBy VMHieu 24/05/2023
 */
 const openCKEditor = () => {
    answerMain.value.classList.remove("not-focus");
        
    focusEditor.value = true;
};

/**
 * Xóa ảnh
 * VMHieu 13/06/2023
 */
const removeImg = () => {
    dataAnswer.AnswerImage = "";
}

/**
 * Xóa thùng rác ở vị trí index
 * VMHieu 02/06/2023
 */
const removeAnswer = () => {
    emit('removeAnswer');
}
/**
 * Hiển thị form chọn ảnh
 * VMHieu 13/06/2023
 */
const openFormImage = () => {
    emit('openFormImage', props.index);
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
    } 
    // else {
    //     if (props.data) {
    //         for (let i in props.data) {
    //             dataAnswer[i] = props.data[i];
    //         }
    //         //dataAnswer.AnswerStatus = Enum.AnswerStatus.False;
    //         showCKEditor.value = true;
    //     }
    // }
})
/**
 * Xem sự thay đổi của dataAnswer để đẩy lên cha
 * VMHieu 05/06/2023
 */
watch((dataAnswer), () => {
    dataAnswer.SortOder = props.index;
    if (typeof dataAnswer.SortOder !== 'string') {
        dataAnswer.SortOder = dataAnswer.SortOder.toString();
    }
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
        //dataAnswer.AnswerStatus = Enum.AnswerStatus.False;
        showCKEditor.value = true;
        
        if (props.data.AnswerStatus == Enum.AnswerStatus.True) {
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
    }
})

onMounted(() => {
    // Thực hiện biding dữ liệu
    if (props.data) {
        for (let i in props.data) {
            dataAnswer[i] = props.data[i];
        }
        showCKEditor.value = true;
        
        if (props.data.AnswerStatus == Enum.AnswerStatus.True) {
            answerTrue();
        }
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
    width: 0;
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

.answer-image{
    width: 100%;
    height: 100%;
    display: flex;
    align-items: flex-end;
    justify-content: center;
    position: relative;
}

.answer-image img{
    padding-bottom: 12px;
    width: 80%;
    height: 80%;
}

.remove-img{
    position: absolute;
    top: 0;
    right: 4px;
}
</style>