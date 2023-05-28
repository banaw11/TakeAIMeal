<template>
    <LoaderComponent :is-loading="isLoading"></LoaderComponent>
     <router-view />
</template>

<script>
    import { defineComponent } from 'vue'
    import { mapActions } from 'vuex'
    import client from './modules/http/client/client'
    import LoaderComponent from './components/shared/LoaderComponent.vue'
    export default defineComponent({
        name: 'App',
        components: {
            LoaderComponent
        },
        data() {
            return {
                isLoading: false,
                responseStarted: false,
                requests : [],
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
                this.requests.push(this.requests.length);
                this.pooling = setTimeout(() => {
                    if (this.requests.length > 0) {
                        this.isLoading = true;
                    }
                }, 200);
            },
            _hideLoader() {
                this.requests.pop();
                if (this.requests.length === 0) {
                    this.isLoading = false;
                }
            }
        }
    })
</script>