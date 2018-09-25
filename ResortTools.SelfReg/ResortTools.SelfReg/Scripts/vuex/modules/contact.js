import Vue from 'vue/dist/vue.js';

const state = {
    results: [],
    thisContact: null,
    saveBillingAddressUrl: '/api/contact',
}

const getters = {
    results: state => state.results,
    thisContact: state => state.thisContact
}

const mutations = {

    setSearchResults(state, results) {
        state.results = results
    },

    setContact(state, contact) {
        state.thisContact = contact
    },
    addContact(state, newContact) {

        state.results = []
        state.results.push({ firstName: newContact.firstName, lastName: newContact.lastName, selected: false })

        //var contactApiUrl = '/api/contact/' + newContact.firstName
        console.log('This is where I would have added a contact' + newContact.firstName + ' ' + newContact.lastName)

        //dispatch('api/post',
        //    { url: contactApiUrl },
        //    { root: true }
        //).then(data => {
        //    commit('setSearchResults', data)
        //})
    }
}

const actions = {

    searchByAccountId({ state, getters, commit, dispatch }, accountId) {

        var newContact = null;
        var contactApiUrl = '/api/contact/accountId/' + accountId

        console.log('URL ' + contactApiUrl)

        return new Promise((resolve, reject) => {
            dispatch('api/get',
                { url: contactApiUrl },
                { root: true }
            ).then(data => {
                resolve(data)
            })
        })
        
    },
    searchByOrder({ state, getters, commit, dispatch }, orderId) {

        var contactApiUrl = '/api/contact/orderid/' + orderId

        console.log('URL ' + contactApiUrl)

        dispatch('api/get',
            { url: contactApiUrl },
            { root: true }
        ).then(data => {
            commit('setSearchResults', data.results)
        })
    },
    searchByPersonalInfo({ state, getters, commit, dispatch }, searchData) {

        var contactApiUrl = '/api/contact/firstname/' + searchData.firstName + '/lastname/' + searchData.lastName

        console.log('URL ' + contactApiUrl)

        dispatch('api/get',
            { url: contactApiUrl },
            { root: true }
        ).then(data => {
            commit('setSearchResults', data.results)
        })
    },
    saveContact({
        state,
        getters,
        commit,
        dispatch
    }, contact) {

        var addContactUrl = '/api/contact'
        var dataType = 'application/json; charset=utf-8';

        var postData = {
            FirstName: contact.firstName,
            LastName: contact.lastName,
            ContactPhotoUrl: 'sdfsd',
            Hometown: 'abc',
            OrderArrivalDate: 'abc',
            JCardNumber: 'abc',
            PhotoUrl: 'abc'
        }

        return new Promise((resolve, reject) => {
            dispatch('api/post',
                { url: addContactUrl, data: JSON.stringify(postData), config: { headers: { 'Content-Type': 'application/json' } } },
                { root: true }
            ).then(data => {
                dispatch('searchByAccountId', data.recordId).then(contactData => {
                    commit('addContact', contactData.results[0])
                    resolve(data)
                })
                
            }).catch(err => console.log('Error adding contact: ' + err));
        });

    }
}

export default {
    namespaced: true,
    state,
    getters,
    mutations,
    actions
}
