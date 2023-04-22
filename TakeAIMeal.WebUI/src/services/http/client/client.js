/**
 * https://www.section.io/engineering-education/customized-axios-for-mulitple-servers-in-vue/
 */

import axios from 'axios'
import {apiBaseUrl} from "@/environment";
import {AuthService} from "@/modules/auth";
/**
 * Axios basic configuration
 */
const config = {
  baseURL: apiBaseUrl
}

/**
 * Creating the instance of Axios
 * It is because in large-scale application, we may need
 * to consume APIs from more than a single server,
 */
const client = axios.create(config)

/**
 * Auth interceptors
 * @description Add auth tokens to every outgoing request.
 * @param {*} config
 */
const authInterceptor = config => {
  config.headers.Authorization = `Bearer ${AuthService.token}`
  config.headers.common.Accept = 'Application/json'
  config.headers['Access-Control-Allow-Origin'] = '*'
  return config
}

/**
 * Logger interceptors
 * @description Log app requests.
 * @param {*} config
 */
const loggerInterceptor = config =>
/** Add logging here */
  config

/** Adding the request interceptors */
client.interceptors.request.use(authInterceptor)
client.interceptors.request.use(loggerInterceptor)

/** Adding the response interceptors */
client.interceptors.response.use(
  response => Promise.resolve(response),
  error => {
    Event.$emit('error', 500, error.response.data.message)
    if (error.response.status === 401) AuthService.logout()
    const errorMessage = error.response.data.message
    error.response.data.message = errorMessage.length > 200
      ? JSON.parse(errorMessage.split('code :').pop()).error.message.split(':')[0]
      : errorMessage
    throw error
    // Promise.reject(error)
  }
)

export default client