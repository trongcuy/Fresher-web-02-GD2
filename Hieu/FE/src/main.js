import { createApp } from 'vue'
import router from './router/router'
import store from './store/store';
import App from './App.vue'
import clickOutside from './common/directive/clickOutside';

const app = createApp(App);

app.directive('clickOutside', clickOutside);

app.use(router);
app.use(store);

app.mount('#app');
