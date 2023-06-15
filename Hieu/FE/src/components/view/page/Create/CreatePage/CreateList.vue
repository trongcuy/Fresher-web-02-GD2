<template>
    <div class="create-main__list" v-show="showListQuestion">
        <div class="question-list">
            <div v-for="(question, index) in props.data" :key="index">
                <BaseQuestion :data="question" :index="index"></BaseQuestion>
            </div>
        </div>
        <div class="create-option option-list" v-tooltip="{
                theme: {
                    placement: 'left',
                },
            }">
            <div class="question-library" v-tooltip="'Thư viện'">
                <div class="question-avatar question-hover">
                    <img :src="libraryImg" alt="">
                </div>
            </div>
            <div class="question-select" @click="openFormQuestion(Enum.FormQuestion.Select)" v-tooltip="'Thêm câu chọn đáp án'">
                <div class="question-avatar question-hover">
                    <img :src="selectImg" alt="">
                </div>
            </div>
            <div class="question-yesno" @click="openFormQuestion(Enum.FormQuestion.YesOrNo)" v-tooltip="'Thêm câu chọn đúng sai'">
                <div class="question-avatar question-hover">
                    <img :src="yesnoImg" alt="">
                </div>
            </div>
            <div class="question-fill" @click="openFormQuestion(Enum.FormQuestion.Fill)" v-tooltip="'Thêm câu điền vào chỗ trống'">
                <div class="question-avatar question-hover">
                    <img :src="fillImg" alt="">
                </div>
            </div>
            <div class="question-essay" @click="openFormQuestion(Enum.FormQuestion.Essay)" v-tooltip="'Thêm câu tự luận'">
                <div class="question-avatar question-hover">
                    <img :src="essayImg" alt="">
                </div>
            </div>
            <div class="question-group" @click="openFormQuestion(Enum.FormQuestion.Group)" v-tooltip="'Thêm câu hỏi nhóm'">
                <div class="question-avatar question-hover">
                    <img :src="groupImg" alt="">
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import BaseQuestion from '@/components/base/question/BaseQuestion.vue';
import { useRouter, useRoute } from 'vue-router';
import { useStore } from 'vuex';
import { computed, reactive, onBeforeMount, watch, ref, defineProps, onMounted } from 'vue'; 
import * as Enum from '@/common/enum/Enum.js';
import * as Resource from '@/common/resource/Resource.js';

const props = defineProps({
    data: null
})

// Các biến lưu đường dẫn
const libraryImg = require("@/assets/img/library.svg");
const selectImg = require("@/assets/img/select.svg");
const yesnoImg = require("@/assets/img/yesorno.svg");
const fillImg = require("@/assets/img/fill.svg");
const essayImg = require("@/assets/img/essay.svg");
const groupImg = require("@/assets/img/group.svg");

const store = useStore();
const router = useRouter();
const route = useRoute();
// Show danh sách câu hỏi
const showListQuestion = computed(() => store.state.question.showListQuestion);

/**
 * Mở form question theo status truyền vào
 * @param {*} status 
 * CreatedBy VMHieu 21/05/2023
 */
const openFormQuestion = (status) => {
    store.dispatch("showFormQuestion", status);
    store.dispatch("updateFormModeQuestion", Enum.FormModeQuestion.Add);
}
</script>

<style scoped>
.create-main__list{
    height: calc(100vh - 188px);
    padding: 0 32px 32px 32px;
    margin: 0 -32px 24px -32px;
    margin-top: 36px;
    overflow: auto;
}

.create-main__form{
    color: #4e5b6a;
    height: 100%;
    padding-bottom: 20px;
}

.form-import{
    flex-direction: column;
    padding-top: 1.5rem;
}

.import-title, .create-title{
    margin-bottom: 1.5rem;
    font-weight: 700;
    font-size: 18px;
    line-height: 26px;
}

.import-box{
    width: 840px;
    height: 224px;
    justify-content: center;
    flex-direction: column;
    position: relative;
    border: 2px dashed #b6b9ce;
    padding: 24px 0;
    border-radius: 10px;
    text-align: center;
    box-sizing: border-box;
}

.import-box__avatar{
    margin-bottom: 16px;
    height: 64px;
}

.import-box__content{
    font-size: 16px;
}

.import-box__btn{
    margin-top: 12px;
}

.form-create{
    display: flex;
    flex-direction: column;
    align-items: center;
    margin-top: 24px;
}

.create-option{
    display: flex;
}

.question-library{
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
    margin-right: 8px;
    width: 120px;
}

.question-line{
    margin-right: 56px;
    display: block;
    margin-left: 1.25rem;
}

.question-text{
    width: max-content;
    margin-top: 12px;
    font-size: 16px;
}

.question-list{
    display: grid;
    grid-template-columns: 1fr;
    grid-row-gap: 20px;
}
.question-library>.question-avatar>img{
    width: 64px;
    height: 64px;
}

.question{
    display: flex;
    padding-left: .5rem;
    width: 100%;
}
.question>div{
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
    margin-right: 56px;
    width: 64px;
}

.question-hover:hover{
    cursor: pointer;
    transform: scale(1.1);
    transition: .3s;
    outline: none;
}

.option-list{
    position: absolute;
    right: -88px;
    top: 0;
    flex-direction: column;
}

.option-list>div{
    height: 40px;
    margin-bottom: 8px;
    margin-right: 0;
}
.option-list>.question-library{
    width: auto;
}

.option-list img{
    width: 40px !important;
    height: 40px !important;
}
</style>