<template>
    <div>
        <h2>{{selectedContact.firstName}} {{selectedContact.lastName}} Family</h2>
        <div v-for="member in members" class="row contact-search-result">
            <div>
                {{member.firstName}} {{member.lastName}}  <router-link :to="{ name: 'updateGroupMember', params: {thisMember: member }}" tag="Span" class="btn btn-warning">Update</router-link>
            </div>
        </div>
        <h4>Need to add additional family members?</h4>
        <div class="tip">Note: All adults (18+) must be present to sign their waiver.</div>
        <div class="center-in-parent">
            <div>
                <router-link :to="{ name: 'addGroupMember' }" tag="a" class="btn btn-warning">Yes</router-link>
                <router-link :to="{ name: 'selectGroupMembers' }" tag="a" class="btn btn-warning">No</router-link>

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
            if (this.reloadMembers) {
                this.loadMembers()
            }
        },
        computed: {
            ...mapGetters({
                selectedContact: 'contact/selectedContact',
                members: 'group/members',
                terminalId: 'progress/terminalId',
                currentStep: 'progress/currentStep',
                nextStep: 'progress/nextStep',
            }),
            searchData: function () {
                return {
                    accountId: this.selectedContact.accountId,
                    terminalId: this.terminalId
                }
            }
        },
        methods: {
            updateMember: function (member) {
                this.selectedMember = member
                this.$router.push({ name: 'updateGroupMember' })

            },
            loadMembers: function () {
                this.$store.dispatch('group/searchByAccountId', this.searchData)
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