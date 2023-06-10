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
                            <p class="number-result">0</p>
                        </div>
                    </div>
                    <div class="result-box result-success">
                        <div>
                            <p>Hợp lệ</p>
                            <p class="number-result">0</p>
                        </div>
                    </div>
                    <div class="result-box result-fail">
                        <div>
                            <p>Không hợp lệ</p>
                            <p class="number-result">0</p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="import-footer">
                <BaseButton class="ms-button btn-active btn-white" text="Hủy" @click="closeForm"></BaseButton>
                <BaseButton class="ms-button btn-active btn-white" text="Đổi file khác" @click="saveClose"></BaseButton>
                <BaseButton class="ms-button btn-active btn-blue" text="Tiếp tục" @click="saveAdd"></BaseButton>
            </div>
        </div>
    </div>
</template>

<script setup>
import BaseButton from '@/components/base/button/BaseButton.vue';
import { computed } from 'vue';
import { useStore } from 'vuex';

const store = useStore();

const showImport = computed(() => store.state.exercise.showImport);
/**
 * Đóng form import 
 * VMHieu 07/06/2023
 */
const closeForm = () => {
    store.dispatch("showImport", false);
}
</script>

<style lang="scss" scoped>
.import-container{
    position: fixed;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    z-index: 2000;
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
        }

        .import-footer{
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