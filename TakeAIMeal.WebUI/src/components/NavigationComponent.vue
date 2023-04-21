<template>
    <div class="page-header">
        <nav class="navbar navbar-expand-lg navbar-light bg-white">
            <router-link class="navbar-brand" to="/"><i class="logo"></i></router-link>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
                <div class="navbar-nav">
                <router-link class="nav-item nav-link active" to="/">{{ t('Header.Home') }}<span class="sr-only">(current)</span></router-link>
                    <router-link class="nav-item nav-link" to="/about">{{ t('Header.About') }}</router-link>
                    <router-link class="nav-item nav-link" to="">{{ t('Header.SignIn') }}</router-link>
                </div>
            </div>
            <div class="language-container">
                <i v-if="isCurrentLocale('pl')" class="icon-flag pl" @click="changeLanguage('en')"></i>
                <i v-if="isCurrentLocale('en')" class="icon-flag en" @click="changeLanguage('pl')"></i>
            </div>
        </nav>
    </div>
</template>

<script>
    import { defineComponent } from 'vue'
    import { useI18n } from 'vue-i18n'
    export default defineComponent({
        name: 'NavigationComponent',
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
  