<template>
  <router-view/>
</template>

<script>
  import httpClient from './modules/http/client';
  export default {
      name: 'App',
      mounted(){
        httpClient.get('https://api.github.com/users/section-engineering-education')
          .then(function(response) {
            console.log(response.data.login);
          })
      }
  }
  
</script>