
<template>
    <div>
        <aside  id="progress-map-container" class="left-nav full-height">
            <progressmap></progressmap>
        </aside>
        <div id="group-screen" class="full-screen full-height">
            <div class="outer-panel">
                <div class="inner-panel center-in-parent">
                    <h2>{{thisContact.firstName}} {{thisContact.lastName}} Family</h2>
                    <div v-for="member in members">
                        <div class="contact-search-result">{{member.firstName}} {{member.lastName}}</div>
                    </div>
                    <div v-if="!addingMember" class="center-in-parent">
                        <div>
                            <span class="btn btn-warning" v-on:click="completeStep">Looks Good</span>
                            <span class="btn btn-warning" v-on:click="addMember">Add Member</span>
                        </div>

                    </div>
                    <addmember v-if="addingMember"></addmember>
                    <navigation></navigation>
                </div>
            </div>
            <aside class="member-list">
                <div class="contact-name">
                    <div class="contact-first-name">
                        {{thisContact.firstName}}
                    </div>
                    <div class="contact-last-name">
                        {{thisContact.lastName}}
                    </div>
                </div>
                <div v-for="member in members" class="group-member">
                    <img :src="member.photoUrl" class="group-member-image">
                    <div class="member-name">{{member.firstName}}</div>
                </div>
            </aside>

        </div>
    </div>

</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'
    import progressmap from '../components/progress-map'
    import navigation from '../components/navigation'
    import addmember from '../components/add-member'

    export default {
        name: 'member-list',
        data: function () {
            return {
                addingMember: false
            }
        },
        computed: {
            ...mapGetters({
                thisContact: 'contact/thisContact',
                members: 'group/members',
                currentStepNumber: 'progress/currentStepNumber'
            })
        },
        mounted: function () {
            this.$store.dispatch('group/searchByAccountId', 123)
        },
        methods: {
            completeStep: function () {
                this.$store.commit('progress/completeStep', this.currentStepNumber)
            },
            addMember: function () {
                this.addingMember = true
            }
        },
        components: {
            progressmap,
            navigation,
            addmember
        },

    }

</script>