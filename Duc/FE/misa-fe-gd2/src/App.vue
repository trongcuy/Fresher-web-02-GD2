
<template>
	<TheHeader v-if="!$route.path.includes('/course/create')"/>
	<RouterView name="main"/>
</template>
<script>
import TheHeader from './components/TheHeader.vue'
export default {
	name: "App",
	props: [],
	components: {
		TheHeader,
		TheHeader
	},
}
</script>

<style>
@import url('./css/general.css')
</style>
