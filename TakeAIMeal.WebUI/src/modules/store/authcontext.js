import httpClient from '../http/client'

export default {
    namespaced: true,
    state: {
        profile: {},
    },
    getters: {
        isAuthenticated: state => state.profile.userName?.length > 0 && state.profile.email?.length > 0 
    },
    mutations: {
        setProfile(state, profile) {
            state.profile = profile;
        },
    },
    actions: {
        login({ commit }, credentials) {
            return httpClient.post('/api/account/sign-in', credentials).then((res) => {
                console.log(res.data);
                commit('setProfile', res.data);
            })
        },
        logout({ commit }) {
            return httpClient.post('/api/account/sign-out').then(() => {
                commit('setProfile', {})
            })
        },
        restoreSession({ commit }) {
            return httpClient.get('/api/account/check-session').then((res) => {
                commit('setProfile', res.data ?? {})
            })
        }
    }
}