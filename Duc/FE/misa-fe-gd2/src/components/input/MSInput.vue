<template>
    <!-- phan tư input -->
    <div class="div-input">
        <p v-if="title">{{ title }}<span style="color: red;" v-if="require"> *</span></p>
        <input class="input" type="text" :placeholder="defaultValue" v-model="inputValue"
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
        }
    },
    data() {
        return {
            inputValue: "",
        }
    },
    methods: {

    }
}
</script>

<style scoped>
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
.div-input {
    display: flex;
    flex-direction: column;
}
</style>
