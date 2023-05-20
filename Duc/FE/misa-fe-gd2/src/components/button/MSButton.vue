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
    height: 40px;
    border-radius: 10px;
    border: 0px solid #E0E0E0;
    padding: 0 12px;
    position: relative;
    font-size: 14px;
    font-weight: 550;
    background-color: inherit;
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
