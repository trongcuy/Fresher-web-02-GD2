<template>
    <div class="import-container" v-if="showImport">
        <div class="import-dialog">
            <div class="import-header flex">
                <div class="header-left">Nhập khẩu</div>
                <div class="header-right">
                    <div class="import-close" @click="closeForm" v-tooltip="'Thoát'">
                        <icon class="icon icon-exit"></icon>
                    </div>
                </div>
            </div>
            <div class="import-main">
                <div class="result">
                    <div class="result-box result-total">
                        <div>
                            <p>Số bản ghi</p>
                            <p class="number-result">{{ resultCheckFile.Total || 0 }}</p>
                        </div>
                    </div>
                    <div class="result-box result-success">
                        <div>
                            <p>Hợp lệ</p>
                            <p class="number-result">{{ resultCheckFile.TotalSuccess || 0 }}</p>
                        </div>
                    </div>
                    <div class="result-box result-fail">
                        <div>
                            <p>Không hợp lệ</p>
                            <p class="number-result">{{ resultCheckFile.TotalFail || 0 }}</p>
                        </div>
                    </div>
                </div>
                <div class="fail-msg" v-if="resultCheckFile.TotalFail != 0">
                    <img :src="imgInfo" alt="">
                    <div class="fail-content">
                        <p>Bạn có thể tải file dữ liệu không hợp lệ để chỉnh sửa và nhập khẩu lại. Hoặc tiếp tục nhập khẩu những bản ghi hợp lệ.</p>
                        <p><span @click="downloadFileFail">Tải file lỗi</span></p>
                    </div>
                </div>
            </div>
            <div class="import-footer">
                <BaseButton class="ms-button btn-active btn-white" text="Hủy" @click="closeForm"></BaseButton>
                <BaseButton class="ms-button btn-active btn-white" text="Đổi file khác" @click="changeFile"></BaseButton>
                <BaseButton class="ms-button btn-active btn-blue" text="Tiếp tục" @click="importFile"></BaseButton>
            </div>
            <input type="file" id="upload-file" ref="upload" @input="handleUploadFile" accept=".xls, .xlsx"/>
        </div>
    </div>
</template>

<script setup>
import BaseButton from '@/components/base/button/BaseButton.vue';
import { computed, ref, defineProps } from 'vue';
import { useStore } from 'vuex';
import { useRouter, useRoute } from 'vue-router';
import * as Resource from "@/common/resource/Resource";
import * as Enum from "@/common/enum/Enum";

const props = defineProps({
    dataExercise: null
})
// Đường dẫn ảnh
const imgInfo = require("@/assets/img/icon-info.svg");

const store = useStore();
const router = useRouter();
const route = useRoute();

const showImport = computed(() => store.state.exercise.showImport); // Mở form import
const resultCheckFile = computed(() => store.state.exercise.resultCheckFile); // Kết quả check file
const exerciseID = computed(() => store.state.exercise.exerciseID); // ID bài tập mới thêm

const upload = ref("upload");
/**
 * Đóng form import 
 * VMHieu 07/06/2023
 */
const closeForm = () => {
    store.dispatch("showImport", false);
}
/**
 * Thay đổi file
 * VMHieu 11/06/2023
 */
const changeFile = () => {
    upload.value.click();
}

/**
 * Thực hiện chọn file
 * VMHieu 07/06/2023
 */
const handleUploadFile = async () => {
    let files = "";
    let file = upload.value.files[0];
    var extension = file.name.split('.').pop().toLowerCase();
    let fileSize = (file.size/1024/1024).toFixed(3);
    if (!Resource.FileFormat.includes(extension)) {
        showToast(Enum.ToastStatus.Warning, Resource.ToastWarning.ErrorFileExcel)
    } 
    else if (fileSize > 5) {
        showToast(Enum.ToastStatus.Warning, Resource.ToastWarning.ErrorFileSize)
    }
    else {
        files = upload.value.files[0];
    }

    if (files) {
        var formData = new FormData();
        formData.append("excelFile", files);

        store.dispatch("checkFile", formData);
    }
}
/** 
 * Hiển thị toast theo status và msg
 * VMHieu 15/06/2023
 */
 const showToast = (status, msg) => {
    store.dispatch("showToast", true);
    store.dispatch("updateToastStatus", status);
    store.dispatch("updateToastMsg", msg);
}
/**
 * Tải file lỗi
 * VMHieu 11/06/2023
 */
