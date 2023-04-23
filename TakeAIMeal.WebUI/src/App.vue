<template>
  <router-view/>
</template>

<script>
  import axios from 'axios'
  export default {
      name: 'App'
  }
  
  function getAxios(axios, url) {
    axios
      .get(url)
      .then(function(res){
        console.log(res.data);
      })
    .catch(function(err){
      console.log(err);
    })
    .finally(function(){
      // do something
    })
  }

  function postAxios(axios, url, data) {
    axios
      .post(url, {
        data: data
      })
  }

  function deleteAxios(axios, url, data) {
    axios
      .delete(url, {
        data: data
      })
      .then(function(response)
      {
        console.log(response);
      })
      .catch(function(err){
        console.log(err);
      })
      .finally(function(){
        // do something
      })
  }

  getAxios(axios, 'https://api.github.com/users/section-engineering-education');
</script>