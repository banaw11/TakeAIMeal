<template>
    <h1>Aktywacja konta</h1>
    <p>Dziękujemy za wybranie naszej aplikacji, weryfikacja jest konieczna ze względu na politykę aplikacji</p>
    <!-- Bartek zostawiam poglądowo te 2 poniższe linijki, pobiera prawidłowo -->
    <h1>Email: {{$route.query.email}}</h1>
    <h1>Code: {{$route.query.code}}</h1>
</template>

<script>
    import { defineComponent } from 'vue'
    import { useI18n } from 'vue-i18n'
    import { mapGetters, mapState, mapActions } from 'vuex'
    import httpClient from '@/modules/http/client'
    export default defineComponent({
        name: 'EmailConfirmationComponent',
        computed: {
            ...mapState('context', ['profile']),
            ...mapGetters('context', ['isAuthenticated']),
        },
        data() {
            return {
                email: '',
                code: ''
            }
        },
        methods: {
            ...mapActions('context', ['logout']),
            changeLanguage(locale) {
                this.$i18n.locale = locale
            },
            isCurrentLocale(locale) {
                return this.$i18n.locale === locale
            },
            emailConfirmation() {
                httpClient.post(`/api/Account/email-confirmation`, {
                    email: this.$route.query.email,
                    code: this.$route.query.code
                })
                    .then(() => {
                        this.$toast.success(this.$t("Account.Login.Failed"));
                        setInterval(redirectToLogin(), 5000);
                    }).catch(() => {
                        this.$toast.error(this.$t("Account.Login.Failed"));
                    });
            },
            redirectToLogin() {
                this.$router.push('/account/login');
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
  