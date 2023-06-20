
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

//khai báo biến dùng chung
import Resource from './helper/resource'
import Enums from './helper/enums'
window.Resource = Resource.VI
window.Enums = Enums
//khai báo ckeditor dùng nhiều lần
import CKEditor from '@ckeditor/ckeditor5-vue'
import ClassicEditor from '@ckeditor/ckeditor5-build-classic'
app.use(CKEditor)
// Tạo một biến toàn cục để sử dụng ClassicEditor
window.ckeditor = CKEditor.component
window.ClassicEditor = ClassicEditor


//khai báo directive
import clickOutside from './helper/directive/click-outside'
import esc from './helper/directive/esc'
import tooltipDirective from './helper/directive/tooltip'
app.directive('tooltip', tooltipDirective)
app.directive("click-outside", clickOutside)
app.directive("esc", esc)

//khai báo thư viện các hàm tự tạo
import { myLibrary  } from './helper/myLibrary'
app.mixin(myLibrary)

app.mount('#app')
