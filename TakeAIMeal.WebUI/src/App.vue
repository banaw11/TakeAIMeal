<template>
     <router-view />
</template>

<script>
    import { defineComponent } from 'vue'
    import { mapActions } from 'vuex'
    import client from './modules/http/client/client'
    export default defineComponent({
        name: 'App',
        data() {
            return {
                loader: null,
                isLoading: false
            }
        },
        beforeMount: function () {
            client.interceptors.request.use((config) => {
                this._showLoader();
                return config
            }, (error) => {
                return Promise.reject(error);
            });
            client.interceptors.response.use((config) => {
                this._hideLoader();
                return config
            }, (error) => {
                this._hideLoader();
                return Promise.reject(error);
            });
        },
        mounted: function(){
            this.restoreSession();

        },
        beforeUnmount: function () {
            clearTimeout(this.pooling);
        },
        methods: {
            ...mapActions('context', ['restoreSession']),
            _showLoader() {
                this.isLoading = true;
                this.pooling = setTimeout(() => {
                    if (this.isLoading) {
                        this.loader = this.$loading.show({
                            container: this.$refs.loaderContainer,
                            canCancel: false
                        });
                    }
                }, 200)
            },
            _hideLoader() {
                this.isLoading = false;
                this.loader?.hide();
            }
        }
    })
</script>