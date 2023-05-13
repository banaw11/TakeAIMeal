<template>
    <h1>Zaloguj się</h1>
    <form>
        <input type="text" placeholder="E-mail" /><br/>
        <input type="password" placeholder="Hasło" /><br/>
        <router-link class="btn btn-primary" to="">{{ t('Header.SignIn') }}</router-link>
    </form>
</template>

<script>
import { defineComponent } from 'vue'
import { useI18n } from 'vue-i18n'
export default defineComponent({
    name: 'LoginComponent',
    setup() {
        const { t } = useI18n({
            inheritLocale: true,
            useScope: 'local'
        })

        return { t }
    }
})
</script>