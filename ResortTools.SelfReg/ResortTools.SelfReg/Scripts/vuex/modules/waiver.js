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
        var minors = []
        var minorNames = ''

        //Get all minors
        var i
        for (i = 0; i < participants.groupMembers.length; i++) {
            if (participants.groupMembers[i].age <= 14) {
                //minorNames += " " + participants.groupMembers[i].firstName
                minors.push(participants.groupMembers[i])
            }
        }

        //Add the Contact as the first waiver
        participantWaivers.push({
            signer: participants.contact,
            text: ' signing for ' + minorNames,
            waiverSigned: false,
            isActive: true,
            minors: minors,
            Text: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Eget duis at tellus at urna condimentum mattis pellentesque. At tellus at urna condimentum mattis pellentesque. Laoreet id donec ultrices tincidunt. Non curabitur gravida arcu ac tortor dignissim. Elementum curabitur vitae nunc sed velit dignissim sodales ut eu. Amet est placerat in egestas erat imperdiet sed euismod. Faucibus pulvinar elementum integer enim neque volutpat ac tincidunt vitae. Scelerisque mauris pellentesque pulvinar pellentesque habitant. Malesuada fames ac turpis egestas maecenas pharetra convallis posuere.  \
                    Condimentum vitae sapien pellentesque habitant morbi tristique senectus et netus.Tincidunt vitae semper quis lectus nulla at volutpat.Nulla at volutpat diam ut venenatis tellus in metus vulputate.Pellentesque pulvinar pellentesque habitant morbi.Odio eu feugiat pretium nibh.Tortor vitae purus faucibus ornare suspendisse sed.Aliquam ultrices sagittis orci a scelerisque.In ornare quam viverra orci sagittis eu.Lectus sit amet est placerat in egestas erat imperdiet.Tincidunt ornare massa eget egestas purus.Ornare arcu dui vivamus arcu felis bibendum ut tristique et.Sit amet dictum sit amet justo donec enim diam.Tellus id interdum velit laoreet id donec ultrices tincidunt.Et netus et malesuada fames ac turpis egestas maecenas pharetra.Sit amet porttitor eget dolor morbi non.Arcu non odio euismod lacinia at quis risus.Lorem ipsum dolor sit amet.Vel turpis nunc eget lorem dolor sed viverra ipsum.Molestie a iaculis at erat pellentesque adipiscing commodo elit at.Quis imperdiet massa tincidunt nunc pulvinar sapien et ligula.\
                    Dolor sit amet consectetur adipiscing elit.Euismod nisi porta lorem mollis.Viverra nibh cras pulvinar mattis nunc sed blandit.Commodo elit at imperdiet dui accumsan sit amet nulla facilisi.Malesuada proin libero nunc consequat.Eget mi proin sed libero enim sed faucibus turpis in.Vestibulum morbi blandit cursus risus at.Amet mauris commodo quis imperdiet massa tincidunt.Amet dictum sit amet justo donec enim diam vulputate ut.Quis enim lobortis scelerisque fermentum dui.Vulputate sapien nec sagittis aliquam.Pellentesque diam volutpat commodo sed egestas egestas.\
                    Sed adipiscing diam donec adipiscing tristique risus.Semper eget duis at tellus at urna.Laoreet suspendisse interdum consectetur libero.Egestas pretium aenean pharetra magna.Ipsum dolor sit amet consectetur adipiscing elit ut aliquam purus.Rutrum tellus pellentesque eu tincidunt tortor.Massa tempor nec feugiat nisl pretium fusce.Consequat interdum varius sit amet mattis vulputate enim nulla.Sagittis orci a scelerisque purus semper.Id leo in vitae turpis massa sed elementum tempus egestas.Fringilla ut morbi tincidunt augue interdum velit.Ut lectus arcu bibendum at varius'
        })

        //add all non-minors to waivers
        for (i = 0; i < participants.groupMembers.length; i++) {

            if (participants.groupMembers[i].age > 14) {
                participantWaivers.push({
                    signer: participants.groupMembers[i],
                    text: ' signing for themself',
                    waiverSigned: false,
                    isActive: false,
                    minors: null
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
