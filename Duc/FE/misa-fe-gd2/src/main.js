
import { createApp } from 'vue'
import App from './App.vue'
//import router
import Routes from './router'
//import router
import { createRouter, createWebHistory } from 'vue-router'
//import vuex
import Vuex from 'vuex'
//import store
import Store from './store'

const app = createApp(App)

//khởi tạo router
const router = createRouter({
    history: createWebHistory(),
    routes: Routes
})
//dang ky router vao app
app.use(router)

//đăng ký vuex vào app
app.use(Vuex);
const store = new Vuex.Store(Store)
app.use(store)

app.mount('#app')
