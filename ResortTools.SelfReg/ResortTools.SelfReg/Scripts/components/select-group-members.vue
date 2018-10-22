<template>
    <div>
        <h2>{{selectedContact.firstName}} {{selectedContact.lastName}} Family</h2>
        <!--<a v-for="member in members" class="row contact-search-result" v-bind:class="{'contact-selected': member.selected}" v-on:click="selectMember(member)">
        <div class="col-xs-1">
            <input type="checkbox" v-model="member.selected" v-on:click="selectMember(member)" />
        </div>
        <div>
            {{member.firstName}} {{member.lastName}}
        </div>
    </a>-->
        <a v-for="member in paginatedData" class="row contact-search-result" v-bind:class="{'contact-selected': member.selected}" v-on:click="selectMember(member)">
            <div class="col-xs-1">
                <input type="checkbox" v-model="member.selected" v-on:click="selectMember(member)" />
            </div>
            <div>
                {{member.firstName}} {{member.lastName}}
            </div>
        </a>
        <a v-if="pageIndex > 0" class="btn btn-info" @click="prevPage">
            Previous
        </a>
        <a v-if="pageIndex + 1 < pageCount" class="btn btn-info" @click="nextPage">
            Next
        </a>
        <div>Select all family members who will be purchasing a season pass or signing season pass waivers.</div>
        <div class="tip">
            Tip: Adults (18+) must be present to sign their season-pass waiver.
        </div>
        <div class="tip">
            Tip: Only leagal guardians or parents may sign for minors.
        </div>
        <h3 v-if="selectedMembers.length > 0">{{selectedMembers.length}} family members selected</h3>
        <router-link :to="{ name: 'addGroupMember' }" tag="a" class="btn btn-warning">Add New Family Member</router-link>
    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'

    export default {
        name: 'select-group-members',
        data: function () {
            return {
                selectedMember: null,
                pageIndex: 0  // default to page 0
            }
        },
        mounted: function () {
            console.log('Checking')
            if (this.reloadMembers) {
                //this.loadMembers()
            }
        },
        computed: {
            ...mapGetters({
                selectedContact: 'contact/selectedContact',
                members: 'group/members',
                selectedMembers: 'group/selectedMembers',
                currentStep: 'progress/currentStep',
                nextStep: 'progress/nextStep'
            }),
            participants: function () {
                return {
                    contact: this.selectedContact,
                    groupMembers: this.selectedMembers
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
            loadMembers: function () {
                this.$store.dispatch('group/searchByContactId', this.selectedContact.contactId)
            },
            selectMember: function (member) {
                this.$store.commit('group/selectMember', member)
                this.$store.commit('progress/completeStep', this.currentStep.stepNumber)
                //If this step is configured to automatically go to the next step, go there.
                if (this.currentStep.nextStepOnComplete) {
                    this.$router.push({ name: this.nextStep.routeName })
                }
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
                default: 9
            }
        }

    }

</script>