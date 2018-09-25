import Vue from 'vue/dist/vue.js';

const state = {
    members: []
}

const getters = {
    members: state => state.members,
    selectedMembers: state => state.members.filter(thisMember => {
        return (thisMember.selected)
    })
}

const mutations = {
    setMembers(state, members) {
        state.members = members
    },
    signWaiver(state, member) {

        //TODO: Match this in a different manner
        var i
        for (i = 0; i < state.members.length; i++) {
            if (state.members[i].firstName == member.firstName && state.members[i].lastName == member.lastName) {
                console.log('Signed waiver for ' + state.members[i].firstName + ' ' + state.members[i].lastName)
                Vue.set(state.members[i],"waiverSigned",true)
            }
        }
    }
}

const actions = {

    addMember({ state, getters, commit, dispatch }, { member, accountId }) {
        console.log('Adding ' + member.firstName + ' ' + member.lastName + ' to account ID ' + accountId)
        var addMemberUrl = '/api/contact/' + accountId + '/addgroupmember'
        if (member) {
            var newMember = { firstName: member.firstName, lastName: member.lastName, photoUrl: member.contactPhotoUrl, selected: false, waiverSigned: false }
            var postData = {newMember, accountId};
            console.log('URL ' + addMemberUrl)
            return new Promise((resolve, reject) => {
                dispatch('api/post',
                    { url: addMemberUrl, data: postData, config: { headers: { 'Content-Type': 'application/json' } } },
                    { root: true }
                ).then(data => {
                    resolve(data); 
                    state.members.push(newMember)
                }).catch(err => alert(err));
            });

            
        }
    },
    searchByAccountId({ state, getters, commit, dispatch }, accountId) {

        var contactApiUrl = '/api/contact/' + accountId + '/group'

        console.log('URL ' + contactApiUrl)

        dispatch('api/get',
            { url: contactApiUrl },
            { root: true }
        ).then(data => {
            commit('setMembers', data.results)
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
