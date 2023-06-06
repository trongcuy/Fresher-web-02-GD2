<template>
    <div class="fill-input">
        <span v-if="showSpan" class="default-span">Nhập đáp án rồi nhấn Enter...</span>
        <div v-for="(item, index) in dataInputSpan" :key="index">
            <span class="input-tag">
                {{ item }}
                <span @click="removeTag(item)">
                    <img class="remove-tag" :src="removeTagImg" alt="">
                </span>
            </span>
        </div>
        <span 
            @input="getValueSpan" 
            @keypress.enter="getItemSpan" 
            @blur="getItemSpan"
            contenteditable="true" 
            class="new-tag"
            ref="inputTag"
        ></span>
    </div>
</template>

<script setup>
import { ref, reactive, computed, watch, defineProps, onMounted, defineEmits, nextTick } from 'vue';
import { useStore } from 'vuex';

const props = defineProps({
    data: null,
})

const emit = defineEmits([]);

const store = useStore();
// Biến đóng mở form
const showFormQuestion = computed(() => store.state.question.showFormQuestion);

const showSpan = ref(true); // Hiển thị span mặc định khi chưa có giá trị
const dataInputSpan = reactive([]); // Mảng các item input tag
const inputTag = ref("inputTag"); // Span nhập các tag

// Các biến lưu đường dẫn
const removeTagImg = require("@/assets/img/remove-tag.svg");

/**
 * Xét giá trị của span
 * CreatedBy VMHieu 23/05/2023
 */
const getValueSpan = () => {
    let value = event.target.innerHTML;
    if (value.length > 0 || dataInputSpan.length > 0) {
        showSpan.value = false;
    } else {
        showSpan.value = true;
    }
}
/**
 * Lấy các item khi ấn enter hoặc blur
 * CreatedBy VMHieu 23/05/2023
 */
const getItemSpan = (event) => {
    event.preventDefault();
    let value = event.target.innerText;
    if (value.length > 0) {
        dataInputSpan.push(value.trim());
    } 

    event.target.innerText = "";
}
/**
 * Ấn nút xóa tag input
 * @param {} removeItem
 * CreatedBy VMHieu 24/05/2023 
 */
const removeTag = (removeItem) => {
    const itemIndex = dataInputSpan.findIndex(item => item == removeItem);
    dataInputSpan.splice(itemIndex, 1);
}

onMounted(() => {
    // Biding dữ liệu vào ô input span
    dataInputSpan.splice(0);
    let arr = [];
    if (props.data.length > 0) {
        arr = props.data.split(";");
    }
    if (arr.length > 0) {
        for(let i = 0; i < arr.length; i++)
        {
            dataInputSpan.push(arr[i]);
        }
        showSpan.value = false;
    }
})

/**
 * Bắt sự kiện đóng form để remove tag
 * CreatedBy VMHieu 25/05/2023
 */
watch((showFormQuestion), () => {
    if (!showFormQuestion.value) {
        dataInputSpan.splice(0);
        showSpan.value = true;
    } else {
        console.log(props.data)
    }
})
/**
 * Bắt sự thay đổi của dữ liệu input để đẩy lên component cha
 * VMHieu 05/06/2023
 */
watch((dataInputSpan), () => {
    let str = dataInputSpan.join(";");
    emit("update:modelValue", str);
})

/**
 * Xem sự thay đổi của data truyền từ cha xuống để render lại
 * VMHieu 05/06/2023
 */
watch((props), () => {
    dataInputSpan.splice(0);
    let arr = [];
    if (props.data.length > 0) {
        arr = props.data.split(";");
    }
    if (arr.length > 0) {
        for(let i = 0; i < arr.length; i++)
        {
            dataInputSpan.push(arr[i]);
        }
        showSpan.value = false;
    }
})


</script>

<style scoped>
.fill-input{
    display: flex;
    align-items: center;
    flex-wrap: wrap;
    padding-right: 46px;
    border: 1px solid rgb(182, 185, 206);
    padding: 0 4px 0px;
    border-radius: 10px;
    min-height: 42px;
    width: 100%;
    box-sizing: border-box;
}

.fill-input:focus-within{
    border: 1px solid rgb(138, 107, 246);
}

.default-span{
    opacity: 0.6;
    position: absolute;
}
.new-tag{
    display: inline-block;
    height: 32px;
    max-height: 32px;
    line-height: 32px;
    min-width: 20px;
    flex-grow: 1;
    font-size: 14px;
    margin-bottom: 4px;
    z-index: 10;
    border: none;
    outline: none;
    word-break: break-all;
}

.input-tag{
    background-color: #f1f2f7;
    margin-right: 4px;
    border-color: #b6b9ce;
    color: #4e5b6a;
    height: 32px;
    line-height: 30px;
    display: inline-block;
    max-width: 100%;
    overflow: hidden;
    text-overflow: ellipsis;
    position: relative;
    padding-right: 20px;
    border-radius: 10px!important;
    user-select: none;
    border: 1px solid #e8e1fd;
    box-sizing: border-box;
    white-space: nowrap;
    padding: 0 20px 0 10px;
}

.remove-tag{
    cursor: pointer;
    position: absolute;
    right: 4px;
    top: 9px;
    width: 12px;
    height: 12px;
}
</style>