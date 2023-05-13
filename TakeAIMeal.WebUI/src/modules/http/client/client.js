/**
 * https://www.section.io/engineering-education/customized-axios-for-mulitple-servers-in-vue/
 */

import axios from 'axios'
// import {apiBaseUrl} from "@/environment";
/**
 * Axios basic configuration
 */
const config = {
    baseURL: process.env.VUE_APP_BASE_API_URL
}

/**
 * Creating the instance of Axios
 * It is because in large-scale application, we may need
 * to consume APIs from more than a single server,
 */
const client = axios.create(config)

/**
 * Logger interceptors
 * @description Log app requests.
 * @param {*} config
 */
const loggerInterceptor = config =>
/** Add logging here */
  config

/** Adding the request interceptors */
client.interceptors.request.use(loggerInterceptor)

/** Adding the response interceptors */
client.interceptors.response.use(
  response => Promise.resolve(response.data),
  error => {
    Event.$emit('error', 500, error.response.data.message)

    // AuthService - został wywalony
    // if (error.response.status === 401) AuthService.logout()

    const errorMessage = error.response.data.message
    error.response.data.message = errorMessage.length > 200
      ? JSON.parse(errorMessage.split('code :').pop()).error.message.split(':')[0]
      : errorMessage
    throw error
    // Promise.reject(error)
  }
)

export default client