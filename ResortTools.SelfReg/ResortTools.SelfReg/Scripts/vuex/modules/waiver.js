import Vue from 'vue/dist/vue.js';

const state = {
    //All waivers
    waivers: [],
    //The waiver data specific to this location
    locationWaiver: null,
    //the waiver currently being signed
    signingWaiver: null,
    //The array index of the signing waiver
    activeWaiverIndex: null,
    //Boolean indicating whether all waivers are signed
    allWaiversSigned: false
}

const getters = {
    waivers: state => state.waivers,
    locationWaiver: state => state.locationWaiver,
    signingWaiver: state => state.signingWaiver,
    activeWaiverIndex: state => state.activeWaiverIndex,
    allWaiversSigned: state => state.allWaiversSigned
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
    setActiveWaiverIndex(state, waiverIndex) {
        state.activeWaiverIndex = waiverIndex
    },
    signWaiver(state, waiver) {

        //var signWaiverUrl = '/api/waiver/sign'
        //var dataType = 'application/json; charset=utf-8'

        //return new Promise((resolve, reject) => {
        //    dispatch('api/post',
        //        { url: signWaiverUrl, data: waiver, config: { headers: { 'Content-Type': 'application/json' } } },
        //        { root: true }
        //    ).then(data => {
        //        resolve(data);
        //    }).catch(err => console.log('Error storing waiver: ' + err));
        //});


        waiver.waiverSigned = true
        waiver.isActive = false
        if (state.activeWaiverIndex < state.waivers.length - 1) {
            state.waivers[state.activeWaiverIndex + 1].isActive = true
        }
        else {
            state.allWaiversSigned = true;
        }
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
                waiverText: state.locationWaiver.waiverText,
                signatureString: '',
                signatureBase64String: ''
            })

            //Add a waiver for all non-minors
            for (i = 0; i < waiverData.groupMembers.length; i++) {

                //Only add adults who are not the signer (already added) 
                if (waiverData.groupMembers[i].age > 17 && waiverData.groupMembers[i].contactId != waiverData.contact.contactId) {

                    participantWaivers.push({
                        signer: waiverData.groupMembers[i],
                        text: ' signing for themself',
                        waiverSigned: false,
                        isActive: false,
                        minors: null,
                        waiverText: state.locationWaiver.waiverText,
                        signatureString: '',
                        signatureBase64String: ''
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
    }

}

export default {
    namespaced: true,
    state,
    getters,
    mutations,
    actions
}
