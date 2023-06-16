<template>
    <div class="popup">
        <div class="popup-container">
            <div class="popup-head">
                <div class="popup-top">
                    <div class="popup-text" >
                        <slot name="slotTitle"></slot>
                    </div>
                </div>
                <div class="popup-content">
                    <slot name="slotContent"></slot>
                </div>
            </div>
            <div class="popup-btn">
                <slot name="slotButton"></slot>
            </div>
        </div>
    </div>
</template>

<script setup>
import { computed } from 'vue';
import { useStore } from 'vuex';

const store = useStore();
// Biến trạng thái popup
const showPopup = computed(() => store.state.app.showPopup);
// Biến trạng thái dạng của popup
const popupStatus = computed(() => store.state.app.popupStatus);
/**
 * Đóng popup
 * CreatedBy VMHieu 23/05/2023
 */
const handleClosePopup = () => {
    store.dispatch('showPopup', false);
}
</script>

<style scoped>
.popup {
    position: fixed;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    z-index: 4000;
    height: 100%;
    background-color: rgba(0,0,0,.5);
    overflow: auto;
    scrollbar-width: thin;
}

.popup-container {
    position: absolute;
    transform: translate(-50%, -50%);
    top: 50%;
    left: 50%;
    padding: 0;
    width: 414px;
    display: inline-block;
    background-color: #fff;
    border-radius: 4px;
    border: 1px solid #ebeef5;
    font-size: 18px;
    box-shadow: 0 2px 12px 0 rgba(0,0,0,.1);
    text-align: left;
    overflow: hidden;
    vertical-align: middle;
    backface-visibility: hidden;
}

.popup-top {
    padding: 10px 16px;
    background-color: #eaebf5;
    box-sizing: border-box;
}

.popup-icon {
    width: 24px;
    height: 24px;
    min-width: 24px;
    min-height: 24px;
    cursor: pointer;
    display: flex;
    align-items: center;
}

.popup-text{
    font-weight: 500;
    font-size: 14px;
    line-height: 20px;
}

.popup-btn{
    padding: 24px;
    display: flex;
    justify-content: flex-end;
    text-align: right;
    box-sizing: border-box;
}

.popup-content{
    padding: 24px 24px 0;
    color: #606266;
    font-size: 14px;
    box-sizing: border-box;
}
</style>
    