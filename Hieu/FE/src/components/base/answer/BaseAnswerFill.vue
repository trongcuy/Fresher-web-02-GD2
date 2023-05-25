<template>
    <div class="answer-fill__container">
        <div class="answer-fill__item flex">
            <div class="fill-number">
                Ô trống 1
            </div>
            <BaseInputTag></BaseInputTag>
            <div class="fill-remove">
                <div class="remove-icon">
                    <img :src="removeImg" alt="">
                </div>
            </div>
        </div>
        <div class="answer-fill__add flex">
            <div class="fill-number"></div>
            <div class="fill-add__button flex">
                <div class="fill-add__img">
                    <img :src="addImg" alt="">
                </div>
                <div class="fill-add__text">
                    Thêm đáp án
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import BaseInputTag from "../input/BaseInputTag.vue";

// Các biến lưu đường dẫn
const removeImg = require("@/assets/img/icon-remove.svg");
const addImg = require("@/assets/img/icon-add.svg");
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
}

.fill-add__button{
    height: 40px;
    display: flex;
    justify-content: center;
    align-items: center;
    border: 1px dashed rgb(182, 185, 206);
    border-radius: 10px;
    background-color: rgb(241, 242, 247);
    cursor: pointer;
    color: rgba(78, 91, 106, 0.7);
    width: 100%;
    font-size: 16px;
    line-height: 24px;
    font-weight: 700;
}

.fill-add__img{
    margin-right: 12px;
}



</style>