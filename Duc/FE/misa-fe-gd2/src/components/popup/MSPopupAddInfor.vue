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
                <img :src="this.urlImageExercise" id="img-avatar" />
                <div class="load-image">
                    <img src="../../assets/img/ic_image.svg" @click="convertToInput" />
                    <input ref="fileInput" type="file" accept=".png,.jpg,.jpeg,.bmp" @change="onUploadImage" />
                </div>

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

                <MSComboboxTag title="Chủ đề" defaultValue="Chọn chủ đề" valueField="topicID" labelField="topicName"
                    v-model="topicSelecteds" :data="topics" />
                <MSInput title="Thẻ tìm kiếm" class="input-search" />
            </div>
        </div>
        <!-- nút button lấy bên popupAdd-->
        <div class="div-button">
            <MSButton title="Đóng" @click="onClosePopup" />
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
            temporaryImgId: '',//lưu id ảnh tạm thời upload lên
        }
    },

    computed: {
        ...mapGetters([
            'subjectList',
            'gradeList',
            'subjectOptions',
            'gradeOptions',
            'topicExercise',//ds chủ đề đã chọn lưu trong state
            'imageExercise',
            'urlImageExercise'
        ])
    },
    methods: {
        ...mapMutations([
            'setTopicExercise',
            'setImageIdExercise',
            'setUrlImageExercise'
        ]),
        ...mapActions([
            'getTopic',
            'getTopicExercise',
            'uploadImage'
        ]),
        /**
         * bắt sự kiện đóng popup
         * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
         */
        onClosePopup() {
            this.setImageIdExercise('')
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
            //reset lại chủ đề đã chọn
            this.topicSelecteds = []
            this.setTopicExercise(this.topicSelecteds)
        },
        /**
         * bắt sự kiện lưu thông tin
         * CreatedBy: Trịnh Huỳnh Đức (1-6-2023)
         */
        onClickSave() {
            //nếu chưa nhập tên thì ko cho lưu
            if (!this.newExercise.exerciseName)
                return
            //lưu id ảnh 
            this.setImageIdExercise(this.temporaryImgId)
            //lấy lại ảnh về
            const urlImage = this.buildImage(this.temporaryImgId)
                this.setUrlImageExercise(urlImage)
            //lưu thông tin bài tập
            this.$emit('onSaveInfor', this.newExercise)
            //lưu thông tin chủ đề
            this.setTopicExercise(this.topicSelecteds)
            this.$emit('onClosePopup')
        },
        /**
         * bat su kien set value input exercise name
         * CreatedBy: Trịnh Huỳnh Đức (1-6-2023)
         * @param {*} value 
         */
        setExerciseName(value) {
            this.newExercise.exerciseName = value
        },
        /**
         * bat su kien click ảnh thì mở input file
         * CreatedBy: Trịnh Huỳnh Đức (1-6-2023)
         * @param {*} value 
         */
        convertToInput() {
            this.$refs.fileInput.click()
        },
        /**
         * bắt sự kiện load ảnh lên
         * CreatedBy: Trịnh Huỳnh Đức (7-6-2023)
         */
        onUploadImage(event) {
            var input = event.target
            var img = document.getElementById('img-avatar')
            //gán ảnh mới tải lên vào thẻ img
            if (input.files && input.files[0]) {
                var reader = new FileReader()
                reader.onload = function (e) {
                    img.src = e.target.result
                };
                reader.readAsDataURL(input.files[0])
            }
            //gán ảnh vào state
            const formData = new FormData()
            formData.append('image', input.files[0])
            //lưu ảnh rồi lấy id lưu tạm, khi click lưu mới lưu ảnh vào bài tập
            this.uploadImage(formData).then(data=> {
                this.temporaryImgId = data
            })
        }
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
        this.topicSelecteds = this.topicExercise
    }
}
</script>

<style scoped lang="scss">
.div-main {
    background-color: #ffffff;
    border-radius: 10px;
    width: 800px;
    height: auto;
    left: calc(50vw - 400px);
    top: 50px;
    position: absolute;
    z-index: 103;

    .header {
        height: 84px;
        padding: 24px;
        position: relative;

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

            img {
                width: 16px;
                height: 16px;
            }
        }

    }

    .body {
        display: flex;
        justify-content: space-between;
        padding: 0px 24px 24px 24px;

        .body-image {
            width: 260px;
            height: 100%;
            position: relative;

            p {
                padding-bottom: 4px;
                font-size: 14px;
                font-weight: 500;
            }

            &>img {
                height: 156px;
                width: 100%;
            }

            .load-image {
                height: 40px;
                width: 40px;
                position: absolute;
                right: 12px;
                bottom: 20px;

                img {
                    height: 40px;
                    width: 40px;
                }

                input {
                    display: none;
                }
            }
        }

        .body-right {
            width: 452px;
            height: 100%;
            gap: 16px;

            .div-select {
                gap: 12px;
            }

            .input-search img {
                height: 60px;
            }
        }
    }

    .div-button {
        display: flex;
        align-items: center;
        justify-content: end;
        padding: 0px 24px 24px 24px;
        gap: 12px;
    }
}
</style>