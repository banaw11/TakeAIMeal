<template>
    <NavigationComponent /> 
    <div class="page-container">
        <div class="content-container">
            <router-view></router-view>
        </div>
    </div>
</template>

<script>
// @ is an alias to /src
import NavigationComponent from '@/router/Navigation.vue'

export default { 
    name: 'MainView',
    components: {
        NavigationComponent
    },
}
</script>