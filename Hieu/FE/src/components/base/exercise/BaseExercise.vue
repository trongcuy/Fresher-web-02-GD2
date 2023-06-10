<template>
    <div class="exercise-container"  @click="openExercise(props.data.ExerciseID)">
        <div class="exercise-img">
            <img :src="subjectImg" alt="">
            <div class="img-content center">{{ props.data.GradeName || "Khối 1" }} - {{ props.data.SubjectName || "Toán" }}</div>
        </div>
        <div class="exercise-content">
            <div class="content-title flex">
                <div>{{ ExerciseName  }}</div>
                <div class="btn-more" @click="toggleOption" @click.stop=""  v-tooltip="'Tùy chọn'">
                    <img :src="optionImg" alt="">
                </div>
            </div>
            <div class="content-number flex">
                <div class="flex" v-if="props.data.CountQuestion != 0">
                    <img :src="numberImg" alt="">
                    <div>{{ props.data.CountQuestion }} Câu</div>
                </div>
                <div class="exercise-status">{{ props.data.ExerciseStatus || "Đang soạn"}}</div>
            </div>
            <div class="content-creater flex">
                <img :src="userImg" alt="" />
                <div>{{ props.data.CreatedBy || "Vũ Minh Hiếu" }}</div>
            </div>
            <div class="more-option" v-show="showOption" v-clickOutside="closeOption">
                <div class="option" @click.stop="" @click="openExercise(props.data.ExerciseID)">Xem</div>
                <div class="option" @click.stop="" @click="handleOpenPopup(ExerciseName, props.data.ExerciseID)">Xóa</div>
            </div>
        </div>
    </div>
</template> 

<script setup>
import { ref, defineProps, watch, toRef, onMounted, computed } from 'vue';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';
import * as Enum from '@/common/enum/Enum';
import * as Resource from '@/common/resource/Resource';
import { changeBreakpoint } from '@/common/common';

const props = defineProps({
  data: [], // data của bài tập biding vào component
})

const dataRef = toRef(props, 'data');

const store = useStore();
const router = useRouter();
// Show option xem hoặc xóa bài tập
const showOption = ref(false);
// Các đường dẫn hình ảnh
const subjectImg = require("@/assets/subjects-avatar/diali.png");
const optionImg = require("@/assets/img/icon_option.svg");
const numberImg = require("@/assets/img/socau.svg");
const userImg = require("@/assets/img/user.svg");

const ExerciseName = computed(() => {
    if (!props.data.ExerciseName) {
        return "Bài nháp " + props.data.SubjectName + " " + props.data.GradeName;
    }
    return props.data.ExerciseName;
})

/**
 * Toggle toolbar
 * CreatedBy VMHieu 23/05/2023
 */
const toggleOption = () => {
    showOption.value = !showOption.value;
}
/**
 * Đóng option
 * VMHieu 06/01/2023
 */
const closeOption = () => {
    showOption.value = false;
}
/**
 * Mở form sửa bài tập
 * @param {*} id 
 * VMHieu 06/01/2023
 */
const openExercise = (id) => {
    store.dispatch("showListQuestion", true);
    store.dispatch("updateHideMainPage", true);
    store.dispatch("updateFormModeExercise", Enum.FormModeExercise.Edit);
    router.push({ path: "/storage/create", query: {id} });
}

/**
 * Mở popup xóa
 * CreatedBy VMHieu 23/05/2023
 */
const handleOpenPopup = (name, id) => {
    store.dispatch("updatePopupMsg", changeBreakpoint(Resource.PopupMessage.DeleteExercise, name));
    store.dispatch("updatePopupStatus", Enum.PopupStatus.Delete);
    store.dispatch("showPopup", true);
    store.dispatch("updateStatusDelete", Enum.StatusDelete.Exercise);
    showOption.value = false;

    store.dispatch("updateIdExerciseDelete", id);
}

// onMounted(() => {
   
// })
</script>

<style lang="scss" scoped>
.exercise-container {
  position: relative;
  background-color: #fff;
  border-radius: 10px;
  box-shadow: 0 3px 20px rgba(90, 125, 141, 0.16);
  height: 200px;
  width: 100%;
  display: block;
  cursor: pointer;

  .exercise-img {
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

    img {
      width: 100%;
      height: 100%;
      object-fit: cover;
      border-radius: 10px 10px 0 0;
    }
  }

  .img-content {
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
    border-top-left-radius: 10px;
    color: rgba(255, 255, 255, 1);
    background-color: rgba(138, 107, 246, 1);
  }

  .exercise-content {
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

    .content-creater img,
    .content-number img {
      margin-right: 0.5rem;
      height: 1rem;
    }

    .content-title {
      text-overflow: ellipsis;
      white-space: nowrap;
      overflow: hidden;
      color: #4e5b6a;
      font-weight: 700;
      font-size: 16px;
      line-height: 24px;
      justify-content: space-between;
    }

    .btn-more {
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
      z-index: 9;
    }
  }

  .more-option {
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
    box-shadow: 0 2px 12px 0 rgba(0, 0, 0, 0.1);

    .option {
      width: 112px;
      padding: 8px;
      cursor: pointer;

      &:hover {
      color: #8a6bf6;
      font-weight: 700;
    }
    }
  }

  .exercise-status {
    font-size: 12px;
    padding: 5px;
    border-radius: 6px;
    text-align: center;
    overflow: hidden;
    color: #8a6bf6 !important;
    background: #e8e1fd;
    margin-left: 0.625rem;
    margin-top: 0.125rem;
  }
}
</style>