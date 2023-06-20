<template>
    <!-- câu hỏi lựa chọn -->
    <div class="popup-body">
        <MSCardAnswer v-for="(item, index) in answers" :key="item" 
            :title="item.answerContent" 
            :answer="index"  
            :state="item.answerState"
            :image="item.answerImage"
            @onClickRemove="onClickRemoveAnswer(index)" placeholder="Nhập đáp án..."
            @onClickChangeState="(value)=>item.answerState=value"
            @setValueAnswer="(value)=>item.answerContent=value"
            @onClickAddImage="onClickAddImage(index)"
            @onClickRemoveImage="onClickRemoveImage(index)"/>
        
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
         * hàm xóa một đáp án
         * CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
         */
        onClickRemoveAnswer(index){
            this.$emit('onClickRemoveAnswer', index)
        },
        /**
         * bắt sự kiện thêm ảnh đáp án
         * CreatedBy: Trịnh Huỳnh Đức (10-6-2023)
         */
        onClickAddImage(index) {
            this.$emit('onClickAddImage', index)
        },
        /**
         * bắt sự kiện xóa ảnh đáp án
         * CreatedBy: Trịnh Huỳnh Đức (12-6-2023)
        */
        onClickRemoveImage(index) {
            this.$emit('onClickRemoveImage', index)
        },
    },
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