const downloadFileFail = () => {
    if (resultCheckFile.value.TotalFail !== 0) {
        store.dispatch("downloadFileCheck", true);
    } else {
        store.dispatch("showPopup", true);
        store.dispatch("updatePopupStatus", Enum.PopupStatus.Error);
        store.dispatch("updatePopupMsg", Resource.PopupMessage.NotValidFileFali);
    }
}

/**
 * Thực hiện import các bản ghi hợp lệ
 * CreatedBy VMHieu 20/04/2023
 */
const importFile = async () => {
    let id = route.query.id;
    let dataExercise = {};
    if (props.dataExercise.Topics) {
        dataExercise = {
            Exercise: props.dataExercise,
            TopicIDs: Object.values(props.dataExercise.Topics) || []
        }
    } else {
        dataExercise = {
            Exercise: props.dataExercise
        }
    }

    if (resultCheckFile.value.TotalSuccess > 0){
        await store.dispatch("importExcel", dataExercise);
        // import thành công thì tắt form và reset form 
        store.dispatch("showImport", false);
        upload.value.files = null;
        // Load lại trang
        if (!id){
            id = exerciseID.value;
        } 
        await store.dispatch("getAllByID", id);
        // Đổi route về form bài tập đã có câu hỏi
        router.push({ path: "/storage/create", query: {id} });
    } else {
        store.dispatch("showPopup", true);
        store.dispatch("updatePopupStatus", Enum.PopupStatus.Error);
        store.dispatch("updatePopupMsg", Resource.PopupMessage.NotValidFileSuccess);
    }
}

</script>

<style lang="scss" scoped>
.import-container{
    position: fixed;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    z-index: 1000;
    height: 100%;
    background-color: rgba(0,0,0,.5);
    overflow: auto;
    scrollbar-width: thin;

    .import-dialog{
        position: absolute;
        transform: translate(-50%, -50%);
        top: 50%;
        left: 50%;
        width: 600px;
        border-radius: 10px;
        box-shadow: 0 0 20px rgba(0,0,0,.16);
        margin: 0 auto 50px;
        background: #fff;

        .import-header {
            padding: 24px;
            justify-content: space-between;

            .header-left {
                font-weight: 700;
                font-size: 24px;
                line-height: 32px;
                color: #4e5b6a;
            }
        }

        .import-main{
            padding: 0 24px 24px;
            word-break: break-word;
            color: #606266;
            font-size: 14px;
            line-height: 20px;
            font-weight: 400;

            .result{
                display: grid;
                gap: 12px;
                grid-template-columns: 1fr 1fr 1fr;
                position: relative;
                .result-box{
                    border-radius: 10px;
                    aspect-ratio : 1 / 0.8;
                    overflow: hidden;
                    position: relative;

                    &.result-total{
                    background-color: #ae99fa;
                    }
                    &.result-success{
                        background-color: #00c542;
                    }
                    &.result-fail{
                        background-color: #ee0000;
                    }

                    div{
                        position: absolute;
                        top: 50%;
                        left: 50%;
                        transform: translate(-50%, -50%);
                        text-align: center;
                        color: #fff;

                        .number-result{
                            font-size: 32px;
                            font-weight: 700;
                            line-height: 40px;
                        }
                    }
                }
            }

            .fail-msg{
                display: flex;
                justify-content: space-between;
                padding: 0.5rem;
                background-color: rgba(241,242,247,1);
                border-radius: 10px;
                align-items: center;
                width: 100%;
                height: 5rem;
                margin-top: 0.75rem;
                box-sizing: border-box;

                img{
                    margin-left: .75rem;
                    margin-right: .75rem;
                    width: 1.75rem;
                    height: 1.75rem;
                }

                span{
                    color: rgba(138,107,246,1);
                    cursor: pointer;
                }
            }
        }

        .import-footer{
            padding: 0 24px 24px;
            text-align: right;
            box-sizing: border-box;

            >.ms-button:last-child {
                margin: 0;
            }
        }

        #upload-file{
            opacity: 0;
            position: absolute;
            z-index: -1;
        }
    }
}
</style>