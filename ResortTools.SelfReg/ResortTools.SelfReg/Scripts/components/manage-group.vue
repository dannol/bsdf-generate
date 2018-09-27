<template>
    <div>
        <!--Manage Group Step-->
        <aside id="progress-map-container" class="left-nav full-height">
            <progressmap></progressmap>
        </aside>
        <div id="group-screen" class="full-screen full-height">
            <div class="outer-panel">
                <a href="./" class="start-over-btn">Start Over</a>
                <h3 class="page-title">Manage Family</h3>
                <div id="group-management-panel" class="inner-panel center-in-parent">
                    <router-view></router-view>
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

    export default {
        name: 'manage-group',
        data: function () {
            return {
                selectedMember: null
            }
        },
        computed: {
            ...mapGetters({
                thisContact: 'contact/selectedContact',
                members: 'group/members',
                currentStepNumber: 'progress/currentStepNumber'
            })
        },
        methods: {
            completeStep: function () {
                this.$store.commit('progress/completeStep', this.currentStepNumber)
            }
        },
        components: {
            progressmap,
            navigation
        },

    }

</script>