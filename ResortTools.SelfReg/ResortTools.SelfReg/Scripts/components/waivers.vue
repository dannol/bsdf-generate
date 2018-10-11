<template>
    <div>
        <div v-for="(waiver, index ) in builtWaivers" v-if="waiver.isActive">
            <h3> Sign Waiver for {{waiver.signer.firstName}} {{waiver.signer.lastName}}</h3>
            <div class="legal-text">{{waiverTextHeader(waiver)}}</div>
            <div class="waiver-container">
                {{waiver.waiverText}}
            </div>
            <div class="legal-text">{{waiverTextFooter(waiver)}}</div>
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
                currentStep: 'progress/currentStep',
                nextStep: 'progress/nextStep',
                selectedMembers: 'group/selectedMembers',
                selectedContact: 'contact/selectedContact',
                waivers: 'waiver/waivers',
                locationWaiver: 'waiver/locationWaiver',
                stepChanged: 'progress/stepChanged',
                terminalId: 'progress/terminalId',
                locationAuthCode: 'progress/terminalId'
            }),
            waiverData: function () {
                return {
                    contact: this.selectedContact,
                    groupMembers: this.selectedMembers,
                    locationAuthCode: this.locationAuthCode,
                    terminalId: this.terminalId
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
                    this.$store.dispatch('waiver/buildWaivers', this.waiverData)
                    this.$store.commit('progress/setStepChanged', false)
                }
                return this.waivers
            }
        },
        methods: {
            signWaiver: function (index) {
                this.$store.commit('waiver/signWaiver', index)
                if (this.allWaiversSigned) {
                    this.$store.commit('progress/completeStep', this.currentStep.stepNumber)
                    //If this step is configured to automatically go to the next step, go there.
                    if (this.currentStep.nextStepOnComplete) {
                        this.$router.push({ name: this.nextStep.routeName })
                    }
                }
            },
            waiverTextHeader: function (waiver) {
                var text = ''
                text += waiver.signer.firstName + ' ' + waiver.signer.lastName + ', Age ' + waiver.signer.age
                text += ' is legally allowed to sign'
                if (waiver.minors != null && waiver.minors.length > 0) {
                    text += ' and is signing for '

                    var i
                    for (i = 0; i < waiver.minors.length; i++) {
                        if (i == waiver.minors.length - 1) {

                            if (waiver.minors.length > 1) {
                                text += 'and' 
                            }
                            text += ' ' + waiver.minors[i].firstName
                        }
                        else {
                            text += waiver.minors[i].firstName + ', '
                        }
                    }
                    text += '. '
                    text += 'If you are not ' + waiver.signer.firstName + ' ' + waiver.signer.lastName
                    text += ' or are not allowed to sign for these ' + waiver.minors.length + ' minors (under age 18),'
                    text += ' please go back to Manage Family.'
                }
                text += '.'


                return text
            },
            waiverTextFooter: function (waiver) {

                var text = ''
                if (waiver.minors != null && waiver.minors.length > 0) {
                    text += 'By clicking "Sign" below and activating the signature pad, I authorize that I am ' + waiver.signer.firstName + ' ' + waiver.signer.lastName
                    text += ' and that I am legally allowed to sign for '
                    var i
                    for (i = 0; i < waiver.minors.length; i++) {
                        if (i == waiver.minors.length - 1) {
                            text += ' and ' + waiver.minors[i].firstName + ' ' + waiver.minors[i].lastName
                        }
                        else {
                            text += waiver.minors[i].firstName + ' ' + waiver.minors[i].lastName + ' ,'
                        }
                    }
                    text += ', and I certify that I am the legal guardian of the above listed minors.'
                }
                return text
            }
        },
        components: {

        }
    }

</script>