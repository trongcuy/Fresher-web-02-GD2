<template>
    <div class="div-popup">
        <div class="popup-header">
            Nhập khẩu
            <div class="div-center" @click="onClickClose"><img src="../../assets/img/x.jpg"/></div>
        </div>
        <div class="popup-body">
            <div class="num-record card">
                <p>Số bản ghi</p>
                <h1>{{ validRecord + invalidRecord }}</h1>
            </div>
            <div class="valid card">
                <p>Hợp lệ</p>
                <h1>{{ validRecord }}</h1>
            </div>
            <div class="invalid card">
                <p>Không hợp lệ</p>
                <h1>{{ invalidRecord }}</h1>
            </div>
        </div>
        <div class="div-warning div-flex-row" v-if="invalidRecord > 0">
            <img src="../../assets/img/ic_Info.svg"/>
            <div>
                <p>Bạn có thể tải file dữ liệu không hợp lệ để chỉnh sửa và nhập khẩu lại. Hoặc tiếp tục nhập khẩu những bản ghi hợp lệ.</p>
                <p @click="onDowloadFileInvalid">Tải file lỗi</p>
            </div>
        </div>
        <div class="popup-footer">
            <MSButton title="Hủy" class="btn-cancel" @click="onClickClose" />
            <MSButton title="Đổi file khác" @click="onClickOtherFile" />
            <MSButton title="Tiếp tục" class="btn-complete" @click="onClickContinue" />
        </div>
    </div>
</template>

<script>
import MSButton from '../button/MSButton.vue'
import { mapActions } from 'vuex'
export default {
    name: "MSPopupImport",
    props: {
        validRecord: {
            type: Number,
            default: 0
        },
        invalidRecord: {
            type: Number,
            default: 0
        },
    },
    components: {
        MSButton
    },
    data() {
        return {

        }
    },
    methods: {
        ...mapActions(['dowloadFileInvalid']),
        /**
         * Bắt sự kiện đóng popup
         * CreatedBy: Trịnh Huỳnh Đức (7-6-2023)
         */
        onClickClose(){
            this.$emit('onClickClose')
        },
        /**
         * Bắt sự kiện chọn file khác
         * CreatedBy: Trịnh Huỳnh Đức (7-6-2023)
         */
        onClickOtherFile() {
            this.$emit('onClickOtherFile')
        },
        /**
         * bắt sự kiện tải file lỗi
         * CreatedBy: Trịnh Huỳnh Đức (14-6-2023)
         */
        onDowloadFileInvalid() {
            this.dowloadFileInvalid()
        },

        /**
         * bắt sự kiện insert bản ghi hợp lệ vào database
         * CreatedBy: Trịnh Huỳnh Đức (7-6-2023)
         */
        onClickContinue() {
            this.$emit('onClickContinue')
        }
    }
}
</script>
<style scoped lang="scss">
.div-popup {
    position: absolute;
    transform: translate(-50%, -50%);
    top: 50%;
    left: 50%;
    width: 600px;
    border-radius: 10px;
    box-shadow: 0 2px 12px 0 rgba(0, 0, 0, .1);
    background-color: #fff;
    z-index: 999;


    .popup-header {
        padding: 24px;
        color: #4E5B6A;
        font-weight: 700;
        height: 80px;
        font-size: 24px;
        position: relative;
        div {
            position: absolute;
            top: 0px;
            right: 0px;
            width: 40px;
            height: 40px;
            cursor: pointer;
            img {
                width: 16px;
                height: 16px;
            }
        }
    }

    .popup-body {
        height: 173px;
        padding: 0px 24px 30px 24px;
        display: flex;
        justify-content: space-between;
        .card {
            width: 100%;
            border-radius: 8px;
            display: flex;
            justify-content: center;
            align-items: center;
            flex-direction: column;
            p {
                font-size: 14px;
            }
            p, h1 {
                color: #fff;
            }
        }
        .num-record {
            background-color: #a3a1fb;
        }
        .valid {
            background-color: #5bd885;
            margin-left: 12px;
            margin-right: 12px;
        }
        .invalid {
            background-color: #f98888;
        }
    }
    .div-warning {
        background-color: rgba(241,242,247,1);
        padding: 8px;
        border-radius: 10px;
        justify-content: space-between;
        align-items: center;
        width: auto;
        height: 80px;
        margin: -12px 24px 24px 24px;
        p {
            font-size: 14px;
            line-height: 20px;
        }
        p:first-child {
            color: #606266;
        }
        p:nth-child(2) {
            color: #8A6BF6;
            cursor: pointer;
        }
        img {
            width: 28px;
            height: 28px;
            margin-left: 12px;
            margin-right: 12px;
        }
    }
    .popup-footer {
        padding: 0px 24px 24px 24px;
        height: 64px;
        display: flex;
        justify-content: flex-end;
    }

    .btn-complete {
        margin-left: 12px;
        background-color: #8a6bf6;
        color: #fff;
    }
    .btn-cancel {
        margin-right: 12px;
    }
}
</style>
