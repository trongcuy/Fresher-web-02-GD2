<template>
    <div class="div-main">
        <div class="header">
            <div class="header-title">Bổ sung thông tin</div>
            <div class="header-close div-center" @click="onClosePopup"><img src='../../assets/img/x.jpg' /></div>
        </div>
        <!-- body -->
        <div class="body">
            <div class="body-image">
                <p>Ảnh đại diện</p>
                <img src="../../assets/img/default.png" />
            </div>
            <div class="body-right div-flex-column">
                <MSInput title="Tên bài tập" defaultValue="Nhập tên bài tập..." :value="newExercise.exerciseName"
                    :require="true" @setValueInput="setExerciseName" />
                <div class="div-flex-row div-select">
                    <MSCombobox title="Môn" :require="true" :defaultValue="newExercise.subjectName"
                        :listSelect="subjectOptions" @setDefaultValue="setValueSubject" />
                    <MSCombobox title="Khối" :require="true" :defaultValue="newExercise.gradeName"
                        :listSelect="gradeOptions" @setDefaultValue="setValueGrade" />
                </div>

                <MSComboboxTag title="Chủ đề" defaultValue="Chọn chủ đề" 
                    valueField="topicID" 
                    labelField="topicName"
                    v-model="topicSelecteds" 
                    :data="topics" />
                <MSInput title="Thẻ tìm kiếm" class="input-search" />
            </div>
        </div>
        <!-- nút button lấy bên popupAdd-->
        <div class="div-button">
            <MSButton title="Hủy" @click="onClosePopup" />
            <MSButton title="Lưu" id="btn-save-new" @click="onClickSave" />
        </div>
    </div>
</template>

<script>
import MSInput from '../input/MSInput.vue'
import MSCombobox from '../input/MSCombobox.vue'
import MSButton from '../button/MSButton.vue'
import MSComboboxTag from '../input/MSComboboxTag.vue'
import { mapGetters, mapMutations } from 'vuex'
import { mapActions } from 'vuex'
export default {
    name: "MSPopupAddInfor",
    props: ['exercise'],
    components: {
        MSInput,
        MSCombobox,
        MSButton,
        MSComboboxTag
    },
    data() {
        return {
            newExercise: [],//lưu bài tập đang sửa
            topics: [],//danh sách chủ đề theo id lớp và môn
            topicSelecteds: [],//danh sách chủ đề đã chọn
        }
    },

    computed: {
        ...mapGetters([
            'subjectList',
            'gradeList',
            'subjectOptions',
            'gradeOptions',
            'topicExercise',//ds chủ đề đã chọn lưu trong state
        ])
    },
    methods: {
        ...mapMutations([
            'setTopicExercise'
        ]),
        ...mapActions([
            'getTopic',
            'getTopicExercise'
        ]),
        /**
         * bắt sự kiện đóng popup
         * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
         */
        onClosePopup() {
            this.$emit('onClosePopup')
        },
        /**
         * sự kiên set giá trị môn học của combobox
         * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
         */
        setValueSubject(value) {
            this.newExercise.subjectName = value
            //lấy id môn học được chọn gán vào bài tập đang chọn
            for (const key in this.subjectList) {
                if (this.subjectList[key].subjectName == value) {
                    this.newExercise.subjectID = this.subjectList[key].subjectID
                }
            }
            //load lại topic
            this.reloadTopic()
        },
        /**
         * sự kiên set giá trị khối của combobox
         * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
         */
        setValueGrade(value) {
            this.newExercise.gradeName = value
            //lâý id khối 
            for (const key in this.gradeList) {
                if (this.gradeList[key].gradeName == value) {
                    this.newExercise.gradeID = this.gradeList[key].gradeID
                }
            }
            //load lại topic
            this.reloadTopic()
        },
        /**
         * load lại chủ đề khi môn học hoặc khối thay đổi
         * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
         */
        reloadTopic() {
            //lấy thông tin môn và khối
            const data = {
                subjectID: this.newExercise.subjectID,
                gradeID: this.newExercise.gradeID
            }
            this.getTopic(data).then(topicData => {
                this.topics = topicData
            })
        },
        /**
         * bắt sự kiện lưu thông tin
         * CreatedBy: Trịnh Huỳnh Đức (1-6-2023)
         */
        onClickSave() {
            //lưu thông tin bài tập
            this.$emit('onSaveInfor', this.newExercise)
            //lưu thông tin chủ đề
            this.setTopicExercise(this.topicSelecteds)
            this.onClosePopup()
        },
        /**
         * bat su kien set value input exercise name
         * CreatedBy: Trịnh Huỳnh Đức (1-6-2023)
         * @param {*} value 
         */
        setExerciseName(value) {
            this.newExercise.exerciseName = value
        },
    },
    created() {
        this.newExercise = { ...this.exercise }
        //lấy các chủ đề theo môn và khối
        let data = {
            subjectID: '',
            gradeID: ''
        }
        if (this.exercise) {
            data = {
                subjectID: this.exercise.subjectID,
                gradeID: this.exercise.gradeID
            }
        }
        this.getTopic(data).then(topicData => {
            this.topics = topicData
        })
        //lấy chủ đề theo id bài tập  
        if(this.topicExercise.length > 0) {//nếu đã chọn trước đó thì lấy luôn mà ko gọi api
            this.topicSelecteds = this.topicExercise
        }  
        else {
            const exerciseID = this.$route.query.exerciseID
            if (exerciseID) {//nếu là sửa bài tập thì mới lấy chủ đề của bài tập đó
                this.getTopicExercise(exerciseID).then(data => {
                    this.topicSelecteds = data
                })
            }
        }       
    }
}
</script>

<style scoped>
.div-main {
    background-color: #ffffff;
    border-radius: 10px;
    width: 800px;
    height: auto;
    left: calc(50vw - 400px);
    top: 50px;
    position: absolute;
    z-index: 3;
}

.header {
    height: 84px;
    padding: 24px;
    position: relative;
}

.header-title {
    font-weight: 700;
    font-size: 28px;
    line-height: 36px;
    color: #4e5b6a;
}

.header-close {
    cursor: pointer;
    position: absolute;
    top: 8px;
    right: 8px;
    width: 32px;
    height: 32px;
}

.header-close img {
    width: 16px;
    height: 16px;
}

.body {
    display: flex;
    justify-content: space-between;
    padding: 0px 24px 24px 24px;
}

.body-image {
    width: 260px;
    height: 100%;
}

.body-image>p {
    padding-bottom: 4px;
    font-size: 14px;
    font-weight: 500;
}

.body-image img {
    height: auto;
    width: 100%;
}

.body-right {
    width: 452px;
    height: 100%;
    gap: 16px;
}

.div-select {
    gap: 12px;
}

.input-search img {
    height: 60px;
}

.div-button {
    display: flex;
    align-items: center;
    justify-content: end;
    padding: 0px 24px 24px 24px;
    gap: 12px;
}
</style>