<template>
    <div class="div-answer-item div-flex-row">
        <div class="div-center div-answer-true"
            :class="{ 'div-answer-false': answer.answerState == enums.answerState.false }">{{
                this.indexToLetter(index) }}</div>
        <div v-if="answer.answerContent">
            <p v-html="answer.answerContent"></p>
        </div>
        <div><img class="image-answer" v-if="this.urlAnswer" :src="this.urlAnswer" @click="onShowImage(this.urlAnswer)" />
        </div>
    </div>
</template>

<script>
import { mapMutations } from 'vuex'
export default {
    name: "MSAnswerItem",
    props: {
        answer: {
            type: Object,
            default: {}
        },
        index: {
            type: Number,
            default: 0
        },
    },
    data() {
        const enums = window.Enums
        return {
            enums,
        }
    },
    computed: {
        urlAnswer() {
            return this.buildImage(this.answer.answerImage)
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
         * hàm bắt sự kiện click vào ảnh
         * CreatedBy: Trịnh Huỳnh Đức (14-6-2023)
         */
        onShowImage(urlImage) {
            this.setUrlImageShow(urlImage)
        }
    },
}
</script>
<style scoped lang="scss">
.div-answer-item {
    align-items: unset;

    div:first-child {
        margin-right: 4px;
        width: 24px;
        height: 24px;
        border-radius: 50px;
        color: white;
    }

    p {
        text-align: center;
        height: 100%;
        padding-top: 2px;
        padding-left: 4px;
    }

    .div-answer-true {
        background-color: #00C542;
    }

    .div-answer-false {
        background-color: #B6B9CE;
    }

    .image-answer {
        width: 163px;
        height: auto;
        object-fit: contain;
    }
}
</style>
