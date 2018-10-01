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
                cardNumber: null
            }
        },
        computed: {
            ...mapGetters({
                currentStepNumber: 'progress/currentStepNumber'
            })
        },
        methods: {
            search: function () {
                this.$store.dispatch('contact/searchByCardNumber', this.cardNumber)
                this.$router.push('contact-list')
            }
        },
        components: {
            //Components Here
        }
    }
</script>