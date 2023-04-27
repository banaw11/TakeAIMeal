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
                tips: [],
                i: 0,
                currentLanguage: ''
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
            let self = this;
            self.getTips();
            self.showTip(self.tips[0])
            self.currentLanguage = self.$i18n.locale

            // get new tips from API
            window.setInterval(function() {
                self.getTips();
            }, 30000); // 5 minutes 

            // reload tip
            window.setInterval(function() {
                if(self.currentLanguage != self.$i18n.locale) {
                    self.currentLanguage = self.$i18n.locale;
                    self.getTips();
                }
                if(self.i >= self.tips.length)
                    self.i = 0;
                self.showTip(self.tips[self.i]);
            }, 5000); // 5 seconds
            
        },
        methods: {
            showTip: function(tip) {
                this.hintText = tip;
                this.i++;
            },
            getTips: function() {
                let self = this;
                httpClient.get('https://take-ai-meal-app.azurewebsites.net/api/Tips/' + self.$i18n.locale)
                    .then(function(response) {
                        self.tips = response.data;
                        self.hintText = self.tips[0];
                    });
            }
        }
    })
</script>
