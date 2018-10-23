import Vue from 'vue/dist/vue.js';

const state = {
    //All registrations
    registrations: [],
    //the waiver currently being completed
    activeRegistration: null,
    //The array index of the active registration
    activeRegistrationIndex: null,
    //Boolean indicating whether all registrations are complete
    allRegistrationsComplete: false
}

const getters = {
    registrations: state => state.registrations,
    activeRegistration: state => state.activeRegistration,
    activeRegistrationIndex: state => state.activeRegistrationIndex,
    allRegistrationsComplete: state => state.allRegistrationsComplete
}

const mutations = {

    setRegistrations(state, registrations) {
        state.registrations = registrations
    },
    setActiveRegistration(state, registration) {
        state.activeRegistration = registration
    },
    setActiveRegistrationIndex(state, registrationIndex) {
        state.activeRegistrationIndex = registrationIndex
    },
    setAllRegistrationsComplete(state, isComplete) {
        state.allRegistrationsComplete = isComplete
    }

}

const actions = {

    //* This function build registration objects based on an array of registrants passed
    getRegistrations({ state, getters, commit, dispatch }, registrantsData) {

        debugger
        console.log('Building registrations for ' + registrantsData.registrants.length + ' registrants')

        //Attempt to get any existing registration data for each registrant
        var i
        for (i = 0; i < registrantsData.registrants.length; i++) {

            //First get the waiver for this location
            var registrationApiUrl = '/api/registration/contactid/' + registrantsData.registrants[i].contactId + '/terminalid/' + registrantsData.terminalId

            dispatch('api/get',
                { url: registrationApiUrl },
                { root: true }
            ).then(data => {
                //Should get one result or none
                if (data.results && data.results.length > 0) {
                    if (i == 0) {
                        //make the first registration the active one
                        Vue.set(data.results[0], 'isActive', true)
                    }
                    else {
                        Vue.set(data.results[0], 'isActive', false)
                    }

                    //Add this registration to the array
                    state.registrations.push(data.results[0])
                }

            }).catch(err => console.log('Error retieving registration: ' + err));

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
