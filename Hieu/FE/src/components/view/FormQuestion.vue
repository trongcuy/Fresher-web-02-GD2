<template>
    <div class="form-question" v-if="showFormQuestion != 0">
        <div class="form-container" v-show="showNoteQuestion">
            <div class="form-body">
                <div class="form-body__header flex">
                    <div class="choose-question-type flex">
                        <div class="form-question__number"> Câu {{ dataQuestion.SortOder || numberQuestion }} - </div>
                        <BaseCombobox 
                            cbbClass="combobox-question"
                            :data=Resource.FormQuestionType
                            :propValue=Resource.PropName.FormQuestion
                            propText="Data"
                            v-model="typeQuestion"
                            :autoFocus=showFormQuestion
                            :openClear="false"
                        ></BaseCombobox>
                    </div>
                </div>
                <div class="form-body__main" ref="main">
                    <CKEditor 
                    class="question-ckeditor" 
                    v-model="dataQuestion.QuestionContent"
                    :dataEditor="dataQuestion.QuestionContent"
                    ></CKEditor>
                </div>
                <div class="form-body__answer" v-if="showFormQuestion == Enum.FormQuestion.Select">
                    <div class="list-answer" >
                        <div v-for="(answer, index) in dataAnswerSelect" :key="index">
                            <BaseAnswer 
                                :data="dataAnswerSelect[index]" 
                                :index="index" 
                                @removeAnswer="removeAnswer(index)"
                                v-model="dataAnswerSelect[index]"
                                :saveData="saveData"
                            ></BaseAnswer>
                        </div>
                    </div>
                </div>
                <div class="form-body__answer" v-if="showFormQuestion == Enum.FormQuestion.YesOrNo">
                    <div class="list-answer">
                        <div></div>
                        <BaseAnswer 
                            :data="dataAnswerYesNo[0]" 
                            :index="Resource.AnswerYes.SortOder"
                            v-model="dataAnswerYesNo[0]"
                            @removeTick="removeTick(Resource.AnswerYes.SortOder)"
                            :indexRemove="indexRemove"
                        ></BaseAnswer>
                        <BaseAnswer 
                            :data="dataAnswerYesNo[1]"
                            :index="Resource.AnswerNo.SortOder"
                            v-model="dataAnswerYesNo[1]"
                            @removeTick="removeTick(Resource.AnswerNo.SortOder)"
                            :indexRemove="indexRemove"
                        ></BaseAnswer>
                        <div></div>
                    </div>
                </div>
                <div class="form-body__answer" v-if="showFormQuestion == Enum.FormQuestion.Fill">
                    <div class="answer-fill">
                        <div v-for="(answer, index) in dataAnswerFill" :key="index">
                            <BaseAnswerFill
                                :data="dataAnswerFill[index]"
                                :index="index"
                                v-model="dataAnswerFill[index]"
                                @removeAnswer="removeAnswer(index)"
                            ></BaseAnswerFill>
                        </div>
                    </div>
                    <div class="answer-fill__add flex">
                        <div class="fill-number"></div>
                        <div class="fill-add__button flex" @click="addAnswer">
                            <div class="fill-add__img">
                                <img :src="addImg" alt="">
                            </div>
                            <div class="fill-add__text" >
                                Thêm đáp án
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-footer flex">
                <div class="footer-left">
                    <BaseButton class="ms-button btn-active btn-white" v-if="showFormQuestion == Enum.FormQuestion.Select" text="Thêm đáp án" @click="addAnswer"></BaseButton>
                </div>
                <div class="footer-right">
                    <BaseButton class="ms-button btn-active btn-white" text="Hủy" @click="closeForm"></BaseButton>
                    <BaseButton class="ms-button btn-active btn-white" text="Lưu và đóng" @click="saveClose"></BaseButton>
                    <BaseButton class="ms-button btn-active btn-blue" text="Lưu và thêm câu" @click="saveAdd"></BaseButton>
                </div>
            </div>
        </div>
        <div class="form-container form-note" v-show="!showNoteQuestion">
            <div class="form-body">
                <div class="form-body__header flex">
                    <div class="choose-question-type flex">
                        <div class="form-question__number">Câu {{ numberQuestion }} </div>
                    </div>
                </div>
                <div class="form-body__main fullscreen">
                    <CKEditor 
                    class="question-ckeditor" 
                    v-model="dataQuestion.QuestionNote"
                    :dataEditor="dataQuestion.QuestionNote"
                    ></CKEditor>
                </div>
            </div>
            <div class="form-footer flex">
                <div class="footer-right">
                    <BaseButton class="ms-button btn-active btn-white" text="Hủy" @click="closeForm"></BaseButton>
                    <BaseButton class="ms-button btn-active btn-blue" text="Lưu"></BaseButton>
                </div>
            </div>
        </div>
        <div class="add-note flex" v-show="showNoteQuestion" @click="showNoteQuestion = false">
            <div class="note-img">
                <img :src="noteaddImg" alt="">
            </div>
            <div class="note-text" >
                Thêm lời giải
            </div>
        </div>
        <div class="close-note flex" v-show="!showNoteQuestion" @click="showNoteQuestion = true">
            <div class="note-img">
                <img :src="notebackImg" alt="">
            </div>
            <div class="note-text">
                Quay lại câu hỏi
            </div>
        </div>
    </div>
    <PopupMessage></PopupMessage>
