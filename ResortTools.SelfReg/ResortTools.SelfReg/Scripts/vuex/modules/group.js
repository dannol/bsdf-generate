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
    }
}

const actions = {

    addMember({ state, getters, commit, dispatch }, { member, accountId }) {
        //console.log('Adding ' + member.firstName + ' ' + member.lastName + ' to account ID ' + accountId)
        var addMemberUrl = '/api/contact/' + accountId + '/addgroupmember'
        if (member) {
            var newMember = { firstName: member.firstName, lastName: member.lastName, photoUrl: member.photoUrl, selected: false}
            var postData = {newMember, accountId};

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
    updateMember({ state, getters, commit, dispatch }, member) {
        if (member) {
            //console.log('Updating ' + member.firstName + ' ' + member.lastName)
            var updateMemberUrl = '/api/contact/update'
            return new Promise((resolve, reject) => {
                dispatch('api/post',
                    { url: updateMemberUrl, data: member, config: { headers: { 'Content-Type': 'application/json' } } },
                    { root: true }
                ).then(data => {
                    resolve(data)
                }).catch(err => alert(err));
            });


        }
    },
    searchByAccountId({ state, getters, commit, dispatch }, accountId) {

        var contactApiUrl = '/api/contact/' + accountId + '/group'

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
