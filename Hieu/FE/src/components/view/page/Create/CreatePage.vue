<template>
    <div class="create-container">
        <div class="create-header">
            <div class="header-top flex" v-tooltip="{
                        theme: {
                            placement: 'right',
                        },
                    }">
                <div class="btn-back" @click="backMainPage" v-tooltip="'Trở về trang trước'">
                    <img :src="btnBackImg" alt="">
                </div>
                <div class="header-top__avatar">
                    <img :src="createImg" alt="" v-if="!dataExercise.ExerciseImage">
                    <img :src="`${constants.API_URL}/${dataExercise.ExerciseImage}`" v-if="dataExercise.ExerciseImage" alt="">
                </div>
                <div class="header-top__context">
                    <input type="text" class="exercise-name__input" v-model="dataExercise.ExerciseName" placeholder="Nhập tên bài tập...">
                </div>
            </div>
            <div class="header-bottom flex">
                <div class="header-bottom__left flex">
                    <div class="combobox-subject">
                        <BaseCombobox
                            :data="subjects"
                            propText="SubjectName"
                            propValue="SubjectID"
                            v-model="dataExercise.SubjectID"
                            :valueCombobox="dataExercise.SubjectID"
                            :openClear="false"
                            @changeValue="changeValue"
                        ></BaseCombobox>
                    </div>
                    <div class="combobox-class">
                        <BaseCombobox
                            :data="grades"
                            propText="GradeName"
                            propValue="GradeID"
                            v-model="dataExercise.GradeID"
                            :valueCombobox="dataExercise.GradeID"
                            :openClear="false"
                            @changeValue="changeValue"
                        ></BaseCombobox>
                    </div>
                    <div class="btn-addinfo">
                        <BaseButton class="ms-button btn-white btn-active" text="Bổ sung thông tin" @click="openFormExercise"></BaseButton>
                    </div>
                </div>
                <div class="header-bottom__right flex">
                    <div class="btn-question">
                        <BaseButton class="ms-button btn-white btn-active btn-onlyicon  btn-40" v-tooltip="'Trợ giúp'">
                            <template v-slot:icon>
                                <div class="center">
                                    <img :src="questionImg" alt="">
                                </div>
                            </template>
                        </BaseButton>
                    </div>
                    <div class="btn-try">
                        <BaseButton class="ms-button btn-white btn-active" text="Làm thử"></BaseButton>
                    </div>
                    <div class="btn-submit">
                        <BaseButton class="ms-button btn-blue btn-active" text="Hoàn thành" @click="saveExercise"></BaseButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="create-main">
            <CreateForm v-show="!showListQuestion"></CreateForm>
            <CreateList v-show="showListQuestion" :data="dataQuestion"></CreateList>
        </div>
        <ToastMessage></ToastMessage>
        <PopupMessage></PopupMessage>
    </div>
    <FormQuestion :data="dataQuestion" :dataExercise="dataExercise"></FormQuestion>
    <FormExercise :data="dataExercise" v-model="dataExerciseAdd" @saveForm="saveForm"></FormExercise>
    <FormImport :dataExercise="dataExercise"></FormImport>
</template>

<script setup>
import BaseCombobox from '@/components/base/combobox/BaseCombobox.vue';
import BaseButton from '@/components/base/button/BaseButton.vue';
import FormQuestion from '@/components/view/FormQuestion.vue';
import FormExercise from '@/components/view/FormExercise.vue';
import FormImport from '@/components/view/FormImport.vue';
import ToastMessage from '@/components/view/ToastMessage.vue';
import PopupMessage from '@/components/view/PopupMessage.vue';
import CreateForm from '@/components/view/page/Create/CreatePage/CreateForm.vue'
import CreateList from '@/components/view/page/Create/CreatePage/CreateList.vue'
import { useRouter, useRoute } from 'vue-router';
import { useStore } from 'vuex';
import { computed, reactive, onBeforeMount, watch, ref, defineProps, onMounted } from 'vue'; 
import * as Enum from '@/common/enum/Enum.js';
import * as Resource from '@/common/resource/Resource.js';
import { constants } from '@/config/config';

