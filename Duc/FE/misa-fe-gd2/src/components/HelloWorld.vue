<template>
	<div style="margin-top: 100px;">		
		<h3>
			You’ve successfully created a project with {{ username }}
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
		...mapGetters(['username'])
	}
}
</script>
<style scoped>

</style>
