<template>
    <div>
        <div id="group-screen" class="full-screen full-height">
            <div class="outer-panel">
                <a href="./" class="start-over-btn">Start Over</a>
                <h2 class="page-title">Complete</h2>
                <h3 class="page-title" v-if="printOnComplete">Please take paperwork from printer</h3>
                <h3 class="page-title">Please continue to cashier.</h3>
                <div class="inner-panel center-in-parent">
                    <div>
                        <h3>All set!  Please continue to cashier.</h3>
                        <div>
                            <a href="./" class="btn btn-primary">Start Over</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store';


    export default {
        name: 'complete',
        mounted: function () {
            if (this.printOnComplete) {
                //TODO: SRK-28 -Add Printing Logic Here
            }

            //We don't really need to complete the "Complete" step,but in case it's needed for consistency
            //this.$store.commit('progress/completeStep', this.currentStep.stepNumber)
        },
        computed: {
            ...mapGetters({
                results: 'contact/results',
                currentStep: 'progress/currentStep',
                printOnComplete: 'progress/printOnComplete'
            })
        }
    }
</script>