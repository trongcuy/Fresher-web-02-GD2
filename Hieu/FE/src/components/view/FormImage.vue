<template>
    <div class="image-container">
        <div class="image-dialog">
            <div class="image-header flex">
                <div class="header-left">Thêm hình ảnh</div>
                <div class="header-right">
                    <div class="image-close" @click="closeForm" v-tooltip="'Thoát'">
                        <icon class="icon icon-exit"></icon>
                    </div>
                </div>
            </div>
            <div class="image-main">
                <div class="main-note">Bạn có thể thêm nhanh hình ảnh tìm kiếm được từ Google</div>
                <div class="main-search flex">
                    <div class="search-input">
                        <input type="text" placeholder="Tìm kiếm hình ảnh từ Google SafeSearch...">
                    </div>
                    <div class="search-button">
                        <button>
                            <img :src="searchImg" alt="">
                        </button>
                    </div>
                </div>
                <div class="main-box"
                    @dragover="dragover"
                    @dragleave="dragleave"
                    @drop="drop"
                    @click="chooseFile"
                    v-if="!imageUrl"
                >
                    <div class="box-icon">
                        <img :src="importImg" alt="">
                    </div>
                    <div class="box-content">
                        Nhấn để tải ảnh lên hoặc kéo thả ảnh vào đây. 
                        <br>
                        Ảnh có định dạng JPG, JPEG, PNG
                    </div>
                </div>
                <div class="main-img" v-if="imageUrl">
                    <img :src="imageUrl" alt="">
                </div>
                <input type="file" id="upload-file" ref="upload" @input="handleUploadFile" accept=".png, .jpg, .jpeg"/>
            </div>
            <div class="image-footer">
                <BaseButton class="ms-button btn-active btn-white" text="Hủy" @click="closeForm"></BaseButton>
                <BaseButton class="ms-button btn-active btn-blue btn-gray" ref="refBtn" text="Thêm" @click="uploadImg"></BaseButton>
            </div>
        </div>
    </div>
</template>

<script setup>
import BaseButton from "@/components/base/button/BaseButton.vue";
import { computed, ref, defineEmits, onMounted, nextTick } from 'vue';
import { useStore } from "vuex";
import { generateUUID } from "@/common/common";
import { uploadImage } from "@/utils/image.js"
// Đường dẫn ảnh
const importImg = require("@/assets/img/import-file.svg");
const searchImg = require("@/assets/img/icon-search.svg");

const emit = defineEmits(['update:modelValue'])

const store = useStore();


const upload = ref("upload");
const imageUrl = ref();
const files = ref();
const refBtn = ref("refBtn");

/**
 * Thực hiện chọn file
 * VMHieu 07/06/2023
 */
const handleUploadFile = () => {
    files.value = upload.value.files[0];
    let btn = refBtn.value.btn;
    if (files.value) {
        const reader = new FileReader();

        reader.onload = (e) => {
            imageUrl.value = e.target.result;
        };

        reader.readAsDataURL(files.value);
        btn.classList.remove("btn-gray");
    } else {
        btn.classList.remove("btn-gray");
    }
}

/**
 * Đóng form
 */
const closeForm = () => {
    let btn = refBtn.value.btn;
    store.dispatch("showFormImage", false);
    imageUrl.value = "";
    files.value = "";
    btn.classList.add("btn-gray");
}
/**
 * Thực hiện upload ảnh khi ấn nút thêm
 * VMHieu 13/06/2023
 */
const uploadImg = async () => {
    if (files.value) {
        const id = generateUUID();
        const urlImage = await uploadImage(files.value, id);

        if (urlImage) {
            imageUrl.value = urlImage;
            emit("update:modelValue", imageUrl.value);
        }
    }

    closeForm();
}

/**
 * Chọn file 
 * VMHieu 07/06/2023
 */
const chooseFile = () => {
    upload.value.click();
}
/**
 * Sự kiện drag file
 * CreatedBy VMHieu 07/06/2023
 */
const dragover = (e) => {
    e.preventDefault();
}
/**
 * Sự kiện dropfile
 * @param {*} e
 * CreatedBy VMHieu 07/06/2023 
 */
const drop = (e) => {
    e.preventDefault();
    upload.value.files = e.dataTransfer.files;
    handleUploadFile();
}


</script>



<style lang="scss" scoped>
.image-container{
    position: fixed;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    z-index: 3000;
    height: 100%;
    background-color: rgba(0,0,0,.5);
    overflow: auto;
    scrollbar-width: thin;

    .image-dialog{
        position: absolute;
        transform: translate(-50%, -50%);
        top: 50%;
        left: 50%;
        width: 600px;
        border-radius: 10px;
        box-shadow: 0 0 20px rgba(0,0,0,.16);
        margin: 0 auto 50px;
        background: #fff;

        .image-header {
            padding: 24px;
            justify-content: space-between;

            .header-left {
                font-weight: 500;
                line-height: 24px;
                font-size: 18px;
                color: #303133;
            }
        }

        .image-main{
            padding: 0 24px 24px;
            word-break: break-word;
            color: #606266;
            font-size: 14px;
            line-height: 20px;
            font-weight: 400;

            .main-search{
                margin-top: .75rem;
                margin-bottom: .75rem;
                justify-content: space-between;

                .search-input{
                    padding-right: 12px;
                    width: 100%;

                    input{
                        width: 100%;
                        height: 32px;
                        box-sizing: border-box;
                        border: 1px solid #dfe1e5;
                        background: #fff;

                        &::placeholder{
                            padding-left: 8px;
                        }
                    }
                }

                .search-button{
                    button{
                        font-size: 0;
                        padding: 6px 27px;
                        width: auto;
                        vertical-align: middle;
                        border: 1px solid #666;
                        border-radius: 2px;
                        border-color: #3079ed;
                        background-color: #4d90fe;
                        cursor: pointer;

                        img{
                            width: 13px;
                            height: 18px;
                        }
                    }
                }
            }

            .main-box{
                display: flex;
                flex-direction: column;
                align-items: center;
                justify-content: center;
                width: 100%;
                height: 224px;
                border: 2px dashed #b6b9ce;
                border-radius: 10px;
                text-align: center;
                cursor: pointer;
                box-sizing: border-box;
            }

            .main-img{
                width: 100%;
                height: 100%;
                display: flex;
                align-items: center;
                justify-content: center;

                img{
                    width: 80%;
                    height: 80%;
                }
            }

            
            #upload-file{
            opacity: 0;
            position: absolute;
            z-index: -1;
            }
        }

        .image-footer{
            padding: 0 24px 24px;
            text-align: right;
            box-sizing: border-box;

            >.ms-button:last-child {
                margin: 0;
            }
        }
    }
}
</style>