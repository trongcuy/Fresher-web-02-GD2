<template>
    <div class="card" :class="{ 'card-rightwrong': typePopupAdd == 'truefalse' }">
        <div class="head" :class="{ 'head-right-check': typePopupAdd == 'truefalse' }">
            <div class="head-left" v-if="typePopupAdd == 'select'">{{ answer }}</div>
            <div class="head-right">
                <div class="zoom-div" v-if="typePopupAdd == 'select'" @click="onClickRemove"><img
                        src="../../assets/img/icon_delete.9097d258.svg" />
                </div>
                <div class="zoom-div" v-if="typePopupAdd == 'select'"><img src="../../assets/img/icon_image.svg" /></div>
                <div class="zoom-div" @click="onClickChangeState" :class="{'answer-true': stateAnswer}">
                    <img v-if="!stateAnswer" src="../../assets/img/ic_uncheck.ceabec80.svg"/>
                    <img v-if="stateAnswer" src="../../assets/img/ic_check.true.svg"/>
                </div>
            </div>
        </div>
        <div class="body" :class="{ 'body-boder': isEditting, 'remove-decor': !isEditting}" v-click-outside="onBlurEditor">
            <textarea v-show="!isEditting&& !editorData" @click="onClickEdit">{{ placeholder }}</textarea>
            <div v-if="(typePopupAdd == 'select' && isEditting)||editorData" style="width: 100%" @click="onClickEdit">
                <ckeditor :editor="editor" v-model="editorData" :config="editorConfig"></ckeditor>
            </div>
        </div>
    </div>
</template>

<script>
import { mapGetters } from 'vuex'
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
        }
    },
    watch: {
        editorData(newValue) {
            this.$emit('setValueAnswer', newValue)
        },
        stateAnswer(newValue) {
            if (this.typePopupAdd == 'select'){
                const value = newValue?1:2
                this.$emit('onClickChangeState', value)
            }
        },
        state() {
            if (this.typePopupAdd == 'truefalse'){
                this.stateAnswer = this.state==this.enums.AnswerState.True
            }
            
        },
    },
    data() {
        const enums = window.Enums
        return {
            enums,
            editorData: '',//gía trị của ckeditor
            editor: ClassicEditor,
            editorConfig: {
                toolbar: ['bold', 'italic', 'Underline', 'undo']
            },
            isEditting: false,//dang hien editor,
            stateAnswer: true,//trạng thái câu trả lời đúng hay sai
        }
    },
    computed: {
        ...mapGetters(['typePopupAdd'])
    },
    methods: {
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
        onClickChangeState(){
           this.stateAnswer=!this.stateAnswer
           this.$emit('onClickChangeState')
        }
    },
    created(){
        this.editorData = this.title
        this.stateAnswer = this.state==this.enums.AnswerState.True
    }
}
</script>

<style scoped>
.zoom-div {
    cursor: pointer;
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
    width: 197px;
}

.card-rightwrong textarea {
    font-size: 24px;
    color: #4e5b6a;
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
    margin-top: 20px;
} 
.answer-true {
    background-color: #00C542 !important;
}
</style>
