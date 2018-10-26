<template>
    <div>
        <h3>Scan your Card at any time</h3>
        <img src="/images/cardsilhouette.png" />
        <div>
            <input v-model="cardNumber" v-on:keyup.enter="search" name="card-number" type="text" autofocus class="hidden-input"/>
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
            search: function (event) {
                event.preventDefault();

                this.$store.dispatch('contact/searchByCardNumber', this.searchData)
                this.$router.push('contact-list')
            }
        },
        components: {
            //Components Here
        }
    }
</script>