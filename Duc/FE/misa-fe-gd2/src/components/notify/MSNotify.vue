<template>
    <div class="div-notify div-flex-row">
        <div class="div-flex-row">
            <div class="div-line" 
                :class="{'div-line-error': this.showNotify!='success' && this.showNotify!='successImport' && this.showNotify!='successAddExercise', 'div-line-warning':this.showNotify=='warnning'}"></div>
            <img src='../../assets/img/icon-success.jpg' class="icon-success" v-if="this.showNotify=='success' || this.showNotify=='successImport' || this.showNotify=='successAddExercise'"/>
            <img src='../../assets/img/ic_error.svg' class="icon-error" v-if="this.showNotify=='exception' || this.showNotify=='errorRemove' || this.showNotify=='error' || this.showNotify=='errorFile' || this.showNotify=='errorSizeExcel'"/>
            <img src='../../assets/img/ic_warning.svg' class="icon-error" v-if="this.showNotify=='warnning'"/>
            {{ this.content }}
        </div>
        <img src="../../assets/img/ic_close_notify.jpg" class="icon-close" @click="onClickClose" />
    </div>
</template>

<script>
import { mapMutations } from 'vuex'
import { mapGetters } from 'vuex'  
export default {
    name: "MSNotify",
    props: {
        content: {
            type: String,
            default: ''
        }
    },
    components: {
    },
    data() {
        return {
            
        }
    },
    computed: {
        ...mapGetters(['showNotify'])
    },
    methods: {
        ...mapMutations(['setShowNotify']),
        /**
         * bắt sự kiện đóng thông báo
         * CreatedBy: Trịnh Huỳnh Đức (23-5-2023)
         */
        onClickClose() {
            this.setShowNotify('no')
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
    z-index: 999;
    position: fixed;
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
.div-line-error {
    background-color: #ff6161;
}
.div-line-warning {
    background-color: #FFD200;
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
.icon-error {
    height: 32px;
    width: auto;
    margin-right: 12px;
    margin-left: 8px;
}</style>
