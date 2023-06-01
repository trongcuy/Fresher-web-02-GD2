<template>
    <div class="div-input div-flex-row">
        <div>Ô trống 1 </div>
        <div class="list-fill div-flex-row">
            
            <span v-for="(item, index) in listFill" :key="index">{{ item }} <img src="../../assets/img/ic_x.svg" @click="onRemoveFill(index)"/></span>     
            <p style="position: absolute;top: 12px;left: 95px;" v-if="showPlaceHolder">Nhập đáp án rồi nhấn Enter...</p>
            <span contenteditable 
                @keydown.enter="handleEnter" ref="mySpan" 
                @focus="()=>{showPlaceHolder=false}"
                @blur="onBlurInput"
                v-click-outside="onBlurInput"></span>
           
        </div>
        <div class="div-center div-img"><img src="../../assets/img/ic_x.svg" /></div>
    </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
    name: "MSInputTag",
    props: [],
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
            }
        },
        /**
         * bắt sự kiện click xóa list fill
         * CreatedBy: Trịnh Huỳnh Đức 24-5-2023
         * @} event 
         */
        onRemoveFill(index) {
            this.listFill.splice(index, 1)
        },
        /**
         * bắt sự kiện blur ra khỏi input span
         * CreatedBy: Trịnh Huỳnh Đức 24-5-2023
         */
        onBlurInput(){
            //nếu đang nhập
            const value = this.$refs.mySpan.innerText
            if(this.listFill.length==0 && !value)
                this.showPlaceHolder = true
        }
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

</style>
