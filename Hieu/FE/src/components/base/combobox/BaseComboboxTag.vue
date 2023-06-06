<template>
    <div>
      <div class="combobox" :autoFocus="autoFocus" :class="cbbClass" v-clickOutside="hideListData" ref="combobox">
        <div class="box-tag" @click="btnSelectDataOnClick">
            <div v-for="(item, index) in dataInputSpan" :key="index">
                <span class="input-tag">
                    {{ item }}
                    <span @click="removeTag(item)" @click.stop="">
                        <img class="remove-tag" :src="removeTagImg" alt="">
                    </span>
                </span>
            </div>
            <input
            :id="id"
            type="text"
            class="input combobox__input"
            ref="input"
            v-model="textInput"
            @input="inputOnChange"
            @keydown="selecItemUpDown"
            @mouseover="handleMouse"
            :tabindex="tabindex"
            :placeholder="placeholder"
            :resetValue="resetValue"
            :bidingValue="bidingValue"
            autocomplete="off"
            />
        </div>
        <div class="combobox-icon" @click="btnSelectDataOnClick" v-if="!clearIp">
            <img src="@/assets/img/icon_arrow.svg" alt="">
        </div>
        <div class="combobox-icon icon-clear" @click="clearInput" v-if="clearIp && openClear">
            <img src="@/assets/img/clear-input.jpg" alt="">
        </div>
        <div
          v-if="isShowListData"
          class="combobox__data"
          :class="{
            'combobox-top': isPositionTop
          }"
          ref="combobox__data"
        >
          <a
            class="combobox__item"
            v-for="(item, index) in dataFilter"
            :class="{
              'combobox__item--focus': index == indexItemFocus,
              'combobox__item--selected': index == indexItemSelected,
            }"
            :key="item[this.propValue]"
            :ref="'toFocus_' + index"
            :value="item[this.propValue]"
            @click="itemOnSelect(item, index)"
            @focus="saveItemFocus(index)"
            @keydown="selecItemUpDown(index)"
            @keyup="selecItemUpDown(index)"
            tabindex="1"
          >
            <span>{{ item[this.propText] }}</span>
            <div class="check-selected">
              <icon class="icon icon-check"></icon>
            </div>
          </a>
        </div>
      </div>
      <div class="combobox-error" v-show="showError">{{ errorMsg }}</div>
    </div>
</template>
<script>
/* eslint-disable */
import { mapState } from 'vuex';

function removeVietnameseTones(str) {
    if(str && typeof(str) == "string") {
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
    str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
    str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
    str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
    str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
    str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
    str = str.replace(/Đ/g, "D");
    // Some system encode vietnamese combining accent as individual utf-8 characters
    // Một vài bộ encode coi các dấu mũ, dấu chữ như một kí tự riêng biệt nên thêm hai dòng này
    str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
    str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư
    // Remove extra spaces
    // Bỏ các khoảng trắng liền nhau
    str = str.replace(/ + /g, " ");
    str = str.trim();
    // Remove punctuations
    // Bỏ dấu câu, kí tự đặc biệt
    str = str.replace(
        /!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'|\"|\&|\#|\[|\]|~|\$|_|`|-|{|}|\||\\/g,
        " "
    );
    return str;
    }
    
}

const keyCode = {
    Enter: 13,
    ArrowUp: 38,
    ArrowDown: 40,
    ESC: 27,
};

