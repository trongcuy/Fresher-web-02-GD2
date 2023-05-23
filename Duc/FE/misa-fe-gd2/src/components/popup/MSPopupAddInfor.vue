<template>
    <div class="div-main">
        <div class="header">
            <div class="header-title">Bổ sung thông tin</div>
            <div class="header-close div-center" @click="onClosePopup"><img src='../../assets/img/x.jpg' /></div>
        </div>
        <!-- body -->
        <div class="body">
            <div class="body-image">
                <p>Ảnh đại diện</p>
                <img src="../../assets/img/default.png" />
            </div>
            <div class="body-right div-flex-column">
                <MSInput title="Tên bài tập" :defaultValue="name" :require="true" />
                <div class="div-flex-row div-select">
                    <MSCombobox title="Môn" :require="true" :defaultValue="subject"
                        :listSelect="['Toán', 'Tiếng Việt', 'Tiếng Anh', 'Tự nhiên xã hội', 'Lịch sử', 'Địa lý']" />
                    <MSCombobox title="Khối" :require="true" :defaultValue="grade"
                        :listSelect="['Khối 1', 'Khối 2', 'Khối 3', 'Khối 4', 'Khối 5']" />
                </div>
                <MSCombobox title="Chủ đề" defaultValue="Chọn chủ đề" :listSelect="['Chủ đề 1', 'Chủ đề 2']" />
                <MSInput title="Thẻ tìm kiếm" class="input-search" />
            </div>
        </div>
        <!-- nút button lấy bên popupAdd-->
        <div class="div-button">
            <MSButton title="Hủy" @click="onClosePopup"/>
            <MSButton title="Lưu" id="btn-save-new" />
        </div>
    </div>
</template>

<script>
import MSInput from '../input/MSInput.vue'
import MSCombobox from '../input/MSCombobox.vue'
import MSButton from '../button/MSButton.vue'
export default {
    name: "MSPopupAddInfor",
    props: ['name', 'subject', 'grade'],
    components: {
        MSInput,
        MSCombobox,
        MSButton
    },
    data() {
        return {

        }
    },
    methods: {
        /**
         * bắt sự kiện đóng popup
         * CreatedBy: Trịnh Huỳnh Đức (22-5-2023)
         */
        onClosePopup(){
            this.$emit('onClosePopup')
        }
    }
}
</script>

<style scoped>
.div-main {
    background-color: #ffffff;
    border-radius: 10px;
    width: 800px;
    height: auto;
    left: calc(50vw - 400px);
    top: 50px;
    position: absolute;
    z-index: 3;
}

.header {
    height: 84px;
    padding: 24px;
    position: relative;
}

.header-title {
    font-weight: 700;
    font-size: 28px;
    line-height: 36px;
    color: #4e5b6a;
}

.header-close {
    cursor: pointer;
    position: absolute;
    top: 8px;
    right: 8px;
    width: 32px;
    height: 32px;
}

.header-close img {
    width: 16px;
    height: 16px;
}

.body {
    display: flex;
    justify-content: space-between;
    padding: 0px 24px 24px 24px;
}

.body-image {
    width: 260px;
    height: 100%;
}

.body-image>p {
    padding-bottom: 4px;
    font-size: 14px;
    font-weight: 500;
}

.body-image img {
    height: auto;
    width: 100%;
}

.body-right {
    width: 452px;
    height: 100%;
    gap: 16px;
}

.div-select {
    gap: 12px;
}

.input-search img {
    height: 60px;
}
.div-button {
    display: flex;
    align-items: center;
    justify-content: end;
    padding: 0px 24px 24px 24px;
    gap:12px;
}
</style>