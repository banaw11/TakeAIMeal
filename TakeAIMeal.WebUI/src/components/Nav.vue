<template>
    <div class="page-header">
        <nav class="navbar navbar-expand-lg navbar-light bg-white">
            <a class="navbar-brand" href="#">Take ai meal</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
                <div class="navbar-nav">
                    <a class="nav-item nav-link active" href="#">{{ t('Header.Home') }}<span class="sr-only">(current)</span></a>
                    <a class="nav-item nav-link" href="#">{{ t('Header.About') }}</a>
                    <a class="nav-item nav-link" href="#">{{ t('Header.SignIn') }}</a>
                </div>
            </div>
            <div class="language-container">
                <i v-if="isCurrentLocale('pl')" class="icon-flag pl" @click="changeLanguage('en')"></i>
                <i v-if="isCurrentLocale('en')" class="icon-flag en" @click="changeLanguage('pl')"></i>
            </div>
        </nav>

        <!-- TODO: nie działa "hamburger" na widoku mobilnym -->
    </div>
</template>

<script>
    import { defineComponent } from 'vue'
    import { useI18n } from 'vue-i18n'
    export default defineComponent({
        name: 'NavComponent',
        methods: {
            changeLanguage(locale) {
                this.$i18n.locale = locale
            },
            isCurrentLocale(locale) {
                return this.$i18n.locale === locale
            }
        },
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
  