<template>
    <div class="content-header">
        <h1>{{ t('About.Header') }}</h1>
    </div>
    <div class="recipe-container">
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
