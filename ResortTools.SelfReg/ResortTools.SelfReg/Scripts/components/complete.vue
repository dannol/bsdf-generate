<template>
    <div>
        <div id="group-screen" class="full-screen full-height">
            <div class="outer-panel">
                <a href="./" class="start-over-btn">Start Over</a>
                <h2 class="page-title">Complete</h2>
                <h3 class="page-title">Please continue to cashier.</h3>
                <div class="inner-panel center-in-parent">
                    <div>
                        <h3>All set!  Please continue to cashier.</h3>
                        <div>
                            <router-link :to="{ name: 'start' }" tag="a" class="btn btn-warning">Start Over</router-link>
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
            //this.$store.commit('progress/completeStep', this.currentStepNumber)
        },
        computed: {
            ...mapGetters({
                results: 'contact/results',
                currentStepNumber: 'progress/currentStepNumber'
            })
        }
    }
</script>