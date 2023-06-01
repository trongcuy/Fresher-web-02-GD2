<template>
    <div class="div-btn" :class="{'btn-small':dimention=='small'}">        
        <img :src="src" class="zoom-div">
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
        },
        dimention: {
            type: String,
            default: '',//kich thuowc to hay nho
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
    position: relative;
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
.btn-small {
    width: 40px;
    height: 40px;
}
.btn-small img {
    width: 40px;
    height: 40px;
    border-radius: 4px;
}
</style>
