<template>
    <div class="content-header">
        <h1>{{ t('Home.SignUp') }}</h1>
    </div>
    <div class="form-container account-signup">
        <form @submit.prevent="signUp">
            <div class="form-group">
                <input type="text" placeholder="E-mail" v-model="form.email" class="text-dark" />
            </div>
            <div class="form-group">
                <input type="password" v-bind:placeholder="$t('Account.Password')" v-model="form.password" class="text-dark" />
            </div>
            <div class="form-group">
                <input type="password" v-bind:placeholder="$t('Account.RepeatPassword')" v-model="form.repeatPassword" class="text-dark" />
            </div>
            <div class="form-group">
                <input type="text" v-bind:placeholder="$t('Account.UserName')" v-model="form.userName" class="text-dark" />
            </div>
            <div class="form-group">
                <button class="btn btn-secondary" :disabled="!isInValid()" type="submit">{{ t('Home.SignUp') }}</button>
            </div>
        </form>
    </div>
</template>

<script>
import { defineComponent } from 'vue'
import { useI18n } from 'vue-i18n'
import httpClient from '@/modules/http/client'
export default defineComponent({
    name: 'LoginComponent',
    data(){
        return {
            form: {
                email: '',
                password: '',
                repeatPassword: '',
                userName: '',
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
                return this.emailValidate() && this.passwordValidate() && this.repeatPasswordValidate() && this.firstNameValidate();
            }
        }
    },
    methods: {
        signUp() {
            httpClient.post(`/api/Account/sign-up`, this.form)
                .then((response) => {
                    console.log(response);
                    this.$toast.success(this.$t("Account.Registration.Success"));
                    this.$router.push('/');
                }).catch(() => {
                    this.$toast.error(this.$t("Account.Registration.Failed"));
                });
        },
        emailValidate() {
            console.log(this.$route.params); // będzie zawarty obiekt email i code - sprawdzić jak to wygląda
            // check basic e-mail structure
            var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            return this.form.email.length > 0 && this.form.email.match(emailRegex);
        },
        passwordValidate() {
            return this.form.password.length > 7;
        },
        repeatPasswordValidate() {
            return this.form.repeatPassword.length > 7 && this.form.password === this.form.repeatPassword;
        },
        firstNameValidate() {
            return this.form.userName.length > 0;
        }
    }
})
</script>