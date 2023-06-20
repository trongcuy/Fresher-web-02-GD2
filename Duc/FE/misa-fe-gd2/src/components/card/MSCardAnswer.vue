<template>
    <div class="card"
        :class="{ 'card-rightwrong': typePopupAdd == 'truefalse', 'card-pink': answer % 4 == 0, 'card-blue': answer % 4 == 1, 'card-orange': answer % 4 == 2, 'card-purple': answer % 4 == 3 }">
        <div class="head" :class="{ 'head-right-check': typePopupAdd == 'truefalse' }">
            <div class="head-left" v-if="typePopupAdd == 'select'">{{ this.indexToLetter(answer) }}</div>
            <div class="head-right">
                <div class="zoom-div" v-if="typePopupAdd == 'select'" @click="onClickRemove"><img
                        src="../../assets/img/icon_delete.9097d258.svg" />
                </div>
                <div class="zoom-div" v-if="typePopupAdd == 'select'" @click="onClickAddImage"><img
                        src="../../assets/img/icon_image.svg" /></div>
                <div class="zoom-div" @click="onClickChangeState" :class="{ 'answer-true': stateAnswer }">
                    <img v-if="!stateAnswer" src="../../assets/img/ic_uncheck.ceabec80.svg" />
                    <img v-if="stateAnswer" src="../../assets/img/ic_check.true.svg" />
                </div>
            </div>
        </div>
        <div class="body" :class="{ 'body-boder': isEditting, 'remove-decor': !isEditting }" v-click-outside="onBlurEditor">
            <textarea  v-show="!isEditting && !editorData" @click="onClickEdit"  @focus="this.onFocusInput()" v-model="textAreaData">{{ placeholder }}</textarea>
            <div class="div-editor" :class="{ 'div-hidden': !(typePopupAdd == 'select' && isEditting) && !editorData }"
                @click="onClickEdit" @keydown.tab="onKeyTab">
                <ckeditor ref="editorRef" :editor="editor" v-model="editorData" :config="editorConfig"></ckeditor>
            </div>
            <!-- ảnh đáp án nếu có -->
            <div v-if="urlAnswer" class="img-answer div-center">
                <img :src="urlAnswer" @click="onShowImage(urlAnswer)" />
                <img src="../../assets/img/ic_close.svg" @click="onRemoveAnswerImage" />
            </div>
        </div>
    </div>
</template>

<script>
import { mapGetters, mapMutations } from 'vuex'
export default {
    name: 'MSCardAnswer',
    props: {
        title: {
            type: String,
            default: ''
        },
        placeholder: {
            type: String,
            default: ''
        },
        answer: {
            type: String,
            default: 'A'
        },
        state: {
            type: String,
            default: '2'
        },
        image: {
            type: String,
            default: ''
        }
    },
    watch: {
        editorData(newValue) {
            this.$emit('setValueAnswer', newValue)
        },
        stateAnswer(newValue) {
            if (this.typePopupAdd == 'select') {
                const value = newValue ? 1 : 2
                this.$emit('onClickChangeState', value)
            }
        },
        state() {
            if (this.typePopupAdd == 'truefalse') {
                this.stateAnswer = this.state == this.enums.answerState.true
            }
        },
        textAreaData(newValue) {
            if(this.typePopupAdd == 'truefalse'){
                this.$emit('setValueAnswer', newValue)
            }
            
        }
    },
    data() {
        const enums = window.Enums
        return {
            enums,
            editorData: '',//gía trị của ckeditor
            textAreaData: '',//giá trị textarea
            editor: ClassicEditor,
            editorConfig: {
                toolbar: ['bold', 'italic', 'Underline']
            },
            isEditting: false,//dang hien editor,
            stateAnswer: true,//trạng thái câu trả lời đúng hay sai
            isFocused: false
        }
    },
    computed: {
        ...mapGetters(['typePopupAdd']),
        urlAnswer() {
            return this.buildImage(this.image)
        }
    },
    methods: {
        ...mapMutations(['setUrlImageShow']),
        /**
         * hàm chuyển từ index sang chữ cái tương ứng
         * CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
         * @param {*} index 
         */
        indexToLetter(index) {
            const letterCode = 65 + index;
            return String.fromCharCode(letterCode);
        },
        /**
         * bắt sự kiện xóa câu hỏi
         * CreatedBy: Trịnh Huỳnh Đức (23-5-2023)
         */
        onClickRemove() {
            this.$emit('onClickRemove')
        },
        /**
         * bắt sự kiện click edit, nếu popup là select thì mới mở editor
         * CreatedBy: Trịnh Huỳnh Đức (24-5-2023)
         */
        onClickEdit() {
            if (this.typePopupAdd == 'select')
                this.isEditting = true

            this.$nextTick(() => {
                this.$el.querySelector('.ck-editor__editable').focus()

            })
        },
        /**
         * bắt sự kiện blur khỏi edit
         * CreatedBy: Trịnh Huỳnh Đức (24-5-2023)
         */
        onBlurEditor() {
            this.isEditting = false
        },
        /**
         * thay đổi trạng thái đáp án
         * CreatedBy: Trịnh Huỳnh Đức (4-6-2023)
         */
        onClickChangeState() {
            this.stateAnswer = !this.stateAnswer
            this.$emit('onClickChangeState')
        },
        /**
         * bắt sự kiện thêm ảnh
         * CreatedBy: Trịnh Huỳnh Đức (10-6-2023)
         */
        onClickAddImage() {
            this.$emit('onClickAddImage')
        },

        /**
         * bắt sự kiện xóa ảnh
         * CreatedBy: Trịnh Huỳnh Đức (12-6-2023)
         */
        onRemoveAnswerImage() {
            this.$emit('onClickRemoveImage')
        },
        /**
         * hàm bắt sự kiện click vào ảnh
         * CreatedBy: Trịnh Huỳnh Đức (14-6-2023)
         */
        onShowImage(imgQuestion) {
            this.setUrlImageShow(imgQuestion)
        },
        /**
         * bắt sự kiện tab vào input
         * CreatedBy: Trịnh Huỳnh Đức (12-6-2023)
         */
        onFocusInput(){
            this.onClickEdit()
        },
        /**
         * bắt sự kiện xóa ảnh
         * CreatedBy: Trịnh Huỳnh Đức (12-6-2023)
         */
        onKeyTab(){
            this.onBlurEditor()
        }
    },
    created() {
        this.editorData = this.title
        this.stateAnswer = this.state == this.enums.answerState.true
        this.textAreaData = this.placeholder
    },
}
</script>

