<template>
    <div class="loader" v-if="showLoading">
        <div id="loading"></div>
    </div>
</template>

<script setup>
import { computed } from 'vue';
import { useStore } from 'vuex';
import * as Resource from '@/common/resource/Resource';
import * as Enum from '@/common/enum/Enum';

const store = useStore();
// Biến trạng thái của loading
const showLoading = computed(() => store.state.app.showLoading);

</script>

<style lang="scss" scoped>

</style>