<template>
    <div>
        <h2>{{selectedContact.firstName}} {{selectedContact.lastName}} Family</h2>
        <div v-for="member in paginatedData" class="row contact-search-result">
            <div>
                {{member.firstName}} {{member.lastName}}  <!--<router-link :to="{ name: 'updateGroupMember', params: {thisMember: member }}" tag="Span" class="btn btn-warning">Update</router-link>-->
            </div>
        </div>
        <a v-if="pageIndex > 0" class="btn btn-info" @click="prevPage">
            Previous
        </a>
        <a v-if="pageIndex + 1 < pageCount" class="btn btn-info" @click="nextPage">
            Next 
        </a>
        <h4>Need to add additional family members?</h4>
        <div class="tip">Note: All adults ({{adultAge}}+) must be present to sign their waiver.</div>
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
                selectedMember: null,
                pageIndex: 0,
                adultAge: selfRegistrationConfig.minAdultAge
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
                    contactId: this.selectedContact.contactId,
                    terminalId: this.terminalId
                }
            },
            pageCount() {
                let l = this.members.length,
                    s = this.size;
                return Math.ceil(l / s);
            },
            paginatedData() {
                const start = this.pageIndex * this.size,
                    end = start + this.size;
                return this.members.slice(start, end);
            }
        },
        methods: {
            updateMember: function (member) {
                this.selectedMember = member
                this.$router.push({ name: 'updateGroupMember' })

            },
            loadMembers: function () {
                this.$store.dispatch('group/searchByContactId', this.searchData)
            },
            nextPage() {
                this.pageIndex++;
            },
            prevPage() {
                this.pageIndex--;
            }
        },
        props: {
            reloadMembers: {
                type: Boolean,
                default: true
            },
            size: {
                type: Number,
                required: false,
                default: 5
            }
        }

    }

</script>