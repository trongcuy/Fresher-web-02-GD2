<template>
    <div class="toast-box flex">
        <div class="border-wrap">
            <div class="border-inline" :class="lineClass"></div>
        </div>
        <div class="toast-message">
            <div class="toast-icon">
                <icon :class="toastClass"></icon>
            </div>
            <div class="toast-content">
                <slot></slot>
            </div>
            <div class="toast-close" @click="handleCloseToast">
                <icon class="icon icon-exit"></icon>
            </div>
        </div>
    </div>
</template>

<script setup>
import { useStore } from 'vuex';
import { defineProps } from 'vue';

const store = useStore();

const props = defineProps({
    // class xác định icon của toast
  toastClass: {
      type: String,
      default: ''
  },
  lineClass: null, // class của line
})
/**
 * Đóng toast
 */
const handleCloseToast = () => {
    store.dispatch('showToast', false);
}
</script>

<style scoped>
.toast-box{
    min-width: 500px;
    width: 500px;
    background: #fff;
    padding: 20px;
    border-radius: 20px;
    z-index: 1000;
    box-shadow: 0 0 16px rgba(23,27,42,.24);
    box-sizing: border-box;
}

.border-wrap{
    width: 3px;
    margin-right: 20px;
    display: flex;
    align-items: center;
}

.border-inline{
    border-radius: 1.5px;
    height: 32px;
    width: 100%;
}

.border-inline.fail{
    background-color: #ff6161;
}

.border-inline.success{
    background-color: #5ec576;
}

.border-inline.warning{
    background-color: #ffd200;
}

.toast-message{
    display: flex;
    align-items: center;
    width: 100%;
    height: 100%;
}

.toast-content{
    flex-grow: 1;
    margin: 0 20px;
    color: #4e5b6a;
}


.toast-close{
    display: flex;
    align-items: center;
    justify-content: center;
}

.toast-close icon{
    width: 20px;
    height: 20px;
    min-width: 20px;
    min-height: 20px;
}
</style>