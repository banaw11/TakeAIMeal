<template>
    <h1>{{ t('Header.SignIn') }}</h1>
    <form>
        <input type="text" placeholder="E-mail" v-model="email" class="text-dark"  /><br/>
        <input type="password" v-bind:placeholder="$t('Account.Password')" i18n-placeholder="{{ t('Header.SignIn') }}" v-model="password" class="text-dark"  /><br/>
        <button class="btn btn-primary" :disabled="!isInValid()" @click="signIn()">{{ t('Header.SignIn') }}</button>
        <span>{{ t('Account.ForgottenPassword') }}</span>
        <br/>
        <span>{{ t('Home.HaventAccound') }}</span>
        <br/>
        <router-link class="btn btn-secondary" to="/account/registration">{{ t('Home.SignUp') }}</router-link>
    </form>
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
            let self = this;
            httpClient.post(`/api/Account/sign-in`, {
                email: this.email,
                password: this.password
            })
                .then((response) => {
                    console.log(response);
                    const status = JSON.parse(response.status);
                    if(status == '200')
                        self.$router.push('/');
                })
        },
        emailValidate() {
            // https://stackoverflow.com/questions/46155/how-can-i-validate-an-email-address-in-javascript
            // sprawdza czy e-mail jest w formacie e-mail i czy nie zawiera więcej niż jeden znaków @
            var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            return this.email.length > 0 && this.email.match(emailRegex) ? true : false;
        },
        passwordValidate() {
            // ustalić jakie ma być minimalne hasło, ewentualnie jakie ma zawierać znaki
            return this.password.length > 5 ? true : false;
        }
    }
})
</script>