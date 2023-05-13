import { createApp } from 'vue'
import App from './App.vue'
import '@/assets/css/site.css'
import i18n from './i18n'
import router from './router'
import Vuex from 'vuex'
import store from './modules/store'

createApp(App).use(router).use(i18n).use(Vuex).use(store).mount('#app')
