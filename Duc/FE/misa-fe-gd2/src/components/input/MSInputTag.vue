<template>
    <div class="div-input div-flex-row">
        <div>Ô trống {{ num + 1 }} </div>
        <div class="list-fill div-flex-row">
            <span v-for="(item, index) in listFill" :key="index">{{ item }} <img src="../../assets/img/ic_x.svg"
                    @click="onRemoveFill(index)" /></span>
            <p v-if="showPlaceHolder && this.listFill.length==0">Nhập đáp án rồi nhấn Enter...</p>
            <span contenteditable @keydown.enter="handleEnter" ref="mySpan" @focus="() => { showPlaceHolder = false }"
                @blur="onBlurInput" v-click-outside="onBlurInput"></span>
        </div>
        <div class="div-center div-img" @click="() => { this.$emit('onClickRemove') }"><img
                src="../../assets/img/ic_x.svg" />
        </div>
    </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
    name: "MSInputTag",
    props: {
        answer: {
            type: String,
            default: ''
        },
        num: {
            type: Number,
            default: 0
        }
    },
    components: {

    },

    data() {
        return {
            listFill: [],//mảng lưu ds các đáp án điền vào
            showPlaceHolder: 'true',//biến để ẩn hiện placeholder

        }
    },
    computed: {
        ...mapGetters(['typePopupAdd']),
    },
    methods: {
        /**
         * bắt sự kiện click enter
         * CreatedBy: Trịnh Huỳnh Đức 24-5-2023
         * @} event 
         */
        handleEnter(event) {
            event.preventDefault()
            if (event.key === "Enter") {
                const value = this.$refs.mySpan.innerText
                this.listFill.push(value)
                this.$refs.mySpan.innerText = ''
                this.$emit('onSetValueFill', this.fillToString(this.listFill))
            }
        },
        /**
         * bắt sự kiện click xóa list fill
         * CreatedBy: Trịnh Huỳnh Đức 24-5-2023
         * @} event 
         */
        onRemoveFill(index) {
            this.listFill.splice(index, 1)
            this.$emit('onSetValueFill', this.fillToString(this.listFill))
        },
        /**
         * bắt sự kiện blur ra khỏi input span
         * CreatedBy: Trịnh Huỳnh Đức 24-5-2023
         */
        onBlurInput() {
            //nếu đang nhập
            const value = this.$refs.mySpan.innerText
            if (this.listFill.length == 0 && !value)
                this.showPlaceHolder = true
        },
        /**
         * chuyển mảng fill thành string
         * CreatedBy: Trịnh Huỳnh Đức (4-6-2023)
         */
        fillToString(fill){
            let value = ''
            for(let i = 0; i< fill.length; i++) {
                if(value)
                value = value+ "; "
                value = value + fill[i]
            }       
            return value
        }
    },
    created() { 
        if (this.answer.answerContent)
            this.listFill = this.answer.answerContent.split(";")
    }
}
</script>
<style scoped>
.div-input .list-fill {
    min-height: 42px;
    background-color: #fff;
    width: calc(100% - 88px);
    flex-wrap: wrap;
    padding: 4px 46px 4px 4px;
    height: auto;
    border-radius: 10px;
    border: 1px solid rgb(182, 185, 206);
    font-size: 14px;
    outline: none;
    gap: 4px;
}

.div-input .list-fill span:not(:last-child) {
    border: 1px solid #b6b9ce;
    color: #4e5b6a;
    background-color: #f1f2f7;
    height: 32px;
    line-height: 30px;
    padding-left: 10px;
    padding-right: 20px;
    border-radius: 10px;
    margin-right: 4px;
    position: relative;
    width: fit-content;
}

.div-input .list-fill span:focus {
    outline: none;
}

.div-input .list-fill span:last-child {
    border: none;
    padding-left: 0px;
    min-width: 100px;
    z-index: 2;
}

.list-fill img {
    position: absolute;
    width: 12px;
    height: 12px;
    top: 9px;
    right: 4px;
}

.div-input {
    width: 100%;
    position: relative;
}

.div-input>div:first-child {
    margin-right: 8px;
    min-width: 80px;
}

.div-img {
    position: absolute;
    top: 0px;
    right: 0px;
    width: 46px;
    height: 100%;
    border-left: 1px solid rgb(182, 185, 206);
    cursor: pointer;
}

.div-img img {
    width: 16px;
    height: 16px;
}

.list-fill:focus-within {
    border: 1px solid #8a6bf6;
}
.list-fill p {
    position: absolute;
    top: 12px;
    left: 95px;
}
</style>
