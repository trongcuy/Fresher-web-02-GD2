<template>
    <div class="div-dialog">
        <div class="dialog-header">
            EMIS ôn tập
        </div>
        <div class="dialog-body" v-if="content">
            {{ content }}
        </div>
        <div class="dialog-body" v-if="!content">
            {{ firstContent }}<b>{{data}}</b>{{ lastContent }}
        </div>
        <div class="dialog-footer">
                <MSButton title="Đồng ý" @click="onClickCancel" v-if="type=='alert'"/>
                <MSButton title="Hủy bỏ" @click="onClickCancel" v-if="type!='alert'"/>
                <MSButton title="Xóa" class="btn-complete" @click="onClickOk" v-if="type!='alert'"/>
        </div>
    </div>
</template>

<script>
import { mapMutations } from 'vuex'
import MSButton from '../button/MSButton.vue'
export default {
    name: "MSDialog",
    props: {
        content: {
            type: String,
            default: ''
        },
        type: {
            type: String,
            default: ''
        },
        firstContent: {
            type: String,
            default: ''
        },
        lastContent: {
            type: String,
            default: ''
        },
        data: {
            type: String,
            default: ''
        }
    },
    components: {
        MSButton
    },
    data() {
        return {

        }
    },
    methods: {
        ...mapMutations(['setShowDialog', 'setShowNotify']),
        /**
         * bắt sự kiện hủy
         * CreatedBy: Trịnh Huỳnh Đức (23-5-2023)
         */
        onClickCancel(){
            this.$emit('onClickCancel')
        },
        /**
         * bắt sự kiện đồng ý
         * CreatedBy: Trịnh Huỳnh Đức (23-5-2023)
         */
        onClickOk(){
            this.$emit('onClickOk')
        }
    }
}
</script>
<style scoped>
.div-dialog {
    width: 414px;
    height: 177px;
    border-radius: 4px;
    border: 1px solid #ebeef5;
    font-size: 14px;
    box-shadow: 0 2px 12px 0 rgba(0, 0, 0, .1);
    background-color: #fff;
    z-index: 999;
    position: fixed;
    top: calc(50vh - 90px);
    left: calc(50vw - 207px);
}

.dialog-header {
    padding: 12px 16px;
    background-color: #eaebf5;
    color: #303133;
    font-weight: 500;
    height: 40px;

}

.dialog-body {
    height: 48px;
    padding: 24px 24px 0px 24px;
}

.dialog-footer {
    padding: 24px;
    height: 88px;
    display: flex;
    justify-content: flex-end;
}
.btn-complete {
    margin-left: 12px;
    background-color: #8a6bf6;
    color: #fff;
}</style>
