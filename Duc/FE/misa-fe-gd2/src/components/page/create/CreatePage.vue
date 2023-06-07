<template>
    <div class="div-create">
        <CreateHeader :exercise="this.exercise"
            @setValueSubject="setValueSubject"
            @setValueGrade="setValueGrade"
            @setNameExercise="setNameExercise"
            @onClickAddInfor="onClickAddInfor"
            @onClickComplete="onClickSaveExercise"/>
        <!-- phần body khi chưa có câu hỏi-->
        <div class="div-body" v-if="questionList.length == 0">
            <div class="div-import">
                <p class="title">Tách câu hỏi tự động sử dụng công nghệ AI</p>
                <div class="import">
                    <img src="../../../assets/img/import.svg" />
                    <div class="text">Nhấn để tải lên file bài tập hoặc kéo thả file vào đây.
                        <br />File có định dạng xls, xlsx, doc, docx, pdf
                    </div>
                    <div class="btn-import">
                        <MSButton title="Tải lên file bài tập" class="btn-dowload-file" />
                        <MSButton title="Tải file Excel mẫu" class="btn-dowload-excel" />
                    </div>
                </div>
            </div>
            <div class="div-question">
                <div class="title">hoặc tự tạo câu hỏi mới</div>
                <MSQuestionToolbar mode="horizontal" @onClickNewQuesion="onClickNewQuesion"/>
            </div>
        </div>

        <!-- phần body khi số câu hỏi > 0-->
        <div class="div-body div-list-question" v-if="questionList.length > 0">
            <div class="list-question div-flex-column">
                <MSQuestion v-for="(item, index) in questionList" 
                    :question="item"
                    @onRemoveQuestion="onRemoveQuestion(item.questionID)"
                    @onEditQuestion="onEditQuestion"
                    @onCopyQuestion="onCopyQuestion"/>
            </div>
            <MSQuestionToolbar mode="vertical" @onClickNewQuesion="onClickNewQuesion"/>
        </div>

        <!-- popup thêm câu hỏi -->
        <MSPopupAdd v-if="showPopupAdd" @onClosePopup="onClosePopup" 
            @onClickSaveQuestion="onClosePopup"
            @alertQuestionNull="alertQuestionNull" 
            @alertAnswerNull="alertAnswerNull"
            :data="this.questionSelected"
            :dataAnswers="this.answersSelected"
        />
        <!-- popup bổ sung thông tin -->
        <MSPopupAddInfor v-if="showPopupAddInfor" 
            @onClosePopup="onCloseAddInfor" 
            :exercise={...exercise} 
            @onSaveInfor="onSaveInfor"/>
    </div>
    <MSDialog v-if="showDialog" 
        :content="contentDialog"
        :type="typeDialog"
        @onClickOk="onClickOkRemove" 
        @onClickCancel="onClickCancelRemove"/>
	<MSOverlay v-if="showOverlay || showPopupAdd"/>
</template>

