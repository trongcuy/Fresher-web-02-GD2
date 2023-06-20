<template>
    <!-- phan tư combobox -->
    <div class="block">
        <p v-if="title">{{ title }}<span v-if="require"> *</span></p>
        <div class="div-main" id="div-main" :class="customClass" @click="onClickShowOption"
            v-click-outside="onClickOutside">
            <input type="text" class="input-select" v-model="optionValue"
                :placeholder="defaultValue ? defaultValue : listSelect[0]" readonly />
            <div class="icon-select" v-show="optionValue == this.default || !iconRemove"
                :class="{ 'rotate-effect': this.isShowOption, 'rotate-effect2': !this.isShowOption }">
                <img :src="src" />
            </div>
            <div class="div-option"
                :class="{ 'div-option-reverse2': optionReverse && !title, 'div-option-reverse': optionReverse && title }"
                v-if="isShowOption">
                <p v-for="(item, index) in listSelect" :class="{ 'item-selected': item == this.optionValue }" :key="index"
                    class="option" @click="setOptionValue(item)">{{ item }}
                </p>
            </div>

        </div>
        <!-- icon x -->
        <div v-if="optionValue != this.default && iconRemove" class="icon-x" @click="setOptionValue(this.default)"><img
                src="../../assets/img/icons8-x-64.png" /></div>
    </div>
</template>

<script>
export default {
    name: 'MSCombobox',
    props: {
        title: {
            type: String,
            default: ''
        },
        defaultValue: {
            type: String,
            default: ''
        },
        src: {
            type: String,
            default: 'https://cegovapp.misacdn.net/cegov/img/ic_drop-down.fa70eead.svg'
        },
        listSelect: {
            type: Array,
            default: () => []
        },
        optionReverse: {
            type: Boolean,
            default: false
        },
        customClass: {
            type: String,
            default: ''
        },
        require: {
            type: Boolean,
            default: false
        },
        iconRemove: false
    },
    data() {
        return {
            isShowOption: false, //hiển thị option
            optionValue: this.defaultValue,//giá tị của option đã chọn
            default: this.defaultValue,//giá trị default ban đầu dùng để so sánh ẩn nút x
        }
    },
    watch: {
        defaultValue(newValue) {
            this.optionValue = newValue
        }
    },
    methods: {
        /*
         * gọi khi click mở option lên
         * CreatedBy: Trịnh Huỳnh Đức-18-5-23
         */
        onClickShowOption() {
            this.isShowOption = !this.isShowOption
        },
        /*
         * hàm set option value khi click chọn option
         * CreatedBy: Trịnh Huỳnh Đức-18-5-23
         */
        setOptionValue(item) {
            this.optionValue = item//set option
            //gửi giá trị này lên cha nó để thay đổi props default value
            this.$emit("setDefaultValue", this.optionValue)
        },
        /*
         * hàm click outside
         * CreatedBy: Trịnh Huỳnh Đức-18-5-23
         */
        onClickOutside() {
            this.isShowOption = false
        }
    }
}
</script>

<style scoped lang="scss">
.block {
    padding: 0px;
    width: auto;
    box-sizing: border-box;
    position: relative;

    &>p {
        margin: 0px 0px 4px 0px;

        span {
            color: red;
        }
    }

    .div-main {
        position: relative;

        input {
            width: 100%;
            padding-left: 12px;
            padding-right: 34px;
            height: 40px;
            border-radius: 8px;
            border: 1px solid #b6b9ce;
            box-sizing: border-box;
            color: #606266;
            cursor: pointer;
        }

        input:focus {
            outline: none;
        }

        .input-select:focus {
            border-color: #8a6bf6;
        }

        .icon-select {
            position: absolute;
            top: 0px;
            right: 4px;
            width: 25px;
            height: 40px;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .icon-select:hover {
            cursor: pointer;
        }

        // hiệu ứng icon xoay
        .rotate-effect img {
            transition: transform 0.3s ease-in-out;
            transform: rotate(-180deg);
        }

        .rotate-effect2 img {
            transition: transform 0.3s ease-in-out;
            transform: rotate(0deg);
        }

        .div-option-reverse {
            top: -110px;
            left: 0px;
        }

        /* danhf cho input ko cos title */
        .div-option-reverse2 {
            top: -140px;
            left: 0px;
        }

        .div-option {
            padding: 6px 0px;
            width: 100%;
            max-width: 245px;
            border-radius: 10px;
            box-shadow: 0 2px 12px 0 rgba(0, 0, 0, .1);
            position: absolute;
            top: 45px;
            right: 0px;
            box-sizing: border-box;
            z-index: 11;
            background-color: #ffffff;

            p {
                cursor: pointer;
            }

            .option {
                width: 100%;
                height: 33px;
                margin: 0;
                padding: 8px 16px;
                box-sizing: border-box;
                color: #606266;
            }

            .option:hover {
                background-color: #ece7fe;
            }

            .item-selected {
                color: #8a6bf6;
            }
        }
    }

    .icon-x {
        position: absolute;
        top: 0px;
        right: 4px;
        width: 25px;
        height: 40px;
        display: flex;
        justify-content: center;
        align-items: center;

        img {
            width: 14px;
            height: 14px;
        }
    }

    .icon-x:hover {
        cursor: pointer;
    }
}

/* combobox của popupadd */
.combobox-popup {
    width: 312px;

    &>input {
        border-radius: 0px 0px 10px 0px !important;
        width: 100%;
        padding-left: 68px !important;
        font-weight: 700;
        font-size: 16px;
        background-color: #f8e373;
        border: none !important;
    }

    &>input:hover {
        border: none;
    }
}

img {
    width: 10px;
    height: 10px;
}

p {
    font-size: 14px;
}
</style>









