import axios from 'axios'

const getters = {
    // Another common content-type would be 'application/json' which can be passed as a config to each api action below
    config(state, getters, rootState) {
        return {
            baseURL: '/',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'X-Requested-With': 'XMLHttpRequest',
                'Cache-Control': 'no-cache'
            }
        }
    }
}

const actions = {
    post({ getters, commit, dispatch }, { url, data = {}, config = getters.config }) {
        return new Promise((resolve, reject) => {
            // append timestamp to url
            axios.post(url, data, config)
                .then(response => resolve(response.data))
                .catch(error => dispatch('error', { error }))
        })
    },
    get({ getters, commit, dispatch }, { url, data = {}, config = getters.config }) {
        return new Promise((resolve, reject) => {
            // append timestamp to url
            if (!url.includes('?')) {
                url += '?ts=' + new Date().getTime()
            } else {
                url += '&ts=' + new Date().getTime()
            }
            axios.get(url, data, getters.config)
                .then(response => resolve(response.data))
                .catch(error => dispatch('error', { error }))
        })
    },
    error(context, { error }) {
        console.log('API error:', error.message)
    }
}

export default {
    namespaced: true,
    getters,
    actions
}