<script>
import MSButton from '../../button/MSButton.vue'
import MSCombobox from '../../input/MSCombobox.vue'
import MSButtonQuestion from '../../button/MSButtonQuestion.vue'
import MSPopupAdd from '../../../components/popup/MSPopupAdd.vue'
import MSPopupAddInfor from '../../../components/popup/MSPopupAddInfor.vue'
import MSQuestion from '../../card/MSQuestion.vue'
import MSDialog from '../../notify/MSDialog.vue'
import MSOverlay from '../../popup/MSOverlay.vue'
import CreateHeader from './CreateHeader.vue'
import MSQuestionToolbar from './MSQuestionToolbar.vue'
import { mapGetters } from 'vuex'
import { mapMutations } from 'vuex'
import { mapActions } from 'vuex'
export default {
    name: "CreatePage",
    props: [],
    components: {
        MSButton,
        MSDialog,
        MSOverlay,
        MSCombobox,
        MSButtonQuestion,
        MSPopupAdd,
        MSPopupAddInfor,
        MSQuestion,
        CreateHeader,
        MSQuestionToolbar
    },
    data() {
        const resource = window.Resource
        return {
            resource,
            showDialog: false,
            showOverlay: false,
            exercise: {},//biến lưu thông tin của bài tập đang chỉnh sửa
            showPopupAdd: false,//biến show popup thêm câu hỏi
            showPopupAddInfor: false,//biến show popup thêm thông tin cho bài tập
            modePage: '',//chế độ trang sửa hay thêm mới bài tập
            idQuestionSelected: '',//id câu hỏi đang chọn
            questionSelected: [],//câu hỏi đang chọn
            answersSelected: [],//đáp án của câu hỏi đang chọn
            contentDialog: '',//nội dung dialog
            typeDialog: '',//loại dialog
        }
    },
    computed: {
        ...mapGetters([
            'subjectList',
            'gradeList',
            'typePopupAdd',
            'subjectOptions',
            'gradeOptions',
            'questionList',
            'exerciseIDSelected',
            'exerciseSelected'
        ])
    },
    methods: {
        ...mapMutations([
            'setShowOverlay',
            'setTypePopupAdd',
            'setExerciseIDSelected',
            'setExerciseSelected',
            'setQuestionList',
            'setExerciseSubject',
            'setExerciseGrade',
            'setTopicExercise',
            'setNumQuestion'
        ]),
        ...mapActions([
            'getListSubject',
            'getListGrade',
            'getListQuestion',
            'getExerciseById',
            'addExercise',
            'editExercise',
            'addTopic',
            'getTopicExercise',
            'deleteQuestionById'
        ]),
        /**
         * click back về trang chủ
         * CreatedBy: Trịnh Huỳnh Đức (20-5-2023)
         */
        onClickBack() {
            this.$router.push("/course/personal")
        },
        /**
         * click tạo câu hỏi mới -> hiện popup tương ứng
         * CreatedBy: Trịnh Huỳnh Đức (20-5-2023)
         */
        onClickNewQuesion(typeQuestion) {
            //lấy id môn học
            for (const key in this.subjectList) {
                if (this.subjectList[key].subjectName == this.exercise.subjectName) {
                    this.exercise.subjectID = this.subjectList[key].subjectID
                }
            }
            //lấy id khối
            for (const key in this.gradeList) {
                if (this.gradeList[key].gradeName == this.exercise.gradeName) {
                    this.exercise.gradeID = this.gradeList[key].gradeID
                }
            }
            //gán state 
            this.exercise.exerciseState = 1
            //nếu tên chưa nhập thì lấy giá trị mặc định
            if(!this.exercise.exerciseName) 
                this.exercise.exerciseName = "Bài nháp "+this.exercise.subjectName+" - "+this.exercise.gradeName
            //set exercise truoc khi mo popup
            this.setExerciseSelected(this.exercise)
            //set loại popup 
            this.setTypePopupAdd(typeQuestion)
            //gán 
            this.showOverlay = true
            this.showPopupAdd = true
        },
        /**
         * bắt sự kiện emit đóng popup
         * CreatedBy: Trịnh Huỳnh Đức (21-5-2023)
         */
        onClosePopup() {
            this.showPopupAdd = false
            this.showOverlay = false
            //reset lại đối tượng câu hỏi đang sửa 
            this.questionSelected = []
            this.answersSelected = []
        },

        /**
         * bắt sự kiện click nút bổ sung thêm thông tin
         * CreatedBy: Trịnh Huỳnh Đức (22-5-2023)
         */
        onClickAddInfor() {
            //nếu chưa chọn môn và khối thì gán nó gtri mặc định
            if (!this.exercise.subjectName)
                this.exercise.subjectName = this.subjectOptions[0]
            if (!this.exercise.gradeName)
                this.exercise.gradeName = this.gradeOptions[0]
            //lấy id môn và khối cho việc tìm kiếm chủ đề
            //lấy id môn học
            for (const key in this.subjectList) {
                if (this.subjectList[key].subjectName == this.exercise.subjectName) {
                    this.exercise.subjectID = this.subjectList[key].subjectID
                }
            }
            //lấy id khối
            for (const key in this.gradeList) {
                if (this.gradeList[key].gradeName == this.exercise.gradeName) {
                    this.exercise.gradeID = this.gradeList[key].gradeID
                }
            }
            //mở popup add infor
            this.showPopupAddInfor = true
            this.showOverlay = true
        },
        /**
         * bắt sự kiện click đóng popup thêm thông tin
         * CreatedBy: Trịnh Huỳnh Đức (22-5-2023)
         */
        onCloseAddInfor() {
            this.showPopupAddInfor = false
            this.showOverlay = false
        },
        /**
         * sự kiện lưu câu hỏi
         * CreatedBy: Trịnh Huỳnh Đức (21-5-2023)
         */
        onSaveQuestion() {
            // this.numQuestion++
            this.showPopupAdd = false
            this.showOverlay = false
        },
        /**
         * sự kiên set giá trị môn học của combobox
         * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
         */
        setValueSubject(value) {
            this.exercise.subjectName = value
            this.setExerciseSelected(this.exercise)
        },
        /**
         * sự kiên set giá trị khối của combobox
         * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
         */
        setValueGrade(value) {
            this.exercise.gradeName = value
            this.setExerciseSelected(this.exercise)
        },
        /**
         * sự kiên set giá trị khối của combobox
         * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
         */
        setNameExercise() {
            this.setExerciseSelected(this.exercise)
        },
        /**
         * bắt sự kiện lưu bài tập
         * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
         */
        async onClickSaveExercise() {
            //nếu chưa điền tên thì mở form bổ sung thông tin
            if(!this.exercise.exerciseName) {
                this.showPopupAddInfor = true
                this.showOverlay = true
                return
            }
            //lấy các thông tin bài tập để lưu
            //lấy trạng thái
            this.exercise.exerciseState = 2
            //lấy id môn học
            for (const key in this.subjectList) {
                if (this.subjectList[key].subjectName == this.exercise.subjectName) {
                    this.exercise.subjectID = this.subjectList[key].subjectID
                }
            }
            //lấy id khối
            for (const key in this.gradeList) {
                if (this.gradeList[key].gradeName == this.exercise.gradeName) {
                    this.exercise.gradeID = this.gradeList[key].gradeID
                }
            }
            if(this.modePage == "add"){
                //gọi api thêm
                await this.addExercise(this.exercise)
                //thêm chủ đề 
                await this.addTopic()                                 
                //back về trang chủ
                this.onClickBack()             
            }
            else {
                //gọi api sửa bài tập
                this.editExercise(this.exercise)
                //gọi api thêm chủ đề
                this.addTopic()
                //back về trang chủ
                this.onClickBack()
            }             
        },
        /**
         * bắt sự kiện ấn lưu popup thêm thông tin
         * CreatedBy: Trịnh Huỳnh Đức (1-6-2023)
         * @param {*} value 
         */
        onSaveInfor(value) {
            //gán lại các giá trị của exercise
            this.exercise.exerciseName = value.exerciseName
            this.exercise.subjectName = value.subjectName
            this.exercise.gradeName = value.gradeName
            //lấy trạng thái
            this.exercise.exerciseState = 1
            //lấy id môn học
            for (const key in this.subjectList) {
                if (this.subjectList[key].subjectName == this.exercise.subjectName) {
                    this.exercise.subjectID = this.subjectList[key].subjectID
                }
            }
            //lấy id khối
            for (const key in this.gradeList) {
                if (this.gradeList[key].gradeName == this.exercise.gradeName) {
                    this.exercise.gradeID = this.gradeList[key].gradeID
                }
            }
            this.setExerciseSelected(this.exercise)
        },
        /**
         * bắt sự kiện xóa câu hỏi
         * CreatedBy: Trịnh Huỳnh Đức (2-6-2023)
         * @param {*} idQuestion 
         */
        onRemoveQuestion(idQuestion) {
            this.typeDialog = ''
            this.contentDialog = this.resource.DialogContent.removeQuestion
            //hiện dialog xác nhận xóa
            this.showOverlay = true
            this.showDialog = true
            this.idQuestionSelected = idQuestion
        },
        /**
         * đã xác nhận câu hỏi và xóa thôi
         * CreatedBy: Trịnh Huỳnh Đức (2-6-2023)
         */
        onClickOkRemove(){
            //đóng dialog
            this.showOverlay = false
            this.showDialog = false
            this.deleteQuestionById(this.idQuestionSelected)
        },
        /**
         * hủy xóa câu hỏi
         * CreatedBy: Trịnh Huỳnh Đức (2-6-2023)
         */
        onClickCancelRemove(){
            //đóng dialog
            this.showOverlay = false
            this.showDialog = false
        },
        /**
         * bắt sự kiện chỉnh sửa câu hỏi
         * CreatedBy: Trịnh Huỳnh Đức (2-6-2023)
         * @param {*} idQuestion 
         */
        onEditQuestion(data){
            this.questionSelected = data.question
            this.answersSelected = data.answers
            //set loại câu hỏi           
            this.setTypePopupAdd(this.resource.TypeQuestion[data.question.questionType])
            this.showOverlay = true
            this.showPopupAdd = true
        },
        /**
         * bắt sự kiện sao chép câu hỏi
         * CreatedBy: Trịnh Huỳnh Đức (7-6-2023)
         * @param {*} idQuestion 
         */
         onCopyQuestion(data){
            this.questionSelected = data.question
            this.answersSelected = data.answers
            //set loại câu hỏi           
            this.setTypePopupAdd(this.resource.TypeQuestion[data.question.questionType])
            this.showOverlay = true
            this.showPopupAdd = true
        },
        /**
         * bắt sự kiện hiện thông báo validate question
         * CreatedBy: Trịnh Huỳnh Đức (2-6-2023)
         */
        alertQuestionNull(){
            this.contentDialog = this.resource.DialogContent.questionNull
            this.typeDialog = 'alert'
            this.showOverlay = true
            this.showDialog = true
        },
        /**
        * bắt sự kiện hiện thông báo validate answer
        * CreatedBy: Trịnh Huỳnh Đức (2-6-2023)
        */
        alertAnswerNull() {
            this.contentDialog = this.resource.DialogContent.answerNull
            this.typeDialog = 'alert'
            this.showOverlay = true
            this.showDialog = true
        }
    },
    created() {
        //lấy danh sách môn
        this.getListSubject()
        //lấy danh sách khối
        this.getListGrade()
        //lấy ds câu hỏi và bài tập
        const exerciseID = this.$route.query.exerciseID
        if (exerciseID) {//nếu là sửa bài tập
            this.modePage="edit"
            //lấy bài tập đó
            this.getExerciseById(exerciseID).then(data => {
                this.exercise = data
            })
            //lấy câu hỏi
            this.getListQuestion(exerciseID)
            //lấy chủ đề của bài tập
            this.getTopicExercise(exerciseID)
        }   
        else {
            this.modePage="add"
            this.setNumQuestion(0)
            //nếu chưa chọn môn và khối thì gán nó gtri mặc định
            if (!this.exercise.subjectName)
                this.exercise.subjectName = this.subjectOptions[0]
            if (!this.exercise.gradeName)
                this.exercise.gradeName = this.gradeOptions[0]
        }
    },
    unmounted() {
        //reset lại bài tập đang chọn, câu hỏi, chủ đề
        this.setExerciseSelected([])
        this.setQuestionList([])
        this.setTopicExercise([])
        this.setExerciseIDSelected('')
    }
}
</script>
<style scoped>
.div-create {
    height: 100%;
    position: relative;
}
/* phần của body */
.div-body {
    height: calc(100% - 128px);
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    padding-bottom: 20px;
    box-sizing: border-box;
}

