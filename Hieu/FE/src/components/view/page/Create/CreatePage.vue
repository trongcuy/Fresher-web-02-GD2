<template>
    <div class="create-container">
        <div class="create-header">
            <div class="header-top">
                <div class="btn-back">

                </div>
                <div class="header-top__avatar">

                </div>
                <div class="header-top__context">
                    Nhập tên bài tập...
                </div>
            </div>
            <div class="header-bottom">
                <div class="header-bottom__left">
                    <div class="combobox-subject">
                        <BaseCombobox></BaseCombobox>
                    </div>
                    <div class="combobox-class">
                        <BaseCombobox></BaseCombobox>
                    </div>
                    <div class="btn-addinfo">
                        <BaseButton class="ms-button btn-white btn-active" text="Bổ sung thông tin"></BaseButton>
                    </div>
                </div>
                <div class="header-bottom__right">
                    <div class="btn-question">
                        <BaseButton class="ms-button btn-white btn-active" text="Bổ sung thông tin"></BaseButton>
                    </div>
                    <div class="btn-try">
                        <BaseButton class="ms-button btn-white btn-active" text="Làm thử"></BaseButton>
                    </div>
                    <div class="btn-submit">
                        <BaseButton class="ms-button btn-blue btn-active" text="Hoàn thành"></BaseButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="create-main">

        </div>
    </div>
</template>

<script setup>
import BaseCombobox from '@/components/base/combobox/BaseCombobox.vue';
import BaseButton from '@/components/base/button/BaseButton.vue';

</script>

<style scoped>

</style>