<template>
    <!-- câu hỏi lựa chọn -->
    <div class="popup-body">
        <MSCardAnswer v-for="(item, index) in answers" :key="item" 
            :title="item.answerContent" 
            :answer="this.indexToLetter(index)"  
            :state="item.answerState"
            @onClickRemove="onClickRemoveAnswer(index)" placeholder="Nhập đáp án..."
            @onClickChangeState="(value)=>item.answerState=value"
            @setValueAnswer="(value)=>item.answerContent=value"/>
        
    </div>
</template>

<script>
import MSCardAnswer from '../../card/MSCardAnswer.vue'
export default {
    name: "MSBodySelect",
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
         * hàm chuyển từ index sang chữ cái tương ứng
         * CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
         * @param {*} index 
         */
        indexToLetter(index) {
            const letterCode = 65 + index;
            return String.fromCharCode(letterCode);
        },
        /**
         * hàm xóa một đáp án
         * CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
         */
        onClickRemoveAnswer(index){
            this.$emit('onClickRemoveAnswer', index)
        }
    },
    created(){
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
</style>
