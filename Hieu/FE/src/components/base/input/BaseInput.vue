<template>
    <div class="input-container">
        <input type="text" :tabindex="tabindex" :class="inputClass" :placeholder="placeholder" @input="changeInput" ref="input">
        <div class="icon" v-if="showIcon">
            <img :src="searchImg" alt="" />
        </div>
        <div class="clear-input" v-show="clearInput && validInput" @click="clearInput">
            <img :src="clearImg" alt="" />
        </div>
    </div>
</template> 

<script setup>
import { defineProps, defineEmits, ref, watch, toRef, defineExpose } from "vue";

// Các biến lưu đường dẫn
const searchImg = require("@/assets/img/icon-search.svg");
const clearImg = require("@/assets/img/clear-input.jpg");

const emit = defineEmits(['changeInput', 'update:modelValue']);

// Biến kiểm tra input có giá trị không
const validInput = ref(false);

const props = defineProps({
    // Placeholder của input
    placeholder: {
        type: String,
        default: null
    },
    // Class của input
    inputClass: {
        type: String,
        default: null 
    },
    // Có hiển thị icon hay không
    showIcon: {
        type: Boolean,
        default: false
    },
    // Có hiển thị icon clear input không
    clearInput: {
        type: Boolean,
        default: false
    },
    // Giá trị biding input
    valueInput: {
        type: String,
        default: ""
    },
    // Giá trị tabindex
    tabindex: {
        type: Number,
        default: 0
    }
})
// Đại diện cho prop valueInput
const valueRef = toRef(props, 'valueInput');

const input = ref("input"); // Đại diện cho thẻ input
/**
 * Lấy giá trị thẻ input
 * VMHieu 30/05/2023
 */
const changeInput = () => {
    emit("update:modelValue", input.value.value);
    if (input.value.value) {
        validInput.value = true;
    } else {
        validInput.value = false;
    }
}

/**
 * Xóa nội dung input
 * VMHieu 30/05/2023
 */
const clearInput = () => {
    input.value.value = "";
    validInput.value = false;
    emit("update:modelValue", "");
    emit("clearInput");
}
/**
 * Theo dõi sự thay đổi của value input
 * VMHieu 31.05.2023
 */
watch((valueRef), () => {
    input.value.value = props.valueInput;
    validInput.value = true;
    emit("update:modelValue", props.valueInput);
})

defineExpose({props, input})
</script>

<style scoped>
.icon{
    height: 100%;
    text-align: center;
    transition: all .3s;
    position: absolute;
    left: 6px;
    top: 50%;
    transform: translateY(-20%);
    color: #c0c4cc;
}

.clear-input{
    height: 100%;
    text-align: center;
    transition: all .3s;
    position: absolute;
    right: 6px;
    top: 50%;
    transform: translateY(-20%);
    color: #c0c4cc;
}

.clear-input img{
    width: 16px;
    height: 16px;
    margin: 0 4px;
    opacity: 0.2;
}

.clear-input img:hover{
    opacity: 0.4;
    cursor: pointer;
}


.input-container{
    position: relative;
}

.icon img{
    width: 16px;
    height: 16px;
    margin: 0 4px;
}

.no-icon{
    padding: 0 12px;
}
</style>