<template>
    <div class="form-exercise" v-show="showFormExercise">
        <div class="exercise-container">
            <div class="exercise-header flex">
                <div class="exercise-title">
                    Bổ sung thông tin
                </div>
                <div class="exercise-close" @click="closeFormExercise" v-tooltip="'Thoát'">
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
                            <img :src="imageUrl || avatarImg" alt="">
                            <div class="upload-file">
                                <label class="label-input" @click="openUploadFile">
                                    <img :src="uploadImg" alt="">
                                </label>
                                <input type="file" @input="chooseFileImg" 
                                id="upload-file" ref="file" @change="handleFileUpload" accept=".png, .jpg, .jpeg"
                                >
                            </div>
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
                            <BaseInput 
                                :tabindex="1" 
                                v-model="dataExerciseClone.ExerciseName" 
                                :valueInput="dataExerciseClone.ExerciseName" 
                                ref="ipName"
                                :msgError="Resource.InputMessage.InvalidError"
                                inputClass="m-input no-icon">
                            </BaseInput>
                        </div>
                    </div>
                    <div class="form-info">
                        <div class="info-subject">
                            <label for="" class="name-text">
                                Môn
                                <span class="asterik">*</span>
                            </label>
                            <div class="form-input subject-input">
                                <BaseCombobox
                                :data="subjects"
                                propText="SubjectName"
                                propValue="SubjectID"
                                v-model="dataExerciseClone.SubjectID"
                                :valueCombobox="dataExerciseClone.SubjectID"
                                :openClear="false"
                                :tabindex="2"
                                @changeValue="changeValue"
                                :getValue="getValue"
                                ></BaseCombobox>
                            </div>
                        </div>
                        <div class="info-class">
                            <label for="" class="name-text">
                                Khối
                                <span class="asterik">*</span>
                            </label>
                            <div class="form-input class-input">
                                <BaseCombobox
                                    :data="grades"
                                    propText="GradeName"
                                    propValue="GradeID"
                                    v-model="dataExerciseClone.GradeID"
                                    :valueCombobox="dataExerciseClone.GradeID"
                                    :openClear="false"
                                    :tabindex="3"
                                    @changeValue="changeValue"
                                    :getValue="getValue"
                                ></BaseCombobox>
                            </div>
                        </div>
                    </div>
                    <div class="form-topic">
                        <div class="info-subject">
                            <label for="" class="name-text">
                                Chủ đề
                            </label>
                            <div class="form-input topic-input">
                                <BaseComboboxTag
                                    :placeholder=Resource.ComboboxDefault.Topic
                                    :data="listTopic"
                                    propText="TopicName"
                                    propValue="TopicID"
                                    v-model="dataExerciseClone.Topics"
                                    :valueCombobox="listExerciseTopic"
                                    :openClear="true"
                                    :tabindex="4"
                                    :getValue="getValue"
                                ></BaseComboboxTag>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="exercise-footer">
                <BaseButton class="ms-button btn-active btn-white" tabindex="6" text="Đóng" @click="closeFormExercise"></BaseButton>
                <BaseButton class="ms-button btn-active btn-blue" tabindex="5" text="Lưu" @click="saveDataExercise"></BaseButton>
            </div>
        </div>
    </div>
</template>

<script setup>
import BaseButton from "@/components/base/button/BaseButton.vue";
import BaseCombobox from "../base/combobox/BaseCombobox.vue";
import BaseComboboxTag from "../base/combobox/BaseComboboxTag.vue";
import { useStore } from 'vuex';
import { useRoute } from 'vue-router';
import { computed, watch, ref, isReactive, reactive, nextTick, onBeforeMount, defineProps, defineEmits } from 'vue';
import BaseInput from "../base/input/BaseInput.vue";
import * as Resource from "@/common/resource/Resource";
import * as Enum from "@/common/enum/Enum";
import _ from 'lodash'
import { FormModeExercise } from "@/common/enum/Enum";
// Các biến lưu đường dẫn
const avatarImg = require("@/assets/subjects-avatar/toan.png");
const uploadImg = require("@/assets/img/upload-image.svg");

const emit = defineEmits(['update:modelValue', 'saveForm']);
const props = defineProps({
    data: null, // Data mà component cha gửi xuống
})

const store = useStore();
const route = useRoute();
// Biến giá trị trạng thái form bài tập
const showFormExercise = computed(() => store.state.exercise.showFormExercise);
const exercise = computed(() => store.state.exercise.exercise);
const grades = computed(() => store.state.grade.grades);
const subjects = computed(() => store.state.subject.subjects);
const listTopic = computed(() => store.state.topic.listTopic);
const listExerciseTopic = computed(() => store.state.exercisetopic.listExerciseTopic);
const formModeExercise = computed(() => store.state.exercise.formModeExercise);
// Thông tin bản ghi bài tập
const dataExercise = ref({});
let dataExerciseClone = reactive({
    ExerciseName: "",
    GradeID: "",
    SubjectID: "",
    ExerciseImage: "",
    Topics: [],
});
const topicList = ref(""); // List các ID topic
const ipName = ref("ipName"); // Ô input name
const getValue = ref(true); // Tín hiệu lấy giá trị của cbb tag
const file = ref('file'); // Input file
const imageUrl = ref(""); // Đường dẫn ảnh

