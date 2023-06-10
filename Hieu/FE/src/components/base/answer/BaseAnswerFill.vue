<template>
    <div class="answer-fill__container">
        <div class="answer-fill__item flex">
            <div class="fill-number">
                Ô trống {{ props.index + 1 }}
            </div>
            <BaseInputTag :data="dataAnswer.AnswerContent" v-model="dataAnswer.AnswerContent"></BaseInputTag>
            <div class="fill-remove" @click="removeAnswer">
                <div class="remove-icon" >
                    <img :src="removeImg" alt="">
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import BaseInputTag from "../input/BaseInputTag.vue";
import { defineProps, defineEmits, reactive, watch, computed, toRef, onMounted } from 'vue';
import { useStore } from "vuex";

const props = defineProps({
    data: {}, // Data nhận được từ cha
    index: null,    // Index của đáp án
})

const propData = toRef(props, "data");

const emit = defineEmits(['removeAnswer', 'update:modelValue']);

const store = useStore();
// Biến xét đóng mở form question
const showFormQuestion = computed(() => store.state.question.showFormQuestion);

// Các biến lưu đường dẫn
const removeImg = require("@/assets/img/icon-remove.svg");

let dataAnswer = reactive({
    AnswerContent: "",
    SortOder: props.index.toString()
})

/**
 * Xóa đáp án
 * VMHieu 05/06/2023
 */
const removeAnswer = () => {
    emit('removeAnswer');
}

/**
 * Xem sự thay đổi của dữ liệu đáp án để đẩy lên component cha
 * VMHieu 05/06/2023 
 */
watch((dataAnswer) , () => {
    dataAnswer.SortOder = props.index;
    if (typeof dataAnswer.SortOder !== 'string') {
        dataAnswer.SortOder = dataAnswer.SortOder.toString();
    }
    emit('update:modelValue', dataAnswer);
})

// watch((showFormQuestion) , () => {
//     console.log(props.data);
// })

/**
 * Xem sự thay đổi của data để render lại
 * VMHieu 08/06/2023
 */
watch((propData), () => {
    if (props.data) {
        for (let i in props.data) {
            dataAnswer[i] = props.data[i];
        }
    }
})

onMounted(() => {
    dataAnswer.AnswerContent = props.data.AnswerContent;
    dataAnswer.SortOder = props.data.SortOder;
})

</script>

<style scoped>

.answer-fill__container{
    display: grid;
    grid-template-columns: 1fr;
    gap: 12px 16px;
    margin-bottom: 16px;
    width: 100%;
}
.answer-fill__item{
    position: relative;
}
.fill-number{
    width: 5rem;
    margin-right: .5rem;
}

.fill-remove{
    cursor: pointer;
    top: 0px;
    right: 0px;
    width: 46px;
    height: 100%;
    display: flex;
    position: absolute;
    align-items: center;
    justify-content: center;
    border-left: 1px solid rgb(182, 185, 206);
    z-index: 99;
}

.ip-tag{
    width: 100%;
}
</style>