.div-import {
    padding-top: 24px;
    display: flex;
    flex-direction: column;
    align-items: center;
}
.text {
    line-height: 26px;
    text-align: center;
    color: #4e5b6a;
}
.title {
    font-weight: 700;
    margin-bottom: 24px;
    line-height: 26px;
    font-size: 18px;
    line-height: 26px;
    color: #4e5b6a;
    text-align: center;
}
.import {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-between;
    width: 840px;
    height: 224px;
    border: 2px dashed #b6b9ce;
    border-radius: 10px;
    padding: 24px 0;
    box-sizing: border-box;
}

.import>img {
    width: 64px;
    height: 64px;
}

.btn-import {
    display: flex;
    align-items: center;
}

.btn-dowload-file {
    margin-right: 12px;
    background-color: #ece7fe;
    color: #8a6bf6;
    border: none;
}

.btn-dowload-excel {
    color: #00c542;
    background-color: rgba(0, 197, 66, .2);
    border: none;
}

.div-question {
    display: flex;
    flex-direction: column;
    align-items: center;
}
.list-question {
    width: 100%;
    height: calc(100vh - 188px);
    padding: 0px 32px 32px 32px;
    margin: 0;
    margin-top: 36px;
    overflow-y: scroll;
    gap: 20px;
    box-sizing: border-box;
}

.div-list-question {
    margin: 0px 140px 0px 140px;
}
</style>
