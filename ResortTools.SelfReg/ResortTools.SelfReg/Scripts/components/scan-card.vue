<template>
    <div>
        <h3>Scan your Card at any time</h3>
        <img src="/images/cardsilhouette.png" />
        <div>
            <input v-model="cardNumber" v-on:keyup="checkForReturn" name="card-number" type="text" autofocus />
        </div>
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
            },
            checkForReturn: function (event) {
                event.preventDefault();
                // Number 13 is the "Enter" key on the keyboard
                if (event.keyCode === 13) {
                    console.log(event.keyCode + ' pressed')
                    // If return is pressed, perform the search
                    this.$store.dispatch('contact/searchByCardNumber', this.searchData)
                    this.$router.push('contact-list')
                }
            }
        },
        components: {
            //Components Here
        }
    }
</script>