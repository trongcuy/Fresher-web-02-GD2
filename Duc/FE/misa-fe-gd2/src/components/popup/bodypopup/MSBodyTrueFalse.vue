<template>
    <!-- câu hỏi đúng sai -->
    <div class="popup-body popup-body-rightwrong">     
        <MSCardAnswer v-for="(item, index) in answers" :key="item.answerID" :placeholder="item.answerContent" 
            :state="item.answerState"
            @onClickChangeState="onClickChangeState"
            @setValueAnswer="(value)=>item.answerContent=value"/>        
    </div>
</template>

<script>
import MSCardAnswer from '../../card/MSCardAnswer.vue'
export default {
    name: "MSBodyTrueFalse",
    props: {
        answers: {
            type: Array,
            default: []
        },
    },
    components: {
        MSCardAnswer
    },
    data() {
        return {
        }
    },
    methods: {
        /**
         * hàm thay đổi trạng thái của đáp án
         * CreatedBy: Trịnh Huỳnh Đức (14-6-2023)
         */
        onClickChangeState(){
            const temp = this.answers[0].answerState
            this.answers[0].answerState=this.answers[1].answerState
            this.answers[1].answerState=temp
        }
    }
}
</script>
<style scoped>
.popup-body {
    padding: 1rem 24px;
    height: 52%;
    width: 100%;
    overflow-y: scroll;
    display: grid;
    grid-template-columns: repeat(4, 1fr);
    /* 4 cột có tỷ lệ bằng nhau */
    justify-content: center;
    justify-items: center;
    gap: 1rem;
}
.popup-body-rightwrong {
    padding: 1rem 24px;
    height: 52%;
    width: 100%;
    display: flex;
    justify-content: center;
    gap: 1rem;
}
</style>
