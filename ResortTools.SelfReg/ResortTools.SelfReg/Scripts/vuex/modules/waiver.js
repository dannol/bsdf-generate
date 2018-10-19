import Vue from 'vue/dist/vue.js';

const state = {
    //All waivers
    waivers: [],
    //The waiver data specific to this location
    locationWaiver: null,
    //the waiver currently being signed
    signingWaiver: null
}

const getters = {
    waivers: state => state.waivers,
    locationWaiver: state => state.locationWaiver,
    signingWaiver: state => state.signingWaiver
}

const mutations = {

    setWaivers(state, waivers) {
        state.waivers = waivers
    },
    setLocationWaiver(state, locationWaiver) {
        state.locationWaiver = locationWaiver
    },
    setSigningWaiver(state, waiver) {
        state.signingWaiver = waiver
    },
    signWaiver(state, waiverIndex) {
        state.waivers[waiverIndex].waiverSigned = true
        state.waivers[waiverIndex].isActive = false
        if (waiverIndex < state.waivers.length - 1) {
            state.waivers[waiverIndex + 1].isActive = true
        }
        //Once a waiver is signed, set the signingWaiver to null to indicate no waiver is currently being signed
        commit('setSigningWaiver', null)
    }

}

const actions = {

    buildWaivers({ state, getters, commit, dispatch }, waiverData) {

        console.log('Building Waivers for ' + waiverData.groupMembers.length + ' group members')

        var participantWaivers = []
        var minors = []
        var minorNames = ''

        //First get the waiver for this location
        var waiverApiUrl = '/api/waiver/authCode/' + waiverData.authCode + '/terminalId/' + waiverData.terminalId

        dispatch('api/get',
            { url: waiverApiUrl },
            { root: true }
        ).then(data => {
            //Should only get one result
            commit('setLocationWaiver', data.results[0])


            //Get all minors
            var i
            for (i = 0; i < waiverData.groupMembers.length; i++) {
                if (waiverData.groupMembers[i].age <= 17) {
                    minors.push(waiverData.groupMembers[i])
                }
            }

            //Add the Contact as the first waiver
            participantWaivers.push({
                signer: waiverData.contact,
                text: ' signing for ' + minorNames,
                waiverSigned: false,
                isActive: true,
                minors: minors,
                waiverText: state.locationWaiver.waiverText
            })

            //add all non-minors to waivers
            for (i = 0; i < waiverData.groupMembers.length; i++) {

                if (waiverData.groupMembers[i].age > 17) {
                    participantWaivers.push({
                        signer: waiverData.groupMembers[i],
                        text: ' signing for themself',
                        waiverSigned: false,
                        isActive: false,
                        minors: null,
                        waiverText: state.locationWaiver.waiverText
                    })
                }
            }

            commit('setWaivers', participantWaivers)
        })
    },
    getLocationWaiver({ state, getters, commit, dispatch }, searchData) {
        var waiverApiUrl = '/api/waiver/authCode/' + searchData.authCode + '/terminalId/' + searchData.terminalId

        dispatch('api/get',
            { url: waiverApiUrl },
            { root: true }
        ).then(data => {
            //Should only get one result
            commit('setLocationWaiver', data.results[0])
        })
    },

}

export default {
    namespaced: true,
    state,
    getters,
    mutations,
    actions
}
