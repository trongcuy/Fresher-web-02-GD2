<template>
	<div class="div-content">	
        <CourseHeader/>
        <!-- khối main -->
        <RouterView name="course" class="div-main"/>
	</div>
</template>

<script>
import CourseHeader from '../course/CourseHeader.vue'
import CourseMain from '../course/CourseMain.vue'
import { mapActions } from 'vuex'
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
    methods: {
        ...mapActions(['getListExercise', 'getListSubject', 'getListGrade'])
    },
    created() {
        //lấy danh sách bài tập
        this.getListExercise()
        //lấy danh sách môn
        this.getListSubject()
        //lấy danh sách khối
        this.getListGrade()
    }
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
