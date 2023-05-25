<template>
    <div class="form-exercise" v-if="showFormExercise">
        <div class="exercise-container">
            <div class="exercise-header flex">
                <div class="exercise-title">
                    Bổ sung thông tin
                </div>
                <div class="exercise-close" @click="closeFormExercise">
                    <icon class="icon icon-exit"></icon>
                </div>
            </div>
            <div class="exercise-main">
                <div class="main-left">
                    <div class="exercise-avatar">
                        <label for="" class="avatar-text">
                            Ảnh đại diện
                        </label>
                        <div class="img-avatar">
                            <img :src="avatarImg" alt="">
                        </div>
                    </div>
                </div>
                <div class="main-right">
                    <div class="form-name">
                        <label for="" class="name-text">
                            Tên bài tập
                            <span class="asterik">*</span>
                        </label>
                        <div class="form-input name-input">
                            <BaseInput inputClass="m-input no-icon"></BaseInput>
                        </div>
                    </div>
                    <div class="form-info">
                        <div class="info-subject">
                            <label for="" class="name-text">
                                Môn
                                <span class="asterik">*</span>
                            </label>
                            <div class="form-input subject-input">
                                <BaseCombobox></BaseCombobox>
                            </div>
                        </div>
                        <div class="info-class">
                            <label for="" class="name-text">
                                Khối
                                <span class="asterik">*</span>
                            </label>
                            <div class="form-input class-input">
                                <BaseCombobox></BaseCombobox>
                            </div>
                        </div>
                    </div>
                    <div class="form-topic">
                        <div class="info-subject">
                            <label for="" class="name-text">
                                Chủ đề
                            </label>
                            <div class="form-input topic-input">
                                <BaseCombobox></BaseCombobox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="exercise-footer">
                <BaseButton class="ms-button btn-active btn-white" text="Đóng" @click="closeFormExercise"></BaseButton>
                <BaseButton class="ms-button btn-active btn-blue" text="Lưu"></BaseButton>
            </div>
        </div>
    </div>
</template>

<script setup>
import BaseButton from "@/components/base/button/BaseButton.vue";
import BaseCombobox from "../base/combobox/BaseCombobox.vue";
import { useStore } from 'vuex';
import { computed } from 'vue';
import BaseInput from "../base/input/BaseInput.vue";

// Các biến lưu đường dẫn
const avatarImg = require("@/assets/subjects-avatar/toan.png");

const store = useStore();
// Biến giá trị trạng thái form bài tập
const showFormExercise = computed(() => store.state.exercise.showFormExercise);

/**
 * Đóng form bài tập
 * CreatedBy VMHieu 23/05/2023
 */
const closeFormExercise = () => {
    store.dispatch("showFormExercise", false);
}
</script>

<style scoped>
.form-exercise{
    position: fixed;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    z-index: 2000;
    height: 100%;
    background-color: rgba(0,0,0,.5);
    overflow: auto;
    scrollbar-width: thin;
}

.exercise-container{
    position: absolute;
    transform: translate(-50%, -50%);
    top: 50%;
    left: 50%;
    width: 800px;
    border-radius: 10px;
    box-shadow: 0 0 20px rgba(0,0,0,.16);
    margin: 0 auto 50px;
    background: #fff;
    box-sizing: border-box;
}

.exercise-header{
    padding: 24px;
    justify-content: space-between;
}

.exercise-title{
    font-weight: 700;
    font-size: 28px;
    line-height: 36px;
    color: #4e5b6a;
}

.exercise-main{
    padding: 0 24px 24px;
    word-break: break-word;
    color: #606266;
    font-size: 14px;
    display: flex;
}

.main-left{
    width: 260px;
    display: flex;
    flex-direction: column;
}

.avatar-text{
    color: #4e5b6a;
    font-weight: 500;
    margin-bottom: 4px;
    font-size: 14px;
    line-height: 20px;
    display: block;
}

.img-avatar{
    border-radius: 10px;
    background-color: #f1f2f7;
    width: 100%;
    height: 156px;
    overflow: hidden;
    margin-bottom: 8px;
    position: relative;
}

.img-avatar img{
    width: 100%;
    height: 100%;
    -o-object-fit: cover;
    object-fit: cover;
}

.main-right{
    width: calc(100% - 260px);
    padding-left: 40px;
    display: grid;
    grid-template-columns: 100%;
    grid-row-gap: 16px;
}

.main-right label{
    color: #4e5b6a;
    font-weight: 500;
    margin-bottom: 4px;
    font-size: 14px;
    line-height: 20px;
    display: block;
}

.form-info{
    display: grid;
    grid-template-columns: 5fr 3fr;
    grid-column-gap: 12px;
}

.exercise-footer{
    padding: 0 24px 24px;
    text-align: right;
    box-sizing: border-box;
}
</style>