<template>
    <div class="content-header">
        <h1>{{ t('Home.SignUp') }}</h1>
    </div>
    <div class="form-container account-signup">
        <form>
            <div class="form-group">
                <input type="text" placeholder="E-mail" v-model="email" class="text-dark" />
            </div>
            <div class="form-group">
                <input type="password" v-bind:placeholder="$t('Account.Password')" v-model="password" class="text-dark" />
            </div>
            <div class="form-group">
                <input type="password" v-bind:placeholder="$t('Account.RepeatPassword')" v-model="repeatPassword" class="text-dark" />
            </div>
            <div class="form-group">
                <input type="text" v-bind:placeholder="$t('Account.UserName')" v-model="userName" class="text-dark" />
            </div>
            <div class="form-group">
                <button class="btn btn-secondary" :disabled="!isInValid()" @click="signUp()">{{ t('Home.SignUp') }}</button>
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
            email: '',
            password: '',
            repeatPassword: '',
            userName: ''
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
                return this.emailValidate() && this.passwordValidate() && this.repeatPasswordValidate() && this.firstNameValidate() ? true : false;
            }
        }
    },
    methods: {
        signUp() {
            httpClient.post(`/api/Account/sign-up`, {
                email: this.email,
                password: this.password,
                userName: this.userName
            })
                .then((response) => {
                    console.log(response);
                })
        },
        emailValidate() {
            // check basic e-mail structure
            var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            return this.email.length > 0 && this.email.match(emailRegex) ? true : false;
        },
        passwordValidate() {
            return this.password.length > 7 ? true : false;
        },
        repeatPasswordValidate() {
            return this.repeatPassword.length > 7 && this.password === this.repeatPassword ? true : false;
        },
        firstNameValidate() {
            return this.userName.length > 0 ? true : false;
        }
    }
})
</script>