<template>
    <div class="exercise-container">
        <div class="exercise-img">
            <img :src="subjectImg" alt="">
            <div class="img-content center">Khối 1 - Giáo dục công dân</div>
        </div>
        <div class="exercise-content">
            <div class="content-title flex">
                <div>Bài tập toán ngày 10/02</div>
                <div class="btn-more" @click="toggleOption">
                    <img :src="optionImg" alt="">
                </div>
            </div>
            <div class="content-number flex">
                <img :src="numberImg" alt="">
                <div>10 câu</div>
            </div>
            <div class="content-creater flex">
                <img :src="userImg" alt="" />
                <div>Vũ Minh Hiếu</div>
            </div>
            <div class="more-option" v-show="showOption">
                <div class="option" @click="handleOpenPopup(Enum.PopupStatus.Error)">Xem</div>
                <div class="option" @click="handleOpenPopup(Enum.PopupStatus.Delete)">Xóa</div>
            </div>
        </div>
    </div>
</template> 

<script setup>
import { ref } from 'vue';
import { useStore } from 'vuex';
import * as Enum from '@/common/enum/Enum';

const store = useStore();
// Show option xem hoặc xóa bài tập
const showOption = ref(false);
// Các đường dẫn hình ảnh
const subjectImg = require("@/assets/subjects-avatar/diali.png");
const optionImg = require("@/assets/img/icon_option.svg");
const numberImg = require("@/assets/img/socau.svg");
const userImg = require("@/assets/img/user.svg");

/**
 * Toggle toolbar
 * CreatedBy VMHieu 23/05/2023
 */
const toggleOption = () => {
    showOption.value = !showOption.value;
}

/**
 * Mở popup xóa
 * CreatedBy VMHieu 23/05/2023
 */
const handleOpenPopup = (status) => {
    store.dispatch("updatePopupMsg", "Bài tập toán ngày 10/02");
    store.dispatch("updatePopupStatus", status);
    store.dispatch("showPopup", true);
    showOption.value = false;
}

</script>

<style scoped>
.exercise-container{
    position: relative;
    background-color: #fff;
    border-radius: 10px;
    box-shadow: 0 3px 20px rgba(90,125,141,.16);
    height: 200px;
    width: 100%;
    overflow: hidden;
    display: block;
    cursor: pointer;
}
.exercise-img{
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 50%;
    display: block;
    z-index: 1;
    background-size: cover;
    background: no-repeat 50%;
    background-color: #fff;
}

.exercise-img img{
    width: 100%;
    height: 100%;
    object-fit: cover;
}

.img-content{
    position: absolute;
    left: 0;
    top: 0;
    font-size: 16px;
    line-height: 24px;
    font-weight: 700;
    padding-top: 0.375rem;
    padding-bottom: 0.375rem;
    padding-left: 0.5rem;
    padding-right: 0.5rem;
    border-bottom-right-radius: 10px;
    color: rgba(255,255,255,1);
    background-color: rgba(138,107,246,1);
}

.exercise-content{
    box-sizing: border-box;
    position: absolute;
    top: 50%;
    left: 0;
    width: 100%;
    height: 100px;
    padding: 0.75rem;
    display: flex;
    flex-direction: column;
    gap: 0.125rem;
    justify-content: space-between;
    font-size: 16px;
}

.content-creater img, .content-number img{
    margin-right: 0.5rem;
    height: 1rem;
}

.content-title{
    text-overflow: ellipsis;
    white-space: nowrap;
    overflow: hidden;
    color: #4e5b6a;
    font-weight: 700;
    font-size: 16px;
    line-height: 24px;
    justify-content: space-between;
}

.btn-more{
    height: 32px;
    width: 32px;
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: center;
    position: absolute;
    top: 0;
    right: 0;
    border-radius: 50%;
    overflow: hidden;
    margin: 12px 0;
    z-index: 2;
}

.more-option{
    width: 136px;
    z-index: 2824;
    position: absolute;
    top: 10%;
    right: 10%;
    margin: 0px;
    background: #fff;
    min-width: 56px;
    border-radius: 10px;
    border: 1px solid #ebeef5;
    padding: 12px;
    color: #606266;
    line-height: 1.4;
    font-size: 14px;
    box-shadow: 0 2px 12px 0 rgba(0,0,0,.1);
}

.more-option .option{
    width: 112px;
    padding: 8px;
    cursor: pointer;
}

.option:hover{
    color: #8a6bf6;
    font-weight: 700;
}
</style>