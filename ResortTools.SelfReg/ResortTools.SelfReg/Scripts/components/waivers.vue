<template>
    <div>
        <div v-for="(waiver, index ) in builtWaivers" v-if="waiver.isActive">
            <h3>{{waiver.signer.firstName}} {{waiver.signer.lastName}} {{waiver.text}}</h3>
            <div>I hereby agree that ksj sfjh sdjf sfdkjs dfkjsd fkjsn dfkjsndf iuskdjvjksdfn kjsd vkjsn dv jsdkjvsxk jd sdkjn </div>
            <div v-on:click="signWaiver(index)" class="btn btn-primary">Sign</div>
        </div>
    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store';

    export default {
        name: 'waivers',
        computed: {
            ...mapGetters({
                currentStepNumber: 'progress/currentStepNumber',
                selectedMembers: 'group/selectedMembers',
                selectedContact: 'contact/selectedContact',
                waivers: 'waiver/waivers',
                stepChanged: 'progress/stepChanged'
            }),
            participants: function () {
                return {
                    contact: this.selectedContact,
                    groupMembers: this.selectedMembers
                }
            },
            allWaiversSigned: function () {

                var i 
                for (i = 0; i < this.waivers.length; i++) {
                    if (this.waivers[i].waiverSigned == false) {
                        return false
                    }
                }

                return true
            },
            builtWaivers: function () {
                //We need this extra computed value so we can run the rebuild waivers in case the
                //participants changed
                if (this.stepChanged) {
                    this.$store.dispatch('waiver/buildWaivers', this.participants)
                    this.$store.commit('progress/setStepChanged', false)
                }
                return this.waivers
            }
        },
        methods: {
            signWaiver: function (index) {
                this.$store.commit('waiver/signWaiver', index)
                if (this.allWaiversSigned) {
                    this.$store.commit('progress/completeStep', this.currentStepNumber)
                }
            }
        },
        components: {

        }
    }

</script>