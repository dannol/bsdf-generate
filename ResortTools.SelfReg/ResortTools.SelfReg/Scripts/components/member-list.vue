
<template>
    <div>
        <aside class="left-nav full-height">
            <progressmap></progressmap>
        </aside>
        <div class="manage-group full-height">
            <div class="group-main-panel">
                <div class="group-inner-panel center-in-parent">
                    <div v-for="member in members">
                        {{member.firstName}} {{member.lastName}}
                    </div>
                    <div v-if="!addingMember" class="center-in-parent">
                        <div>
                            <span class="btn btn-warning" v-on:click="completeStep">Looks Good</span>
                        </div>
                        <div>
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