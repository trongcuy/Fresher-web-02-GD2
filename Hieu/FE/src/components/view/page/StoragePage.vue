<template>
    <div class="storage-container">
        <div class="header-storage">
            <div class="header-left flex">
                <router-link to="/storage/mine" active-class="active">
                    <BaseButton class="ms-button btn-router" text="Cá nhân"></BaseButton>
                </router-link>
                <router-link to="/storage/school" active-class="active">
                    <BaseButton class="ms-button btn-router btn-margin__left" text="Trường học"></BaseButton>
                </router-link>
                <router-link to="/storage/community" active-class="active">
                    <BaseButton class="ms-button btn-router btn-margin__left" text="Cộng đồng"></BaseButton>
                </router-link>
            </div>
            <div class="header-right flex">
                <BaseButton class="ms-button btn-margin__right btn-white btn-active" text="Chia sẻ"></BaseButton>
                <BaseButton class="ms-button btn-margin__right btn-blue btn-active" text="Tạo trò chơi"></BaseButton>
                <BaseButton class="ms-button btn-blue btn-active" text="Soạn bài" @click="createExercise"></BaseButton>
            </div>
        </div>
        <div class="mine-controller">
            <keep-alive>
                <router-view name="mineview"></router-view>
            </keep-alive>
        </div>
    </div>
</template>

<script setup>
import BaseButton from '@/components/base/button/BaseButton.vue';
import { useRouter } from 'vue-router';
import { useStore } from 'vuex';
import * as Resource from '@/common/resource/Resource';
import * as Enum from '@/common/enum/Enum';
import ToastMessage from '@/components/view/ToastMessage.vue';

const router = useRouter();
const store = useStore();

/**
 * Mở page tạo bài tập
 * CreatedBy VMHieu 23/05/2023
 */
const createExercise = () => {
    store.dispatch("updateFormModeExercise", Enum.FormModeExercise.Add);
    store.dispatch("showListQuestion", false);
    store.dispatch("updateHideMainPage", true);
    router.push("/storage/create");
}

</script>

<style scoped>
.storage-container{
    height: calc(100% - 20px);
    overflow-y: auto;
    display: flex;
    justify-content: center;
    width: 100%;
}
.header-storage{
    display: flex;
    align-items: flex-start;
    justify-content: space-between;
    position: fixed;
    top: 60px;
    width: 1024px;
    z-index: 100;
    height: 54px;
    padding-top: 1rem;
    margin: 0 8px;
    background: #fff;
}

.header-left:first-child{
    margin-left: 8px;
}

.active{
    overflow: hidden;
    background-color: #ece7fe;
    border-radius: 10px;
}

.active button{
    color: #8a6bf6;
    font-weight: 500;
}

.mine-controller{
    width: 1024px;
    margin-top: 4rem;
}

</style>