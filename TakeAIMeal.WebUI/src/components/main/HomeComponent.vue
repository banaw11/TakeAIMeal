<template>
    <div class="content-header">
        <h1>Take a<sup>i</sup> meal</h1>
    </div>
    <div class="hint-container">
        <i class="icon-hint"></i>
        <div class="hint-slider">
            {{ this.hintText }}
        </div>
    </div>
    <div class="action-container">
        <div class="left-part">
            <div>
                <button class="btn btn-primary">{{ t('Home.QuickRecipe') }}</button>
            </div>
            <ul>
                <li>{{ t('Home.SimpleRecipeGenerate') }}</li>
            </ul>
        </div>
        <div class="right-part">
            <div>
                <button class="btn btn-secondary">{{ t('Home.SignUp') }}</button>
            </div>
            
            <ul>
                <li>{{ t('Home.SimpleRecipeGenerate') }}</li>
                <li>{{ t('Home.PersonalizeDietData') }}</li>
                <li>{{ t('Home.ChooseProducts') }}</li>
            </ul>
            <div class="sign-in-container">
                <span>{{ t('Home.HaveAccount') }}</span>
                <button class="btn btn-primary">{{ t('Home.SignIn') }}</button>
            </div>
        </div>
    </div>
</template>

<script>
    import { defineComponent } from 'vue'
    import { useI18n } from 'vue-i18n'
    import httpClient from '@/modules/http/client'
    export default defineComponent({
        name: 'HomeComponent',
        data() {
            return {
                hintText: '',
                tips: []
            }
        },
        setup() {
            const { t } = useI18n({
                inheritLocale: true,
                useScope: 'local'
            })

            return { t }
        },
        mounted: function() {
            this.getTips();
            this.initTipsPool();
        },
        beforeUnmount: function(){
            clearInterval(this.pooling)
        },
        methods: {
            getTips: function() {
                let self = this;
                httpClient.get(`/api/tips/${self.$i18n.locale}`)
                    .then(function(response) {
                        self.tips = response.data;
                        console.log(self.tips);
                    });
            },
            initTipsPool: function() {
                let self = this;
                // get new tips from API
                self.pooling = setInterval(function() {
                    self.getTips();
                }, 30000); // 5 minutes 
            }            
        },
        watch: {
            '$i18n.locale': function (newValue, oldValue) {
                if (newValue != oldValue) {
                    this.getTips();
                }
            }
        }
    })
</script>
