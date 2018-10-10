<template>
    <div class="inner-panel-content">
        <h3>Enter your Order # or Customer ID:</h3>
        <input v-model="orderId" class="wide" type="text" />
        <div v-on:click="search" class="btn btn-primary" :disabled="orderId == null">Search</div>
        <div>Where can I find my Order # or Customer ID?</div>
    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'

    export default {
        name: 'search-by-order',
        data: function () {
            return {
                orderId: null
            }
        },
        computed: {
            ...mapGetters({
                contacts: 'contact/contacts',
                currentStepNumber: 'progress/currentStepNumber',
                terminalId: 'progress/terminalId'
            }),
            searchData: function () {

                return {
                    orderId: this.orderId,
                    terminalId: this.terminalId
                }
            }
        },
        methods: {
            search: function () {
                this.$store.dispatch('contact/searchByOrder', this.searchData)
                this.$router.push('contact-list')
            }
        },
        components: {
            //Child Components Here
        }
    }
</script>