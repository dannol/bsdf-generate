import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router'
import Vuex from 'vuex'
import store from './vuex/books-store'
import router from './vuerouter/books-router'

Vue.use(VueRouter)
Vue.use(Vuex)

Vue.config.devtools = true
Vue.config.productionTip = false

new Vue({
  store,
  router,
  el: '#app',
  render: h => h(App)
})
