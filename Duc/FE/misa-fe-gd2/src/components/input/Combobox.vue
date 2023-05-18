<template>
    <!-- phan tư combobox -->
    <div class="block">
        <p v-if="title">{{ title }}</p>
        <div style="position: relative;">
            <input type="text" class="input-select" v-on:input="setOptionValue(optionValue)" v-model="optionValue" />
            <div class="icon-select" @click="onClickShowOption"><img :src="src" /></div>
            <div class="div-option"
                :class="{ 'div-option-reverse2': optionReverse && !title, 'div-option-reverse': optionReverse && title }"
                v-if="isShowOption" v-click-outside="() => { isShowOption = false }">
                <p v-for="(item, index) in listSelect" :key="index" class="option" @click="setOptionValue(item)">{{ item }}
                </p>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'InputSelect',
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
            default: ''
        },
        listSelect: {
            type: Array,
            default: () => []
        },
        optionReverse: {
            type: Boolean,
            default: false
        }
    },
    data() {
        return {
            isShowOption: false, //hiển thị option
            optionValue: this.defaultValue//giá tị của option đã chọn
        }
    },
    methods: {
        /*
         * CreatedBy: Trịnh Huỳnh Đức-18-5-23
         * gọi khi click mở option lên
         */
        onClickShowOption() {
            this.isShowOption = !this.isShowOption
        },
        /*
         * CreatedBy: Trịnh Huỳnh Đức-18-5-23
         * hàm set option value khi click chọn option
         */
        setOptionValue(item) {
            this.optionValue = item//set option
            //gửi giá trị này lên cha nó để thay đổi props default value
            this.$emit("setDefaultValue", this.optionValue)
            this.isShowOption = false//đóng option
        }
    }
}
</script>

<style scoped>
.block {
    padding: 0px;
    width: 100%;
    box-sizing: border-box;
}

.block>p {
    margin: 0px 0px 8px 0px
}

.input-select {
    width: 100%;
    padding-left: 8px;
    padding-right: 32px;
    height: 33px;
    border-radius: 4px;
    border: 1px solid #E0E0E0;
    box-sizing: border-box;
}


.icon-select {
    position: absolute;
    top: 0px;
    right: 0px;
    width: 34px;
    height: 33px;
    display: flex;
    justify-content: center;
    align-items: center;
}

.icon-select:hover {
    cursor: pointer;
}

.option {
    width: 100%;
    height: 33px;
    margin: 0;
    padding-left: 8px;
    padding-top: 8px;
    box-sizing: border-box;
    border-radius: 4px;
}

.option:hover {
    background-color: #dfecff;
}

input:focus {
    outline: none;
}

input:hover,
input:focus {
    border: 1px solid #0062CC;
}

.div-option {
    margin-top: 4px;
    padding: 8px;
    width: 100%;
    border-radius: 4px;
    box-shadow: 0 0 16px rgb(23 27 42 / 16%);
    position: absolute;
    top: 32px;
    left: 0px;
    box-sizing: border-box;
    z-index: 11;
    background-color: #ffffff;
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

img {
    width: 10px;
    height: 10px;
}
</style>
