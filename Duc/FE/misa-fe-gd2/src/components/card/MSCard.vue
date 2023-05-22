<template>
    <div class="card">
        <div class="grade">{{ grade + ' - ' + subject }}</div>
        <img src="../../assets/img/toan.png" />
        <div class="div-info">
            <div class="div-title">
                <p>{{ title }}</p>
                <div class="icon"><img src="https://sisapapp.misacdn.net/lms/img/icon_option.90d8b4a5.svg" /></div>
            </div>
            <div class="div-state">
                <div class="div-num-question" v-if="numQuestion>0">
                    <img src="https://sisapapp.misacdn.net/lms/img/ic_number_aswer.e4999537.svg" />
                    <p>{{ numQuestion + ' Câu' }}</p>
                </div>
                <div class="state" :class="{'state-share':state=='Chia sẻ', 'state-editing':state=='Đang soạn'}" v-if="state">{{ state }}</div>
            </div>
            <div class="div-state">
                <img src="https://sisapapp.misacdn.net/lms/img/ic_User_16.2bc4d930.svg" />
                <p>{{ username }}</p>
            </div>
        </div>
    </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
    name: 'MSCard',
    props: {
        //khối
        grade: {
            type: String,
            default: ''
        },
        //môn
        subject: {
            type: String,
            default: ''
        },
        src: {
            type: String,
            default: ''
        },
        title: {
            type: String,
            default: ''
        },
        numQuestion: {
            type: Number,
            default: 0
        },
        state: {
            type: String,
            default: ''
        }
    },
    computed: {
        ...mapGetters(['username'])
    }
}
</script>

<style scoped>
p {
    margin: 0px;
    color: #4e5b6a;
}
.card {
    width: 320px;
    height: 206px;
    box-sizing: border-box;
    background-color: #fff;
    border-radius: 10px;
    margin-bottom: 24px;
    position: relative;
    box-shadow: rgba(90, 125, 141, 0.16) 0px 3px 20px;
}

.card>img {
    width: 320px;
    height: 102px;
    border-radius: 10px 10px 0px 0px;
}

.grade {
    height: 36px;
    width: auto;
    font-size: 16px;
    color: #fff;
    font-weight: 700;
    padding-left: 8px;
    padding-right: 12px;
    border-radius: 10px 0px 10px 0px;
    background-color: #8a6bf6;
    position: absolute;
    top: 0px;
    left: 0px;
    display: flex;
    justify-content: center;
    align-items: center;
}

.div-title {
    display: flex;
    position: relative;
}

.div-title>p {
    font-size: 16px;
    font-weight: 700;
    color: #4e5b6a;
    margin: 0px;
}

.icon {
    position: absolute;
    top: 0px;
    right: -12px;
    height: 32px;
    width: 32px;
    display: flex;
    justify-content: center;
    align-items: center;
}

.icon img {
    width: 4px;
    height: 20px;
}

.div-info {
    display: flex;
    padding: 12px;
    height: 104px;
    box-sizing: border-box;
    margin-top: -6px;
    flex-direction: column;
    justify-content: space-between;
    background-color: #fff;
}

.div-state {
    display: flex;
    height: 20px;
}
.state {
    font-size: 12px;
    padding: 3px 5px 5px 5px;
    border-radius: 6px;
    text-align: center;
    margin-left: 6px;
}
.state-editing {
    color: #8a6bf6;
    background: #e8e1fd;
}
.state-share {
    background-color: rgba(0,197,66,.1803921568627451);
    color: #00c542;
}
.div-num-question {
    display: flex;
    justify-content: center;
}

.div-num-question p {
    margin: 0px;
}

img {
    height: 16px;
    width: 16px;
    margin-right: 8px;
}
</style>
