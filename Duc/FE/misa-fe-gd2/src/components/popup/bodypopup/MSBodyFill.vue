<template>
    <!-- câu hỏi điền chỗ trống -->
    <div class="div-flex-column popup-body-fill">
        <MSInputTag v-for="(item, index) in answers" 
            :key="item"
            :answer="item" 
            :num="index"
            @onClickRemove="onClickRemoveAnswer(index)"
            @onSetValueFill="(value)=>item.answerContent=value"/>
        <div class="div-button div-flex-row" @click="onClickAddAnswer">
            <div class="div-flex-row">
                <img src="../../../assets/img/icon_plus.svg" />
                <div>Thêm đáp án</div>
            </div>
        </div>
    </div>
</template>
<script>
import MSInputTag from '../../input/MSInputTag.vue'
export default {
    name: "MSBodyFill",
    props: {
        answers: {
            type: Array,
            default: []
        },
    },
    components: {
        MSInputTag,
    },
    watch: {
        answers: {
            deep: true,
            handler: function (newVal, oldVal) {
                if (newVal.length !== oldVal.length) {
                    // Sự thay đổi về độ dài của mảng đã xảy ra
                    alert("kk");
                }
            }
        }
    },
    data() {
        return {
        }
    },
    methods: {
        /**
         * click thêm một đáp án
         * CreatedBy: Trịnh Huỳnh Đức (4-6-2023)
         */
        onClickAddAnswer(){
            this.$emit('onClickAddAnswer')
        },
        /**
         * click xóa một đáp án
         * CreatedBy: Trịnh Huỳnh Đức (4-6-2023)
         */
        onClickRemoveAnswer(index){
            this.$emit('onClickRemoveAnswer', index)
        }
    }
}
</script>
<style scoped>
.popup-body-fill {
    padding: 1rem 24px;
    height: 52%;
    width: 100%;
    gap: 1rem;
    overflow-y: scroll;
}
.popup-body-fill .div-button {
    justify-content: center;
    height: 40px;
    min-height: 40px;
    width: calc(100% - 88px);
    margin-left: 88px;
    border: 1px dashed rgb(182, 185, 206);
    border-radius: 10px;
    background-color: rgb(241, 242, 247);
    cursor: pointer;
    color: rgba(78, 91, 106, 0.7);
}

.div-button img {
    margin-right: 12px;
    width: 12px;
    height: 12px;
}

</style>
