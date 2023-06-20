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

<style scoped lang="scss">
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
    cursor: pointer;

    img {
        position: absolute;
        top: 7px;
        left: 10px;
        height: 20px;
        width: 20px
    }
}

.button:hover {
    opacity: .8;
}

.button:active {
    transform: translateY(4px);
    box-shadow: inset 1px 1px 4px #c9c8c7;
    transition: all .1s ease;
}

.button-icon {
    padding-left: 35px;
}</style>
