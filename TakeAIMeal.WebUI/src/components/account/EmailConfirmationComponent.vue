<template>
    <h1>{{ t('Account.EmailConfirmation.AccountActivation') }}</h1>
    <p>{{ t('Account.EmailConfirmation.Information') }}</p>
</template>

<script>
    import { defineComponent } from 'vue'
    import { useI18n } from 'vue-i18n'
    import httpClient from '@/modules/http/client'
    export default defineComponent({
        name: 'EmailConfirmationComponent',
        methods: {
            emailConfirmation() {
                httpClient.post(`/api/Account/email-confirmation`, {
                    email: this.$route.query.email,
                    code: this.$route.query.code
                })
                    .then(() => {
                        this.$toast.success(this.$t("Account.EmailConfirmation.Success"));
                        setTimeout(this._redirectToLogin(), 5000);
                    }).catch(() => {
                        this.$toast.error(this.$t("Account.EmailConfirmation.Failed"));
                    });
            },
            _redirectToLogin() {
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
  