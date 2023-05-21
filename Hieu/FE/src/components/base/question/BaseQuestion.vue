<template>
    <div class="question-item">
        <div class="question-decore"></div>
        <div class="question-main">
            <div class="question-content flex">
                <div class="question-number">
                    1.
                </div>
                <div class="question-text">
                    Câu hỏi số 1.
                </div>
            </div>
            <div class="question-answer">
                <div class="question-answer__list">
                    <div class="anwser-item flex">
                        <div class="answer-char">
                            A
                        </div>
                        <div class="answer-text">
                            1000
                        </div>
                    </div>
                    <div class="anwser-item flex">
                        <div class="answer-char answer-correct">
                            B
                        </div>
                        <div class="answer-text">
                            10000
                        </div>
                    </div>
                </div>
            </div>
            <div class="question-toolbar flex">
                <BaseButton class="ms-button btn-white btn-active" text="Chỉnh sửa"></BaseButton>
                <BaseButton class="ms-button btn-white btn-active btn-onlyicon">
                    <template v-slot:icon>
                        <div class="center">
                            <img src="@/assets/img/duplicate.svg" alt="">
                        </div>
                    </template>
                </BaseButton>
                <BaseButton class="ms-button btn-white btn-active btn-onlyicon">
                    <template v-slot:icon>
                        <div class="center">
                            <img src="@/assets/img/trash.svg" alt="">
                        </div>
                    </template>
                </BaseButton>
            </div>
        </div>
    </div>
</template>

<script setup>
import BaseButton from '../button/BaseButton.vue';
</script>

<style scoped>

.question-item{
    box-shadow: 0 3px 20px rgba(90,125,141,.16);
    border-radius: 0 0 6px 6px;
}
.question-decore{
    height: 8px;
    border-radius: 6px 6px 0 0;
    background-color: rgb(255, 88, 140);
}

.question-main{
    padding: 1.25rem;
    font-size: 16px;
}

.question-content{
    margin-bottom: 20px;

}

.question-number{
    font-weight: 700;
    margin-right: 0.25rem;
}

.question-text{
    display: inline-block;
    position: relative;
}

.question-answer{
    padding-bottom: 20px;
    margin-bottom: 20px;
    border-bottom: 1px solid #eaebf5;
}

.question-answer__list{
    display: grid;
    grid-row-gap: 20px;
    grid-template-columns: repeat(4,minmax(0,1fr));
}

.answer-item{
    margin-right: 16px;
}

.answer-char{
    color: #fff;
    width: 24px;
    height: 24px;
    border-radius: 50%;
    text-align: center;
    line-height: 24px;
    background-color: #b6b9ce;
    margin-right: 8px;
    font-weight: 700;
}

.answer-correct{
    background-color: #00c542;
}

.question-toolbar{
    justify-content: flex-end;
}
</style>