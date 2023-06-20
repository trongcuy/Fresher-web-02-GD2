<template>
    <div class="overlay">
        <div class="div-popup">
            <div class="div-header">
                <p>Thêm Hình ảnh</p>
                <div class="div-center" @click="onClickCancelAddImage"><img src="../../assets/img/x.jpg" /></div>
            </div>
            <div class="div-body">
                <div>Bạn có thể thêm nhanh hình ảnh tìm kiếm được từ Google</div>
                <div class="div-search div-flex-row">
                    <input type="text" placeholder="Tìm kiếm hình ảnh bằng Google SafeSearch..." />
                    <button><img src="../../assets/img/search-white.jpg" /></button>
                </div>
                <div class="div-upload div-flex-column" @click="convertToInput">
                    <img src="../../assets/img/ic_extract.svg" />
                    <div>Nhấn để tải ảnh lên hoặc kéo thả ảnh vào đây.<br />Ảnh có định dạng JPG, JPEG, PNG</div>
                    <input ref="inputImage" hidden type="file" accept=".png,.jpg,.jpeg" @change="onUploadImage" />

                    <!-- Ảnh hiển thị khi được chọn -->
                    <img id="img-upload" />
                </div>

            </div>
            <div class="div-footer">
                <MSButton title="Hủy" @click="onClickCancelAddImage" />
                <MSButton title="Thêm" id="button-add" @click="onClickAddImage"
                    :class="{ 'btn-add-able': this.imageData != '' }" />
            </div>
        </div>
    </div>
</template>
  
<script>
import MSButton from '../button/MSButton.vue'
import { mapActions, mapMutations } from 'vuex'
export default {
    name: 'MSPopupAddImage',
    components: {
        MSButton
    },
    data() {
        return {
            urlImage: '',//lưu url ảnh tải lên
            imageData: '',//lưu ảnh taỉ lên
        }
    },
    methods: {
        ...mapActions(['uploadImage']),
        ...mapMutations(['setShowNotify']),
        /**
         * bắt sự kiện đóng popup
         * CreatedBy: Trịnh Huỳnh Đức (10-6-2023)
         */
        onClickCancelAddImage() {
            this.$emit('onClickCancelAddImage')
        },
        /**
         * bắt sự kiện thêm ảnh
         * CreatedBy: Trịnh Huỳnh Đức (10-6-2023)
         */
        onClickAddImage() {
            //lưu ảnh rồi lấy id ảnh
            var idImage
            this.uploadImage(this.imageData).then(data => {
                idImage = data
                //emit lên cha để lưu id ảnh
                this.$emit('onUploadImage', idImage)
            })
        },
        /**
         * bat su kien click chọn ảnh thì mở input file
         * CreatedBy: Trịnh Huỳnh Đức (10-6-2023)
         */
        convertToInput() {
            this.$refs.inputImage.click()
        },
        /**
         * bat su kien tải ảnh lên
         * CreatedBy: Trịnh Huỳnh Đức (10-6-2023)
         */
        onUploadImage(event) {
            var input = event.target
            //validate ảnh trước
            if(!this.validateImage(input.files[0].type)) {
                this.setShowNotify('errorFile')
                return
            }
            var img = document.getElementById('img-upload')
            //gán ảnh mới tải lên vào thẻ img
            if (input.files && input.files[0]) {
                var reader = new FileReader()
                reader.onload = function (e) {
                    img.src = e.target.result
                };
                reader.readAsDataURL(input.files[0])
            }
            const formData = new FormData()
            formData.append('image', input.files[0])
            this.imageData = formData
        }
    }
};
</script>
  
<style scoped lang="scss">
.overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    z-index: 103;

    .div-popup {
        position: absolute;
        transform: translate(-50%, -50%);
        top: 50%;
        left: 50%;
        width: 600px;
        height: 457px;
        z-index: 104;
        background-color: #fff;
        border-radius: 10px;

        .div-header {
            padding: 24px;

            p {
                line-height: 24px;
                font-size: 18px;
                color: #303133;
            }

            div {
                position: absolute;
                top: 12px;
                right: 12px;
                background: transparent;
                cursor: pointer;
                width: 32px;
                height: 32px;

                img {
                    width: 16px;
                    height: 16px;
                }
            }
        }

        .div-body {
            padding: 0px 24px 24px 24px;
            color: #606266;
            font-size: 14px;

            .div-search {
                margin-top: 12px;
                margin-bottom: 12px;
                justify-content: space-between;

                input {
                    width: 464px;
                    height: 32px;
                    padding: 5px 9px;
                    color: #606266;
                    outline: none;
                    border-radius: 0px;
                    border: 1px solid #dfe1e5;
                }

                button {
                    border-color: #3079ED;
                    background-color: #4D90FE;
                    background-image: none;
                    height: 32px;
                    font-size: 0;
                    padding: 6px 27px;
                    width: auto;
                    margin-left: 12px;
                    border: 1px solid #666;
                    border-radius: 2px;

                    img {
                        width: 20px;
                        height: 20px;
                    }
                }
            }

            .div-upload {
                height: 224px;
                border: 2px dashed #b6b9ce;
                padding: 24px 0;
                border-radius: 10px;
                align-items: center;
                cursor: pointer;
                position: relative;

                img {
                    width: 64px;
                    height: 64px;
                    margin-bottom: 16px;
                }

                div {
                    text-align: center;
                }

                #img-upload {
                    border-radius: 10px;
                    position: absolute;
                    object-fit: cover;
                    width: 100%;
                    height: 100%;
                    top: 0px;
                    left: 0px;
                }
            }

        }

        .div-footer {
            padding: 0px 24px 24px 24px;
            float: right;

            #button-add {
                margin-left: 12px;
                background-color: #B6B9CE;
                pointer-events: none;
                cursor: not-allowed;
            }

            .btn-add-able {
                background-color: #8A6BF6 !important;
                color: #fff;
                pointer-events: all !important;
                cursor: pointer !important;
            }
        }
    }
}
</style>
  