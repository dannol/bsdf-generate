<template>
    <div>
        <aside id="progress-map-container" class="left-nav full-height">
            <progressmap></progressmap>
        </aside>

        <div id="account-search-page-screen" class="full-screen full-height">
            <div class="outer-panel">
                <div class="inner-panel center-in-parent">
                    <div class="col-xs-12" v-if="results.length == 0">
                        <h3>Search for Account </h3>
                        <label for="last-name">Last Name:</label>
                        <input v-model="searchData.firstName" name="last-name" type="text" />
                        <label for="first-name">First Name:</label>
                        <input v-model="searchData.lastName" name="first-name" type="text" />
                        <div v-on:click="search" class="btn btn-primary">Search</div>
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


    export default {
        name: 'account-search',
        data: function () {

            return {
                searchData: {
                    firstName: null,
                    lastName: null
                }
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

                //TODO: Make Another search API
                this.$store.dispatch('contact/searchByPersonalInfo', this.searchData)
            },
        },
        components: {
            contactsearchresults,
            navigation
        }
    }</script>