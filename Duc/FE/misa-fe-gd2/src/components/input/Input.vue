<template>
    <!-- phan tư input -->
    <div class="div-input">
        <p style="margin:0px" v-if="title">{{ title }}<span style="color: red;" v-if="require"> *</span></p>
        <input class="input" :placeholder="defaultValue" v-model="inputValue"
            @input="() => this.$emit('setValueProps', this.inputValue)" />
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
        valueProps: {
            type: String,
            default: ''
        }
    },
    data() {
        return {
            inputValue: this.valueProps,
        }
    },
    watch: {
        //khi prop cha truyền lại valueProps thì cập nhật lại value input con
        //->binding chiều cha sang con
        valueProps(newValue) {
            this.inputValue = newValue
        }
    },
    methods: {

    }
}
</script>

<style scoped>
.input {
    padding-left: 8px;
    padding-right: 62px;
    height: 36px;
    width: 250px;
    font-size: 14px;
    border-radius: 4px;
    color: #000000;
    border: 1px solid #E0E0E0;
    box-sizing: border-box;
}

.div-input {
    width: 250px;
    display: flex;
}

</style>
