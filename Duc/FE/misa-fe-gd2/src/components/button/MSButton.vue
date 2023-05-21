<template>
    <button class="button" :class="{ 'button-icon': src }">
        <img :src="src" v-if="src" />
        {{ title }}
    </button>
</template>

<script>
export default {
    name: 'MSButton',
    props: ["title", "src"]
}
</script>

<style scoped>
.button {
    min-width: 113px;
    background-color: #f1f2f7;
    border: 1px solid #b6b9ce;
    height: 40px;
    border-radius: 10px;
    padding: 0 12px;
    position: relative;
    font-size: 14px;
    color: #606266;
    font-weight: 500;
}

.button:hover {
    opacity: .8;
}

img {
    position: absolute;
    top: 7px;
    left: 10px;
    height: 20px;
    width: 20px
}
.button-icon {
    padding-left: 35px;
}</style>
