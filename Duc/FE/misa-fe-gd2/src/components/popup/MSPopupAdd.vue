<template>
    <div class="div-popup-add">        
        <div class="popup-header">
            <MSCombobox 
                customClass="combobox-popup"
                :listSelect="['Chọn đáp án', 'Đúng sai', 'Điền vào chỗ trống', 'Ghép nối', 'Câu hỏi nhóm', 'Tự luận']"
                defaultValue="Chọn đáp án" />
        </div>
    </div>
</template>

<script>
import MSCombobox from '../input/MSCombobox.vue'
export default {
    name: 'MSPopupAdd',
    components: {
        MSCombobox
    },
    props: {
        type: {
            type: String,
            default: ''
        },
    },
}
</script>

<style>
.div-popup-add{
    background-color: #ffffff;
    border-radius: 10px;
    width: calc(72vw);
    height: calc(83vh);
    overflow: hidden;
    left: calc(14vw);
    top: 24px;
    position: absolute;
    z-index: 3;
}
.popup-header {
    display: flex;
    align-items: center;
    justify-content: space-between;
    height: 40px;
}
.combobox-popup {
    width: 250px;
}
.combobox-popup > input {
    border-radius: 0px 0px 10px 0px;
    width: 100%;
}
</style>
