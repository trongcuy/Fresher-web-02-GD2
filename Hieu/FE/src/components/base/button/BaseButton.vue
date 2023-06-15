<template>
    <button :class="str" :disabled="disabled" ref="btn">
        <slot name="icon"></slot>
        <div class="btn-text">
            {{ text }}
        </div>
    </button>
</template>

<script setup>
import { defineProps, defineExpose, ref } from 'vue';

const btn = ref("btn");

const props = defineProps({
    text: String,   // Nội dung button
    str: String,    // Class của button
    // Trạng thái disable của button
    disabled: {
        type: Boolean,
        default: false
    }
})

defineExpose({btn});

</script>

<style scoped>

</style>
