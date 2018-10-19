<template>
    <div>
        <div v-for="(waiver, index ) in builtWaivers" v-if="waiver.isActive">
            <h3> Sign Waiver for {{waiver.signer.firstName}} {{waiver.signer.lastName}}</h3>
            <div class="legal-text">{{waiverTextHeader(waiver)}}</div>
            <div class="waiver-container">
                {{waiver.waiverText}}
            </div>
            <div class="legal-text">{{waiverTextFooter(waiver)}}</div>
            <div v-on:click="signWaiver(waiver, index)" class="btn btn-primary">Sign</div>
        </div>
        <signwaiver v-show="showSignaturePanel" ref="signWaiver"></signwaiver>
    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store';
    import signwaiver from '../components/sign-waiver'

    export default {
        name: 'waivers',        
        data: function () {
            return {
                //Data Here
            }
        },
        computed: {
            ...mapGetters({
                currentStep: 'progress/currentStep',
                nextStep: 'progress/nextStep',
                selectedMembers: 'group/selectedMembers',
                selectedContact: 'contact/selectedContact',
                waivers: 'waiver/waivers',
                locationWaiver: 'waiver/locationWaiver',
                signingWaiver: 'waiver/signingWaiver',
                activeWaiverIndex: 'waiver/activeWaiverIndex',
                stepChanged: 'progress/stepChanged',
                terminalId: 'progress/terminalId',
                locationAuthCode: 'progress/terminalId',
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
            },
            showSignaturePanel: function () {
                //If we are currently signing a waiver, show the signature panel
                if (this.signingWaiver) {
                    return true
                }
                else {
                    return false
                }
            }
        },
        methods: {
            signWaiver: function (waiver, waiverIndex) {
                this.$store.commit('waiver/setActiveWaiverIndex', waiverIndex)
                //TODO: Can set signing waiver based on index above

                ClearTablet()
                this.$store.commit('waiver/setSigningWaiver', waiver)

                this.$refs.signWaiver.enableSignaturePad()
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
            signwaiver
        }
    }

</script>