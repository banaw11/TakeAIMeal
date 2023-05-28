<template>
    <div v-if="isLoading === true" class="loader">
        <div class="lds-ring"><div></div><div></div><div></div><div></div></div>
    </div>
</template>

<script>
    /* eslint-disable */
    import { defineComponent } from 'vue'
    export default defineComponent({
        name: "LoaderComponent",
        props: ["isLoading"]
    })
</script>