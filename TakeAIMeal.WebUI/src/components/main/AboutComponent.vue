<template>
  <div class="recipe-container">
    <h1>{{ t('About.Header') }}</h1>
    <p>{{ t('About.Paragraph') }}</p>
  </div>
</template>

<script>
  import { useI18n } from 'vue-i18n'
  export default {
    name: 'AboutComponent',
    setup() {
      const { t } = useI18n({
          inheritLocale: true,
          useScope: 'local'
      })

      return { t }
    }
  }
</script>
