import Vue from 'vue/dist/vue.js';

const state = {
    results: [],
    selectedContact: null,
    saveBillingAddressUrl: '/api/contact',
}

const getters = {
    results: state => state.results,
    selectedContact: state => state.selectedContact
}

const mutations = {

    setSearchResults(state, results) {
        state.results = results
    },

    setContact(state, contact) {
        state.selectedContact = contact
    },
    updateSearchResult(state, updatedContact) {
        var i
        for (i = 0; i < state.results.length; i++) {
            if (state.results[i].accountId === updatedContact.accountId) {
                state.results[i] = updatedContact
            }
        }

    }
}

const actions = {

    searchByAccountId({ state, getters, commit, dispatch }, accountId) {
        var newContact = null;
        var contactApiUrl = '/api/contact/accountId/' + accountId

        return new Promise((resolve, reject) => {
            dispatch('api/get',
                { url: contactApiUrl },
                { root: true }
            ).then(data => {
                resolve(data)
            })
        })

    },
    searchByCardNumber({ state, getters, commit, dispatch }, cardNumber) {
        var newContact = null;
        var contactApiUrl = '/api/contact/cardNumber/' + cardNumber

        dispatch('api/get',
            { url: contactApiUrl },
            { root: true }
        ).then(data => {
            commit('setSearchResults', data.results)
        })
    },
    searchByOrder({ state, getters, commit, dispatch }, orderId) {

        var contactApiUrl = '/api/contact/orderid/' + orderId

        dispatch('api/get',
            { url: contactApiUrl },
            { root: true }
        ).then(data => {
            commit('setSearchResults', data.results)
        })
    },
    searchByPersonalInfo({ state, getters, commit, dispatch }, searchData) {

        var contactApiUrl = '/api/contact/firstname/' + searchData.firstName + '/lastname/' + searchData.lastName + '/dob/' + searchData.dateOfBirth

        dispatch('api/get',
            { url: contactApiUrl },
            { root: true }
        ).then(data => {
            commit('setSearchResults', data.results)
        })
    },
    addContact({
        state,
        getters,
        commit,
        dispatch
    }, contact) {
        var addContactUrl = '/api/contact/add'
        var dataType = 'application/json; charset=utf-8'
        if (contact) {
            return new Promise((resolve, reject) => {
                dispatch('api/post',
                    { url: addContactUrl, data: contact, config: { headers: { 'Content-Type': 'application/json' } } },
                    { root: true }
                ).then(data => {
                    state.results = []
                    state.results.push(data.updatedRecord)
                    resolve(data)
                }).catch(err => console.log('Error adding contact: ' + err));
            });
        }

    },
    updateContact({ state, getters, commit, dispatch }, contact) {
        if (contact) {
            //console.log('Updating Contact ' + contact.accountId)
            var updateContactUrl = '/api/contact/update'
            return new Promise((resolve, reject) => {
                dispatch('api/post',
                    { url: updateContactUrl, data: contact, config: { headers: { 'Content-Type': 'application/json' } } },
                    { root: true }
                ).then(data => {
                    //Once contact record is updated, update the search results                 
                    commit('updateSearchResult', contact)
                    //Let the calling function know proessing is complete
                    resolve(data)
                }).catch(err => alert(err));
            });
        }
    },
}

export default {
    namespaced: true,
    state,
    getters,
    mutations,
    actions
}
