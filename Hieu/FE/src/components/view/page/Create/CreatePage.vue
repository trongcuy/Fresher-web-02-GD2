<template>
    <div class="create-container">
        <div class="create-header">
            <div class="header-top flex">
                <div class="btn-back" @click="backMainPage">
                    <img src="@/assets/img/btn-back.svg" alt="">
                </div>
                <div class="header-top__avatar">
                    <img src="@/assets/img/create-default.webp" alt="">
                </div>
                <div class="header-top__context">
                    <input type="text" class="exercise-name__input" v-model="dataExercise.ExerciseName" placeholder="Nhập tên bài tập...">
                </div>
            </div>
            <div class="header-bottom flex">
                <div class="header-bottom__left flex">
                    <div class="combobox-subject">
                        <BaseCombobox></BaseCombobox>
                    </div>
                    <div class="combobox-class">
                        <BaseCombobox></BaseCombobox>
                    </div>
                    <div class="btn-addinfo">
                        <BaseButton class="ms-button btn-white btn-active" text="Bổ sung thông tin" @click="openFormExercise"></BaseButton>
                    </div>
                </div>
                <div class="header-bottom__right flex">
                    <div class="btn-question">
                        <BaseButton class="ms-button btn-white btn-active btn-onlyicon  btn-40">
                            <template v-slot:icon>
                                <div class="center">
                                    <img src="@/assets/img/icon-question.svg" alt="">
                                </div>
                            </template>
                        </BaseButton>
                    </div>
                    <div class="btn-try">
                        <BaseButton class="ms-button btn-white btn-active" text="Làm thử"></BaseButton>
                    </div>
                    <div class="btn-submit">
                        <BaseButton class="ms-button btn-blue btn-active" text="Hoàn thành"></BaseButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="create-main">
            <div class="create-main__form" v-show="!showListQuestion">
                <div class="form-import center">
                    <div class="import-title">
                        Tách câu hỏi tự động sử dụng công nghệ AI
                    </div>
                    <div class="import-box flex">
                        <div class="import-box__avatar">
                            <img src="@/assets/img/import-file.svg" alt="">
                        </div>
                        <div class="import-box__content">
                            Nhấn để tải lên file bài tập hoặc kéo thả file vào đây. <br />
                            File có định dạng xls, xlsx, doc, docx, pdf
                        </div>
                        <div class="import-box__btn flex">
                            <BaseButton class="ms-button btn-active btn-upload" text="Tải lên file bài tập"></BaseButton>
                            <BaseButton class="ms-button btn-active btn-green" text="Tải file Excel mẫu"></BaseButton>
                        </div>
                    </div>
                </div>
                <div class="form-create">
                    <div class="create-title">
                        hoặc tự tạo câu hỏi mới
                    </div>
                    <div class="create-option">
                        <div class="create-toolbar flex">
                            <div class="question-library">
                                <div class="question-avatar question-hover">
                                    <img src="@/assets/img/library.svg" alt="">
                                </div>
                                <div class="question-text">Thư viện học liệu</div>
                            </div>
                            <div class="question-line">
                                <div class="question-avatar">
                                    <img src="@/assets/img/line.svg" alt="">
                                </div>
                            </div>
                            <div class="question">
                                <div class="question-select" @click="openFormQuestion(Enum.FormQuestion.Select)">
                                    <div class="question-avatar question-hover">
                                        <img src="@/assets/img/select.svg" alt="">
                                    </div>
                                    <div class="question-text">Chọn đáp án</div>
                                </div>
                                <div class="question-yesno" @click="openFormQuestion(Enum.FormQuestion.YesOrNo)">
                                    <div class="question-avatar question-hover">
                                        <img src="@/assets/img/yesorno.svg" alt="">
                                    </div>
                                    <div class="question-text">Đúng sai</div>
                                </div>
                                <div class="question-fill" @click="openFormQuestion(Enum.FormQuestion.Fill)">
                                    <div class="question-avatar question-hover">
                                        <img src="@/assets/img/fill.svg" alt="">
                                    </div>
                                    <div class="question-text">Điền vào chỗ trống</div>
                                </div>
                                <div class="question-essay" @click="openFormQuestion(Enum.FormQuestion.Essay)">
                                    <div class="question-avatar question-hover">
                                        <img src="@/assets/img/essay.svg" alt="">
                                    </div>
                                    <div class="question-text">Tự luận</div>
                                </div>
                                <div class="question-group" @click="openFormQuestion(Enum.FormQuestion.Group)">
                                    <div class="question-avatar question-hover">
                                        <img src="@/assets/img/group.svg" alt="">
                                    </div>
                                    <div class="question-text">Câu hỏi nhóm</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="create-main__list" v-show="showListQuestion">
                <div class="question-list">
                    <BaseQuestion></BaseQuestion>
                    <BaseQuestion></BaseQuestion>
                    <BaseQuestion></BaseQuestion>
                    <BaseQuestion></BaseQuestion>
                    <BaseQuestion></BaseQuestion>
                    <BaseQuestion></BaseQuestion>
                    <BaseQuestion></BaseQuestion>
                </div>
                <div class="create-option option-list">
                    <div class="question-library">
                        <div class="question-avatar question-hover">
                            <img src="@/assets/img/library.svg" alt="">
                        </div>
                    </div>
                    <div class="question-select" @click="openFormQuestion(Enum.FormQuestion.Select)">
                        <div class="question-avatar question-hover">
                            <img src="@/assets/img/select.svg" alt="">
                        </div>
                    </div>
                    <div class="question-yesno" @click="openFormQuestion(Enum.FormQuestion.YesOrNo)">
                        <div class="question-avatar question-hover">
                            <img src="@/assets/img/yesorno.svg" alt="">
                        </div>
                    </div>
                    <div class="question-fill" @click="openFormQuestion(Enum.FormQuestion.Fill)">
                        <div class="question-avatar question-hover">
                            <img src="@/assets/img/fill.svg" alt="">
                        </div>
                    </div>
                    <div class="question-essay" @click="openFormQuestion(Enum.FormQuestion.Essay)">
                        <div class="question-avatar question-hover">
                            <img src="@/assets/img/essay.svg" alt="">
                        </div>
                    </div>
                    <div class="question-group" @click="openFormQuestion(Enum.FormQuestion.Group)">
                        <div class="question-avatar question-hover">
                            <img src="@/assets/img/group.svg" alt="">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <FormQuestion ></FormQuestion>
    <FormExercise ></FormExercise>