/**
 * Đóng form bài tập
 * CreatedBy VMHieu 23/05/2023
 */
const closeFormExercise = () => {
    store.dispatch("showFormExercise", false);
}
/**
 * Validate Form
 * VMHieu 01/06/2023
 */
const validateFormExercise = () => {
    let isValid = true;

    if (!dataExerciseClone.ExerciseName || !dataExerciseClone.GradeID || !dataExerciseClone.SubjectID) {
        isValid = false;
    }

    if (!dataExerciseClone.ExerciseName) {
        let input = ipName.value.input;
        let error = ipName.value.error;
        input.classList.add("input-error");
        error.style.display = "block";
    }

    return isValid;
}

/**
 * Lưu tạm thông tin bài tập
 * VMHieu 31/05/2023
 */
const saveDataExercise = () => {
    getValue.value = !getValue.value;
    if (validateFormExercise()) {
        emit("saveForm",dataExerciseClone);
        store.dispatch("showFormExercise" , false);
    } 
}
/**
 * Sự kiện khi thay đổi value combobox
 * VMHieu 01/06/2023
 */
const changeValue = () => {
    for (let prop in listExerciseTopic) {
        delete listExerciseTopic[prop];
    }

    let dataGradeSubject = {
        gradeID: dataExerciseClone.GradeID,
        subjectID: dataExerciseClone.SubjectID
    }
    store.dispatch("getTopicByGradeSubject", dataGradeSubject);
}
/**
 * Mở form chọn file
 * VMHieu 05/06/2023
 */
const openUploadFile = () => {
    file.value.click();
}

/**
 * Ấn tải file lên
 * @param {*} event 
 */
const handleFileUpload = (event) => {
    const file = event.target.files[0];
    const reader = new FileReader();

    reader.onload = (e) => {
        imageUrl.value = e.target.result;
    };

    reader.readAsDataURL(file);
};

const chooseFileImg = () => {
    console.log(file.value.files[0]);
}

onBeforeMount(() => {
    const id = route.query.id;
    if (id) {
        store.dispatch("updateFormModeExercise", Enum.FormModeExercise.Edit);
    } else {
        store.dispatch("updateFormModeExercise", Enum.FormModeExercise.Add);
    }
})

/**
 * Theo dõi sự thay đổi của đối tượng bài tập
 * VMHieu 31.05.2023
 */
watch((exercise), () => {
    dataExercise.value = exercise.value;
    let dataGradeSubject = {
        gradeID: dataExercise.value.GradeID,
        subjectID: dataExercise.value.SubjectID
    }
    store.dispatch("getTopicByGradeSubject", dataGradeSubject);
})
/**
 * Mở form và biding dữ liệu vào data clone
 * VMHieu 31/05/2023
 */
watch((showFormExercise), () => {
    if (showFormExercise.value) {
        dataExerciseClone = _.cloneDeep(props.data);
        const id = route.query.id;
        if (id) {
            store.dispatch("getTopicByID", id);
        } 
    } 
})

</script>

<style lang="scss" scoped>

.form-exercise {
  position: fixed;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  z-index: 2000;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  overflow: auto;
  scrollbar-width: thin;

  .exercise-container {
    position: absolute;
    transform: translate(-50%, -50%);
    top: 50%;
    left: 50%;
    width: 800px;
    border-radius: 10px;
    box-shadow: 0 0 20px rgba(0, 0, 0, 0.16);
    margin: 0 auto 50px;
    background: #fff;
    box-sizing: border-box;

    .exercise-header {
      padding: 24px;
      justify-content: space-between;

      .exercise-title {
        font-weight: 700;
        font-size: 28px;
        line-height: 36px;
        color: #4e5b6a;
      }

      .exercise-close {
        cursor: pointer;
      }
    }

    .exercise-main {
      padding: 0 24px 24px;
      word-break: break-word;
      color: #606266;
      font-size: 14px;
      display: flex;

      .main-left {
        width: 260px;
        display: flex;
        flex-direction: column;

        .exercise-avatar {
          .avatar-text {
            color: #4e5b6a;
            font-weight: 500;
            margin-bottom: 4px;
            font-size: 14px;
            line-height: 20px;
            display: block;
          }

          .img-avatar {
            border-radius: 10px;
            background-color: #f1f2f7;
            width: 100%;
            height: 156px;
            overflow: hidden;
            margin-bottom: 8px;
            position: relative;

            img {
              width: 100%;
              height: 100%;
              -o-object-fit: cover;
              object-fit: cover;
            }

            .upload-file{
                position: absolute;
                bottom: 0.75rem;
                right: 0.75rem;

                img{
                    width: 40px;
                    height: 40px;
                    cursor: pointer;
                }

                #upload-file{
                    display: none;
                    bottom: 0.75rem;
                    right: 0.75rem;
                    position: absolute;
                    z-index: 10;
                }
            }
          }
        }
      }

      .main-right {
        width: calc(100% - 260px);
        padding-left: 40px;
        display: grid;
        grid-template-columns: 100%;
        grid-row-gap: 16px;

        label {
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
      }
    }

    .exercise-footer {
      padding: 0 24px 24px;
      text-align: right;
      box-sizing: border-box;
    }
  }
}
</style>