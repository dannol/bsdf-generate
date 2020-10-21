import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    dataLoading: false,
    dataLoaded: false,
    totalAbstractCount: 0,
    sectionCount: 0,
    oralsCount: 0,
    postersCount: 0,
    status: null,
    abstractData: null,
    agendaBookData: null,
    abstractBookData: null
  },
  getters: {
    dataLoading: state => state.dataLoading
  },
  mutations: {
    setDataLoading (state, loading) {
      state.dataLoading = loading
    },
    setStatus (state, status) {
      state.status = status
    },
    setLoadStatus (state, {
      totalAbstractCount = -1,
      sectionCount = -1,
      oralsCount = -1,
      postersCount = -1
    }) {
      state.totalAbstractCount = totalAbstractCount
      state.sectionCount = sectionCount
      state.oralsCount = oralsCount
      state.postersCount = postersCount
      state.dataLoaded = true
    },
    setAgendaBookData( state, data ) {
      state.agendaBookData = data
    },
    setAbstractBookData( state, data ) {
      state.abstractBookData = data
    }
  }
})