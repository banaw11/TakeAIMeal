<template>
  <!-- <img alt="Vue logo" src="./assets/logo.png"> -->
  <!-- <HelloWorld msg="Welcome to Your Vue.js App"/> -->
  <NavComponent />
  <ContentComponent />
  <FooterComponent />
</template>

<script>
// import HelloWorld from './components/HelloWorld.vue'
import NavComponent from './components/Nav.vue'
import ContentComponent from './components/Content.vue'
import FooterComponent from './components/Footer.vue'

export default {
  name: 'App',
  components: {
    // HelloWorld,
    NavComponent,
    ContentComponent,
    FooterComponent
  }
}
</script>

<style>
/* #app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
} */
</style>