</template>

<script setup>
import BaseCombobox from '../base/combobox/BaseCombobox.vue';
import BaseButton from '../base/button/BaseButton.vue';
import BaseAnswer from '../base/answer/BaseAnswer.vue';
import PopupMessage from './PopupMessage.vue';
import ToastMessage from './ToastMessage.vue';
import BaseAnswerFill from '../base/answer/BaseAnswerFill.vue';
import CKEditor from '@/components/base/ckeditor/CKEditor.vue';
import { useStore } from 'vuex';
import { useRoute, useRouter } from 'vue-router';
import { computed, ref, watch, reactive, defineProps, defineEmits, nextTick, onMounted, isReactive } from 'vue'; 
import * as Resource from '@/common/resource/Resource';
import * as Enum from '@/common/enum/Enum';
import { handleShowToast } from '@/common/common';
import _ from 'lodash'

// Các biến lưu đường dẫn
const noteaddImg = require("@/assets/img/icon-noteadd.svg");
const notebackImg = require("@/assets/img/icon-noteback.svg");
const addImg = require("@/assets/img/icon-add.svg");

const props = defineProps({
    data: null, //Data câu hỏi
    dataExercise: null, // Data bài tập
})

const emit = defineEmits(['saveQuestion', 'getValueExercise']);

const store = useStore();
const route = useRoute();
const router = useRouter();

const showFormQuestion = computed(() => store.state.question.showFormQuestion); // Mở form câu hỏi
const exerciseID = computed(() => store.state.exercise.exerciseID); // ID bản ghi bài tập mới thêm
const formModeQuestion = computed(() => store.state.question.formModeQuestion); // ID bản ghi câu hỏi mới thêm
const question = computed(() => store.state.question.dataQuestion); // data bản ghi câu hỏi mới thêm
// Tính toán số thứ tự câu
const numberQuestion = computed(() => {
    if (props.data.length > 0) {
        let num = props.data.length;
        return `${(num + 1)}`;
    } 
    return "1";
})

const typeQuestion = ref(); // Kiểu câu hỏi
const showNoteQuestion = ref(true); // Show form chú thích
const saveData = ref(true); // Dấu hiệu lấy data truyền cho component con
const indexRemove = ref(); // index của đáp án cần xóa tick

const main = ref("main");
// Dữ liệu câu hỏi
let dataQuestion = reactive({
    QuestionContent: "",
    QuestionNote: "",
    QuestionImage: "",
    SortOder: "",
    TypeQuestion: ""
});
// Dữ liệu đáp án chọn
let dataAnswerSelect = reactive([
   {
        AnswerContent: "",
        AnswerImage: "",
        AnswerStatus: "",
        SortOder: "1"
   },
   {
        AnswerContent: "",
        AnswerImage: "",
        AnswerStatus: "",
        SortOder: "2"
   },
   {
        AnswerContent: "",
        AnswerImage: "",
        AnswerStatus: "",
        SortOder: "3"
   },
   {
        AnswerContent: "",
        AnswerImage: "",
        AnswerStatus: "",
        SortOder: "4"
   }
])

// Dữ liệu đáp án chọn
let dataAnswerYesNo = reactive([
   {
        AnswerContent: "Đúng",
        AnswerImage: "",
        AnswerStatus: "",
        SortOder: "1"
   },
   {
        AnswerContent: "Sai",
        AnswerImage: "",
        AnswerStatus: "",
        SortOder: "2"
   }
])

// Dữ liệu đáp án điền vào ô trống
let dataAnswerFill = reactive([
    {
        AnswerContent: "",
        SortOder: "1"
    }
])
/** 
 * Đóng form
 * CreatedBy VMHieu 21/05/2023
 */
