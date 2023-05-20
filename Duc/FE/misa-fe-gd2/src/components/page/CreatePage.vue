<template>
	<div class="div-create">		
		
	</div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
    name: "CreatePage",
    props: [],
    components: {
        
    },
    data() {
        return {

		}
    },
	computed: {
		...mapGetters([''])
	}
}
</script>
<style scoped>
.div-create {
    z-index: 100;
    width: calc(vw);
    height: calc(vh);
    background-color: aqua;
    margin-top: 100px;
}
</style>
