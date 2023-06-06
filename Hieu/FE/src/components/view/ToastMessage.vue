<template>
    <div class="toast-container" :class="classToast" v-show="showToast">
        <BaseToastMessage toastClass="icon icon-success" lineClass="success" v-if="toastStatus == Enum.ToastStatus.Success">
            <div>{{ toastMsg }}</div>
        </BaseToastMessage>
        <BaseToastMessage toastClass="icon icon-fail" lineClass="fail" v-if="toastStatus == Enum.ToastStatus.Fail">
            <div>{{ toastMsg }}</div>
        </BaseToastMessage>
        <BaseToastMessage toastClass="icon icon-warning" lineClass="warning" v-if="toastStatus == Enum.ToastStatus.Warning">
            <div>{{ toastMsg }}</div>
        </BaseToastMessage>
    </div>
</template>

<script setup>
import BaseToastMessage from '@/components/base/toast/BaseToast.vue';
import { computed, ref, defineProps} from 'vue';
import { useStore } from 'vuex';
import * as Resource from '@/common/resource/Resource';
import * as Enum from '@/common/enum/Enum';

const props = defineProps({
    classToast: null
}) 

const store = useStore();
// Biến trạng thái của toast
const showToast = computed(() => store.state.app.showToast);
// Biến giá trị dữ liệu của toast
const toastMsg = computed(() => store.state.app.toastMsg);
// Biến trạng thái của dạng toast
const toastStatus = computed(() => store.state.app.toastStatus);

</script>

<style lang="scss" scoped>
.toast-container {
    position: absolute;
    top: 24px;
    left: 50%;
    z-index: 9999999;
    transform: translateX(-50%);

    &.toast-question {
        top: -80px;
    }

    &.toast-form {
        top: 65px;
    }
}
</style>