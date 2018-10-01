<template>
    <div>
        <!-- Manage Waivers Step-->
        <aside id="progress-map-container" class="left-nav full-height">
            <progressmap></progressmap>
        </aside>
        <div id="group-screen" class="full-screen full-height">
            <div class="outer-panel">
                <a href="./" class="start-over-btn">Start Over</a>
                <h3 class="page-title">Sign Waivers</h3>
                <div id="waiver-management-panel" class="inner-panel center-in-parent">
                    <router-view></router-view>
                    <navigation></navigation>
                </div>
            </div>
            <memberbar></memberbar>
        </div>
    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store';
    import progressmap from '../components/progress-map'
    import navigation from '../components/navigation'
    import memberbar from '../components/member-bar'

    export default {
        name: 'manage-waivers',
        computed: {
            ...mapGetters({
                thisContact: 'contact/selectedContact',
                currentStepNumber: 'progress/currentStepNumber',
                selectedMembers: 'group/selectedMembers'
            })
        },
        methods: {
            signWaiver: function (selectedMember) {
                this.$store.commit('group/signWaiver', selectedMember)
                this.$store.commit('progress/completeStep', this.currentStepNumber)
            }
        },
        components: {
            progressmap,
            navigation,
            memberbar
        }
    }

</script>