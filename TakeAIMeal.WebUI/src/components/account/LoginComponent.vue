<template>
    <div class="content-header">
        <h1>{{ t('Header.SignIn') }}</h1>
    </div>
    <div class="form-container account-signin">
        <form @submit.prevent="signIn">
            <div class="form-group">
                <input type="text" placeholder="E-mail" v-model="form.email" class="text-dark" />
            </div>
            <div class="form-group">
                <input type="password" v-bind:placeholder="$t('Account.Password')" i18n-placeholder="{{ t('Header.SignIn') }}" v-model="form.password" class="text-dark" />
            </div>
            <div class="form-group">
                <button class="btn btn-secondary" :disabled="!isInValid()" type="submit">{{ t('Header.SignIn') }}</button>
                <span>{{ t('Account.ForgottenPassword') }}</span>
            </div>
        </form>
        <div class="button-container">
            <span>{{ t('Home.HaventAccound') }}</span>
            <router-link class="btn btn-primary" to="/account/registration">{{ t('Home.SignUp') }}</router-link>
        </div>
    </div>
</template>

<script>
import { defineComponent } from 'vue'
import { useI18n } from 'vue-i18n'
import { mapActions } from 'vuex'
export default defineComponent({
    name: 'LoginComponent',
    data(){
        return {
            form: {
                email: '',
                password: ''
            }
        }
    },
    setup() {
        const { t } = useI18n({
            inheritLocale: true,
            useScope: 'local'
        })

        return { t }
    },
    computed: {
        isInValid() {
            return () => {
                return this.emailValidate() && this.passwordValidate();
            }
        }
    },
    methods: {
        ...mapActions('context', ['login']),
        signIn() {
            this.login(this.form)
                .then(() => {
                    this.$toast.success(this.$t("Account.Login.Success"));
                    this.$router.push('/');
                }).catch(() => {
                    this.$toast.error(this.$t("Account.Login.Failed"));
                });
        },
        emailValidate() {
            // check basic e-mail structure
            var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            return this.form.email.length > 0 && this.form.email.match(emailRegex);
        },
        passwordValidate() {
            return this.form.password.length > 7;
        }
    }
})
</script>