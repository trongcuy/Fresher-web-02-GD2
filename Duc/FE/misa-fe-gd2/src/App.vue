<template>
	<TheHeader v-if="!$route.path.includes('/course/create')"/>
	<RouterView name="main"/>
	<MSOverlay v-if="showOverlay"/>
	<MSDialog v-if="showDialog"/>
	<MSNotify v-if="showNotify"/>
	<MSLoading v-if="showLoading"/>
</template>
<script>
import { mapGetters } from 'vuex'
import TheHeader from './components/TheHeader.vue'
import MSOverlay from './components/popup/MSOverlay.vue'
import MSDialog from './components/notify/MSDialog.vue'
import MSNotify from './components/notify/MSNotify.vue'
import MSLoading from './components/notify/MSLoading.vue'
export default {
	name: "App",
	props: [],
	components: {
		TheHeader,
		TheHeader,
		MSOverlay,
		MSDialog,
		MSNotify,
		MSLoading
	},
	computed: {
		...mapGetters(['showOverlay', 'showDialog', 'showNotify', 'showLoading'])
	}
}
</script>

<style>
@import url('./css/general.css')
</style>
