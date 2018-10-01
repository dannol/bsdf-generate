import Vue from 'vue/dist/vue.js';

const state = {
    waivers: []
}

const getters = {
    waivers: state => state.waivers
}

const mutations = {

    setWaivers(state, waivers) {
        state.waivers = waivers
    },
    signWaiver(state, waiverIndex) {
        state.waivers[waiverIndex].waiverSigned = true
        state.waivers[waiverIndex].isActive = false
        if (waiverIndex < state.waivers.length - 1) {
            state.waivers[waiverIndex + 1].isActive = true
        }
    }


}

const actions = {

    buildWaivers({ state, getters, commit, dispatch }, participants) {

        console.log('Building Waivers for ' + participants.groupMembers.length + ' group members')

        var participantWaivers = []
        var minorNames = ''

        //Get all minors' names
        var i
        for (i = 0; i < participants.groupMembers.length; i++) {
            if (participants.groupMembers[i].age <= 14) {
                minorNames += " " + participants.groupMembers[i].firstName
            }
        }

        //Add the Contact as the first waiver
        participantWaivers.push({
            signer: participants.contact,
            text: ' signing for ' + minorNames,
            waiverSigned: false,
            isActive: true
        })

        //add all non-minors to waivers
        for (i = 0; i < participants.groupMembers.length; i++) {

            if (participants.groupMembers[i].age > 14) {
                participantWaivers.push({
                    signer: participants.groupMembers[i],
                    text: ' signing for themself',
                    waiverSigned: false,
                    isActive: false
                })
            }
        }

        commit('setWaivers', participantWaivers)

    }

}

export default {
    namespaced: true,
    state,
    getters,
    mutations,
    actions
}
