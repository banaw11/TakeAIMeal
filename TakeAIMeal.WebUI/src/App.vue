<template>
  <router-view/>
</template>

<script>
    import { defineComponent } from 'vue'
    import { mapActions } from 'vuex'
    export default defineComponent({
        name: 'App',
        mounted: function(){
            this.restoreSession();
        },
        methods: {
            ...mapActions('context', ['restoreSession']),
        }
    })
</script>