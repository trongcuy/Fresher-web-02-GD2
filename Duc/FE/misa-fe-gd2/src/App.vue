<template>
	<TheHeader v-if="!$route.path.includes('/course/create')"/>
	<RouterView name="main"/>
	<MSNotify v-if="showNotify!='no'" :content="this.contentNotify"/>
	<MSLoading v-if="showLoading"/>
</template>
<script>
import { mapGetters } from 'vuex'
import {mapMutations} from 'vuex'
import TheHeader from './components/TheHeader.vue'
import MSNotify from './components/notify/MSNotify.vue'
import MSLoading from './components/notify/MSLoading.vue'
export default {
	name: "App",
	props: [],
	components: {
		TheHeader,
		TheHeader,
		MSNotify,
		MSLoading
	},
	watch: {
		showNotify: function() {
			this.contentNotify = this.resource.notifyContent[this.showNotify]
			//sau 3s tự đóng
            setTimeout(() => {
                this.setShowNotify("no")
            }, 5000)
		}
	},
	data() {
		const resource = window.Resource
		return {
			resource,
			contentNotify: '',//lưu nội dung của thông báo
		}
	},
	computed: {
		...mapGetters(['showOverlay', 'showDialog', 'showNotify', 'showLoading'])
	},
	methods: {
		...mapMutations(['setShowNotify'])
	}
}
</script>

<style>
@import url('./css/general.css');
@import url('./css/ckeditor.css');
</style>
