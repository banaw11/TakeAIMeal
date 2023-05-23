<template>
    <div class="content-header">
        <h1>{{t('RandomRecipe.Title')}}</h1>
    </div>
    <div class="form-container">
        <div class="sub-header">
            <label>{{t('RandomRecipe.ProvideMealType')}}</label>
        </div>
        <div class="form-group">
            <div class="meal-dropdown">
                <span>{{t('RandomRecipe.Meal')}}</span>
                <CustomDropdown :data="mealTypes" :multi="false" :translate-path="'Dictionaries.Meals'" v-model="recipeData.mealId"></CustomDropdown>
            </div>
        </div>
        <div class="button-container">
            <button class="btn btn-primary" :disabled="isInValid()" @click="generateRecipe()">{{ t('RandomRecipe.Generate') }}</button>
        </div>
    </div>
    <RecipeComponent v-if="recipe != null" :data="recipe" :meal-type="recipeData.mealId?.value" :can-save="true"></RecipeComponent>
</template>

<script>
    /* eslint-disable */
    import { defineComponent } from 'vue'
    import { useI18n } from 'vue-i18n'
    import httpClient from '@/modules/http/client'
    import CustomDropdown from '../../shared/CustomDropdownComponent.vue'
    import RecipeComponent from './RecipeComponent.vue'
    export default defineComponent({
        name: "RandomRecipeComponent",
        data() {
            return {
                mealTypes: [], // {name : "", value : ""}
                recipeData: { mealId: null },
                recipe: null
            }
        },
        setup() {
            const { t } = useI18n({
                inheritLocale: true,
                useScope: 'local'
            })

            return { t }
        },
        components: {
            CustomDropdown,
            RecipeComponent
        },
        computed: {
            isInValid() {
                return () => {
                    return isNaN(this.recipeData.mealId?.value);
                }
            }
        },
        beforeMount: function () {
            this._loadMealTypes();
        },
        methods: {
            _loadMealTypes() {
                httpClient.get('/api/dictionary/meal-types')
                    .then((response) => {
                        this.mealTypes = response.data;
                    });
            },
            generateRecipe() {
                const model = {
                    language: this.$i18n.locale,
                    mealType: this.recipeData.mealId.value
                };
                httpClient.post('api/recipe/generate-random', model)
                    .then((response) => {
                        this.recipe = response.data;
                    });
            }
        }
    })
</script>