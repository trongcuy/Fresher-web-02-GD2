<template>
    <!-- phan tư comboboxtag -->
    <div class="block">
        <p v-if="title">{{ title }}</p>
        <div style="position: relative;" @click="onClickShowOption">
            <div class="input-select div-flex-row list-fill">
                <span v-for="(item, index) in modelValue" :key="index" @click.stop="">{{ item[labelField] }} <img src="../../assets/img/ic_x.svg"
                        @click="onRemoveFill(item)" /></span>
                <p class="placeholder" v-if="showPlaceHolder && this.modelValue.length==0">Chọn chủ đề</p>
                <span contenteditable @input="() => { showPlaceHolder = false }" ref="spanEdit"
                    @blur="onBlurInput" v-click-outside="onBlurInput"></span>
            </div>
            <!-- icon select -->
            <div class="icon-select" v-show="this.modelValue.length==0"><img :src="src" /></div>
            <!-- khoi lua chon -->
            <div class="div-option" v-if="isShowOption" v-click-outside="() => { isShowOption = false }">
                <p v-for="(item, index) in data" :key="index" class="option" @click="setListValue(item)">{{ item[labelField] }}</p>
            </div>
        </div>
        <!-- icon x -->
        <div v-if="this.modelValue.length>0" class="icon-x" @click="resetListValue"><img
                src="../../assets/img/icons8-x-64.png" /></div>
    </div>
</template>

<script>
export default {
    name: 'MSComboboxTag',
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
        modelValue: {
            type: Array,
            default: []
        },
        data: {
            type: Array,
            default: []
        },
        labelField: {
            type: String,
            default: "name"
        },
        valueField: {
            type: String,
            default: "id"
        },
    },
    data() {
        return {
            isShowOption: false, //hiển thị option
            showPlaceHolder: true,//hieenr thi placegolder
            selectItems: [],//mảng lưu các giá trị được chọn
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
         * CreatedBy: Trịnh Huỳnh Đức-(1-6-2023)
         */
        setListValue(model) {
            this.selectItems = this.modelValue
            this.selectItems.push(model)
            this.$emit("update:modelValue", this.selectItems)
            // //kiểm tra xem chủ đề đã có chưa
            // for(let i=0;i<this.listValue.length;i++){
            //     if(value == this.listValue[i])
            //     return 
            // }
            // this.listValue.push(value)
            // this.showPlaceHolder = false
        },
        /*
         * hàm reset lại list value
         * CreatedBy: Trịnh Huỳnh Đức(1-6-2023)
         */
        resetListValue() {
            this.selectItems = []
            this.$emit("update:modelValue", this.selectItems)
            this.showPlaceHolder = true
        },
        /**
         * bắt sự kiện click xóa list fill
         * CreatedBy: Trịnh Huỳnh Đức 24-5-2023
         */
        onRemoveFill(model) {
            this.selectItems = this.modelValue
            const modelIndex = this.selectItems.findIndex(item => item[this.valueField] == model[this.valueField])
            this.selectItems.splice(modelIndex, 1)
            this.$emit("update:modelValue", this.selectItems)
            debugger
        },
         /**
         * bắt sự kiện blur ra khỏi input span
         * CreatedBy: Trịnh Huỳnh Đức 30-5-2023
         */
        onBlurInput(){
            //nếu blur ra mà span đã có giá trị thì ẩn placeholder
            const value = this.$refs.spanEdit.innerText
            if(!value)
                this.showPlaceHolder = true
        }
    },
}
</script>

<style scoped>
.block {
    padding: 0px;
    width: auto;
    box-sizing: border-box;
    position: relative;
}

.block>p {
    margin: 0px 0px 4px 0px
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
p {
    font-size: 14px;
}
img {
    width: 10px;
    height: 10px;
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

.div-option {
    padding: 6px 0px;
    width: 100%;
    border-radius: 10px;
    box-shadow: 0 2px 12px 0 rgba(0, 0, 0, .1);
    position: absolute;
    top: 45px;
    right: 0px;
    box-sizing: border-box;
    z-index: 11;
    background-color: #ffffff;
}

.icon-x {
    position: absolute;
    top: 10px;
    right: 4px;
    width: 25px;
    height: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
}

.icon-x:hover {
    cursor: pointer;
}

.icon-x img {
    width: 14px;
    height: 14px;
}
.list-fill {
    width: 100%;
    padding: 4px 12px 4px 12px;
    min-height: 40px;
    border-radius: 8px;
    border: 1px solid #b6b9ce;
    box-sizing: border-box;
    color: #606266;
    background-color: #fff;
    flex-wrap: wrap;
    font-size: 14px;
    outline: none;
    gap: 4px;
    box-sizing: border-box;
}
.list-fill span:not(:last-child) {
    border: 1px solid #b6b9ce;
    background-color: #f1f2f7;
    height: 32px;
    line-height: 30px;
    padding-left: 10px;
    padding-right: 20px;
    border-radius: 10px;
    margin-right: 2px;
    position: relative;
    width: fit-content;
}
.list-fill span:focus {
    outline: none;
}
.list-fill span:last-child {
    border: none;
    padding-left: 0px;
    min-width: 100px;
    z-index: 2;
}
.list-fill img {
    position: absolute;
    width: 12px;
    height: 12px;
    top: 9px;
    right: 4px;
}
.placeholder {
    position: absolute;
    top: 12px;
    left: 12px;
}
</style>
