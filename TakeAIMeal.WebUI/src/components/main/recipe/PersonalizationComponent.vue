<template>
    <div class="content-header">
        <h1>{{t('Personalization.Title')}}</h1>
    </div>
    <div class="form-container">
        <div class="form-group personalization-user-area">
            <div class="group">
                <div class="user-container">
                    <i class="user"></i>
                    <span class="user-name">{{t('Personalization.Welcome') + profile.userName }}</span>
                </div>
                <div class="meal-dropdown">
                    <span>{{t('Personalization.Diet')}}</span>
                    <CustomDropdown :data="dietTypes" :multi="false" :translate-path="'Dictionaries.Diets'" v-model="dietData.dietType" @change="() => onDietChange()"></CustomDropdown>
                </div>
            </div>
        </div>
        <div class="sub-header">
            <label>{{t('Personalization.ProductExclusions')}}</label>
        </div>
        <div class="form-group" v-for="data in categoryData" :key="data.index">
            <div class="group">
                <div class="category-dropdown">
                    <span>{{t('Personalization.Category')}}</span>
                    <CustomDropdown :data="categoryTypes" :multi="false" :translate-path="'Dictionaries.Categories'" v-model="data.categoryId" @change="() => onCategoryChange(data.index)"></CustomDropdown>
                </div>
                <div class="product-dropdown" v-if="data.productTypes.length > 0">
                    <span>{{t('Personalization.Product')}}</span>
                    <CustomDropdown :data="data.productTypes" :multi="true" :translate-path="'Dictionaries.Products'" v-model="data.productIds"></CustomDropdown>
                </div>
            </div>
            <div class="action-column" v-if="categoryData.length - 1 === data.index && canAddNew()">
                <i class="icon-add" @click="addNewCategory()" :title="t('QuickRecipe.Add')"></i>
            </div>
        </div>
        <div class="button-container">
            <button class="btn btn-primary" :disabled="isInvalid()" @click="saveDiet()">{{ t('Personalization.Save') }}</button>
        </div>
    </div>
</template>

<script>
    /* eslint-disable */
    import { defineComponent } from 'vue'
    import { useI18n } from 'vue-i18n'
    import httpClient from '@/modules/http/client'
    import CustomDropdown from '../../shared/CustomDropdownComponent.vue'
    import { mapState} from 'vuex'
    export default defineComponent({
        name: "PersonalizationComponent",
        setup() {
            const { t } = useI18n({
                inheritLocale: true,
                useScope: 'local'
            })

            return { t }
        },
        components: {
            CustomDropdown
        },
        data() {
            return {
                dietTypes: [],
                categoryTypes: [], // {name : "", value : ""}
                categoryData: [], // {categoryId : "", productTypes : [], productIds : [] , index : 0 }
                dietData: { dietType: null, productExclusions: [] }
            }
        },
        computed: {
            ...mapState('context', ['profile']),
            isInvalid() {
                return () => {
                    return isNaN(this.dietData.dietType?.value);
                }
            },
            canAddNew() {
                return () => {
                    return this.categoryData.filter((data) => data.productIds.length > 0).length === this.categoryData.length;
                }
            }
        },
        methods: {
            saveDiet() {
                const model = {
                    dietType: this.dietData.dietType.value,
                    productExclusions: this.categoryData.map((data) => {
                        return {
                            categoryId: data.categoryId.value,
                            productIds: data.productIds.map((product) => {
                                return product.value
                            })
                        }
                    })
                };
                httpClient.patch('/api/diet/update', model)
                    .then(() => {
                        this.$toast.success(this.$t("Personalization.Success"));
                    })
            },
            _loadDietTypes() {
                httpClient.get('/api/dictionary/diet-all-types')
                    .then((response) => {
                        this.dietTypes = response.data;
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
            _getProductExclusions(dietType) {
                const params = {
                    dietType: dietType
                };
                return new Promise((resolve, reject) => {
                    httpClient.get(`/api/diet/product-exclusions/`, {params: params})
                        .then((response) => {
                            resolve(response.data);
                        })
                        .catch(() => {
                            resolve([]);
                        });
                });
            },
            _fillCategoryData(data) {
                this.categoryData = [];
                if (data && data.length > 0) {
                    data.forEach((item) => {
                        let temp = {
                            categoryId: this.categoryTypes.find((type) => { return type.value === item.categoryId }),
                            productTypes: [],
                            productIds: [],
                            index: this.categoryData.length
                        };
                        this._getProductTypes(item.categoryId)
                            .then((products) => {
                                temp.productTypes = products;
                                item.productIds.forEach((id) => {
                                    temp.productIds.push(products.find((type) => { return type.value === id }));
                                })
                            })
                            .finally(() => {
                                this.categoryData.push(temp);
                            });

                    })
                }
                else {
                    this.categoryData.push({
                        categoryId: null,
                        productTypes: [],
                        productIds: [],
                        index: 0
                    });
                }
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
            onDietChange() {
                if (!isNaN(this.dietData.dietType?.value)) {
                    this._getProductExclusions(this.dietData.dietType.value)
                        .then((data) => {
                            this._fillCategoryData(data);
                        })
                }
                else {
                    this.categoryData = [];
                    this.categoryData.push({
                        categoryId: null,
                        productTypes: [],
                        productIds: [],
                        index: 0
                    });
                }
            },
            addNewCategory() {
                this.categoryData.push({
                    categoryId: null,
                    productTypes: [],
                    productIds: [],
                    index: this.categoryData.length
                })
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
            this._loadDietTypes();
            this._loadCategoryTypes();
        }
    })
</script>