<template>
    <div class="recipe-container">
        <div class="image-area">
            <img :src="data.model.imageBase64"/>
            <button class="btn btn-primary" @click="saveRecipe" v-if="canSave && !saved && isAuthenticated">{{ t('Recipe.Save') }}</button>
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
    import { useI18n } from 'vue-i18n'
    import httpClient from '@/modules/http/client'
    import { mapGetters } from 'vuex'
    export default defineComponent({
        name: "RecipeComponent",
        props: ['data', 'mealType', 'canSave'],
        data() {
            return {
                saved: false
            }
        },
        setup() {
            const { t } = useI18n({
                inheritLocale: true,
                useScope: 'local'
            })

            return { t }
        },
        computed: {
            ...mapGetters('context', ['isAuthenticated']),
        },
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
            },
            saveRecipe() {
                const recipeReference = {
                    identifier: this.data.identifier,
                    mealType: this.mealType
                };
                httpClient.post('/api/recipe/save-recipe', recipeReference)
                    .then((response) => {
                        this.saved = true;
                        this.$toast.success(this.$t("Recipe.SaveSuccess"));
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