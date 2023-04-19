<template>
    <div class="page-container">
        <!-- Temporary code should be move into dashboard/home component-->
        <div class="content-container">
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
        </div>
    </div>
</template>

<script>
    import { defineComponent } from 'vue'
    import { useI18n } from 'vue-i18n'
    export default defineComponent({
        name: 'ContentComponent',
        setup() {
            const { t } = useI18n({
                inheritLocale: true,
                useScope: 'local'
            })

            return { t }
        }
    })
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
  