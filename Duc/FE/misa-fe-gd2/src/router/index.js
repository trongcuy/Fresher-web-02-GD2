
import BasePage from '../components/BasePage.vue'
import CoursePage from '../components/page/course/CoursePage.vue'
import CourseMain from '../components/page/course/CourseMain.vue'
import CreatePage from '../components/page/create/CreatePage.vue'
const routes =
	[
		{
			path: "/overview",
			components: { main: BasePage },
			props: {

			}
		},
		{
			path: "/class",
			components: { main: BasePage },
			props: {
				title: "class"
			}
		},
		{
			path: "/course",
			components: { main: CoursePage },
			children: [
				{
					path: "/course",
					redirect: "/course/personal",
				},
				{
					path: "/course/personal",
					components: { course: CourseMain },
				},
				{
					path: "/course/school",
					components: { course: BasePage },
				},
				{
					path: "/course/community",
					components: { course: BasePage },
				},
			]
		},
		{
			path: "/course/create",
			components: { main: CreatePage },
		},
		{
			path: "/training",
			components: { main: BasePage },
			props: {

			}
		},
	]

export default routes