</template>

<script setup>
import BaseCombobox from '@/components/base/combobox/BaseCombobox.vue';
import BaseButton from '@/components/base/button/BaseButton.vue';
import BaseQuestion from '@/components/base/question/BaseQuestion.vue';
import FormQuestion from '@/components/view/FormQuestion.vue';
import FormExercise from '../../FormExercise.vue';
import { useRouter } from 'vue-router';
import { useStore } from 'vuex';
import { computed, reactive } from 'vue'; 
import * as Enum from '@/common/enum/Enum.js';

const store = useStore();
const router = useRouter();

const showListQuestion = computed(() => store.state.question.showListQuestion);
const showOver = computed(() => store.state.app.showOver);

const dataExercise = reactive({
    ExerciseName: ""
})
/**
 * Qua lại router trước
 * CreatedBy VMHieu 21/05/2023
 */
const backMainPage = () => {
    store.dispatch("updateHideMainPage", false);
    router.go(-1);
}
/**
 * Mở form question theo status truyền vào
 * @param {*} status 
 * CreatedBy VMHieu 21/05/2023
 */
const openFormQuestion = (status) => {
    store.dispatch("showFormQuestion", status);
}
/**
 * Mở form thông tin bài tập
 * CreatedBy VMHieu 22/05/2023
 */
const openFormExercise = () => {
    store.dispatch("showFormExercise", true);
}

</script>

<style scoped>
.create-container {
    box-sizing: border-box;
}

.create-header {
    box-shadow: 0 3px 20px rgba(90,125,141,.16);
    padding: 1rem 0.75rem 1rem 0;
    background: #fff;
}

.header-top{
    gap: 1rem;
    margin-bottom: 1rem;
}

.btn-back{
    margin-left: 1.5rem;
    cursor: pointer;
}
.header-top__avatar{
    border-radius: 9999px;
}

.header-top__avatar img{
    object-fit: cover;
    border-radius: 9999px;
    width: 40px;
    height: 40px;
}

.header-top__context{
    width: 100%;
    border: none;
    outline: none;
    color: #4e5b6a;
    font-weight: 700;
    font-size: 24px;
    line-height: 32px;
}
.header-bottom{
    justify-content: space-between;
    padding-left: 4rem;
}

.header-bottom__left{
    gap: 0.75rem;
}

.combobox-subject{
    width: 160px;
}

.combobox-class{
    width: 100px;
}

.create-main{
    width: calc(100% - 344px);
    height: calc(100% - 132px);
    position: relative;
    margin: 0 auto;
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

.create-main__list{
    height: calc(100vh - 188px);
    padding: 0 32px 32px 32px;
    margin: 0 -32px 24px -32px;
    margin-top: 36px;
    overflow: auto;
}

.exercise-name__input, .exercise-name__input::placeholder{
    border: none;
    outline: none;
    color: #4e5b6a;
    font-weight: 700;
    width: 100%;
    font-size: 24px;
    line-height: 32px;
    background: #fff;
}
</style>