const closeForm = () => {
    store.dispatch("showFormQuestion", 0);
    showNoteQuestion.value = true;
}

/**
 * Thêm đáp án
 * VMHieu 02/06/2023
 */
const addAnswer = () => {
    if (showFormQuestion.value == Enum.FormQuestion.Select) {
        let bonusAnswer = {
            AnswerContent: "",
            AnswerImage: "",
            AnswerStatus: "",
            SortOder: ""
        }
        dataAnswerSelect.push(bonusAnswer);
    } else if (showFormQuestion.value == Enum.FormQuestion.Fill) {
        let bonusAnswer = {
            AnswerContent: "",
            SortOder: ""
        }
        dataAnswerFill.push(bonusAnswer);
    }

}
/**
 * Xóa đáp án
 * VMHieu 02/06/2023
 */
const removeAnswer = (index) => {
    if (showFormQuestion.value == Enum.FormQuestion.Select) {
        if (dataAnswerSelect.length == 1) {
            showToastWarning(Resource.ToastWarning.LeastAnswer);
        } else {
            dataAnswerSelect.splice(index, 1);
        }
    } else if (showFormQuestion.value == Enum.FormQuestion.Fill) {
        if (dataAnswerFill.length == 1) {
            showToastWarning(Resource.ToastWarning.LeastAnswer);
        } else {
            dataAnswerFill.splice(index, 1);
        }
    }
}

/**
 * Xóa tick đáp án đúng
 * @param {*} index 
 * VMHieu 05/06/2023
 */
const removeTick = (index) => {
    indexRemove.value = index;
}

/**
 * Kiểm tra thông tin câu hỏi và đáp án
 * VMHieu 02/06/2023
 */
const validateQuestion = () => {
    let isValid = true;
    
    // Validate chung cho các câu hỏi
    if (!dataQuestion.QuestionContent) {
        isValid = false;
        openPopupError(Resource.PopupMessage.NotQuestion);
    }

    // Validate riêng từng form câu hỏi
    var checkSelect = false;
    var checkFill = true;
    var arrError = [];
    switch (Number(typeQuestion.value)) {
        case Enum.FormQuestion.Select:
            for (let i = 0; i < dataAnswerSelect.length; i++) {
                if ((dataAnswerSelect[i].AnswerContent || dataAnswerSelect[i].AnswerImage )
                    && dataAnswerSelect[i].AnswerStatus == Enum.AnswerStatus.True) {
                        checkSelect = true;
                } else if (!dataAnswerSelect[i].AnswerContent && !dataAnswerSelect[i].AnswerImage ) {
                    arrError.push(i);
                }
            }

            if (!checkSelect) {
                openPopupError(Resource.PopupMessage.NotAnswerCheck);
                isValid = false;
            } else {
                for (let i = 0; i < arrError.length; i++) {
                    delete dataAnswerSelect[arrError[i]];
                }
            }
            break;
        case Enum.FormQuestion.YesOrNo: 
            if (dataAnswerYesNo[0].AnswerContent && dataAnswerYesNo[1].AnswerContent) {
                if (dataAnswerYesNo[0].AnswerStatus == Enum.AnswerStatus.False &&
                    dataAnswerYesNo[1].AnswerStatus == Enum.AnswerStatus.False) {
                        isValid = false;
                        openPopupError(Resource.PopupMessage.NotAnswerCheck);
                    }
            } else {
                openPopupError(Resource.PopupMessage.NotAnswerCheck);
                isValid = false;
            }

            break;
        case Enum.FormQuestion.Fill: 
            for (let i = 0; i < dataAnswerFill.length; i++) {
                if (!dataAnswerFill[i].AnswerContent) {
                        checkFill = false;
                        break;
                } 
            }
            if (!checkFill) {
                openPopupError(Resource.PopupMessage.NotAnswerFill);
                isValid = false;
            }
            break;
        case Enum.FormQuestion.Essay: 
            break;
        default:
            break;
    }

    return isValid;
}

/**
 * Thêm câu hỏi và đóng form
 */
