import { createRouter, createWebHistory } from 'vue-router'
import MainView from '../views/MainView.vue'
import HomeComponent from '../components/main/HomeComponent.vue'
import AboutComponent from '../components/main/AboutComponent.vue'
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
                path: '/about',
                name: 'about',
                component: AboutComponent
            },
            {
                path: '/quick-recipe',
                name: 'quickRecipe',
                component: QuickRecipeComponent
            }
        ]
    },
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router
