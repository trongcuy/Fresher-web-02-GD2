<template>
    <!--Thanh header  -->
    <div class="div-header-main">
        <div class="div-header">
            <!-- Thanh chứa tên -->
            <div class="div-name">
                <img class="icon-back" src="../../../assets/img/icon_back.svg" @click="onClickBack" />
                <img class="image" :src="this.urlImageExercise" />
                <input type="text" :placeholder="resource.input.nameExercise" v-model="exercise.exerciseName" @input="onInput"/>
            </div>
            <!-- Thanh chứa các menu -->
            <div class="div-menu">
                <div class="menu-left">
                    <MSCombobox class="cb-subject" :defaultValue="exercise.subjectName" :listSelect="subjectOptions"
                        @setDefaultValue="setValueSubject" />
                    <MSCombobox class="cb-grade" :defaultValue="exercise.gradeName" :listSelect="gradeOptions"
                        @setDefaultValue="setValueGrade" />
                    <MSButton :title="resource.button.addInfor" @click="onClickAddInfor" />
                    <MSButton :title="resource.button.changeType" />
                </div>

                <div class="menu-right">
                    <div class="div-help" v-tooltip:top="'Hướng dẫn'"><img src="../../../assets/img/ic_help.svg"/></div>
                    <MSButton :title="resource.button.tryDo" />
                    <MSButton :title="resource.button.complete" class="btn-complete" @click="onClickComplete" />
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import MSButton from '../../button/MSButton.vue'
import MSCombobox from '../../input/MSCombobox.vue'
import { mapGetters } from 'vuex'
export default {
    name: "CreateHeader",
    props: {
        exercise: {
            type: Array,
            default: []
        },
    },
    components: {
        MSButton,
        MSCombobox
    },
    data() {
        const resource = window.Resource
        return {
            resource
        }
    },
    computed: {
        ...mapGetters([
            'subjectOptions',
            'gradeOptions',
            'urlImageExercise'
        ])
    },
    methods: {
        /**
         * click back về trang chủ
         * CreatedBy: Trịnh Huỳnh Đức (20-5-2023)
         */
        onClickBack() {
            this.$router.push("/course/personal")
        },
        /**
         * sự kiên set giá trị môn học của combobox
         * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
         */
        setValueSubject(value) {
            this.$emit('setValueSubject', value)
        },
        /**
         * sự kiên set giá trị khối của combobox
         * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
         */
        setValueGrade(value) {
            this.$emit('setValueGrade', value)
        },

        /**
         * bắt sự kiện click nút bổ sung thêm thông tin
         * CreatedBy: Trịnh Huỳnh Đức (22-5-2023)
         */
        onClickAddInfor() {
            this.$emit('onClickAddInfor')
        },
        /**
         * bắt sự kiện click hoanf thành
         * CreatedBy: Trịnh Huỳnh Đức (22-5-2023)
         */
        onClickComplete() {
            this.$emit('onClickComplete')
        },
        onInput(){
            this.$emit('setNameExercise')
        }
    }
}
</script>
<style scoped lang="scss">
.div-header-main {
    width: 100%;
    height: 128px;
    padding: 16px;
    box-sizing: border-box;
    box-shadow: 0 3px 20px rgba(90, 125, 141, .16);

    .div-header {
        height: 100%;
        display: flex;
        flex-direction: column;
        justify-content: space-between;

        .div-name {
            width: 100%;
            height: 40px;
            display: flex;
            align-items: center;

            .icon-back {
                margin-left: 8px;
                margin-right: 16px;
            }

            .image {
                width: 40px;
                height: 40px;
                border-radius: 50px;
                margin-right: 16px;
                object-fit: cover;
            }

            input {
                border: none;
                outline: none;
                color: #4e5b6a;
                font-size: 24px;
                line-height: 32px;
                font-weight: 700;
                width: 100%;
                height: 32px;
            }
        }

        .div-menu {
            padding-left: 48px;
            display: flex;
            width: auto;
            height: 40px;
            justify-content: space-between;

            .menu-left {
                display: flex;
                gap: 12px;

                .cb-subject {
                    width: 160px;
                }

                .cb-grade {
                    width: 100px;
                }
            }

            .menu-right {
                display: flex;
                align-items: center;
                gap: 12px;
                padding-right: 6px;

                .btn-complete {
                    background-color: #8a6bf6;
                    color: #fff;
                }

                .div-help {
                    background-color: #f1f2f7;
                    border: 1px solid #b6b9ce;
                    height: 40px;
                    border-radius: 10px;
                    width: 40px;
                    height: 40px;
                    display: flex;
                    justify-content: center;
                    align-items: center;
                    cursor: pointer;
                    position: relative;
                }
            }
        }
    }
}
</style>
