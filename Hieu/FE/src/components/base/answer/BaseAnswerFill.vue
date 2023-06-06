<template>
    <div class="answer-fill__container">
        <div class="answer-fill__item flex">
            <div class="fill-number">
                Ô trống {{ props.index + 1 }}
            </div>
            <div class="ip-tag" v-for="(data, index) in dataAnswer" :key="index">
                <BaseInputTag :data="data.AnswerContent" v-model="dataAnswer[index].AnswerContent"></BaseInputTag>
            </div>
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
import { defineProps, defineEmits, reactive, watch, onMounted } from 'vue';

const props = defineProps({
    data: null, // Data nhận được từ cha
    index: null,    // Index của đáp án
})

const emit = defineEmits(['removeAnswer']);

// Các biến lưu đường dẫn
const removeImg = require("@/assets/img/icon-remove.svg");

let dataAnswer = reactive([{
    AnswerContent: "",
    SortOder: props.index.toString()
}])

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
    emit('update:modelValue', dataAnswer);
})

onMounted(() =>{
    if (props.data.length > 0) {
        dataAnswer = {...props.data};
    }
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