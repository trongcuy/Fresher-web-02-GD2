
import HelloWorld from '../components/HelloWorld.vue'
const routes =
	[
		{
			path: "/login",
			component: HelloWorld,
			props: {

			}
		},
		{
			path: "/home",
			component: HelloWorld,
			props: {
				title: "Trang chủ"
			}
		},
		{
			path: "/compete",
			component: HelloWorld,
			props: {

			}
		},
	]

export default routes
