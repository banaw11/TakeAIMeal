<template>
    <NavigationComponent /> 
    <div class="page-container" ref="loaderContainer">
        <div class="content-container">
            <router-view></router-view>
        </div>
    </div>
</template>

<script>
// @ is an alias to /src
import NavigationComponent from '@/components/main/NavigationComponent.vue'

export default { 
    name: 'MainView',
    components: {
        NavigationComponent
    },
}
</script>