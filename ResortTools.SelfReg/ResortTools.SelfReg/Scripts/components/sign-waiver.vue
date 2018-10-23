<template>
    <div class="signature-panel">
        <h3>Please Sign</h3>
        <table border="1" cellpadding="0" width="500">
            <tr>
                <td height="100" width="500">
                    <canvas id="cnv" name="cnv" width="500" height="100"></canvas>
                </td>
            </tr>
        </table>

        <BR />
        <!--<canvas name="SigImg" id="SigImg" width="500" height="100"></canvas>-->
        <div class="missing-signature" v-if="missingSignature">
            No Signature detected. Please use pen to sign on the signature pad.
        </div>
        
        <form action="#" name=FORM1>
            <p>
                <a v-on:click="acceptSignature()" class="btn btn-primary" v-show="!signatureAccepted" >Accept Signature</a>
                <a v-on:click="clearSignature()" class="btn btn-primary" v-show="!signatureAccepted">Clear</a>
                <a v-on:click="cancel()" class="btn btn-primary" v-show="!signatureAccepted">Cancel</a>
                <a v-on:click="done()" class="btn btn-primary" v-show="signatureAccepted">Complete Signing</a>

                <!--These hidden fields are where the actual image data is stored-->
                <INPUT TYPE=HIDDEN NAME="bioSigData" />
                <INPUT TYPE=HIDDEN NAME="sigImgData" />

                <!--These Text Areas can be displayed to see the actual image data-->
                <TEXTAREA NAME="sigStringData" ROWS="5" COLS="50" style="display:none;">SigString: </TEXTAREA>
                <TEXTAREA NAME="sigImageData" ROWS="5" COLS="50" style="display:none;">Base64 String: </TEXTAREA>
            </p>
        </form>

        <br /><br />
    </div>

</template>

<script>

    import Vue from 'vue/dist/vue.js';
    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store';

    export default {
        data() {
            return {
                signatureAccepted: false,
                missingSignature: false
            }
        },
        name: 'sign-waver',
        computed: {
            ...mapGetters({
                signingWaiver: 'waiver/signingWaiver',
                waivers: 'waiver/waivers',
                allWaiversSigned: 'waiver/allWaiversSigned',
                currentStep: 'progress/currentStep',
                nextStep: 'progress/nextStep',
                terminalId: 'progress/terminalId'
            }),
            waiverData: function () {
                return {
                    waiver: this.signingWaiver,
                    terminalId: this.terminalId
                }
            }
        },
        methods: {
            acceptSignature: function () {
                if (NumberOfTabletPoints() == 0) {
                    this.missingSignature = true
                }
                else {

                    this.missingSignature = false
                    //Topaz Code
                    SetTabletState(0, tmr);
                    //RETURN TOPAZ-FORMAT SIGSTRING
                    SetSigCompressionMode(1);
                    document.FORM1.bioSigData.value = GetSigString();
                    document.FORM1.sigStringData.value += GetSigString();
                    //this returns the signature in Topaz's own format, with biometric information

                    //RETURN BMP BYTE ARRAY CONVERTED TO BASE64 STRING
                    SetImageXSize(500);
                    SetImageYSize(100);
                    SetImagePenWidth(5);
                    //This call loads the image data file into the hidden form field after processing is complete
                    GetSigImageB64(SigImageCallback);
                    this.signatureAccepted = true

                }
            },
            clearSignature: function () {
                ClearTablet()
                this.missingSignature = false
            },
            cancel: function () {
                ClearTablet()
                //set the waiver being signed to null which will hide the signature panel
                this.$store.commit('waiver/setSigningWaiver', null)
                this.missingSignature = false
            },
            //This function is needed because the Image Data isn't loaded in the hidden TEXTAREA until
            //processing is complete in the acceptSignature method and a callback is called.  We must
            // have this additional step to load the object once the data is loaded
            done: function () {
                debugger
                this.signingWaiver.signatureString = document.FORM1.bioSigData.value;
                this.signingWaiver.signatureBase64String = document.FORM1.sigImageData.value
                //Set the waiver as signed
                this.$store.dispatch('waiver/signWaiver', this.waiverData).then(data => {
                    if (data.status == 'OK') {
                        //set the waiver being signed to null which will hide the signature panel
                        this.$store.commit('waiver/setSigningWaiver', null)

                        if (this.allWaiversSigned) {
                            this.$store.commit('progress/completeStep', this.currentStep.stepNumber)
                            if (this.currentStep.nextStepOnComplete) {
                                this.$router.push({ name: this.nextStep.routeName })
                            }
                        }

                        ClearTablet()

                        this.signatureAccepted = false
                    }
                }).catch(err => console.log('add-contact.vue - Error signing waiver: ' + err));

                //set the waiver being signed to null which will hide the signature panel
                //this.$store.commit('waiver/setSigningWaiver', null)

                //if (this.allWaiversSigned) {
                //    this.$store.commit('progress/completeStep', this.currentStep.stepNumber)
                //    if (this.currentStep.nextStepOnComplete) {
                //        this.$router.push({ name: this.nextStep.routeName })
                //    }
                //}

                //ClearTablet()

                //this.signatureAccepted = false
            },
            enableSignaturePad: function () {
                //Topaz Code
                var ctx = document.getElementById('cnv').getContext('2d');
                SetDisplayXSize(500);
                SetDisplayYSize(100);
                SetTabletState(0, tmr);
                SetJustifyMode(0);
                ClearTablet();
                if (tmr == null) {
                    tmr = SetTabletState(1, ctx, 50);
                }
                else {
                    SetTabletState(0, tmr);
                    tmr = null;
                    tmr = SetTabletState(1, ctx, 50);
                }
            }
        }
    }

</script>
