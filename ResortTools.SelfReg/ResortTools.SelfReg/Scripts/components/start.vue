<template>
    <div>
        <aside id="progress-map-container" class="left-nav full-height">
            <progressmap></progressmap>
        </aside>
        <div id="start-page-screen" class="full-screen full-height">
            <div class="outer-panel">
                <div class="inner-panel center-in-parent">
                    <div class="start-page-title">GET STARTED</div>
                    <div id="start-panels">
                        <a href="./#/manage-contact/search-by-order">
                            <div id="type-panel" class="start-panel">
                                <h2>TYPE</h2>
                                <h4>order number</h4>
                            </div>
                        </a>
                        <a href="./#/manage-contact/scan-card">
                            <div id="scan-panel" class="start-panel">
                                <h2>SCAN</h2>
                                <h4>card</h4>
                            </div>
                        </a>
                        <a href="./#/manage-contact/search-by-personal-info">
                            <div id="search-panel" class="start-panel">
                                <h2>SEARCH</h2>
                                <h4>name and birth date</h4>
                            </div>
                        </a>
                    </div>
                </div>
            </div>

        </div>
    </div>

</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'
    import progressmap from '../components/progress-map'


    export default {
        name: 'account',
        mounted: function () {
            this.$store.commit('progress/setCurrentStepNumber', 1)
        },
        computed: {
        },
        components: {
            progressmap
        }
    }

</script>