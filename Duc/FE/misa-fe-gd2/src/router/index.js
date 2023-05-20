
import HelloWorld from '../components/HelloWorld.vue'
import CoursePage from '../components/page/course/CoursePage.vue'
import CourseMain from '../components/page/course/CourseMain.vue'
import CreatePage from '../components/page/CreatePage.vue'
const routes =
	[
		{
			path: "/overview",
			components: {main:HelloWorld},
			props: {

			}
		},
		{
			path: "/class",
			components: {main:HelloWorld},
			props: {
				title: "class"
			}
		},
		{
			path: "/course",
			components: {main:CoursePage},
			children: [
				{
					path: "/course",
					redirects: "/course/personal",
				},
				{
					path: "/course/personal",
					components: {course:CourseMain},
				},
				{
					path: "/course/school",
					components: {course:HelloWorld},
				},
				{
					path: "/course/community",
					components: {course:HelloWorld},
				},
			]
		},
		{
			path: "/course/create",
			components: {main:CreatePage},
		},
		{
			path: "/training",
			components: {main:HelloWorld},
			props: {

			}
		},
	]

export default routes
