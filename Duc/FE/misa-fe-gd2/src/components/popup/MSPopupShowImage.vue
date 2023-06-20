<template>
    <div class="overlay" v-esc="onClosePopupImage">
        <div class="div-image">
            <div class="header">
                <div class="div-center" @click="onClosePopupImage">
                    <img src="../../assets/img/x.jpg" />
                </div>
            </div>
            <div class="body div-center">
                <img :src="urlImage" />
            </div>
        </div>
    </div>
</template>
  
<script>
import { mapMutations } from 'vuex'
export default {
    name: 'MSPopupShowImage',
    props: {
        urlImage: {
            Type: String,
            default: ""
        }
    },
    methods: {
        ...mapMutations(['setUrlImageShow']),
        /**
         * hàm đóng popup
         * CreatedBy: Trịnh Huỳnh Đức (14-6-2023)
         */
        onClosePopupImage() {
            this.setUrlImageShow("")
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
    z-index: 200;

    .div-image {
        position: absolute;
        transform: translate(-50%, -50%);
        top: 50%;
        left: 50%;
        width: 840px;
        border-radius: 10px;
        border-radius: 10px;
        margin: 0 auto 50px;
        background: #fff;
        box-sizing: border-box;

        .header {
            padding: 20px;
            position: relative;
            height: 48px;
            div {
                width: 48px;
                height: 48px;
                top: 0;
                right: 0px;
                position: absolute;
                cursor: pointer;
                img {
                    width: 16px;
                    height: 16px;
                }
            }
        }

        .body {
            margin: 0 24px 24px;
            height: 500px;
            background-color: #f1f2f7;
            img {
                height: 400px;
                width: auto;
                max-width: 100%;
            }
        }
    }
}
</style>
  