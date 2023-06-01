<template>
    <div class="div-question" :class="{ 'card-rightwrong': typePopupAdd == 'rightwrong' }">
        <div class="div-decor"></div>
        <div class="div-main">
            <div class="div-title">
                <p><b>{{ num }}.</b> {{ title }}</p>
            </div>
            <div class="div-body">
                <!-- câu trả lời đúng sai -->
                <div v-if="type == 'truefalse'" class="answer-grid">
                    <div class="div-answer-item div-flex-row">
                        <div class="div-center div-answer-true">A</div>
                        <div> Đúng</div>
                    </div>
                    <div class="div-answer-item div-flex-row">
                        <div class="div-center div-answer-false">B</div>
                        <div> Sai</div>
                    </div>
                </div>
                <!-- câu trả lời tự luận -->
                <div v-if="type == 'essay'" class="answer-essay">
                </div>
                <!-- Câu trả lời chọn -->
                <div v-if="type=='select'" class="answer-grid">
                    <div class="div-answer-item div-flex-row">
                        <div class="div-center div-answer-true">A</div>
                        <div> Đúng</div>
                    </div>
                    <div class="div-answer-item div-flex-row">
                        <div class="div-center div-answer-false">B</div>
                        <div> Sai</div>
                    </div>
                    <div class="div-answer-item div-flex-row">
                        <div class="div-center div-answer-false">C</div>
                        <div> Sai</div>
                    </div>
                    <div class="div-answer-item div-flex-row">
                        <div class="div-center div-answer-false">D</div>
                        <div> Sai</div>
                    </div>
                    <div class="div-answer-item div-flex-row">
                        <div class="div-center div-answer-false">E</div>
                        <div> Sai</div>
                    </div>
                    <div class="div-answer-item div-flex-row">
                        <div class="div-center div-answer-false">F</div>
                        <div> Sai</div>
                    </div>          
                </div>
                <!-- câu trả lời điền đáp án -->
                <div v-if="type == 'fill'" class="answer-grid">
                    <div class="div-answer-item div-flex-row">
                        <div class="div-center div-answer-true">1</div>
                        <div> mèo</div>
                    </div>
                    <div class="div-answer-item div-flex-row">
                        <div class="div-center div-answer-false">2</div>
                        <div> chó</div>
                    </div>
                </div>
            </div>
            <!-- Thanh button -->
            <div class="div-button-question">
                <MSButton title="Chỉnh sửa" />
                <div class="div-button-img" v-tooltip="'Sao chép câu hỏi'"><img src="../../assets/img/ic_dublicate.svg" /></div>
                <div class="div-button-img" v-tooltip="'Xóa câu hỏi'"><img src="../../assets/img/icon_delete.9097d258.svg" /></div>
            </div>
        </div>
    </div>
</template>

<script>
import MSButton from '../button/MSButton.vue'
export default {
    name: 'MSQuestion',
    props: {
        num: {//số thứ tự câu
            type: Number,
            default: 0
        },
        title: {//
            type: String,
            default: ''
        },
        type: {
            type: String,
            default: ''
        }
    },
    components: {
        MSButton
    }
}
</script>

<style scoped>
p {
    font-size: 16px;
}

.div-question {
    margin: auto;
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
</style>
