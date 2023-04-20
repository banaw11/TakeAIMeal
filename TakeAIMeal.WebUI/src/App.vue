<template>
  <router-view/>
</template>

<script>
// @ is an alias to /src
import { defineComponent } from 'vue'
import { useI18n } from 'vue-i18n'

export default defineComponent({ 
  name: 'App',
  setup() {
    const { t } = useI18n({
      inheritLocale: true,
      useScope: 'local'
    })

    return { t }
  }
})
</script>