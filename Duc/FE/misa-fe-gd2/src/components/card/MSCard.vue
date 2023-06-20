<template>
    <div class="card">
        <div class="grade">{{ exercise.gradeName + ' - ' + exercise.subjectName }}</div>
        <img :src="urlExercise" />
        <div class="div-info">
            <div class="div-title">
                <p>{{ exercise.exerciseName }}</p>
                <div v-click-outside="onClickOutside">
                    <div class="icon" @click="showOption" @click.stop=""><img :src="iconOption" /></div>
                    <div class="div-menu" v-if="showMenu" @click.stop="">
                        <p v-for="(item, index) in listMenu" @click="onClickOptionMenu(item)">{{ item }}</p>
                    </div>
                </div>
            </div>
            <div class="div-state">
                <div class="div-num-question" v-if="exercise.numQuestion > 0">
                    <img :src="iconNum" />
                    <p>{{ exercise.numQuestion + ' Câu' }}</p>
                </div>
                <div class="state"
                    :class="{ 'state-share': exercise.exerciseState == '2', 'state-editing': exercise.exerciseState == '1' }"
                    v-if="exercise.exerciseState">
                    {{ resource.stateExercise[exercise.exerciseState] }}</div>
            </div>
            <div class="div-state">
                <img :src="iconUser" />
                <p>{{ username }}</p>
            </div>
        </div>
    </div>
</template>

<script>
import { mapGetters } from 'vuex'
import { mapActions } from 'vuex'
export default {
    name: 'MSCard',
    props: {
        //bài tập
        exercise: {
            type: Object,
            default: {}
        },
    },
    data() {
        const resource = window.Resource
        return {
            resource,//resource
            iconOption: 'https://sisapapp.misacdn.net/lms/img/icon_option.90d8b4a5.svg',
            iconNum: 'https://sisapapp.misacdn.net/lms/img/ic_number_aswer.e4999537.svg',
            iconUser: 'https://sisapapp.misacdn.net/lms/img/ic_User_16.2bc4d930.svg',
            showMenu: false,//Biến ẩn hiện menu card
            listMenu: ['Xem', 'Xóa'],//list menu của card
        }
    },
    computed: {
        ...mapGetters(['username']),
        urlExercise() {
            return this.buildImage(this.exercise.exerciseImage) ? this.buildImage(this.exercise.exerciseImage) : 'https://sisapapp.misacdn.net/lms/img/tiengviet1.edd81b92.png'
        }
    },
    methods: {
        ...mapActions([]),
        /**
         * bắt sự kiện click menu
         * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
         * @param {*} value 
         */
        onClickOptionMenu(value) {
            this.showMenu = false
            if (value == "Xóa")
                this.$emit('onClickRemove')
            if (value == "Xem")
                this.$emit('onClickOpen')
        },
        /**
         * bắt sự kiện click menu
         * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
         */
        showOption() {
            this.showMenu = !this.showMenu
        },
        /**
         * bắt sự kiện click menu
         * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
         */
        onClickOutside() {
            this.showMenu = false
        }
    }
}
</script>

<style scoped lang="scss">
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
    position: relative;
    box-shadow: rgba(90, 125, 141, 0.16) 0px 3px 20px;
    cursor: pointer;

    &>img {
        width: 100%;
        object-fit: cover;
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
        background-color: #00c542;
        position: absolute;
        top: 0px;
        left: 0px;
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .div-info {
        display: flex;
        padding: 12px;
        height: 104px;
        border-radius: 0px 0px 10px 10px;
        box-sizing: border-box;
        margin-top: -6px;
        flex-direction: column;
        justify-content: space-between;
        background-color: #fff;

        .div-title {
            display: flex;
            position: relative;

            &>p {
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

                img {
                    width: 4px;
                    height: 20px;
                }

            }

            .div-menu {
                position: absolute;
                top: 14px;
                right: 28px;
                padding: 12px;
                background-color: #fff;
                width: 136px;
                box-shadow: 0 2px 12px 0 rgba(0, 0, 0, .1);
                border-radius: 10px;
                z-index: 3;

                &>p {
                    padding: 8px;
                    font-size: 14px;

                }

                &>p:hover {
                    color: #8a6bf6;
                    font-weight: 700;
                }
            }
        }

        .div-state {
            display: flex;
            height: 20px;

            .div-num-question {
                display: flex;
                justify-content: center;

                p {
                    margin: 0px;
                }
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
                background-color: rgba(0, 197, 66, .1803921568627451);
                color: #00c542;
            }
        }

    }
}

img {
    height: 16px;
    width: 16px;
    margin-right: 8px;
}
</style>
