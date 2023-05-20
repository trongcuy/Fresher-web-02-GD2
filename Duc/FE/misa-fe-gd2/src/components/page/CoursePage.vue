<template>
	<div class="div-content">	
        <CourseHeader/>
        <!-- khối main -->
        <RouterView name="course" class="div-main"/>
	</div>
</template>

<script>
import CourseHeader from './CourseHeader.vue'
import CourseMain from './CourseMain.vue'
export default {
    name: "CoursePage",
    props: [],
    components: {
        CourseHeader,
        CourseMain
    },
    data() {
        return {

		}
    },
}
</script>
<style scoped>
.div-content {
    width: 1024px;
    margin: auto;
    margin-top: 64px;
    padding: 8px;
    box-sizing: border-box;
}
.div-main {
    margin-top: 62px;
}
</style>
