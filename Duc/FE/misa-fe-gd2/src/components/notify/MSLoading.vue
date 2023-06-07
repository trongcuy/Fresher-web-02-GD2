<template>
    <!-- loading -->
    <div class="div-load">
        <div class="loader"></div>
    </div>
</template>
<script>
export default {
    name: "MSLoading"
}
</script>
<style>
/* loading */
.div-load {
    position: fixed;
    top: calc(50vh - 30px);
    left: calc(50% - 30px);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 1000;
}

.loader {
    border: 3px solid #f3f3f3;
    /* Màu nền cho vòng tròn */
    border-top: 3px solid #8A6BF6;
    /* Màu của vòng tròn */
    border-radius: 50%;
    /* Đường cong của vòng tròn */
    width: 30px;
    /* Kích thước vòng tròn */
    height: 30px;
    /* Kích thước vòng tròn */
    animation: spin 0.8s linear infinite;
    /* Tạo hiệu ứng quay vòng */

}

@keyframes spin {
    0% {
        transform: rotate(0deg);
    }

    100% {
        transform: rotate(360deg);
    }
}</style>