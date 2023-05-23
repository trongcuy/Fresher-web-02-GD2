<template>
    <div class="div-question" :class="{ 'card-rightwrong': typePopupAdd == 'rightwrong' }">
        <div class="div-decor"></div>
        <div class="div-main">
            <div class="div-title">
                <p><b>{{ num }}.</b> {{ title }}</p>
            </div>
            <div class="div-answer div-flex-row">
                <div class="div-answer-item div-flex-row">
                    <div class="div-center div-answer-true">A</div>
                    <div> Đúng</div>
                </div>
                <div class="div-answer-item div-flex-row">
                    <div class="div-center div-answer-false">B</div>
                    <div> Sai</div>
                </div>
            </div>
            <!-- Thanh button -->
            <div class="div-button">
                <MSButton title="Chỉnh sửa" />
                <div class="div-button-img"><img src="../../assets/img/ic_dublicate.svg" /></div>
                <div class="div-button-img"><img src="../../assets/img/icon_delete.9097d258.svg" /></div>
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
    height: 20px;
    margin-bottom: 20px;
}

.div-answer {
    height: 24px;
    padding-bottom: 20px;
    margin-bottom: 20px;
    justify-content: space-around;
    border-bottom: 1px solid #E6E6e6;
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
.div-button {
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
}
</style>
