<template>
    <div>
        <h2>{{selectedContact.firstName}} {{selectedContact.lastName}} Family</h2>
        <a v-for="member in members" class="row contact-search-result" v-bind:class="{'contact-selected': member.selected}" v-on:click="selectMember(member)">
            <div class="col-xs-1">
                <input type="checkbox" v-model="member.selected" v-on:click="selectMember(member)" />
            </div>
            <div>
                {{member.firstName}} {{member.lastName}}  <router-link :to="{ name: 'updateGroupMember', params: {thisMember: member }}" tag="Span" class="btn btn-warning">Update</router-link>
            </div>
        </a>
        <h4>Need to add additional family members></h4>
        <div class="tip">Note: All adults (18+) must be present to sign their waiver.</div>
        <div class="center-in-parent">
            <div>
                <router-link :to="{ name: 'addGroupMember' }" tag="a" class="btn btn-warning">Yes</router-link>
                <div class="btn btn-warning" v-on:click="completeStep">No</div>

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
                this.$store.commit('group/selectMember', member)
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