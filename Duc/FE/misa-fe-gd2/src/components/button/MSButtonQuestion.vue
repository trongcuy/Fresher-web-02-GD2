<template>
    <div class="div-btn">        
        <img :src="src">
        <p class="title">{{ title }}</p>
    </div>
</template>

<script>
export default {
    name: 'MSButtonQuestion',
    props: {
        title: {
            type: String,
            default: ''
        },
        src: {
            type: String,
            default: 'https://sisapapp.misacdn.net/lms/img/pairing.4e1fb31f.svg'
        }
    },
}
</script>

<style scoped>
.div-btn {
    width: 64px;
    height: fit-content;
    display: flex;
    flex-direction: column;
    align-items: center;
}
img {
    width: 64px;
    height: 64px;
    border-radius: 10px;
}
.title {
    text-align: center;
    margin-top: 12px;
    white-space: nowrap; 
    color: #4e5b6a;
}
</style>
