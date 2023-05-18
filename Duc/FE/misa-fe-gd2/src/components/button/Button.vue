<template>
    <button class="button" :class="{ 'button-icon': src }">
        <img :src="src" v-if="src" />
        {{ title }}
    </button>
</template>

<script>
export default {
    name: 'Button',
    props: ["title", "src"]
}
</script>

<style scoped>
.button {
    height: 36px;
    border-radius: 4px;
    border: 1px solid #E0E0E0;
    min-width: 100px;
    background-color: #ffff;
    padding-left: 16px;
    padding-right: 16px;
    position: relative;
}

.button:hover {
    background-color: #F5F5F5;
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
