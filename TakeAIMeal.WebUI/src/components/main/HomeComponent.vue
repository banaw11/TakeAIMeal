<template>
    <div class="content-header">
        <h1>Take a<sup>i</sup> meal</h1>
    </div>
    <div class="hint-container">
        <i class="icon-hint"></i>
        <div class="hint-slider">

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
                <router-link class="btn btn-secondary" to="/registration">{{ t('Home.SignUp') }}</router-link>
            </div>
            
            <ul>
                <li>{{ t('Home.SimpleRecipeGenerate') }}</li>
                <li>{{ t('Home.PersonalizeDietData') }}</li>
                <li>{{ t('Home.ChooseProducts') }}</li>
            </ul>
            <div class="sign-in-container">
                <span>{{ t('Home.HaveAccount') }}</span>
                <router-link class="btn btn-primary" to="/login">{{ t('Header.SignIn') }}</router-link>
            </div>
        </div>
    </div>
</template>

<script>
    import { defineComponent } from 'vue'
    import { useI18n } from 'vue-i18n'
    export default defineComponent({
        name: 'HomeComponent',
        setup() {
            const { t } = useI18n({
                inheritLocale: true,
                useScope: 'local'
            })

            return { t }
        }
    })
</script>
