<template>
    <div class="content-header">
        <h1>Take a<sup>i</sup> meal</h1>
    </div>
    <div class="hint-container">
        <i class="icon-hint"></i>
        <div class="hint-slider">
            <CaruselComponent v-bind:data="tips"/>
        </div>
    </div>
    <div class="action-container">
        <div class="left-part">
            <div>
                <router-link to="/quick-recipe" custom v-slot="{ navigate }">
                    <button class="btn btn-primary" @click="navigate">{{ t('Home.QuickRecipe') }}</button>
                </router-link>
            </div>
            <ul>
                <li>{{ t('Home.SimpleRecipeGenerate') }}</li>
            </ul>
        </div>
        <div class="right-part" v-if="!isAuthenticated">
            <div>
                <router-link to="/account/registration" custom v-slot="{ navigate }">
                    <button class="btn btn-secondary" @click="navigate">{{ t('Home.SignUp') }}</button>
                </router-link>
            </div>

            <ul>
                <li>{{ t('Home.SimpleRecipeGenerate') }}</li>
                <li>{{ t('Home.PersonalizeDietData') }}</li>
                <li>{{ t('Home.ChooseProducts') }}</li>
            </ul>
            <div class="sign-in-container">
                <span>{{ t('Home.HaveAccount') }}</span>
                <router-link to="/account/login" custom v-slot="{ navigate }">
                    <button class="btn btn-primary" @click="navigate">{{ t('Home.SignIn') }}</button>
                </router-link>
            </div>
        </div>
        <div class="right-part" v-if="isAuthenticated">
            <div>
                <router-link to="/cookbook" custom v-slot="{ navigate }">
                    <button class="btn btn-secondary" @click="navigate">{{ t('Home.Cookbook') }}</button>
                </router-link>
            </div>
            <ul>
                <li>{{ t('Home.OpenPersonalizedCookBok') }}</li>
            </ul>
        </div>
    </div>
</template>

<script>
    import { defineComponent } from 'vue'
    import { useI18n } from 'vue-i18n'
    import CaruselComponent from '../shared/CaruselComponent.vue'
    import httpClient from '@/modules/http/client'
    import { mapGetters } from 'vuex'
    export default defineComponent({
        name: 'HomeComponent',
        data() {
            return {
                tips : []
            }
        },
        computed: {
            ...mapGetters('context', ['isAuthenticated']),
        },
        setup() {
            const { t } = useI18n({
                inheritLocale: true,
                useScope: 'local'
            })

            return { t }
        },
        components: {
            CaruselComponent
        },
        beforeMount: function () {
            this.loadTips();
        },
        mounted: function(){
            
            this.initTipsPool();
        },
        beforeUnmount: function(){
            clearInterval(this.pooling)
        },
        methods: {
            loadTips() {
                httpClient.get(`/api/tips/${this.$i18n.locale}`)
                    .then((response) => {
                        this.tips = response.data;
                    })
            },
            initTipsPool: function () {
                // get new tips from API
                this.pooling = setInterval(() => {
                    this.loadTips();
                }, 300000); // 5 minutes 
            }
        },
        watch: {
            '$i18n.locale': function (newValue, oldValue) {
                if (newValue != oldValue) {
                    this.loadTips()
                }
            }
        }
    })
</script>
