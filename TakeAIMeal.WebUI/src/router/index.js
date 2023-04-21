import { createRouter, createWebHistory } from 'vue-router'
import MainView from '../views/MainView.vue'
import HomeComponent from '../components/HomeComponent.vue'
import AboutComponent from '../components/AboutComponent.vue'

const routes = [
  {
    path: '/',
    name: 'main',
    component: MainView,
    children: [
      {
        path: '',
        name: 'home',
        component: HomeComponent
      },
      {
        path: '/about',
        name: 'about',
        component: AboutComponent
      }
    ]
  },
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router