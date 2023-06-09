<template>
    <div class="content-header">
        <h1>{{t('PersonalizedRecipe.Title')}}</h1>
    </div>
    <div class="form-container">
        <div class="sub-header">
            <label>{{t('PersonalizedRecipe.ProvideAtleast')}}</label>
        </div>
        <div class="form-group">
            <div class="group">
                <div class="diet-dropdown">
                    <span>{{t('PersonalizedRecipe.Diet')}}</span>
                    <CustomDropdown :data="dietTypes" :multi="false" :translate-path="'Dictionaries.Diets'" v-model="recipeData.dietId"></CustomDropdown>
                </div>
                <div class="meal-dropdown">
                    <span>{{t('PersonalizedRecipe.Meal')}}</span>
                    <CustomDropdown :data="mealTypes" :multi="false" :translate-path="'Dictionaries.Meals'" v-model="recipeData.mealId"></CustomDropdown>
                </div>
            </div>
        </div>
        <div class="form-group" v-for="data in categoryData" :key="data.index">
            <div class="group">
                <div class="category-dropdown">
                    <span>{{t('PersonalizedRecipe.Category')}}</span>
                    <CustomDropdown :data="categoryTypes" :multi="false" :translate-path="'Dictionaries.Categories'" v-model="data.categoryId" @change="() => onCategoryChange(data.index)"></CustomDropdown>
                </div>
                <div class="product-dropdown" v-if="data.productTypes.length > 0">
                    <span>{{t('PersonalizedRecipe.Product')}}</span>
                    <CustomDropdown :data="data.productTypes" :multi="true" :translate-path="'Dictionaries.Products'" v-model="data.productIds"></CustomDropdown>
                </div>
            </div>
            <div class="action-column" v-if="categoryData.length - 1 === data.index && canAddNew()">
                <i class="icon-add" @click="addNewCategory()" :title="t('QuickRecipe.Add')"></i>
            </div>
        </div>
        <div class="button-container">
            <button class="btn btn-primary" :disabled="isInValid()" @click="generateRecipe()">{{ t('PersonalizedRecipe.Generate') }}</button>
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
        name: "PersonalizedRecipeComponent",
        data() {
            return {
                mealTypes: [], // {name : "", value : ""}
                categoryTypes: [], // {name : "", value : ""}
                dietTypes: [], // {name : "", value : ""}
                categoryData: [], // {categoryId : "", productTypes : [], productIds : [] , index : 0 }
                recipeData: { mealId: null, productIds: [] , dietId: null},
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
                    let products = 0;
                    this.categoryData.filter((data) => data.productIds.length > 0).forEach((data) => {
                        products += data.productIds.length;
                    });
                    return isNaN(this.recipeData.mealId?.value) || products < 3 || isNaN(this.recipeData.dietId?.value);
                }
            },
            canAddNew() {
                return () => {
                    return this.categoryData.filter((data) => data.productIds.length > 0).length === this.categoryData.length;
                }
            }
        },
        beforeMount: function () {
            this.categoryData = [
                {
                    categoryId: null,
                    productTypes: [],
                    productIds: [],
                    index: 0
                }];
            this._loadMealTypes();
            this._loadCategoryTypes();
            this._loadDietTypes();
        },
        methods: {
            _loadDietTypes() {
                httpClient.get('/api/dictionary/diet-used-types')
                    .then((response) => {
                        this.dietTypes = response.data;
                    });
            },
            _loadMealTypes() {
                httpClient.get('/api/dictionary/meal-types')
                    .then((response) => {
                        this.mealTypes = response.data;
                    });
            },
            _loadCategoryTypes() {
                httpClient.get('/api/dictionary/category-types')
                    .then((response) => {
                        this.categoryTypes = response.data;
                    });
            },
            _getProductTypes(categoryId) {
                return new Promise((resolve, reject) => {
                    httpClient.get(`/api/dictionary/product-types/${categoryId}`)
                        .then((response) => {
                            resolve(response.data);
                        })
                        .catch(() => {
                            resolve([]);
                        });
                });
            },
            addNewCategory() {
                this.categoryData.push({
                    categoryId: null,
                    productTypes: [],
                    productIds: [],
                    index: this.categoryData.length
                })
            },
            onCategoryChange(index) {
                const dataIndex = this.categoryData.findIndex((item) => item.index === index);
                if (dataIndex > -1 && !isNaN(this.categoryData[dataIndex].categoryId?.value)) {
                    this._getProductTypes(this.categoryData[dataIndex].categoryId.value)
                        .then((dict) => {
                            this.categoryData[dataIndex].productTypes = dict;
                            this.categoryData[dataIndex].productIds = [];
                        });
                }
                else if (dataIndex > -1) {
                    this.categoryData[dataIndex].productTypes = [];
                    this.categoryData[dataIndex].productIds = [];
                }
            },
            generateRecipe() {
                let products = [];
                this.categoryData.filter((data) => data.productIds.length > 0).forEach((data) => {
                    data.productIds.map((item) => item.value).forEach(id => {
                        products.push(id);
                    })
                });
                const model = {
                    language: this.$i18n.locale,
                    mealType: this.recipeData.mealId.value,
                    products: products,
                    dietType: this.recipeData.dietId.value
                };
                httpClient.post('api/recipe/generate-personalized', model)
                    .then((response) => {
                        this.recipe = response.data;
                    });
            }
        }
    })

</script>