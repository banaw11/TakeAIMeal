<template>
    <div class="content-header">
        <h1>{{ t('Header.SignIn') }}</h1>
    </div>
    <div class="form-container account-signin">
        <!-- zastanawiam się czy from jest potrzebny, bo i tak wysyłamy jsona -->
        <!-- <form> -->
            <div class="form-group">
                <input type="text" placeholder="E-mail" v-model="email" class="text-dark" />
            </div>
            <div class="form-group">
                <input type="password" v-bind:placeholder="$t('Account.Password')" i18n-placeholder="{{ t('Header.SignIn') }}" v-model="password" class="text-dark" />
            </div>
            <div class="form-group">
                <button class="btn btn-secondary" :disabled="!isInValid()" @click="signIn()">{{ t('Header.SignIn') }}</button>
                <span>{{ t('Account.ForgottenPassword') }}</span>
            </div>
        <!-- </form> -->
        <div class="button-container">
            <span>{{ t('Home.HaventAccound') }}</span>
            <router-link class="btn btn-primary" to="/account/registration">{{ t('Home.SignUp') }}</router-link>
        </div>
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
            email: '',
            password: ''
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
                return this.emailValidate() && this.passwordValidate() ? true : false;
            }
        }
    },
    methods: {
        signIn() {
            // tutaj póki co api zwraca error ale widzę, że na produkcji też (wyślę screena z consoli)
            httpClient.post(`/api/Account/sign-in`, {
                email: this.email,
                password: this.password
            })
                .then((response) => {
                    const status = JSON.parse(response.success);
                    if(status == true) {
                        this.$toast.success(this.$t("Account.LoginSuccess"));
                        this.$router.push('/');
                    } else {
                        this.$toast.error(this.$t("Account.LoginFailed"));
                    } 
                })
        },
        emailValidate() {
            // check basic e-mail structure
            var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            return this.email.length > 0 && this.email.match(emailRegex) ? true : false;
        },
        passwordValidate() {
            return this.password.length > 7 ? true : false;
        }
    }
})
</script>