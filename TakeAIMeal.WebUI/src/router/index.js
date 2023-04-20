import { createRouter, createWebHistory } from 'vue-router'
import MainView from '../views/MainView.vue'

const routes = [
  {
    path: '/',
    name: 'home',
    component: MainView,
    children: [
      {
        path: '',
        component: () => import('../components/HomeComponent.vue')
      },
      {
        path: '/about',
        component: () => import('../components/AboutComponent.vue')
      }
    ]
  },
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
