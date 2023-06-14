<template>
    <div class="popup" ref="popup" @keyup.esc="closePopup" tabindex="0">
        <div class="popup-container">
            <div class="close-popup" >
                <span></span>
                <icon class="icon icon-exit" @click="closePopup"></icon>
            </div>
            <div class="img">
                <img :src="props.url" alt="">
            </div>
        </div>
    </div>
</template>

<script setup>
import { computed, defineProps, defineEmits, onMounted, ref } from 'vue';
import { useStore } from 'vuex';

const props = defineProps({
    url: null, // Đường dẫn ảnh
})

const emit = defineEmits(["closePopup"]);

const store = useStore();

const popup = ref('popup');

/**
 * Đóng popup
 * CreatedBy VMHieu 23/05/2023
 */
const closePopup = () => {
    emit("closePopup");
}

onMounted(() => {
    // Focus vào popup khi được mounted
    popup.value.focus();
})
</script>

<style scoped>
.popup {
    position: fixed;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    z-index: 2000;
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
    width: 840px;
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: column;
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

.popup-container>.img{
    padding: 0 24px 24px 24px;
    width: 100%;
    height: 100%;
    box-sizing: border-box;
    display: flex;
    align-items: center;
    justify-content: center;
}

.popup-container img {
    width: 80%;
    height: 80%;
}

.close-popup{
    padding: 24px;
    width: 100%;
    box-sizing: border-box;
    position: relative;
}

.close-popup icon{
    position: absolute;
    top: 16px;
    right: 16px;
}
</style>
    