<template>
    <div class="content-header">
        <h1>{{t('SavedRecipe.Title')}}</h1>
    </div>
    <div v-if="data != null" class="action-container saved-recipe">
        <button class="btn btn-primary" @click="deleteRecipe">{{ t('SavedRecipe.Delete') }}</button>
    </div>
    <RecipeComponent v-if="data != null" :data="data" :can-save="false"></RecipeComponent>
</template>

<script>
    /* eslint-disable */
    import { defineComponent } from 'vue'
    import { useI18n } from 'vue-i18n'
    import httpClient from '@/modules/http/client'
    import RecipeComponent from './RecipeComponent.vue'
    export default defineComponent({
        name: "SavedRecipeComponent",
        data() {
            return {
                data: null
            }
        },
        setup() {
            const { t } = useI18n({
                inheritLocale: true,
                useScope: 'local'
            })

            return { t }
        },
        methods: {
            _loadRecipe() {
                const params = {
                    identifier: this.$route.params.identifier,
                    language: this.$i18n.locale
                }
                httpClient.get('/api/recipe/get-saved-recipe', { params })
                    .then((response) => {
                        this.data = response.data;
                    });
            },
            deleteRecipe() {
                httpClient.delete(`api/recipe/delete-recipe/${this.data.id}`)
                    .then(() => {
                        this.$toast.success(this.$t("SavedRecipe.DeleteSuccess"));
                        setTimeout(() => {
                            this.$router.push({
                                name: "cookbook"
                            })
                        }, 2000);
                    })
            }
        },
        components: {
            RecipeComponent
        },
        mounted: function () {
            this._loadRecipe();
        }
    })
</script>