<template>
    <div>
        <h3>Search for Account </h3>
        <label for="last-name">Last Name:</label>
        <input v-model="searchData.firstName" name="last-name" type="text" />
        <label for="first-name">First Name:</label>
        <input v-model="searchData.lastName" name="first-name" type="text" />
        <div v-on:click="search" class="btn btn-primary">Search</div>
    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'

    export default {
        name: 'search-by-order',
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
                currentStepNumber: 'progress/currentStepNumber',
            })
        },
        methods: {
            search: function () {
                this.$store.dispatch('contact/searchByPersonalInfo', this.searchData)
                this.$router.push('contact-list')
            }
        },
        components: {
            //Child Components Here
        }
    }
</script>