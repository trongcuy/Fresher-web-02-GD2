<template>
    <div class="div-popup-add" :class="{ 'popup-essay': typePopupAdd == 'essay' || typePopupAdd == 'more-explane' }">
        <div class="popup-content" v-if="typePopupAdd != 'more-explane'">
            <!-- khối header trái -->
            <div class="header-left">
                <MSCombobox customClass="combobox-popup"
                    :listSelect="['Chọn đáp án', 'Đúng sai', 'Điền vào chỗ trống', 'Ghép nối', 'Câu hỏi nhóm', 'Tự luận']"
                    :defaultValue="this.resource.TitleQuestion[typePopupAdd]" @setDefaultValue="setTypeQuestion" />
                <div class="num-question">Câu {{ this.question.questionNumber }} - </div>
            </div>
            <div class="div-center" style="width: 44px;height: 39px;position: absolute;top: 0px;right: 0px;z-index: 3;"><img
                    src="../../assets/img/ic_help.svg" /></div>
            <ckeditor :editor="editor" v-model="question.questionContent" :config="editorConfig"></ckeditor>
            <p v-if="typePopupAdd == 'fill' && !question.questionContent" class="placeholder-fill">(Sau khi nhập câu hỏi và
                nhấn ra ngoài,
                phần mềm sẽ tự động nhận diện dấu cách, dấu 3 chấm, dấu gạch chân để tạo ô trống.)</p>
        </div>
        <!-- popup thêm lời giải -->
        <MSPopupAddExplane v-if="typePopupAdd == 'more-explane'" @setQuestionExplane="setQuestionExplane"
            :content="!this.question.questionExplane !== undefined ? this.question.questionExplane : ''"
            :numQuestion="this.question.questionNumber" />
        <!-- phần nội dung -->
        <component :is="currentBody" :answers="answers" @onClickRemoveAnswer="onClickRemoveAnswer"
            @onClickAddAnswer="onClickAddAnswer"></component>

    </div>
    <!-- phần button cuối popup -->
    <div class="popup-button popup-button-left" v-if="typePopupAdd == 'select'">
        <MSButton title="Thêm đáp án" @click="onClickAddAnswer" />
    </div>
    <div class="popup-button" v-if="typePopupAdd != 'more-explane'">
        <MSButton title="Hủy" @click="onClickClosePopup" />
        <MSButton title="Lưu và đóng" @click="onClickSave" />
        <MSButton id="btn-save-new" title="Lưu và thêm câu" />
    </div>
    <div class="popup-button" v-if="typePopupAdd == 'more-explane'">
        <MSButton title="Hủy" @click="onClickCancelExplane" />
        <MSButton title="Lưu" id="btn-save-new" @click="onClickReturnQuestion" />
    </div>

    <!-- nuts thêm lời giải -->
    <div class="more-explane" @click="onClickMoreExplane" v-if="showMoreExplane">
        <div>
            <img src="../../assets/img/icon_answer.svg" />
            <div>Thêm lời giải</div>
        </div>
    </div>
    <!-- nut quay lại câu hỏi -->
    <div class="more-explane return-question" @click="onClickReturnQuestion" v-if="!showMoreExplane">
        <div>
            <img src="../../assets/img/icon_back2.svg" />
            <div>Quay lại câu hỏi</div>
        </div>
    </div>
</template>

