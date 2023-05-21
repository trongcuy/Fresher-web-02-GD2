<template>
    <div>
      <div class="combobox" v-clickOutside="hideListData" ref="combobox">
        <input
          :id="id"
          type="text"
          class="input combobox__input"
          ref="input"
          v-model="textInput"
          @input="inputOnChange"
          @keydown="selecItemUpDown"
          @mouseover="handleMouse"
          @click="btnSelectDataOnClick"
          :tabindex="tabidx"
          :placeholder="placeholder"
          :resetValue="resetValue"
          :bidingValue="bidingValue"
          autocomplete="off"
        />
        <div class="combobox-icon">
            <i class="icon-dropdown"></i>
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
    valueDefault: null,
    resetValue: true,
    url: String,
    propValue: String,
    propText: String,
    isLoadData: {
        type: Boolean,
        default: true,
    },
    placeholder: {
        type: String,
        default: ""
    },
    data: [],
    refInput: {
        type: String,
        default: ""
    },
    isPositionTop: {
        type: Boolean,
        default: false
    },
    errorMsg: {
        type: String,
        default: ""
    },
    timer: {
        type: Number,
        default: 300
    },
    id: {
        type: String,
        default: ""
    },
    tabidx: {
        type: Number,
        default: -1
    }
    },
    watch: {
    resetValue: function() {
        this.indexItemSelected = null;
        this.indexItemFocus = null;
        this.textInput = null;
        this.showError = false;
    }, 
    emulation: function() {
        if (this.showForm) {
        for (let i = 0; i < this.data.length; i++) {
            if (this.data[i].Data == this.emulation.RewardLevelName) {
            this.indexItemSelected = i;
            this.indexItemFocus = i;
            this.textInput = this.data[i].Data;
            }
        }
        }
    },
    showError: function() {
        if (this.showError) {
        this.$refs.combobox.classList.add("combobox__error");
        this.indexItemSelected = null;
        this.indexItemFocus = null;
        this.$emit("getValueCombobox");
        } else {
        this.$refs.combobox.classList.remove("combobox__error");
        }
    }
    },
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
        const value = item;
        this.textInput = text; // Hiển thị text lên input.
        this.indexItemSelected = index;
        this.indexItemFocus = index;
        this.isShowListData = false;
        this.showError = false;
        this.$refs.combobox.classList.remove("combobox__error");

        this.$emit("getValueCombobox", value);
    },
    /**
     * Hàm check input để in ra lỗi
     * CreatedBy VMHieu 31/03/2023
     */
    handleChange() {
        let arrs = this.data;
        let flag = 0;
        if (this.data && this.textInput && typeof(this.textInput) == "string") {
        for (const arr in arrs) {
            let str1 = removeVietnameseTones(arrs[arr][this.propText]).toLowerCase().replace(" ", "");
            let str2 = removeVietnameseTones(this.textInput).toLowerCase().replace(" ", "");
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

        if (this.textInput == "") {
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
            if (!this.textInput || this.textInput.length == 0 || this.showError){
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
            if (!this.textInput || this.textInput.length == 0 || this.showError){
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
            if (!this.textInput) {
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
    this.textInput = "";
    },
    data() {
    return {
        textInput: null,
        dataFilter: [], // data đã được filter
        isShowListData: false, // Hiển thị list data hay không
        indexItemFocus: null, // Index của item focus hiện tại
        indexItemSelected: null, // Index của item được selected
        tabindex: {
        type: Number,
        default: 0
        },
        showError: false,
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

.combobox__error>input{
    outline: 1px solid #ef5350;
    border: none;
}

.combobox__error>input:focus{
    outline: 1px solid #ef5350;
}

.cbb-error:focus{
    border: none;
    outline: none;
}

.combobox-icon{
    position: absolute;
    right: 3%;
    top: 45%;
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
    line-height: 32px;
    padding: 0 8px;
    height: 32px;
    cursor: pointer;
    /* border: 1px solid #ccc; */
    outline: none;
    width: -webkit-fill-available;
    min-height: 32px;
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
    color: #000;
}

.combobox__item--selected>div{
    display: block;
}

.check-selected {
    display: none;
}

.combobox-error{
    color: #ef5350;
    margin-top: 6px;
}
</style>