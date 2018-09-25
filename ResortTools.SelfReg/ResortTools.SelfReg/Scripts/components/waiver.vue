<template>
    <div>
        <aside id="progress-map-container" class="left-nav full-height">
            <progressmap></progressmap>
        </aside>
        <div id="group-screen" class="full-screen full-height">
            <div class="outer-panel">
                <div class="inner-panel center-in-parent">
                    <h2>Waivers</h2>
                    <div v-for="selectedMember in selectedMembers">
                        <div class="col-xs-1">
                            <input type="checkbox" disabled v-model="selectedMember.waiverSigned" />
                        </div>
                        <div class="col-xs-11">
                            <div>Waiver for {{selectedMember.firstName}} {{selectedMember.lastName}}</div>
                            <div>I hereby agree that ksj sfjh sdjf sfdkjs dfkjsd fkjsn dfkjsndf iuskdjvjksdfn kjsd vkjsn dv jsdkjvsxk jd sdkjn </div>
                            <div v-on:click="signWaiver(selectedMember)" class="btn btn-primary">Sign</div>
                        </div>
                    </div>
                    <navigation></navigation>
                </div>
            </div>
        </div>
    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store';
    import progressmap from '../components/progress-map'
    import navigation from '../components/navigation'

    export default {
        name: 'waiver',
        computed: {
            ...mapGetters({
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
            navigation
        }
    }

</script>