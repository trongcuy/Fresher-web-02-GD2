<template>
    <div class="app-container">
      <div v-show="!hideMainPage">
        <TheHeader></TheHeader>
        <div class="layout-main">
          <TheContent></TheContent>
        </div>
      </div>
      <router-view name="create"></router-view>
      <ToastMessage></ToastMessage>
      <LoadingOver></LoadingOver>
    </div>
</template>

<script setup>
import TheContent from '@/components/layout/TheContent.vue';
import TheHeader from '@/components/layout/TheHeader.vue';
import ToastMessage from '@/components/view/ToastMessage.vue';
import LoadingOver from '@/components/view/LoadingOver.vue';
import { computed, ref } from 'vue';
import { useStore } from 'vuex';

const store = useStore();

const hideMainPage = computed(() => store.state.app.hideMainPage);
</script>

<style>
@import url(@/assets/css/main.css);

.layout-main {
  height: calc(100% - 64px);
  position: relative;
  background: #fff;
}

.app-container {
  height: 100%;
}

.app-container>div{
  height: 100%;
}
</style>
