<template>
    <div class="banner-container">
        <div class="background-banner">
            <img :src="bannerImg" alt="">
        </div>
        <div>
            <slot name="content"></slot>
        </div>
    </div>
</template>

<script setup>
// Các biến lưu đường dẫn
const bannerImg = require("@/assets/img/banner.png");
</script>

<style scoped>
.banner-container{
    padding: 0.5rem;
    width: calc(100% - 16px);
    height: auto;
}

.background-banner{
    position: relative;
    border-radius: 10px;
}

.background-banner img{
    width: 100%;
    border-radius: 10px;
}
</style>