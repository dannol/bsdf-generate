<template>
    <div>
        <h3>Scan your Card at any time</h3>
        <img src="/images/cardsilhouette.png" />
        <a v-on:click="search" class="btn btn-primary">Pretend to Scan Card</a>
    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'

    export default {
        name: 'scan-card',
        data: function () {
            return {
                //TODO: This is fake data unil we get the card scan to work
                cardNumber: '987675'
            }
        },
        computed: {
            ...mapGetters({
                currentStepNumber: 'progress/currentStepNumber',
                terminalId: 'progress/terminalId'
            }),
            searchData: function () {

                return {
                    cardNumber: this.cardNumber,
                    terminalId: this.terminalId
                }
            }
        },
        methods: {
            search: function () {
                this.$store.dispatch('contact/searchByCardNumber', this.searchData)
                this.$router.push('contact-list')
            }
        },
        components: {
            //Components Here
        }
    }
</script>