<template>
    <h1>Zaloguj się</h1>
    <form>
        <input type="text" placeholder="E-mail" v-model.lazy="email" style="color:black" /><br/>
        <input type="password" placeholder="Hasło" v-model.lazy="password" style="color:black" /><br/>
        <router-link class="btn btn-primary" to="" :disabled="isInValid()">{{ t('Header.SignIn') }}</router-link>
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
                return this.emailValidate() ? true : false;
                // let email = this.email.length;
                // console.log(this.emailValidate());
                // return startValidation.value ? /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(email.value) : null;
            }
        }
    },
    methods: {
        emailValidate() {
            return this.email.length > 0 ? true : false;
        }
    }
})
</script>