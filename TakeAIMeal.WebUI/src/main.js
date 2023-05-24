import { createApp } from 'vue'
import App from './App.vue'
import '@/assets/css/site.css'
import 'vue-multiselect/dist/vue3-multiselect.css'
import i18n from './i18n'
import router from './router'
import Vuex from 'vuex'
import store from './modules/store'
import Toaster from "@meforma/vue-toaster";
import { LoadingPlugin } from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/css/index.css';

createApp(App)
    .use(router)
    .use(i18n)
    .use(Vuex)
    .use(store)
    .use(Toaster)
    .use(LoadingPlugin, {
        color: 'rgb(55, 188, 247)',
        width: 80,
        height: 80
    })
    .mount('#app')
