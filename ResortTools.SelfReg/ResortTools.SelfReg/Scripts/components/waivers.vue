<template>
    <div>
        <div v-for="(waiver, index ) in builtWaivers" v-if="waiver.isActive">
            <h3> Sign Waiver for {{waiver.signer.firstName}} {{waiver.signer.lastName}}</h3>
            <div class="legal-text">{{waiverTextHeader(waiver)}}</div>
            <div class="waiver-container">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Eget duis at tellus at urna condimentum mattis pellentesque. At tellus at urna condimentum mattis pellentesque. Laoreet id donec ultrices tincidunt. Non curabitur gravida arcu ac tortor dignissim. Elementum curabitur vitae nunc sed velit dignissim sodales ut eu. Amet est placerat in egestas erat imperdiet sed euismod. Faucibus pulvinar elementum integer enim neque volutpat ac tincidunt vitae. Scelerisque mauris pellentesque pulvinar pellentesque habitant. Malesuada fames ac turpis egestas maecenas pharetra convallis posuere.

                Condimentum vitae sapien pellentesque habitant morbi tristique senectus et netus. Tincidunt vitae semper quis lectus nulla at volutpat. Nulla at volutpat diam ut venenatis tellus in metus vulputate. Pellentesque pulvinar pellentesque habitant morbi. Odio eu feugiat pretium nibh. Tortor vitae purus faucibus ornare suspendisse sed. Aliquam ultrices sagittis orci a scelerisque. In ornare quam viverra orci sagittis eu. Lectus sit amet est placerat in egestas erat imperdiet. Tincidunt ornare massa eget egestas purus. Ornare arcu dui vivamus arcu felis bibendum ut tristique et. Sit amet dictum sit amet justo donec enim diam. Tellus id interdum velit laoreet id donec ultrices tincidunt. Et netus et malesuada fames ac turpis egestas maecenas pharetra. Sit amet porttitor eget dolor morbi non. Arcu non odio euismod lacinia at quis risus. Lorem ipsum dolor sit amet. Vel turpis nunc eget lorem dolor sed viverra ipsum. Molestie a iaculis at erat pellentesque adipiscing commodo elit at. Quis imperdiet massa tincidunt nunc pulvinar sapien et ligula.

                Dolor sit amet consectetur adipiscing elit. Euismod nisi porta lorem mollis. Viverra nibh cras pulvinar mattis nunc sed blandit. Commodo elit at imperdiet dui accumsan sit amet nulla facilisi. Malesuada proin libero nunc consequat. Eget mi proin sed libero enim sed faucibus turpis in. Vestibulum morbi blandit cursus risus at. Amet mauris commodo quis imperdiet massa tincidunt. Amet dictum sit amet justo donec enim diam vulputate ut. Quis enim lobortis scelerisque fermentum dui. Vulputate sapien nec sagittis aliquam. Pellentesque diam volutpat commodo sed egestas egestas.

                Sed adipiscing diam donec adipiscing tristique risus. Semper eget duis at tellus at urna. Laoreet suspendisse interdum consectetur libero. Egestas pretium aenean pharetra magna. Ipsum dolor sit amet consectetur adipiscing elit ut aliquam purus. Rutrum tellus pellentesque eu tincidunt tortor. Massa tempor nec feugiat nisl pretium fusce. Consequat interdum varius sit amet mattis vulputate enim nulla. Sagittis orci a scelerisque purus semper. Id leo in vitae turpis massa sed elementum tempus egestas. Fringilla ut morbi tincidunt augue interdum velit. Ut lectus arcu bibendum at varius
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
                currentStepNumber: 'progress/currentStepNumber',
                currentStep: 'progress/currentStep',
                nextStep: 'progress/nextStep',
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
                            text += ' and ' + waiver.minors[i].firstName
                        }
                        else {
                            text += waiver.minors[i].firstName + ' ,'
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