<script>
import MSCombobox from '../input/MSCombobox.vue'
import MSButton from '../button/MSButton.vue'
import MSPopupAddExplane from './MSPopupAddExplane.vue'
import MSBodyFill from './bodypopup/MSBodyFill.vue'
import MSBodySelect from './bodypopup/MSBodySelect.vue'
import MSBodyTrueFalse from './bodypopup/MSBodyTrueFalse.vue'
import { mapActions, mapGetters } from 'vuex'
import { mapMutations } from 'vuex'
export default {
    name: 'MSPopupAdd',
    components: {
        MSCombobox,
        MSButton,
        MSPopupAddExplane,
        MSBodyFill,
        MSBodySelect,
        MSBodyTrueFalse
    },
    props: {
        title: {
            type: String,
            default: ''
        },
        data: {
            type: Array,
            default: []
        },
        dataAnswers: {
            type: Array,
            default: []
        },
    },
    data() {
        const resource = window.Resource
        const enums = window.Enums
        return {
            resource,//resource,
            enums,
            currentBody: "MSBodySelect",
            showPopupAdd: false,//biến show popup thêm câu hỏi
            showMoreExplane: true,//biến hiện form thêm lời giải
            typeQuestionBefore: '',//lưu loại câu hỏi lúc thêm lời giải xong còn quay lại
            editor: ClassicEditor,
            editorConfig: {
                toolbar: ['bold', 'italic', 'underline', 'uploadImage', 'undo', 'redo', '|', '|'],
                placeholder: "Nhập câu hỏi tại đây..."
            },
            editorConfigExplane: {
                toolbar: ['bold', 'italic', 'underline', 'uploadImage', 'undo', 'redo', '|', '|'],
                placeholder: "Nhập lời giải tại đây..."
            },
            question: {},//lưu đối tượng câu hỏi 
            answers: [],//lưu đáp án
            formMode: 'add',//lưu chế độ form sửa hay thêm
        }
    },
    computed: {
        ...mapGetters(['typePopupAdd', 'numQuestion', 'exerciseIDSelected', 'exerciseSelected', 'topicExercise']),
    },
    methods: {
        ...mapMutations(['setTypePopupAdd']),
        ...mapActions(['addQuestion', 'updateQuestion', 'addExercise', 'addAnswer', 'insertAll']),
        /**
         * hàm bắt sự kiện đóng popup
         * CreatedBy: Trịnh Huỳnh Đức (21-5-2023)
         */
        onClickClosePopup() {
            this.$emit('onClosePopup')
        },

        /**
         * hàm bắt sự kiện thay đổi loại câu hỏi bằng combobox
         * CreatedBy: Trịnh Huỳnh Đức (21-5-2023)
         */
        setTypeQuestion(typeQuestion) {
            switch (typeQuestion) {
                case this.resource.TitleQuestion.select:
                    this.setTypePopupAdd('select')
                    this.currentBody = 'MSBodySelect'
                    break;
                case this.resource.TitleQuestion.truefalse:
                    this.setTypePopupAdd('truefalse')
                    this.currentBody = 'MSBodyTrueFalse'
                    break;
                case this.resource.TitleQuestion.fill:
                    this.setTypePopupAdd('fill')
                    this.currentBody = 'MSBodyFill'
                    break;
                case this.resource.TitleQuestion.match:
                    this.setTypePopupAdd('match')
                    this.currentBody = ''
                    break;
                case this.resource.TitleQuestion.group:
                    this.setTypePopupAdd('group')
                    this.currentBody = ''
                    break;
                case this.resource.TitleQuestion.essay:
                    this.setTypePopupAdd('essay')
                    this.currentBody = ''
                    break;
                default:
                    break;
            }
        },
        /**
         * sự kiện click nút thêm lời giải
         * CreatedBy: Trịnh Huỳnh Đức (21-5-2023)
         */
        onClickMoreExplane() {
            this.typeQuestionBefore = this.typePopupAdd
            this.setTypePopupAdd('more-explane')
            this.showMoreExplane = false
        },
        /**
         * sự kiện click nút quay lại câu hỏi
         * CreatedBy: Trịnh Huỳnh Đức (21-5-2023)
         */
        onClickReturnQuestion() {
            this.setTypePopupAdd(this.typeQuestionBefore)
            this.showMoreExplane = true
        },
        /**
         * bắt sự kiện set lời giải cho câu hỏi
         * CreatedBy: Trịnh Huỳnh Đức (2-6-2023)
         * @param {*} value 
         */
        setQuestionExplane(value) {
            this.question.questionExplane = value
        },
        /**
         * bắt sự kiện hủy thêm lời giải
         * CreatedBy: Trịnh Huỳnh Đức (2-6-2023)
         */
        onClickCancelExplane() {
            this.question.questionExplane = ''
            this.onClickReturnQuestion()
        },
        /**
         * validate câu hỏi
         * CreatedBy: Trịnh Huỳnh Đức (2-6-2023)
         */
        validateQuestion() {
            if (!this.question.questionContent) {
                this.$emit('alertQuestionNull')
            }
        },
        /**
         * validate câu hỏi
         * CreatedBy: Trịnh Huỳnh Đức (2-6-2023)
         */
        validateQuestion() {
            if (!this.question.questionContent) {
                this.$emit('alertQuestionNull')
                return false
            }
            return true
        },
        /**
         * validate đáp án
         * CreatedBy: Trịnh Huỳnh Đức (2-6-2023)
         */
        validateAnswer() {
            //đếm số đáp án đúng và có nội dung
            let answerTrue = this.answers.filter(answer => answer.answerState == 1 && (answer.answerContent || answer.answerImage)).length
            if ((this.typePopupAdd == 'select') && answerTrue == 0) {
                this.$emit('alertAnswerNull')
                return false
            }
            return true
        },
        /**
         * sự kiện click nút lưu câu hỏi
         * CreatedBy: Trịnh Huỳnh Đức (2-6-2023)
         */
        onClickSave() {
            //validate tên câu hỏi và đáp án
            if (!this.validateQuestion() || !this.validateAnswer()) {
                return
            }
            //lấy loại câu hỏi
            this.question.questionType = this.enums.TypeQuestion[this.typePopupAdd]
            //lấy id bài tập
            this.question.exerciseID = this.exerciseIDSelected
            //nếu chưa tạo bài tập thì gọi api insertAll
            if (!this.question.exerciseID) {
                //lấy các id của chủ đề
                let topicIDs = ""
                for (const key in this.topicExercise) {
                    if (topicIDs)
                        topicIDs = topicIDs + ","
                    topicIDs = topicIDs + this.topicExercise[key].topicID
                }
                //đổi thứ tự câu hỏi sang string
                this.question.questionNumber = this.question.questionNumber.toString()
                //xóa id bài tập trong câu hỏi ko là lỗi do ko chuyển đc string sang Guid
                delete this.question.exerciseID
                for (let i = 0; i < this.answers.length; i++) {
                    delete this.answers[i].questionID
                }
                const data = {
                    exercise: this.exerciseSelected,
                    question: this.question,
                    answer: this.answers.filter(answer => answer.answerContent || (answer.answerImage!= '00000000-0000-0000-0000-000000000000'&&answer.answerImage)),
                    topicIDs: topicIDs
                }
                this.insertAll(data).then(data => {
                    this.$router.push({ path: "/course/create", query: { exerciseID: data } })
                })
            }
            //đã tạo bài tập
            else {
                //nếu là thêm câu hỏi
                if (this.formMode == 'add') {
                    this.addQuestion(this.question).then(data => {
                        //thay đổi đáp án
                        this.addAnswer({  answers: this.answers.filter(answer => answer.answerContent || answer.answerImage != '00000000-0000-0000-0000-000000000000'), questionID: data })
                    })
                }
                //nếu là sửa câu hỏi
                else {
                    this.updateQuestion(this.question)
                    //thay đổi đáp án
                    this.addAnswer({
                        answers: this.answers.filter(answer => answer.answerContent || answer.answerImage != '00000000-0000-0000-0000-000000000000'),
                        questionID: this.question.questionID
                    })
                }               
            }
            //đóng popup
            this.$emit('onClickSaveQuestion')
        },
        /**
         * sự kiện click thêm đáp án
         * CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
         */
        onClickAddAnswer() {
            this.answers.push({ questionID: this.question.questionID })
        },
        /**
         * sự kiện click xóa đáp án
         * CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
         */
        onClickRemoveAnswer(index) {
            debugger
            this.answers.splice(index, 1)
        }
    },
    created() {
        //nếu sửa câu hỏi
        if (this.data.questionContent !== undefined && this.data.questionID !== undefined) {
            this.formMode = 'edit'
            this.question = this.data
            this.answers = [...this.dataAnswers]
            this.setTypePopupAdd(this.resource.TypeQuestion[this.question.questionType])
        }
        //nếu  sao chép câu hỏi
        else if(this.data.questionContent !== undefined ) {
            this.formMode = 'add'
            this.question = this.data
            this.question.questionNumber = this.numQuestion + 1
            this.answers = [...this.dataAnswers]
            this.setTypePopupAdd(this.resource.TypeQuestion[this.question.questionType])
        }
        //nếu thêm mới
        else {
            this.formMode = 'add'
            this.question.questionNumber = this.numQuestion + 1
            this.answers = []
        }
        if (this.answers.length == 0) {
            //gán giá trị đáp án mắcj định
            if (this.typePopupAdd == 'select') {
                for (let i = 1; i <= 4; i++) {
                    this.answers.push({ questionID: this.question.questionID })
                }
            }
            if (this.typePopupAdd == 'truefalse') {
                this.answers.push({ questionID: this.question.questionID, answerContent: "Đúng", answerState: 1 })
                this.answers.push({ questionID: this.question.questionID, answerContent: "Sai", answerState: 2 })
            }
            if (this.typePopupAdd == 'fill') {
                this.answers.push({ answerState: 1 })
            }
        }
        //set loại body của form 
        switch (this.typePopupAdd) {
            case 'select':
                this.currentBody = 'MSBodySelect'
                break;
            case 'truefalse':
                this.currentBody = 'MSBodyTrueFalse'
                break;
            case 'fill':
                this.currentBody = 'MSBodyFill'
                break;
            default:
                this.currentBody = ''
                break;
        }
    }
}
</script>

