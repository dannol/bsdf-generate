import Vue from 'vue/dist/vue.js';
import Vuex from 'vuex'

import api from './modules/api'
import group from './modules/group'
import contact from './modules/contact'
import progress from './modules/progress'

Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        api,
        group,
        contact,
        progress
    }
})
