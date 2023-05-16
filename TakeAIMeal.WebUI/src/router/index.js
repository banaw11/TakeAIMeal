import { createRouter, createWebHistory } from 'vue-router'
import MainView from '../views/MainView.vue'
import HomeComponent from '../components/main/HomeComponent.vue'
import AboutComponent from '../components/main/AboutComponent.vue'
import AccountView from '../views/AccountView.vue'
import LoginComponent from '../components/account/LoginComponent.vue'
import RegistrationComponent from '../components/account/RegistrationComponent.vue'
import QuickRecipeComponent from '../components/main/recipe/QuickRecipeComponent.vue'

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
        path: 'about',
        name: 'about',
        component: AboutComponent
      },
      {
        path: 'quick-recipe',
        name: 'quickRecipe',
        component: QuickRecipeComponent
      }
    ]
  },
  {
    path: '/account',
    name: 'account',
    component: AccountView,
    children: [
      {
        path: 'login',
        name: 'login',
        component: LoginComponent
      },
      {
        path: 'registration',
        name: 'registration',
        component: RegistrationComponent
      }
    ]
  }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router
