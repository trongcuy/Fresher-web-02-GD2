<template>
    <div class="card" :class="{'card-rightwrong': typePopupAdd=='rightwrong'}">
        <div class="head" :class="{'head-right-check': typePopupAdd=='rightwrong'}">
            <div class="head-left" v-if="typePopupAdd=='select'">{{ answer }}</div>
            <div class="head-right" >
                <div v-if="typePopupAdd=='select'"><img src="../../assets/img/icon_delete.9097d258.svg"/></div>
                <div v-if="typePopupAdd=='select'"><img src="../../assets/img/icon_image.svg"/></div>
                <div><img src="../../assets/img/ic_uncheck.ceabec80.svg"/></div>
            </div>
        </div>
        <div class="body">
            <textarea>{{ title }}</textarea>
        </div>
    </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
    name: 'MSCardAnswer',
    props: {
        title: {
            type: String,
            default: ''
        },
        answer: {
            type: String,
            default: 'A'
        }
    },
    computed: {
        ...mapGetters(['typePopupAdd'])
    }
}
</script>

<style scoped>
.card {
    width: 100%;
    height: 200px;
    box-sizing: border-box;
    background-color: #fff;
    border-radius: 10px;
    background-color: rgb(255, 214, 240);
    position: relative;
}
.card-rightwrong {
    width: 197px;
}
.card-rightwrong textarea {
    font-size: 24px;
    color: #4e5b6a;
}
img {
    height: 18px;
    width: 18px;
}
.head {
    height: 48px;
    padding: 8px;
    display: flex;
    align-items: center;
    justify-content: space-between;
}
.head-left {
    width: 15px;
    height: 28px;
    margin: 4px 0px 0px 4px;
    font-size: 20px;
    line-height: 28px;
    font-weight: 700;
    color: #4E5B6A;
}
.head-right {
    display: flex;
    align-items: center;  
}
.head-right > div {
    width: 32px;
    height: 32px;
    background-color: #fff;
    margin-left: 8px;
    border-radius: 10px;
    display: flex;
    align-items: center;
    justify-content: center;
}
.head-right-check {
    justify-content: end;
}
.body {
    position: absolute;/*kỹ thuật tràn toàn bộ cha*/
    top: 48px;
    left: 8px;
    right: 8px;
    bottom: 8px;
    padding-top: 32px;
    border-radius: 10px;
    display: flex;
    justify-content: center;
}
textarea {
    color: #606266;
    resize: none;
    border: none;
    outline: none;
    line-height: 40px;
    height: 40px;
    text-align: center;
    overflow: hidden;
    background-color: inherit;
    font-weight: 700;
    font-size: 18px;
}
</style>