<style scoped>
.zoom-div {
    cursor: pointer;
}

.div-hidden {
    display: none;
}

.card {
    width: 100%;
    height: 180px;
    box-sizing: border-box;
    background-color: #fff;
    border-radius: 10px;
    background-color: rgb(255, 214, 240);
    position: relative;
}

.card-rightwrong {
    width: 25%;
    min-height: 88px;
}

.card-rightwrong textarea {
    padding-top: 4px;
    font-size: 24px;
    color: #4e5b6a;
}

.card-pink {
    background-color: rgb(255, 214, 240);
}

.card-blue {
    background-color: #ACEBF1;
}

.card-orange {
    background-color: #FCD0C6;
}

.card-purple {
    background-color: #C4E5FF;
}

img {
    height: 18px;
    width: 18px;
}

.head {
    height: 48px;
    padding: 8px;
    display: flex;
    align-items: center;
    justify-content: space-between;
}

.head-left {
    width: 15px;
    height: 28px;
    margin: 4px 0px 0px 4px;
    font-size: 20px;
    line-height: 28px;
    font-weight: 700;
    color: #4E5B6A;
}

.head-right {
    display: flex;
    align-items: center;
}

.head-right>div {
    width: 32px;
    height: 32px;
    background-color: #fff;
    margin-left: 8px;
    border-radius: 10px;
    display: flex;
    align-items: center;
    justify-content: center;
}

.head-right-check {
    justify-content: end;
}

.body {
    position: absolute;
    box-sizing: border-box;
    /*kỹ thuật tràn toàn bộ cha*/
    top: 48px;
    left: 8px;
    right: 8px;
    bottom: 8px;
    border-radius: 10px;
    display: flex;
    justify-content: center;
}

.body-boder {
    border: 3px solid #ec8bc8;
    box-sizing: border-box;
}

.div-textarea {
    position: absolute;
    top: 0px;
    left: 0px;
}

textarea {
    color: #606266;
    resize: none;
    border: none;
    outline: none;
    line-height: 40px;
    height: 100px;
    text-align: center;
    overflow: hidden;
    background-color: inherit;
    font-weight: 700;
    font-size: 18px;
    margin-top: 25px;
}

.answer-true {
    background-color: #00C542 !important;
}

.img-answer {
    height: 95%;
    width: 100%;
    position: absolute;
    top: 0px;
    left: 0px;
    background-color: #fff;
    border-radius: 6px;
}

.img-answer img:first-child {
    width: 80%;
    height: 70%;
}

.img-answer img:nth-child(2) {
    width: 12px;
    height: 12px;
    position: absolute;
    top: 2px;
    right: 4px;
}

.div-editor {
    width: 100%;
    box-sizing: border-box;
}
</style>