<style lang="scss" scoped>
.div-popup-add {
    background-color: #ffffff;
    border-radius: 10px;
    width: calc(72vw);
    height: calc(83vh);
    overflow: auto;
    left: calc(14vw);
    top: 24px;
    position: absolute;
    z-index: 102;
}

.more-explane {
    width: 96px;
    height: 160px;
    padding: 0 20px;
    right: 0px;
    top: calc(50vh - 80px);
    position: absolute;
    z-index: 103;
    cursor: pointer;
    background-color: #fff2ab;
    border-radius: 10px 0px 0px 10px;
}

.more-explane>div {
    text-align: center;
    display: flex;
    flex-direction: column;
    align-items: center;
    color: #4e5b6a;
}

.more-explane img {
    width: 48px;
    height: 42px;
    margin-top: 28px;
    margin-bottom: 12px;
}

.return-question {
    left: 0px;
    border-radius: 10px;
}

.popup-content {
    background-color: #fff2ab;
    width: 100%;
    min-height: 48%;
    height: auto;
    box-sizing: border-box;
    position: relative;
}

.popup-content .header-left {
    position: absolute;
    top: 0px;
    left: 0px;
    z-index: 103;
}

.popup-header {
    display: flex;
    align-items: center;
    justify-content: space-between;
    height: 40px;
    position: relative;
}

