<template>
    <div class="div-question" :class="{ 'card-rightwrong': typePopupAdd == 'truefalse' }">
        <div class="div-decor"></div>
        <div class="div-main">
            <div class="div-title">
                <p><b>{{ question.questionNumber }}.</b> <span v-html="question.questionContent"></span></p>
            </div>
            <div class="div-body">
                <!-- câu trả lời tự luận -->
                <div v-if="isEssay" class="answer-essay"></div>

                <!-- Câu trả lời chọn, đúng sai, điền đáp án -->
                <div v-if="isSelect || isTrueFalse || isFill" class="answer-grid">
                    <div class="div-answer-item div-flex-row" v-for="(item, index) in answers">
                        <div class="div-center div-answer-true"
                            :class="{ 'div-answer-false': item.answerState == enums.AnswerState.False }">{{
                                this.indexToLetter(index) }}</div>
                        <div>{{ this.convertHtmlToText(item.answerContent) }}</div>
                    </div>
                </div>

                <!-- Lời giải -->
                <div v-if="question.questionExplane" class="question-explane">
                    <div style="display: flex;"><b>Lời giải: </b><span v-html="question.questionExplane"></span></div>
                </div>
            </div>

            <!-- Thanh button -->
            <div class="div-button-question">
                <MSButton title="Chỉnh sửa" @click="onEditQuestion" />
                <div class="div-button-img" v-tooltip="'Sao chép câu hỏi'" @click="onCopyQuestion"><img src="../../assets/img/ic_dublicate.svg" />
                </div>
                <div class="div-button-img" v-tooltip="'Xóa câu hỏi'" @click="() => { this.$emit('onRemoveQuestion') }">
                    <img src="../../assets/img/icon_delete.9097d258.svg" />
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import MSButton from '../button/MSButton.vue'
import { mapActions, mapGetters } from 'vuex'
export default {
    name: 'MSQuestion',
    props: {
        question: {
            type: Array,
            default: []
        },
    },
    components: {
        MSButton
    },
    watch: {
        question: function () {
            if (this.isSelect || this.isTrueFalse || this.isFill)
                this.getAnswers(this.question.questionID).then(res => {
                    this.answers = res
                })
        }
    },
    data() {
        const resource = window.Resource
        const enums = window.Enums
        return {
            resource,
            enums,
            answers: []
        }
    },
    computed: {
        isSelect() {
            return this.resource.TypeQuestion[this.question.questionType] == 'select'
        },
        isTrueFalse() {
            return this.resource.TypeQuestion[this.question.questionType] == 'truefalse'
        },
        isFill() {
            return this.resource.TypeQuestion[this.question.questionType] == 'fill'
        },
        isEssay() {
            return this.resource.TypeQuestion[this.question.questionType] == 'essay'
        }
    },
    methods: {
        ...mapActions([
            'getAnswers'
        ]),
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
         * hàm bắt sự kiện chỉnh sửa câu hỏi
         * CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
         * @param {*} index 
         */
        onEditQuestion() {
            this.$emit('onEditQuestion', { answers: this.answers, question: this.question })
        },
         /**
         * hàm bắt sự kiện sao chép câu hỏi
         * CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
         * @param {*} index 
         */
        onCopyQuestion() {
            //tạo câu hỏi mới và xóa id
            let newQuestion = {...this.question}
            delete newQuestion.questionID
            this.$emit('onCopyQuestion', { answers: this.answers, question: newQuestion })
        },
        /**
         * đổi html sang text
         * @param {*} html 
         */
        convertHtmlToText(html) {
            let element = document.createElement("div");
            element.innerHTML = html;
            return element.innerText;
        }
    },
    created() {
        //nếu là form 
        if (this.isSelect || this.isTrueFalse || this.isFill)
            this.getAnswers(this.question.questionID).then(res => {
                this.answers = res
            })
    }
}
</script>

<style scoped>
p {
    font-size: 16px;
}

.div-question {
    width: 100%;
    box-shadow: rgba(90, 125, 141, 0.16) 0px 3px 20px;
    border-radius: 10px;
}

.div-decor {
    height: 8px;
    width: 100%;
    background-color: #FF588C;
    border-radius: 10px 10px 0px 0px;
}

.div-main {
    width: 100%;
    padding: 20px;
}

.div-title {
    height: auto;
    margin-bottom: 20px;
}
.div-title p {
    display: flex;
}
.div-body {
    border-bottom: 1px solid #E6E6e6;
    margin-bottom: 20px;
}

.div-answer-item {
    height: 24px;
}

.div-answer-item div:first-child {
    margin-right: 4px;
    width: 24px;
    height: 24px;
    border-radius: 50px;
    color: white;
}

.div-answer-true {
    background-color: #00C542;
}

.div-answer-false {
    background-color: #B6B9CE;
}

.div-button-question {
    display: flex;
    align-items: center;
    gap: 12px;
    padding-right: 6px;
    justify-content: end;
}

.div-button-img {
    background-color: #f1f2f7;
    border: 1px solid #b6b9ce;
    height: 40px;
    border-radius: 10px;
    width: 40px;
    height: 40px;
    display: flex;
    justify-content: center;
    align-items: center;
    position: relative;
}

.div-button-img img {
    width: 24px;
    height: 24px;
}

.answer-grid {
    display: grid;
    grid-template-columns: repeat(4, 1fr);
    padding-bottom: 20px;
    gap: 20px;
}

.answer-essay {
    height: 20px;
    width: 480px;
    margin-bottom: 20px;
    border-bottom: 1px dashed #4E5B6A;
    border-top: 1px dashed #4E5B6A;
}

.question-explane {
    width: 100%;
    border-top: 1px solid #E6E6e6;
    height: 60px;
    box-sizing: border-box;
    padding-top: 20px;
}

.question-explane>div {
    height: 20px;
}
</style>
