<template>
    <BaseMinePage>
        <template v-slot:content>
            <div class="list-my-post">
                <div class="list-header">
                    <div class="list-search w-full">
                        <BaseInput 
                            showIcon="true" 
                            v-model="keyword" 
                            inputClass="m-input" 
                            placeholder="Nhập tên học liệu để tìm kiếm"
                            @keypress.enter="handleSearch"
                            :clearInput="true"
                            @clearInput="handleSearch"
                        >
                        </BaseInput>
                    </div>
                    <div class="list-filter flex">
                        <div class="filter-courseware">
                            <BaseCombobox
                                :placeholder=Resource.ComboboxDefault.Courseware
                                :propValue=Resource.PropName.ExerciseStatus
                                propText="Data"
                                :data=Resource.DataExerciseStatus
                                v-model="filterData.ExerciseStatus"
                                :openClear="true"
                            ></BaseCombobox>
                        </div>
                        <div class="filter-subject">
                            <BaseCombobox
                                :placeholder=Resource.ComboboxDefault.Subject
                                :data="subjects"
                                propText="SubjectName"
                                propValue="SubjectID"
                                v-model="filterData.SubjectID"
                                :openClear="true"
                            ></BaseCombobox>
                        </div>
                        <div class="filter-class">
                            <BaseCombobox
                                :placeholder=Resource.ComboboxDefault.Grade
                                :data="grades"
                                propText="GradeName"
                                propValue="GradeID"
                                v-model="filterData.GradeID"
                                :openClear="true"
                            ></BaseCombobox>
                        </div>
                    </div>
                </div>
                <div class="list-body">
                    <div v-for="(exercise, index) in exercises" :key="index" v-show="showListBody">
                        <BaseExercise :data="exercise"></BaseExercise>
                    </div>
                </div>
                <div class="no-data" v-show="!showListBody">
                    <img :src="nodataImg" alt="">
                    <div class="nodata-msg">Không tìm thấy học liệu</div>
                </div>
                <div class="list-footer">
                    <div class="footer-content center" v-show="showExtend">
                        <BaseButton class="ms-button btn-blue btn-active" @click="extendExercise" text="Xem thêm"></BaseButton>
                    </div>
                </div>
            </div>
        </template>
    </BaseMinePage>
    <PopupMessage></PopupMessage>
    <!-- <div class="loader">
        <div id="loading"></div>
    </div> -->
</template>

<script setup>
import BaseButton from '@/components/base/button/BaseButton.vue';
import BaseCombobox from '@/components/base/combobox/BaseCombobox.vue';
import BaseExercise from '@/components/base/exercise/BaseExercise.vue';
import BaseInput from '@/components/base/input/BaseInput.vue';
import BaseMinePage from '@/components/base/minepage/BaseMinePage.vue';4
import PopupMessage from '../../PopupMessage.vue';
import CreatePage from '../Create/CreatePage.vue';
import * as Resource from '@/common/resource/Resource';
import * as Enum from '@/common/enum/Enum';
import { computed, onMounted, watch, reactive, ref } from 'vue';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';

const store = useStore();
const router = useRouter();

const exercises = computed(() => store.state.exercise.exercises); // Bản ghi bài tập
const grades = computed(() => store.state.grade.grades);    // Bản ghi khối
const subjects = computed(() => store.state.subject.subjects); // Bản ghi môn học
const showExtend = computed(() => store.state.exercise.showExtend); // Ẩn hiện nút xem thêm
const take = computed(() => store.state.exercise.take); // Ẩn hiện nút xem thêm
const refresh = computed(() => store.state.exercise.refresh); // Load lại trang
// Các biến lưu đường dẫn
const nodataImg = require("@/assets/img/nodata.svg");

const keyword = ref(""); // Dữ liệu tìm kiếm
const showListBody = ref(true); // Hiển thị danh sách bài tập
// Object chứa dữ liệu lọc 
const filterData = reactive({
    GradeID: "",
    SubjectID: "",
    ExerciseStatus: "",
})

/**
 * Mở rộng thêm bản ghi bài tập
 * VMHieu 30/05/2023
 */
const extendExercise = () => {
    store.dispatch("updateTake", take.value + Enum.TakeExercise.Take);
    store.dispatch("getPaging");
}
/**
 * Thực hiện tìm kiếm khi ấn nút enter trong ô tìm kiếm
 * VMHieu 30/05/2023
 */
const handleSearch = () => {
    store.dispatch("updateTake", Enum.TakeExercise.Take);
    store.dispatch("updateSearch", keyword.value);
    store.dispatch("getPaging");
}

onMounted(() => {
    store.dispatch("updateFilter", filterData);
    store.dispatch("getPaging");
    store.dispatch("getAllSubject");
    store.dispatch("getAllGrade");
    showListBody.value = false;
})
/**
 * Xem sự thay đổi của dữ liệu lọc để cập nhật
 * VMHieu 30/05/2023
 */
watch((filterData) , () => {
    store.dispatch("updateTake", Enum.TakeExercise.Take);
    store.dispatch("updateFilter", filterData);
    store.dispatch("getPaging");
})

/**
 * Xem sự thay đổi của dữ liệu tìm kiếm để cập nhật
 * VMHieu 30/05/2023
 */
watch((keyword) , () => {
    store.dispatch("updateSearch", keyword.value);
})
/**
 * Xem sự thay đổi của danh sách bài tập
 * VMHieu 06/01/2023
 */
watch((exercises), () => {
    if (exercises.value.length > 0) {
        showListBody.value = true;
    } else {
        showListBody.value = false;
    }
})
</script>

<style scoped>
.list-my-post{
    margin: 0.5rem 0;
    width: 100%;
}

.list-filter{
    margin-top: 0.75rem;
}

.list-filter div{
    margin-right: 16px;
}

.list-filter div:last-child{
    margin-right: 0;
}

.list-body{
    margin-top: 1.25rem;
    padding-bottom: 10px;
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    grid-gap: 24px;
}

.filter-courseware{
    width: 150px;
}

.filter-subject, .filter-class{
    width: 174px;
}

.no-data{
    width: 100%;
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    gap: 24px;
}

.nodata-msg{
    font-size: 16px;
    line-height: 24px;
    color: #4e5b6a;
}
</style>