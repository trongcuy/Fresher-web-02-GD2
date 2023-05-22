<template>
    <div class="input-container">
        <input type="text" :class="inputClass" :placeholder="placeholder">
        <div class="icon" v-if="showIcon">
            <img src="@/assets/img/icon-search.svg" alt="">
        </div>
    </div>
</template> 

<script setup>
import { computed, defineProps, onMounted, ref, watch, defineEmits, onUpdated } from "vue";

const props = defineProps({
    placeholder: {
        type: String,
        default: null
    },
    inputClass: {
        type: String,
        default: null 
    },
    showIcon: {
        type: Boolean,
        default: false
    }
})

</script>

<style scoped>
.icon{
    height: 100%;
    text-align: center;
    transition: all .3s;
    position: absolute;
    left: 6px;
    top: 50%;
    transform: translateY(-20%);
    color: #c0c4cc;
}

.input-container{
    position: relative;
}

.icon img{
    width: 16px;
    height: 16px;
    margin: 0 4px;
}

.no-icon{
    padding: 0 12px;
}
</style>