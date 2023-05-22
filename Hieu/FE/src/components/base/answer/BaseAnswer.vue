<template>
    <div class="answer-container">
        <div class="answer-header flex">
            <div class="answer-char">
                A
            </div>
            <div class="answer-toolbar flex">
                <div class="icon-trash" v-show="showFormQuestion == 1">
                    <img src="@/assets/img/trash.svg" alt="" />
                </div>
                <div class="icon-img" v-show="showFormQuestion == 1">
                    <img src="@/assets/img/icon-img.svg" alt="" />
                </div>
                <div class="icon-tick">
                    <img src="@/assets/img/icon-tick.svg" alt="" />
                </div>
            </div>
        </div>
        <div class="answer-main">

        </div>
    </div>
</template>

<script setup>
import { computed } from 'vue';
import { useStore } from 'vuex';

const store = useStore();

const showFormQuestion = computed(() => store.state.question.showFormQuestion);
</script>

<style scoped>
.answer-container {
    aspect-ratio : 1 / 1;
    overflow: hidden;
    border-radius: 10px;
    background-color: #ffaec7;
    width: 100%;
    font-size: 20px;
    font-weight: 700;
}

.answer-header{
    justify-content: space-between;
    padding: 8px;
}

.answer-char{
    margin: 0.25rem 0 0 0.25rem;
}

.answer-toolbar>div{
    background-color: #fff;
    width: 32px;
    height: 32px;
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: 10px;
    cursor: pointer;
    margin-left: 8px;
    outline: none;
}

.answer-toolbar>div:hover{
    transform: scale(1.1);
    transition: all 0.2s;  
}
</style>