<template>
    <div>
        <h2>{{selectedContact.firstName}} {{selectedContact.lastName}} Family</h2>
        <div v-for="member in members">
            <div class="contact-search-result" v-bind:class="{'contact-selected': member.selected}">
                <input type="checkbox" v-model="member.selected" v-on:click="selectMember(member)" />
                {{member.firstName}} {{member.lastName}}  <router-link :to="{ name: 'updateGroupMember', params: {thisMember: member }}" tag="Span" class="btn btn-warning">Update</router-link>
            </div>
        </div>
        <div class="center-in-parent">
            <div>
                <span class="btn btn-warning" v-on:click="completeStep">Looks Good</span>
                <router-link :to="{ name: 'addGroupMember' }" tag="a" class="btn btn-warning">Add New Member</router-link>
            </div>
        </div>
    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'

    export default {
        name: 'group-member-list',
        data: function () {
            return {
                selectedMember: null
            }
        },
        mounted: function () {
            console.log('Checking')
            if (this.reloadMembers) {
                this.loadMembers()
            }
        },
        computed: {
            ...mapGetters({
                selectedContact: 'contact/selectedContact',
                members: 'group/members',
                selectedMembers: 'group/selectedMembers',
                currentStepNumber: 'progress/currentStepNumber'
            }),
            participants: function () {
                return {
                    contact: this.selectedContact,
                    groupMembers: this.selectedMembers
                }
            },
        },
        methods: {
            completeStep: function () {
                this.$store.commit('progress/completeStep', this.currentStepNumber)
            },
            updateMember: function (member) {
                this.selectedMember = member
                this.$router.push({ name: 'updateGroupMember' })

            },
            loadMembers: function () {
                this.$store.dispatch('group/searchByAccountId', this.selectedContact.accountId)
            },
            selectMember: function (member) {
                this.$store.commit('progress/completeStep', this.currentStepNumber)
            }
        },
        props: {
            reloadMembers: {
                type: Boolean,
                default: true
            }
        }

    }

</script>