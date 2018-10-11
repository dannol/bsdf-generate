import Vue from 'vue/dist/vue.js';

const state = {
    contacts: [],
    //selectedContact: null,
    saveBillingAddressUrl: '/api/contact',
}

const getters = {
    contacts: state => state.contacts,
    selectedContact: state => state.contacts.find(contact => {
        return (contact.selected)
    })
}

const mutations = {

    setSearchResults(state, contacts) {
        state.contacts = contacts
    },
    selectContact(state, contact) {
        //set all selections
        var i
        for (i = 0; i < state.contacts.length; i++) {
            if (state.contacts[i].accountId == contact.accountId) {
                Vue.set(state.contacts[i], 'selected', true)
            }
            else {
                Vue.set(state.contacts[i], 'selected', false)
            }
        }
    },
    updateSearchResult(state, updatedContact) {
        var i
        for (i = 0; i < state.contacts.length; i++) {
            if (state.contacts[i].accountId === updatedContact.accountId) {
                state.contacts[i] = updatedContact
            }
        }

    }
}

const actions = {

    //searchByAccountId({ state, getters, commit, dispatch }, accountId) {
    //    var newContact = null;
    //    var contactApiUrl = '/api/contact/accountId/' + accountId

    //    return new Promise((resolve, reject) => {
    //        dispatch('api/get',
    //            { url: contactApiUrl },
    //            { root: true }
    //        ).then(data => {
    //            resolve(data)
    //        })
    //    })

    //},
    searchByCardNumber({ state, getters, commit, dispatch }, searchData) {
        var newContact = null;
        var contactApiUrl = '/api/contact/cardNumber/' + searchData.cardNumber + '/terminalId/' + searchData.terminalId

        dispatch('api/get',
            { url: contactApiUrl },
            { root: true }
        ).then(data => {
            commit('setSearchResults', data.results)
        })
    },
    searchByOrder({ state, getters, commit, dispatch }, searchData) {

        var contactApiUrl = '/api/contact/orderid/' + searchData.orderId + '/terminalid/' + searchData.terminalId

        dispatch('api/get',
            { url: contactApiUrl },
            { root: true }
        ).then(data => {
            commit('setSearchResults', data.results)
        })
    },
    searchByPersonalInfo({ state, getters, commit, dispatch }, searchData) {
        var contactApiUrl = '/api/contact/firstname/' + searchData.firstName + '/lastname/' + searchData.lastName + '/dob/' + searchData.dateOfBirth + '/terminalid/' + searchData.terminalId

        dispatch('api/get',
            { url: contactApiUrl },
            { root: true }
        ).then(data => {
            commit('setSearchResults', data.results)
        })
    },
    addContact({ state, getters, commit, dispatch }, contact) {
        var addContactUrl = '/api/contact/add'
        var dataType = 'application/json; charset=utf-8'
        if (contact) {
            return new Promise((resolve, reject) => {
                dispatch('api/post',
                    { url: addContactUrl, data: contact, config: { headers: { 'Content-Type': 'application/json' } } },
                    { root: true }
                ).then(data => {
                    state.contacts = []
                    state.contacts.push(data.updatedRecord)
                    resolve(data)
                }).catch(err => console.log('Error adding contact: ' + err));
            });
        }

    },
    updateContact({ state, getters, commit, dispatch }, contact) {
        if (contact) {
            if (contact.dateOfBirth) {
                var birthday = new Date(contact.dateOfBirth)
                var ageDifMs = Date.now() - birthday.getTime();
                var ageDate = new Date(ageDifMs); // miliseconds from epoch
                contact.age = Math.abs(ageDate.getUTCFullYear() - 1970)
            }

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
    }
}

export default {
    namespaced: true,
    state,
    getters,
    mutations,
    actions
}
