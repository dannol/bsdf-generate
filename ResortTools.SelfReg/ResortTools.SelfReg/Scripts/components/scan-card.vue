<template>
    <div>
        <aside id="progress-map-container" class="left-nav full-height">
            <progressmap></progressmap>
        </aside>

        <div id="order-search-page-screen" class="full-screen full-height">
            <div class="outer-panel">
                <div class="inner-panel center-in-parent">
                    <div class="col-xs-12" v-if="results.length == 0">
                        <h3>Scan your Card at any time</h3>
                        <img src="/images/cardsilhouette.png" />
                    </div>
                    <div class="col-xs-12">
                        <contactsearchresults v-if="results.length > 0"></contactsearchresults>
                    </div>
                    <navigation></navigation>
                </div>
            </div>

        </div>

 
    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'
    import contactsearchresults from '../components/contact-search-results'
    import navigation from '../components/navigation'
    import progressmap from '../components/progress-map'



    export default {
        name: 'order-search',
        data: function () {
            return {
                orderId: null
            }
        },
        computed: {
            ...mapGetters({
                results: 'contact/results',
                currentStepNumber: 'progress/currentStepNumber'
            })
        },
        methods: {
            search: function () {
                this.$store.dispatch('contact/searchByOrder', this.orderId)
            }

        },
        components: {
            contactsearchresults,
            navigation,
            progressmap
        }
    }
</script>