export default {
    name: "MSCombobox",
    props: {
    getValue: null,     // Dấu hiệu cần lấy dataValue
    openClear: true,    // Hiện nút xóa input
    cbbClass: null,     // Class bố sung của combobox
    valueCombobox: null,     // Giá trị biding vào
    resetValue: true,   // Reset lại value của cbb
    propValue: String,  // prop để lấy value
    propText: String,   // prop để lấy text hiển thị
    placeholder: {      // giá trị placeholder
        type: String,
        default: ""
    },
    data: [],           // dữ liệu combobox
    isPositionTop: {    // Vị trí positiop top
        type: Boolean,
        default: false
    },                  
    errorMsg: {         // Msg lỗi
        type: String,
        default: ""
    },                  
    id: {               // id của cbb
        type: String,
        default: ""
    },                  // tabindex
    tabindex: {
        type: Number,
        default: -1
    },                  // Focus vào vị trí nào lúc ban đầu
    autoFocus: {
        type: Number,
        default: null
    }
    },
    watch: {
        /**
         * Lấy giá trị trong combobox gửi lên cho component cha
         * VMHieu 01/06/2023
         */
        getValue: function() {
            this.$emit("update:modelValue", this.dataValue);
        },
        /**
         * Thực hiện 1 số thao tác khi data thay đổi
         * VMHieu 31/05/2023
         */
        data: function() {
            this.dataInputSpan = [];
            this.dataValue = [];
            // Biding các dữ liệu có sẵn
            if (Object.getOwnPropertyNames(this.valueCombobox).length != 0) {
                for (let i = 0; i < this.data.length; i++) {
                    for (let j = 0; j < this.valueCombobox.length; j++) {
                        if (this.data[i][this.propValue] == this.valueCombobox[j]) {
                            if (!this.dataInputSpan.includes(this.data[i][this.propText])) {
                                this.dataInputSpan.push(this.data[i][this.propText]);
                            }
                            if (!this.dataValue.includes(this.data[i][this.propValue])) {
                                this.dataValue.push(this.data[i][this.propValue]);
                            }
                        }
                    }
                } 
                if (this.dataInputSpan.length > 0)
                {
                    this.$refs.input.placeholder = "";
                    if (this.openClear) {
                        this.clearIp = true;
                    }
                }
            } else {
                this.dataInputSpan = [];
                this.dataValue = [];
                this.$refs.input.placeholder = this.placeholder;
                this.clearIp = false;
                this.indexItemFocus = null;
                this.indexItemSelected = null;
                this.$emit("update:modelValue", "");
            }
        },
        valueCombobox() {
            this.dataInputSpan = [];
            this.dataValue = [];
            // Biding các dữ liệu có sẵn
            if (Object.getOwnPropertyNames(this.valueCombobox).length != 0) {
                for (let i = 0; i < this.data.length; i++) {
                    for (let j = 0; j < this.valueCombobox.length; j++) {
                        if (this.data[i][this.propValue] == this.valueCombobox[j]) {
                            if (!this.dataInputSpan.includes(this.data[i][this.propText])) {
                                this.dataInputSpan.push(this.data[i][this.propText]);
                            }
                            if (!this.dataValue.includes(this.data[i][this.propValue])) {
                                this.dataValue.push(this.data[i][this.propValue]);
                            }
                        }
                    }
                } 
                if (this.dataInputSpan.length > 0)
                {
                    this.$refs.input.placeholder = "";
                    if (this.openClear) {
                        this.clearIp = true;
                    }
                }
            } else {
                this.dataInputSpan = [];
                this.dataValue = [];
                this.$refs.input.placeholder = this.placeholder;
                this.clearIp = false;
                this.indexItemFocus = null;
                this.indexItemSelected = null;
                this.$emit("update:modelValue", "");
            }
        }
    },
    computed: 
        mapState({
            showFormExercise : (state) => state.exercise.showFormExercise,
        }),
    methods: {
    /**
     * Lưu lại index của item đã focus
     * CreatedBy VMHieu 01/04/2023
     */
    saveItemFocus(index) {
        this.indexItemFocus = index;
    },
    /**
     * Sự kiện hover khi có lỗi
     * CreatedBy VMHieu 09/05/2023
     */
    handleMouse() {
        let input = this.$refs.input;
        let combobox = this.$refs.combobox;
        if (input.classList.contains("input-error")) {
        combobox.classList.add("cbb-error");
        } else {
        combobox.classList.remove("cbb-error");
        }
    },

    /**
     * Ẩn danh sách item
     * CreatedBy VMHieu 01/04/2023
     */
    hideListData() {
        this.isShowListData = false;
    },

    /**
     * Nhấn vào button thì hiển thị hoặc ẩn List Item
     * CreatedBy VMHieu 01/04/2023
     */
    btnSelectDataOnClick() {
        this.dataFilter = this.data;
        this.isShowListData = !this.isShowListData;
    },

    /**
     * Click chọn item trong danh sách
     * CreatedBy VMHieu 01/04/2023
     */
    itemOnSelect(item, index) {
        const text = item[this.propText];
        const value = item[this.propValue];
        // this.indexItemSelected = index;
        // this.indexItemFocus = index;
        this.isShowListData = false;
        if (!this.dataInputSpan.includes(text)){
            this.dataInputSpan.push(text);
        }

        if (!this.dataValue.includes(value)){
            this.dataValue.push(value);
        }
        
        if (this.openClear) {
            this.clearIp = true;
        }
        if (this.dataInputSpan.length > 0) {
            this.showInput = false;
            this.$refs.input.placeholder = "";
            this.$refs.input.focus();
        }

        
    },
    /**
     * Ấn nút clear input và cập nhật lại value
     * VMHieu 31/05/2023
     */
    clearInput() {
        this.dataInputSpan = [];
        this.showInput = true;
        this.$refs.input.placeholder = this.placeholder;
        this.indexItemSelected = null;
        this.indexItemFocus = null;
        this.clearIp = false;
        this.dataValue = [];
    },
    /**
     * Hàm check input để in ra lỗi
     * CreatedBy VMHieu 31/03/2023
     */
    handleChange() {
        let arrs = this.data;
        let flag = 0;
        if (this.data && this.$refs.input.placeholder && typeof(this.$refs.input.placeholder) == "string") {
        for (const arr in arrs) {
            let str1 = removeVietnameseTones(arrs[arr][this.propText]).toLowerCase().replace(" ", "");
            let str2 = removeVietnameseTones(this.$refs.input.placeholder).toLowerCase().replace(" ", "");
            if (str1.includes(str2)){
            flag++;
            }
        }

        if (flag == 0) {
            this.showError = true;
            this.isShowListData = false;
        } else {
            this.showError = false;
        }
        } else {
        this.showError = false;
        }

        if (this.$refs.input.placeholder == "") {
        this.dataFilter = this.data;
        }
        
    },

    /**
     * Nhập text thì thực hiện filter dữ liệu và hiển thị
     * CreatedBy VMHieu 01/04/2023
     */
    inputOnChange() {
        var me = this;
        // Thực hiện lọc các phần tử phù hợp trong data:
        this.dataFilter = this.data.filter((e) => {
        if(me.textInput) {
            let text = removeVietnameseTones(me.textInput)
            .toLowerCase()
            .replace(" ", "");
            let textOfItem = removeVietnameseTones(e[me.propText])
            .toLowerCase()
            .replace(" ", "");
            return textOfItem.includes(text);
        }
        });

        this.handleChange();

        if (this.dataFilter.length > 0 || !me.textInput) {
        this.isShowListData = true;
        }
    },

    /**
     * Ấn nút xóa tag input
     * @param {} removeItem
     * CreatedBy VMHieu 24/05/2023 
     */
    removeTag (removeItem) {
        const itemIndex = this.dataInputSpan.findIndex(item => item == removeItem);
        this.dataInputSpan.splice(itemIndex, 1);
        this.dataValue.splice(itemIndex, 1);
        if (this.dataInputSpan.length == 0) {
            this.showInput = true;
            this.$refs.input.placeholder = this.placeholder;
            this.clearIp = false;
        }
    },

    /**
     * Lựa chọn item bằng cách nhấn các phím lên, xuống trên bàn phím
     * CreatedBy VMHieu 01/04/2023
     */
    selecItemUpDown(event) {
        var me = this;
        var keyCodePress = event.keyCode;
        var elToFocus = null;

        switch (keyCodePress) {
        case keyCode.ESC:
            this.isShowListData = false;
            break;
        case keyCode.ArrowDown:
            this.isShowListData = true;
            if (!this.$refs.input.placeholder || this.$refs.input.placeholder.length == 0 || this.showError){
            this.dataFilter = this.data;
            }
            elToFocus = this.$refs[`toFocus_${me.indexItemFocus + 1}`];
            if (
            this.indexItemFocus == null ||
            !elToFocus ||
            elToFocus.length == 0
            ) {
            this.indexItemFocus = 0;
            } else {
            this.indexItemFocus += 1;
            }
            break;
        case keyCode.ArrowUp:
            this.isShowListData = true;
            if (!this.$refs.input.placeholder || this.$refs.input.placeholder.length == 0 || this.showError){
                this.dataFilter = this.data;
            }
            elToFocus = this.$refs[`toFocus_${me.indexItemFocus - 1}`];
            if (
            this.indexItemFocus == null ||
            !elToFocus ||
            elToFocus.length == 0
            ) {
            this.indexItemFocus = this.dataFilter.length - 1;
            } else {
            this.indexItemFocus -= 1;
            }
            break;
        case keyCode.Enter:
            elToFocus = this.$refs[`toFocus_${me.indexItemFocus}`];
            if (!this.$refs.input.placeholder) {
            this.dataFilter = this.data;
            this.isShowListData = !this.isShowListData;
            } else {
            if (this.dataFilter.length > 0) {
                this.isShowListData = !this.isShowListData;
            }
            }
            if (elToFocus && elToFocus.length > 0) {
            elToFocus[0].click();
            this.isShowListData = false;
            this.dataFilter = this.data
            }
            break;
        default:
            break;
        }
    }
    },

    mounted() {
        this.dataFilter = this.data;
    },
    beforeDestroy() {
        this.$refs.input.placeholder = "";
    },
    data() {
        return {
            textInput: null,    // Giá trị text của cbb
            dataFilter: [], // data đã được filter
            isShowListData: false, // Hiển thị list data hay không
            indexItemFocus: null, // Index của item focus hiện tại
            indexItemSelected: null, // Index của item được selected
            clearIp: false,
            dataInputSpan: [], // Mảng chứa dữ liệu biding ra input
            dataValueSpan: [], // Mảng chứa dữ liệu value
            removeTagImg : require("@/assets/img/remove-tag.svg"),
            showInput: true,
            dataValue: [],
        };
    },
};
</script>
<style scoped>
.combobox {
    position: relative;
    /* flex-direction: row; */
    border-radius: 10px;
    box-sizing: border-box;
    /* min-width: 150px; */
    cursor: pointer;
    background: #fff;
}

