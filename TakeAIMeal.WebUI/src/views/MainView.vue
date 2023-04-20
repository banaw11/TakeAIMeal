<template>
    <NavigationComponent /> 
    <router-view></router-view>
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