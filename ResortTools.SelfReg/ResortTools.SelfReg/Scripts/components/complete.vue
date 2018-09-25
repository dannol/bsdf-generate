<template>
    <div>
        <aside id="progress-map-container" class="left-nav full-height">
            <progressmap></progressmap>
        </aside>
        <div id="group-screen" class="full-screen full-height">
            <div class="outer-panel">
                <div class="inner-panel center-in-parent">
                    <h2>COMLPLETE </h2>
                    <div>
                        <a href="./" class="btn btn-primary">Print Waivers</a>
                    </div>
                    <div>
                        <a href="./" class="btn btn-primary">I'm Done</a>
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


    export default {
        name: 'complete',
        mounted: function () {
            this.$store.commit('progress/completeStep', this.currentStepNumber)
        },
        computed: {
            ...mapGetters({
                results: 'contact/results',
                currentStepNumber: 'progress/currentStepNumber'
            })
        }
    }
</script>