// Các biến lưu đường dẫn
const btnBackImg = require("@/assets/img/btn-back.svg");
const createImg = require("@/assets/img/create-default.webp");
const questionImg = require("@/assets/img/icon-question.svg");
const importImg = require("@/assets/img/import-file.svg");
const libraryImg = require("@/assets/img/library.svg");
const lineImg = require("@/assets/img/line.svg");
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
const grades = computed(() => store.state.grade.grades);    // Bản ghi khối
const subjects = computed(() => store.state.subject.subjects); // Bản ghi môn học
const exercise = computed(() => store.state.exercise.exercise); // Thông tin bài tập và câu hỏi, đán án của bt
const exerciseID = computed(() => store.state.exercise.exerciseID); // ID bản ghi mới thêm
const formModeExercise = computed(() => store.state.exercise.formModeExercise); // Thông tin bài tập và câu hỏi, đán án của bt
const refresh = computed(() => store.state.exercise.refresh); // Refresh lại trang
// Object data bài tập
const dataExercise = ref({
    ExerciseName: "", 
    ExerciseImage: "",
});
// Object data câu hỏi
const dataQuestion = ref({});
// Object data đáp án
const dataAnswer = ref({});

const dataExerciseAdd = ref({});


/**
 * Qua lại router trước
 * CreatedBy VMHieu 21/05/2023
 */
const backMainPage = () => {
    store.dispatch("updateHideMainPage", false);
    router.push("/storage/mine");
}
/**
 * Mở form question theo status truyền vào
 * @param {*} status 
 * CreatedBy VMHieu 21/05/2023
 */
const openFormQuestion = (status) => {
    store.dispatch("showFormQuestion", status);
    store.dispatch("updateFormModeQuestion", Enum.FormModeQuestion.Add);
}
/**
 * Mở form thông tin bài tập
 * CreatedBy VMHieu 22/05/2023
 */
const openFormExercise = () => {
    store.dispatch("showFormExercise", true);
}
/**
 * Validate dữ liệu bài tập
 * VMHieu 01/06/2023
 */
const validateExercise = () => {
    let isValid = true;
    if (!dataExercise.value.ExerciseName || !dataExercise.value.GradeID || !dataExercise.value.SubjectID) {
        isValid = false;
    } 
    if (dataExercise.value.ExerciseName.length > 255) {
        store.dispatch('updatePopupMsg', Resource.PopupMessage.ExerciseNameMaxLength);
        store.dispatch('showPopup', true);
        store.dispatch('updatePopupStatus', Enum.PopupStatus.Error);
        isValid = false;
    }
    return isValid;
}

/**
 * Sự kiện lưu form bài tập
 * VMHieu 06/01/2023
 */
const saveForm = (data) => {
    dataExercise.value = data;
}

/**
 * Sự kiện khi thay đổi value combobox
 * VMHieu 01/06/2023
 */
 const changeValue = () => {
    let dataGradeSubject = {
        gradeID: dataExercise.value.GradeID,
        subjectID: dataExercise.value.SubjectID
    }
    store.dispatch("getTopicByGradeSubject", dataGradeSubject);
}


/**
 * Thực hiện cập nhật bài tập
 * VMHieu 01/06/2023
 */
const saveExercise = async () => {
    const id = route.query.id;
    if (validateExercise()) {
        if (id) {
            await store.dispatch("putExercise", dataExercise.value);
        } else {
            await store.dispatch("postExercise", dataExercise.value);
        }

        if (dataExercise.value.Topics && dataExercise.value.Topics.length > 0) {
            let dataTopic = {
                ExerciseID: dataExercise.value.ExerciseID || exerciseID.value,
                Topics: Object.values(dataExercise.value.Topics)
            }
            // Thêm chủ đề
            await store.dispatch("postTopicMultiple", dataTopic);
        }
        router.push("/storage/mine");
        store.dispatch("updateHideMainPage", false);
    } else {
        store.dispatch("showFormExercise", true);
    }
}

/**
 * Thực hiện công việc trước khi component được mount
 * VMHieu 31/05/2023
 */
onBeforeMount(() => {
    store.dispatch("getAllSubject");
    store.dispatch("getAllGrade");
    const id = route.query.id;
    if (id) { 
        store.dispatch("getAllByID", id);
    }
})

/**
 * Theo dõi sự thay đổi của đối tượng bài tập
 * VMHieu 31.05.2023
 */
watch((exercise), () => {
    dataExercise.value = exercise.value;
    dataQuestion.value = exercise.value.Question;
    // Xét hiện câu hỏi nếu có
    if (dataQuestion.value.length > 0) {
        store.dispatch("showListQuestion", true);
    } else {
        store.dispatch("showListQuestion", false);
    }
})
/**
 * Bắt việc refresh lại trang và back về trang chủ
 * VMHieu 06/01/2023 
 */
// watch((refresh), () => {
//     store.dispatch("updateHideMainPage", false);
//     router.push("/storage/mine");
// })




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