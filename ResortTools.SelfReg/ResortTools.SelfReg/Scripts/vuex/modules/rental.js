import Vue from 'vue/dist/vue.js';

const state = {
    //All waivers
    profiles: [],
    //The array index of the active profile
    activeProfileIndex: null,
    //Boolean indicating whether all waivers are signed
    allProfilesProcessed: false
}

const getters = {
    contacts: profiles => state.profiles,
    selectedContact: state => state.contacts.find(contact => {
        return (contact.selected)
    })
}

const mutations = {

    setContacts(state, contacts) {
        state.contacts = contacts
    },
    selectContact(state, contact) {
        //set all selections
        var i
        for (i = 0; i < state.contacts.length; i++) {
            if (state.contacts[i].contactId == contact.contactId) {
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
            if (state.contacts[i].contactId === updatedContact.contactId) {
                state.contacts[i] = updatedContact
            }
        }

    }
}

const actions = {

    searchByCardNumber({ state, getters, commit, dispatch }, searchData) {
        var newContact = null;
        var contactApiUrl = '/api/contact/cardNumber/' + searchData.cardNumber + '/terminalId/' + searchData.terminalId

        dispatch('api/get',
            { url: contactApiUrl },
            { root: true }
        ).then(data => {
            commit('setContacts', data.results)
        })
    },
    searchByOrder({ state, getters, commit, dispatch }, searchData) {

        var contactApiUrl = '/api/contact/orderid/' + searchData.orderId + '/terminalid/' + searchData.terminalId

        dispatch('api/get',
            { url: contactApiUrl },
            { root: true }
        ).then(data => {
            commit('setContacts', data.results)
        })
    },
    searchByPersonalInfo({ state, getters, commit, dispatch }, searchData) {
        var contactApiUrl = '/api/contact/firstname/' + searchData.firstName + '/lastname/' + searchData.lastName + '/dob/' + searchData.dateOfBirth + '/terminalid/' + searchData.terminalId

        dispatch('api/get',
            { url: contactApiUrl },
            { root: true }
        ).then(data => {
            commit('setContacts', data.results)
        })
    },
    addContact({ state, getters, commit, dispatch }, contactData) {
        debugger
        var addContactUrl = '/api/contact/add/terminalId/' + contactData.terminalId
        var dataType = 'application/json; charset=utf-8'
        if (contactData.contact) {
            return new Promise((resolve, reject) => {
                dispatch('api/post',
                    { url: addContactUrl, data: contactData.contact, config: { headers: { 'Content-Type': 'application/json' } } },
                    { root: true }
                ).then(data => {
                    state.contacts = []
                    state.contacts.push(data.updatedRecord)
                    resolve(data)
                }).catch(err => console.log('Error adding contact: ' + err));
            });
        }

    },
    updateContact({ state, getters, commit, dispatch }, contactData) {

        if (contactData.contact) {
            if (contactData.contact.dateOfBirth) {
                var birthday = new Date(contactData.contact.dateOfBirth)
                var ageDifMs = Date.now() - birthday.getTime();
                var ageDate = new Date(ageDifMs); // miliseconds from epoch
                contactData.contact.age = Math.abs(ageDate.getUTCFullYear() - 1970)
            }

            //console.log('Updating Contact ' + contactData.contact.contactId)
            var updateContactUrl = '/api/contact/update/terminalid/' + contactData.terminalId
            return new Promise((resolve, reject) => {
                dispatch('api/post',
                    { url: updateContactUrl, data: contactData.contact, config: { headers: { 'Content-Type': 'application/json' } } },
                    { root: true }
                ).then(data => {
                    //Once contact record is updated, update the search results                 
                    commit('updateSearchResult', contactData.contact)
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
