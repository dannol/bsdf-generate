<template>
    <div>
        <label for="order-id">Input Order ID:</label>
        <input v-model="orderId" name="order-id" type="text" />
        <div v-on:click="search" class="btn btn-primary" :disabled="orderId == null">Search</div>
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
                //currentStepNumber: 'progress/currentStepNumber',
            })
        },
        methods: {
            search: function () {
                this.$store.dispatch('contact/searchByOrder', this.orderId)
                this.$router.push('contact-list')
            }
        },
        components: {
            //Child Components Here
        }
    }
</script>