.combobox__input,
select {
    width: 100%;
    min-height: 40px;
    flex: 1;
    padding-right: 34px !important;
    padding-left: 12px;
    border-radius: 10px;
    border: none;
    outline: 1px solid #bbbbbb;
    box-sizing: border-box;
    z-index: 10;
    font-family: GoogleSans;
    font-size: 14px ;
    cursor: pointer;
}

.combobox__input:focus {
    outline: 1px solid #1a73e8;
}

.combobox-icon{
    position: absolute;
    right: 8px;
    top: 50%;
    transform: translateY(-50%);
    z-index: 99;
}

.combobox__data {
    display: flex;
    flex-direction: column;
    padding: 6px 0;
    margin-top: 4px;
    position: absolute;
    top: 100%;
    left: 0;
    border-radius: 10px;
    background-color: #fff;
    box-shadow: 0 0 16px rgba(23,27,42,.24);
    z-index: 999;
    max-height: 198px;
    overflow: auto;
    width: -webkit-fill-available;
}

.combobox-top{
    top: -450%;
}

.combobox__item {
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 8px 16px;
    cursor: pointer;
    /* border: 1px solid #ccc; */
    outline: none;
    width: -webkit-fill-available;
    margin: 1px 0;
}

.combobox__item-icon {
    width: 16px;
    height: 16px;
    font-size: 16px;
    margin-right: 10px;
    display: flex;
    align-items: center;
    justify-content: center;
}

