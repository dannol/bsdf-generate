<template>
    <div>
        <aside id="progress-map-container" class="left-nav full-height">
            <progressmap></progressmap>
        </aside>

        <div id="order-search-page-screen" class="full-screen full-height">
            <div class="outer-panel">
                <a href="./" class="start-over-btn">Start Over</a>
                <h3 class="page-title">Find Customer</h3>
                <div class="inner-panel center-in-parent">
                   <router-view></router-view>
                    <navigation></navigation>
                </div>
            </div>

        </div>

 
    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'
    //import contactsearchresults from '../components/contact-search-results'
    import navigation from '../components/navigation'
    import progressmap from '../components/progress-map'

    export default {
        name: 'manage-contact',
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
            },
            loadContacts: function () {
                this.$store.dispatch('contact/searchByOrder', this.orderId)
            }

        },
        components: {
            //contactsearchresults,
            navigation,
            progressmap
        }
    }
</script>