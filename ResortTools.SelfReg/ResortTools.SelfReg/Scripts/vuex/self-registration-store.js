import Vue from 'vue/dist/vue.js';
import Vuex from 'vuex'

import api from './modules/api'
import group from './modules/group'
import contact from './modules/contact'
import waiver from './modules/waiver'
import progress from './modules/progress'
import registration from './modules/registration'

Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        api,
        group,
        contact,
        waiver,
        progress,
        registration
    }
})