.combobox__item-icon--selected {
    width: 14px;
    height: 11px;
}

.combobox__item:hover,
.combobox__item:focus,
.combobox__item--focus,
.combobox__item--hover {
    background-color: #dfebff;
    color: #000;
}

.combobox__item--selected {
    pointer-events: none;
    background-color: #dfebff;
    color: #8a6bf6;
}

.combobox__item--selected>div{
    display: block;
}

.combobox__item>span{
    line-height: initial;
}

.check-selected {
    display: none;
}
.combobox-question{
    border: none;
    outline: none;
    font-weight: 500;
    font-size: 14px;
}

.combobox-question>input{
    border: none;
    outline: none;
    background-color: #f8e373;
}

.combobox-question>input:focus{
    border: none;
    outline: none;
}

.combobox-question .combobox-icon{
    top: 50%;
    transform: translateY(-50%);
}

.combobox-icon img{
    position: relative;
    z-index: 99;
}

.combobox-question input::placeholder{
    font-weight: 700;
    font-size: 16px;
    opacity: 1;
    color: #000;
}

.combobox input::placeholder{
    opacity: 1;
    color: #000;
}

.icon-clear{
    z-index: 99;
}
.icon-clear img{
    width: 16px;
    height: 16px;
    opacity: 0.2;
    display: flex;
    align-items: center;
}

.icon-clear img:hover{
    opacity: 0.4;
}

.input-tag{
    background-color: #f1f2f7;
    margin: 2px 0 2px 6px;
    border-color: #b6b9ce;
    color: #4e5b6a;
    height: 32px;
    line-height: 30px;
    display: inline-block;
    max-width: 100%;
    overflow: hidden;
    text-overflow: ellipsis;
    position: relative;
    padding-right: 20px;
    border-radius: 10px!important;
    user-select: none;
    border: 1px solid #e8e1fd;
    box-sizing: border-box;
    white-space: nowrap;
    padding: 0 20px 0 10px;
}

.input-tag:focus{
    border: 1px solid #1a73e8;
}

.box-tag{
    border: 1px solid #babec5;
    border-radius: 10px;
    display: flex;
    align-items: center;
    flex-wrap: wrap;
}

.box-tag:focus-within{
    border: 1px solid #1a73e8;
}

.box-tag input{
    border:none;
    outline: none;
}

.box-tag input:hover{
    border:none;
    outline: none;
}
.box-tag input:focus{
    border:none;
    outline: none;
}
.box-tag>div{
    width: min-content;
    display: contents;
}

.box-tag .remove-tag{
    cursor: pointer;
    position: absolute;
    right: 4px;
    top: 9px;
    width: 12px;
    height: 12px;
}

</style>