.num-question {
    position: absolute;
    top: 10px;
    left: 12px;
    width: fit-content;
    font-weight: 700;
    font-size: 16px;
    color: #606266;
    background-color: #f8e373;
}

.list-edit {
    display: flex;
    align-items: center;
}

.list-edit>div {
    width: 30px;
    height: 30px;
    display: flex;
    align-items: center;
    justify-content: center;
}

.list-edit div:not(:last-child) img {
    width: 16px;
    height: 16px;
}

#list-edit__line {
    margin: 0px;
    width: min-content;
}

.input-question {
    font-size: 16px;
    color: #4e5b6a;
    border: none;
    outline: none;
    margin: 10px 0px 0px 0px;
    padding-left: 24px;
    padding-right: 24px;
    background-color: inherit;
    resize: none;
    width: 100%;
    height: calc(100% - 40px - 12px);
    box-sizing: border-box;
    white-space: pre-wrap;
    /* Xuống dòng khi gặp ký tự xuống dòng (\n) */
}

.popup-button {
    display: flex;
    align-items: center;
    justify-content: end;
    gap: 12px;
    height: 40px;
    position: absolute;
    right: calc(14vw);
    top: calc(83vh + 48px);
    z-index: 103;
}

.popup-button-left {
    width: min-content;
    left: calc(14vw);
}

#btn-save-new {
    border: none;
    background-color: #8a6bf6;
    color: #fff;
}

/* xử lý css riêng từng loại kiểu câu hỏi khác nhau */
/* câu tự luận */
.popup-essay {
    background-color: #fff2ab;
}

.popup-essay .popup-content {
    height: 89%;
}

.popup-essay .input-question {
    height: 100%;
}

.popup-essay .popup-body {
    display: none;
}

.popup-body {
    padding: 1rem 24px;
    height: 52%;
    width: 100%;
    overflow-y: scroll;
    display: grid;
    grid-template-columns: repeat(4, 1fr);
    /* 4 cột có tỷ lệ bằng nhau */
    justify-content: center;
    justify-items: center;
    gap: 1rem;
}

.popup-body-rightwrong {
    padding: 1rem 24px;
    height: 52%;
    width: 100%;
    display: flex;
    justify-content: center;
    gap: 1rem;
}

.popup-body-fill {
    padding: 1rem 24px;
    height: 52%;
    width: 100%;
    gap: 1rem;
    overflow-y: scroll;
}

.popup-body-fill .div-button {
    justify-content: center;
    height: 40px;
    width: calc(100% - 88px);
    margin-left: 88px;
    border: 1px dashed rgb(182, 185, 206);
    border-radius: 10px;
    background-color: rgb(241, 242, 247);
    cursor: pointer;
    color: rgba(78, 91, 106, 0.7);
}

.div-button img {
    margin-right: 12px;
    width: 12px;
    height: 12px;
}

.placeholder-fill {
    padding: 0px 24px;
    margin-top: -4px;
    margin-top: -12px;
    position: relative;
    z-index: 102;
}
</style>
