<template>
	<div class="greetings">		
		<h3>
			You’ve successfully created a project with {{ name }}
		</h3>
	</div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
    name: "HelloWorld",
    props: [],
    components: {
        
    },
    data() {
        return {

		}
    },
	computed: {
		...mapGetters(['name'])
	}
}
</script>
<style scoped>

</style>
