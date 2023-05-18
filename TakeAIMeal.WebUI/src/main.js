import { createApp } from 'vue'
import App from './App.vue'
import '@/assets/css/site.css'
import 'vue-multiselect/dist/vue3-multiselect.css'
import i18n from './i18n'
import router from './router'
import Vuex from 'vuex'
import store from './modules/store'
import Toaster from "@meforma/vue-toaster";

createApp(App).use(router).use(i18n).use(Vuex).use(store).use(Toaster).mount('#app')
