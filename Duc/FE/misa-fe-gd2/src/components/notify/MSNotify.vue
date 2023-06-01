<template>
    <div class="div-notify div-flex-row">
        <div class="div-flex-row">
        <div class="div-line"></div>
        <img src="../../assets/img/icon-success.jpg" class="icon-success"/>
        Bạn đã xóa thành công
        </div>
        <img src="../../assets/img/ic_close_notify.jpg" class="icon-close" @click="onClickClose"/>
    </div>
</template>

<script>
import { mapMutations } from 'vuex'
export default {
    name: "MSNotify",
    props: [],
    components: {
    },
    data() {
        return {

        }
    },
    methods: {
        ...mapMutations(['setShowNotify']),
        /**
         * bắt sự kiện đóng thông báo
         * CreatedBy: Trịnh Huỳnh Đức (23-5-2023)
         */
        onClickClose(){
            this.setShowNotify(false)
        }
    }
}
</script>
<style scoped>
.div-notify {
    height: 70px;
    width: 500px;
    box-shadow: 0 2px 12px 0 rgba(0, 0, 0, .1);
    background-color: #fff;
    z-index: 100;
    position: absolute;
    top: -100px;
    left: calc(50vw - 250px);
    border-radius: 20px;
    padding: 20px;
    justify-content: space-between;
    animation: slideDown 1s forwards;
}
/* hiệu ứng chuyển động */
@keyframes slideDown {
    to {
        top: 20px;
    }
}
.div-line {
    width: 3px;
    height: 35px;
    background-color: #579e3d;
    border-radius: 10px;
    margin-right: 12px;
}
.icon-close {
    height: 100%;
    width: auto;
}
.icon-success {
    height: 36px;
    width: auto;
    margin-right: 12px;
}
</style>
