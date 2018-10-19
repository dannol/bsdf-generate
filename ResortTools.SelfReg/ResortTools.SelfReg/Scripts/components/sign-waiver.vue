<template>
    <div class="signature-panel">
        <table border="1" cellpadding="0" width="500">
            <tr>
                <td height="100" width="500">
                    <canvas id="cnv" name="cnv" width="500" height="100"></canvas>
                </td>
            </tr>
        </table>

        <BR/>
        <canvas name="SigImg" id="SigImg" width="500" height="100"></canvas>

        <form action="#" name=FORM1>
            <p>
                <input id="SignBtn" name="SignBtn" class="btn btn-primary" type="button" value="Sign" onclick="javascript:onSign()" />&nbsp;&nbsp;&nbsp;&nbsp;
                <input id="button1" name="ClearBtn" type="button" value="Clear" onclick="javascript:onClear()" />&nbsp;&nbsp;&nbsp;&nbsp
                <input id="button2" name="DoneBtn" type="button" value="Done" onclick="javascript:onDone()" />&nbsp;&nbsp;&nbsp;&nbsp

                <a v-on:click="acceptSignature()" class="btn btn-primary">Accept Signature</a>

                <INPUT TYPE=HIDDEN NAME="bioSigData" />
                <INPUT TYPE=HIDDEN NAME="sigImgData" />
                <BR />
                <BR />
                <!--These Text Areas can be uncommented to see the actual image data-->
                <TEXTAREA NAME="sigStringData" ROWS="10" COLS="50">SigString: </TEXTAREA>
                <TEXTAREA NAME="sigImageData" ROWS="10" COLS="50">Base64 String: </TEXTAREA>
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
                currentWaiver: false
            }
        },
        name: 'sign-waver',
        computed: {
            ...mapGetters({
                //Map Getters here
            }),
            showSignaturePanel: function () {
                if (this.currentWaiver) {
                    return true
                }
                else {
                    return false
                }
            }
        },
        methods: {
            acceptSignature: function () {
                onDone()
                this.$store.commit('waiver/setSigningWaiver', null)
            }
        }
    }

</script>
