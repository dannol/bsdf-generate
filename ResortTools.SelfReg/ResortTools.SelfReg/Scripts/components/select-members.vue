<template>
    <div>
        <div>
            <aside id="progress-map-container" class="left-nav full-height">
                <progressmap></progressmap>
            </aside>
            <div id="group-screen" class="full-screen full-height">
                <div class="outer-panel">
                    <div class="inner-panel center-in-parent">
                        <H2>Select Participants</H2>
                        <div v-for="member in members" class="row contact-search-result">
                            <div class="col-xs-1">
                                <input type="checkbox" v-model="member.selected" v-on:click="selectMember(member)" />
                            </div>
                            <div class="col-xs-11">
                                {{member.firstName}} {{member.lastName}}
                            </div>
                        </div>
                        <navigation></navigation>
                    </div>
                </div>
                <memberbar></memberbar>
            </div>
        </div>
    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'
    import progressmap from '../components/progress-map'
    import navigation from '../components/navigation'
    import memberbar from '../components/member-bar'

    export default {
        name: 'member-list',
        mounted: function () {
        },
        data: function () {
            return { stepComplete: false }
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
            },
            selectMember: function (member) {
                //this.$store.dispatch('group/addSelectedMember', member)
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