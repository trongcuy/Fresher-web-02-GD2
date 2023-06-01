<template>
    <div class="card" :class="{ 'card-rightwrong': typePopupAdd == 'rightwrong' }">
        <div class="head" :class="{ 'head-right-check': typePopupAdd == 'rightwrong' }">
            <div class="head-left" v-if="typePopupAdd == 'select'">{{ answer }}</div>
            <div class="head-right">
                <div class="zoom-div" v-if="typePopupAdd == 'select'" @click="onClickRemove"><img
                        src="../../assets/img/icon_delete.9097d258.svg" />
                </div>
                <div class="zoom-div" v-if="typePopupAdd == 'select'"><img src="../../assets/img/icon_image.svg" /></div>
                <div class="zoom-div" @click="()=>{stateAnswer=!stateAnswer}" :class="{'answer-true': stateAnswer}">
                    <img v-if="!stateAnswer" src="../../assets/img/ic_uncheck.ceabec80.svg"/>
                    <img v-if="stateAnswer" src="../../assets/img/ic_check.true.svg"/>
                </div>
            </div>
        </div>
        <div class="body" :class="{ 'body-boder': isEditting, 'remove-decor': !isEditting}" v-click-outside="onBlurEditor">
            <textarea v-show="!isEditting&& !editorData" @click="onClickEdit">Nhập đáp án...</textarea>
            <div v-if="(typePopupAdd == 'select' && isEditting)||editorData" style="width: 100%" @click="onClickEdit">
                <ckeditor :editor="editor" v-model="editorData" :config="editorConfig"></ckeditor>
            </div>
        </div>
    </div>
</template>

<script>
import { mapGetters } from 'vuex'
import { mapMutations } from 'vuex'
export default {
    name: 'MSCardAnswer',
    props: {
        title: {
            type: String,
            default: ''
        },
        answer: {
            type: String,
            default: 'A'
        },
    },
    data() {
        return {
            editorData: '',//gía trị của ckeditor
            editor: ClassicEditor,
            editorConfig: {
                toolbar: ['bold', 'italic', 'Underline', 'undo']
            },
            isEditting: false,//dang hien editor,
            stateAnswer: false,//trạng thái câu trả lời đúng hay sai
        }
    },
    computed: {
        ...mapGetters(['typePopupAdd'])
    },
    methods: {
        ...mapMutations(['setShowDialog']),
        /**
         * bắt sự kiện xóa câu hỏi
         * CreatedBy: Trịnh Huỳnh Đức (23-5-2023)
         */
        onClickRemove() {
            this.setShowDialog(true)
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
        }
    }
}
</script>

<style scoped>
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
