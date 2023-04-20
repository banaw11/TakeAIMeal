<template>
  <div>
    <ContentComponent />
  </div>
</template>

<script>
// @ is an alias to /src
import ContentComponent from '@/components/Content.vue' // content

export default {
  name: 'HomeView',
  components: {
    ContentComponent
  }
}
</script>
