import client from './client'

    class HttpClient {

        constructor() {

        }

        async get(url, data) {
            return client.get(url, data);
        }

        async post(url, data) {
            return client.post(url, data);
        }

        async delete(url, data) {
            return client.delete(url, data);
        }

        async patch(url, data) {
            return client.patch(url, data);
        }
    }

    export default new HttpClient()