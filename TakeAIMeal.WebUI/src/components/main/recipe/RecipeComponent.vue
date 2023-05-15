<template>
    <div class="recipe-container">
        <div class="image-area">
            <img :src="data.imageBase64"/>
        </div>
        <div class="recipe-area">
            <label>{{data.title}}</label>
            <span v-html="data.recipe.replace(/(?:\r\n|\r|\n)/g, '<br>')"></span>
        </div>
    </div>
</template>
<script>
    /* eslint-disable */
    import { defineComponent } from 'vue'
    export default defineComponent({
        name: "RecipeComponent",
        props: ['data']
    })
</script>