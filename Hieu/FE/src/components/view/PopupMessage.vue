<template>
    <div>
        <BasePopup v-if="popupStatus == Enum.PopupStatus.Delete && showPopup">
            <template v-slot:slotTitle>EMIS Ôn tập</template>
            <template v-slot:slotContent>
                <div class="content">{{ popupMsg }}</div>
            </template>
            <template v-slot:slotButton>
                <div class="btn btn-warning">
                    <BaseButton text="Hủy bỏ" class="ms-button btn-active btn-white" @click="handleClose"/>
                    <BaseButton text="Xóa" class="ms-button btn-blue btn-active" @click="handleSaveDelete"/>
                </div>
            </template>
        </BasePopup>
        <BasePopup v-if="popupStatus == Enum.PopupStatus.Error && showPopup">
            <template v-slot:slotTitle>EMIS Ôn tập</template>
            <template v-slot:slotContent>
                <div class="content"> {{ popupMsg }} </div>
            </template>
            <template v-slot:slotButton>
                <div class="btn btn-warning">
                    <BaseButton text="Đồng ý" class="ms-button btn-blue btn-active" @click="handleClose"/>
                </div>
            </template>
        </BasePopup>
        <BasePopup v-if="popupStatus == Enum.PopupStatus.ErrorServer && showPopup">
            <template v-slot:slotTitle>EMIS Ôn tập</template>
            <template v-slot:slotContent>
                <div v-for="msg in popupMsg" :key="msg">
                    <div class="content"> - {{ msg }}</div>
                </div>
            </template>
            <template v-slot:slotButton>
                <div class="btn btn-warning">
                    <BaseButton text="Đóng" class="ms-button btn-red btn-right" @click="handleClose"/>
                </div>
            </template>
        </BasePopup>
    </div>
</template>

<script setup>
import BasePopup from "@/components/base/popup/BasePopup.vue";
import BaseButton from "@/components/base/button/BaseButton.vue";
import { computed } from "vue";
import { useStore } from "vuex";
import * as Resource from '@/common/resource/Resource';
import * as Enum from '@/common/enum/Enum';

const store = useStore();
// Biến trạng thái popup
const showPopup = computed(() => store.state.app.showPopup);
// Biến trạng thái dạng của popup
const popupStatus = computed(() => store.state.app.popupStatus);
// Biến dữ liệu trong popup
const popupMsg = computed(() => store.state.app.popupMsg);
// ID bản ghi bài tập cần xóa
const idExercise = computed(() => store.state.exercise.idDelete);

/**
 * Đóng popup
 * CreatedBy VMHieu 23/05/2023
 */
const handleClose = () => {
    store.dispatch('showPopup', false);
}
/**
 * Xác nhận xóa bản ghi
 * VMHieu 06/01/2023
 */
const handleSaveDelete = async () => {
   await store.dispatch("deleteExercise", idExercise.value);
   await store.dispatch("getPaging");
    store.dispatch("showPopup", false);
}
</script>

<style scoped>

.btn {
    display: flex;
}

.btn-error {
    justify-content: center;
}

.btn-warning,
.btn-question {
    justify-content: flex-end;
}

.btn-required {
    justify-content: flex-end;
}

.error-validate{
    padding: 4px 0;
}

.btn-right{
    margin-left: 8px;
}
</style>