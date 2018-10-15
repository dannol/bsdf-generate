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
    selectMember(state, member) {
        //set all selections
        var i
        for (i = 0; i < state.members.length; i++) {
            if (state.members[i].contactId == member.contactId) {
                if (state.members[i].selected) {
                    Vue.set(state.members[i], 'selected', false)
                }
                else {
                    Vue.set(state.members[i], 'selected', true)
                }
            }
        }
    }
}

const actions = {

    addMember({ state, getters, commit, dispatch }, member) {
        //console.log('Adding ' + member.firstName + ' ' + member.lastName + ' to account ID ' + contactId)
        if (member) {
            var addMemberUrl = '/api/contact/' + member.parentContactId + '/addgroupmember'
            var dataType = 'application/json; charset=utf-8'
 
            return new Promise((resolve, reject) => {
                dispatch('api/post',
                    { url: addMemberUrl, data: member, config: { headers: { 'Content-Type': 'application/json' } } },
                    { root: true }
                ).then(data => {
                    resolve(data); 
                    state.members.push(data.updatedRecord)
                }).catch(err => console.log('Error Adding Contact: ' + err));
            });

            
        }
    },
    updateMember({ state, getters, commit, dispatch }, member) {
        if (member) {

            if (member.dateOfBirth) {
                var birthday = new Date(member.dateOfBirth)
                var ageDifMs = Date.now() - birthday.getTime();
                var ageDate = new Date(ageDifMs); // miliseconds from epoch
                member.age = Math.abs(ageDate.getUTCFullYear() - 1970);
            }

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
    searchByContactId({ state, getters, commit, dispatch }, searchData) {

        var contactApiUrl = '/api/contact/' + searchData.contactId + '/group/terminalid/' + searchData.terminalId

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
