const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
    transpileDependencies: true,
    outputDir: '../TakeAIMeal.API/wwwroot',
    publicPath: '',
    pluginOptions: {
      i18n: {
        locale: 'pl',
        fallbackLocale: 'en',
        localeDir: 'i18n',
        enableLegacy: false,
        runtimeOnly: false,
        compositionOnly: false,
        fullInstall: true
      }
    }
})