const saveClose = async () => {
    let dataAll = {
        Exercise: {},
        Question: {},
        Answers: [],
        TopicIDs: []
    }
    const id = route.query.id;
    if (validateQuestion()) {
        // Thêm các trường còn thiếu cho câu hỏi
        dataQuestion = {...dataQuestion, TypeQuestion: Number(typeQuestion.value)};
        dataQuestion = {...dataQuestion, SortOder: numberQuestion.value};

        // Thêm bài tập nếu ở form thêm
        if (!id) {
            dataQuestion = {...dataQuestion, ExerciseID: exerciseID.value};
            for (let prop in dataQuestion) {
                if (!dataQuestion[prop]) {
                    delete dataQuestion[prop];
                }
            }
        } else {
            for (let prop in dataQuestion) {
                if (!dataQuestion[prop]) {
                    delete dataQuestion[prop];
                }
            }
        }
        // Thêm đáp án cho câu hỏi
        if (dataQuestion.TypeQuestion != Enum.FormQuestion.Essay) {
            let arrayAnswer = [];
            switch (dataQuestion.TypeQuestion) {
                // Câu hỏi chọn đáp án
                case Enum.FormQuestion.Select:
                    for (let prop in dataAnswerSelect) {
                        for (let i in dataAnswerSelect[prop]) {
                            if (!dataAnswerSelect[prop][i]) {
                                delete dataAnswerSelect[prop][i];
                            }
                        }
                        arrayAnswer.push(dataAnswerSelect[prop]);
                    }
                    break;
                case Enum.FormQuestion.YesOrNo:
                    // Câu hỏi đúng sai
                    for (let prop in dataAnswerYesNo) {
                        for (let i in dataAnswerYesNo[prop]) {
                            if (!dataAnswerYesNo[prop][i]) {
                                delete dataAnswerYesNo[prop][i];
                            }
                        }
                        arrayAnswer.push(dataAnswerYesNo[prop]);
                    }
                    break;
                case Enum.FormQuestion.Fill: 
                    // Câu hỏi điền vào ô trống
                    for (let prop in dataAnswerFill) {
                        arrayAnswer.push(dataAnswerFill[prop]);
                    }
                    break;
                default:
                    break;
            }
            if (arrayAnswer) {
                dataAll.Answers = arrayAnswer;
            }
        }

        dataAll.Exercise = props.dataExercise;
        dataAll.Question = dataQuestion;
        if (props.dataExercise.Topics) {
            dataAll.TopicIDs = Object.values(props.dataExercise.Topics);
        }
        if (formModeQuestion.value == Enum.FormModeQuestion.Edit) {
            await store.dispatch("putMultipleData", dataAll);
            store.dispatch("getAllByID", id);
        } else if (formModeQuestion.value == Enum.FormModeQuestion.Add) {
            await store.dispatch("postMultipleData", dataAll);
            store.dispatch("getAllByID", exerciseID.value);
            let id = exerciseID.value;
            // Đổi route về form bài tập đã có câu hỏi
            router.push({ path: "/storage/create", query: {id} });
        }
        // Đóng form
        store.dispatch("showFormQuestion", false);
    }
}

const resetForm = () => {

}

/**
 * Mở popup lỗi khi validate
 * VMHieu 02/06/2023
 */
const openPopupError = (msg) => {
    store.dispatch("showPopup", true);
    store.dispatch("updatePopupStatus", Enum.PopupStatus.Error);
    store.dispatch("updatePopupMsg", msg);
}

/**
 * Hiển thị toast
 */
const showToastWarning = (msg) => {
    store.dispatch("showToast", true);
    store.dispatch("updateToastStatus", Enum.ToastStatus.Warning);
    store.dispatch("updateToastMsg", msg);

    setTimeout(() => {
        store.dispatch("showToast", false);
    }, 2000);
}

/** 
 * Xem sự thay đổi của trạng thái form
 * CreatedBy VMHieu 22/05/2023
 */
watch((typeQuestion), () => {
    if (typeQuestion.value == Enum.FormQuestion.Essay) {
        main.value.classList.add("fullscreen");
    } else {
        main.value.classList.remove("fullscreen");
    }
    store.dispatch("showFormQuestion", typeQuestion.value);
    dataQuestion = {...dataQuestion, QuestionNote: ""};
})
/** 
 * Xem sự thay đổi đóng mở form
 * CreatedBy VMHieu 22/05/2023
 */
