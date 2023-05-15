<template>
    <div class="recipe-container">
        <div class="image-area">
            <img :src="data.model.imageBase64"/>
        </div>
        <div class="recipe-area">
            <label>{{data.model.title}}</label>
            <span v-html="data.model.recipe.replace(/(?:\r\n|\r|\n)/g, '<br>')"></span>
        </div>
    </div>
</template>
<script>
    /* eslint-disable */
    import { defineComponent } from 'vue'
    import httpClient from '@/modules/http/client'
    export default defineComponent({
        name: "RecipeComponent",
        props: ['data'],
        methods: {
            _reloadRecipe(language) {
                const params = {
                    identifier: this.data.identifier,
                    language: language
                }
                httpClient.get('/api/recipe/get-recipe', { params })
                    .then((response) => {
                        const model = response.data;
                        this.data.model.title = model.title;
                        this.data.model.recipe = model.recipe
                    });
            }
        },
        watch: {
            '$i18n.locale': function (newValue, oldValue) {
                if (newValue != oldValue) {
                    this._reloadRecipe(newValue);
                }
            }
        }
    })
</script>