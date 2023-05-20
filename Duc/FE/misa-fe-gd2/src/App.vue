
<template>
	<TheHeader v-if="!$route.path.includes('/course/create')"/>
	<RouterView name="main"/>
	<MSOverlay v-if="showOverlay"/>
</template>
<script>
import { mapGetters } from 'vuex'
import TheHeader from './components/TheHeader.vue'
import MSOverlay from './components/popup/MSOverlay.vue'
export default {
	name: "App",
	props: [],
	components: {
		TheHeader,
		TheHeader,
		MSOverlay
	},
	computed: {
		...mapGetters(['showOverlay'])
	}
}
</script>

<style>
@import url('./css/general.css')
</style>
