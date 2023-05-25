<template>
    <div class="toast-container" v-show="showToast">
        <BaseToastMessage toastClass="icon icon-success" lineClass="success" v-show="toastStatus == Enum.ToastStatus.Success">
            <div>{{ toastMsg }}</div>
        </BaseToastMessage>
        <BaseToastMessage toastClass="icon icon-fail" lineClass="fail" v-show="toastStatus == Enum.ToastStatus.Fail">
            <div>{{ toastMsg }}</div>
        </BaseToastMessage>
    </div>
</template>

<script setup>
import BaseToastMessage from '@/components/base/toast/BaseToast.vue';
import { computed,ref } from 'vue';
import { useStore } from 'vuex';
import * as Resource from '@/common/resource/Resource';
import * as Enum from '@/common/enum/Enum';

const store = useStore();
// Biến trạng thái của toast
const showToast = computed(() => store.state.app.showToast);
// Biến giá trị dữ liệu của toast
const toastMsg = computed(() => store.state.app.toastMsg);
// Biến trạng thái của dạng toast
const toastStatus = ref(Enum.ToastStatus.Success);

</script>

<style scoped>

</style>