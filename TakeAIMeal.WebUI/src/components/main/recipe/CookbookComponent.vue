<template>
    <div class="content-header">
        <h1>{{t('Cookbook.Title')}}</h1>
    </div>
    <div class="action-container cookbook">
        <router-link to="/quick-recipe" custom v-slot="{ navigate }">
            <button class="btn btn-primary" @click="navigate">{{ t('Cookbook.QuickRecipe') }}</button>
        </router-link>
        <router-link to="/personalized-recipe" custom v-slot="{ navigate }">
            <button class="btn btn-secondary" @click="navigate">{{ t('Cookbook.PersonalizedRecipe') }}</button>
        </router-link>
        <router-link to="/random-recipe" custom v-slot="{ navigate }">
            <button class="btn btn-secondary" @click="navigate">{{ t('Cookbook.RandomRecipe') }}</button>
        </router-link>
    </div>
    <div class="grid-container" v-if="collection.length > 0">
        <div class="list-row" v-for="data in collection" :key="data.recipeIdentifier">
            <img :src="data.imageBase64" />
            <label>{{data.title}}</label>
            <button class="btn btn-primary" @click="goToRecipe(data.recipeIdentifier)">{{ t('Cookbook.See') }}</button>
        </div>
    </div>
</template>

<script>
    /* eslint-disable */
    import { defineComponent } from 'vue'
    import { useI18n } from 'vue-i18n'
    import httpClient from '@/modules/http/client'
    export default defineComponent({
        name: "CookbookComponent",
        data() {
            return {
                collection: [] // { imageBase64 : "", title : "", recipeIdentifier : ""}
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
            goToRecipe(identifier) {
                this.$router.push({
                    name: "savedRecipe",
                    params: {identifier : identifier}
                })
            },
            _loadCollection() {
                const query = {
                    language: this.$i18n.locale
                };
                httpClient.get('/api/recipe/get-list', { params: query })
                    .then((response) => {
                        this.collection = response.data;
                    })
            }
        },
        mounted: function () {
            this._loadCollection();
        },
        watch: {
            '$i18n.locale': function (newValue, oldValue) {
                if (newValue != oldValue) {
                    this._loadCollection();
                }
            }
        }
    })
</script>