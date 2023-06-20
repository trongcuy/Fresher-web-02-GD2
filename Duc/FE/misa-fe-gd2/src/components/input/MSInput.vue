<template>
    <!-- phan tư input -->
    <div class="div-input">
        <p v-if="title">{{ title }}<span style="color: red;" v-if="require"> *</span></p>
        <input class="input" type="text" 
            :placeholder="defaultValue" 
            v-model="inputValue"
            :class="{ 'icon-left': type == 'icon-left','input-error': this.require &&showError}" 
            @blur="onBlurInput" @input="onInput" />
        <p class="error" v-if="this.require && showError">Không được bỏ trống</p>
    </div>
</template>

<script>
export default {
    name: 'Input',
    props: {
        title: {
            type: String,
            default: ''
        },
        defaultValue: {
            type: String,
            default: ''
        },
        require: {
            type: Boolean,
            default: false
        },
        type: {
            type: String,
            default: ''
        },
        value: {
            type: String,
            default: ''
        },
        validate: {
            type:Boolean,
            default: true
        }
    },
    data() {
        return {
            inputValue: this.value,
            showError: false,//show error validate
        }
    },
    watch: {
        //khi prop cha truyền lại valueInput thì cập nhật lại value input con
        //->binding chiều cha sang con
        value(newValue) {
            this.inputValue = newValue
        },
        validate(newValue) {
            this.showError = !newValue
        }
    },
    methods: {
        /**
         * sự kiện blur input
         * CreatedBy: Trịnh Huỳnh Đức(25-5-2023)
         */
        onBlurInput() {
            if (!this.inputValue)
                this.showError = true
        },
        /**
         * sự kiện blur input
         * CreatedBy: Trịnh Huỳnh Đức(25-5-2023)
         */
        onInput() {
            this.$emit('setValueInput', this.inputValue)
            if (this.inputValue)
                this.showError = false
        }
    }
}
</script>

<style scoped lang="scss">
.div-input {
    display: flex;
    flex-direction: column;

    p {
        margin-bottom: 4px;
        font-size: 14px;
    }

    .input {
        border-radius: 10px;
        border: 1px solid #b6b9ce;
        width: 100%;
        height: 40px;
        padding: 0px 12px;
        outline: none;
    }

    .input:focus {
        border: 1px solid #8a6bf6;
    }
    .input-error {
        border: 1px solid #ff6161 !important;
    }
    .icon-left {
        padding-left: 34px;
    }

    .error {
        font-size: 14px;
        color: #ff6161;
        margin: 0px;
        font-weight: 500;
    }
}
</style>
