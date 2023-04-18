<template>
  <NavComponent />
  <ContentComponent />
</template>

<script>
import NavComponent from './components/Nav.vue'
import ContentComponent from './components/Content.vue'

export default {
    name: 'App',
    components: {
        NavComponent,
        ContentComponent
    }
}
</script>