watch((showFormQuestion), () => {
    nextTick(() => {
        if (showFormQuestion.value == Enum.FormQuestion.Essay) {
            main.value.classList.add("fullscreen");
        } else if (showFormQuestion.value != 0){
            main.value.classList.remove("fullscreen");
        }
    })

    // Gán giá trị cho data câu hỏi và đáp án nếu ở form sửa
    if (showFormQuestion.value && formModeQuestion.value == Enum.FormModeQuestion.Edit) {
        Object.assign(dataQuestion, question.value);

        switch (question.value.TypeQuestion) {
            case Enum.FormQuestion.Select:
                dataAnswerSelect.length = 0;
                for (let i = 0; i < question.value.Answer.length; i++) {
                    dataAnswerSelect.push(question.value.Answer[i]);
                }
                break;
            case Enum.FormQuestion.YesOrNo:
                dataAnswerYesNo.length = 0;
                for (let i = 0; i < question.value.Answer.length; i++) {
                    dataAnswerYesNo.push(question.value.Answer[i]);
                }
                break;
            case Enum.FormQuestion.Fill:
                dataAnswerFill.length = 0;
                for (let i = 0; i < question.value.Answer.length; i++) {
                    dataAnswerFill.push(question.value.Answer[i]);
                }
                break;
            default:
                break;
        }
    } 

    if (!showFormQuestion.value) {
        resetForm();
    }
})

</script>

<style scoped>
.form-question{
    position: fixed;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    z-index: 2000;
    height: 100%;
    background-color: rgba(0,0,0,.5);
    overflow: auto;
}

.form-container{
    margin: 24px auto 0 auto;
    width: 80vw;
    min-width: 800px;
    border-radius: 10px;
    box-shadow: 0 0 20px rgba(0,0,0,.16);
    height: calc(100vh - 104px);
    background-color: #fff2ab;
    position: relative;
    background: #fff;
    border-radius: 10px;
    box-sizing: border-box;
}

.form-body{
    height: 100%;
    overflow: auto;
    border-radius: 10px;
}

.form-body__header{
    height: 40px;
    background-color: #fff2ab;
    border-radius: 10px 10px 0 0;
    justify-content: space-between;
    position: fixed;
    z-index: 5;
}

.form-body__main{
    height: auto;
    background-color: #fff2ab;
    border-radius: 10px 10px 0 0;
}

.form-body__main>div{
    height: 100%;
}

.question-content{
    margin: 50px 0 10px 0;
    background-color: #fff2ab;
    width: 100%;
    height: 100%;
    border: none;
    outline: none;
    padding: 0 24px;
    font-size: 16px;
    color: #4e5b6a;
    box-sizing: border-box;
    resize: none;
    font-family: GoogleSans;
}

.question-content::placeholder{
    font-size: 16px;
    opacity: 0.4;
}

.question-content:focus, .question-content:focus-visible{
    border: none;
    outline: none;
}

.form-footer{
    position: absolute;
    bottom: -60px;
    right: 0;
    width: 100%;
    justify-content: space-between;
}

.footer-right>button:last-child{
    margin-right: 0;
}

.choose-question-type{
    background-color: #f8e373;
    padding-left: 12px;
    border-radius: 10px 0 10px 0;
    font-weight: 700;
    font-size: 16px;
    line-height: 24px;
}

.form-body__answer{
    padding: 0 1.5rem 1.5rem 1.5rem;
    margin-top: 66px;
}

.list-answer{
    display: grid;
    grid-template-columns: repeat(4,minmax(0,1fr));
    gap: 1rem;
}

.answer-fill{
    width: 100%;
}

.fullscreen{
    height: 100%;
}

.fullscreen>.question-content{
    height: calc(100% - 64px);
}

.add-note{
    position: fixed;
    height: 160px;
    top: calc(50% - 80px);
    overflow: hidden;
    right: 0;
    border-radius: 10px 0 0 10px;
    padding-left: 1.25rem;
    padding-right: 1.25rem;
    flex-direction: column;
    width: 6rem;
    box-sizing: border-box;
    background: transparent url(@/assets/img/bg-noteadd.svg) no-repeat;
}

.close-note{
    position: fixed;
    background-color: blue;
    height: 160px;
    top: calc(50% - 80px);
    overflow: hidden;
    left: 0;
    border-radius: 0 10px 10px 0 ;
    padding-left: 1.25rem;
    padding-right: 1.25rem;
    flex-direction: column;
    width: 6rem;
    box-sizing: border-box;
        background: transparent url(@/assets/img/bg-noteadd.svg) no-repeat;
}

.add-note:hover, .close-note:hover{
    cursor: pointer;
}

.note-img{
    margin: 1.75rem 0 0.75rem 0;
}

.note-text{
    font-size: 14px;
    line-height: 20px;
    text-align: center;
}

.form-note .form-body__header{
    background-color: #f8e373;
    border-radius: 10px 0 10px 0;
}

.form-note .choose-question-type{
    padding: 0 12px;
}

.form-note .form-footer{
    justify-content: flex-end;
}

.fill-number{
    width: 5rem;
    margin-right: .5rem;
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