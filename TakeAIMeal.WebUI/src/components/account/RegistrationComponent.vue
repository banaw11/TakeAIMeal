<template>
    <h1>{{ t('Home.SignUp') }}</h1>
    <form>
        <input type="text" placeholder="E-mail" v-model="email" class="text-dark"  /><br/>
        <input type="password" v-bind:placeholder="$t('Account.Password')" v-model="password" class="text-dark"  /><br/>
        <input type="password" v-bind:placeholder="$t('Account.RepeatPassword')" v-model="repeatPassword" class="text-dark"  /><br/>
        <input type="text" v-bind:placeholder="$t('Account.FirstName')" v-model="firstName" class="text-dark"  /><br/>
        <button class="btn btn-primary" :disabled="!isInValid()">{{ t('Home.SignUp') }}</button>
    </form>
</template>

<script>
import { defineComponent } from 'vue'
import { useI18n } from 'vue-i18n'
export default defineComponent({
    name: 'LoginComponent',
    data(){
        return {
            email: '',
            password: '',
            repeatPassword: '',
            firstName: ''
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
        emailValidate() {
            // https://stackoverflow.com/questions/46155/how-can-i-validate-an-email-address-in-javascript
            // sprawdza czy e-mail jest w formacie e-mail i czy nie zawiera więcej niż jeden znaków @
            var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            return this.email.length > 0 && this.email.match(emailRegex) ? true : false;
        },
        passwordValidate() {
            // ustalić jakie ma być minimalne hasło, ewentualnie jakie ma zawierać znaki
            return this.password.length > 5 ? true : false;
        },
        repeatPasswordValidate() {
            return this.repeatPassword.length > 5 && this.password === this.repeatPassword ? true : false;
        },
        firstNameValidate() {
            return this.firstName.length > 0 ? true : false;
        }
    }
})
</script>