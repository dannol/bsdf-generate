<template>
    <div class="inner-panel-content">
        <h3>Enter your Name and Date of Birth:</h3>
        <div>Tip: If you're entering for a child enter parent or guardian name first.</div>
        <div>
            <input v-model="firstName" name="last-name" type="text" placeholder="First Name" class="wide" />
        </div>
        <div>
            <input v-model="lastName" name="first-name" type="text" placeholder="LastName" class="wide" />
        </div>
        <div>
            <div class="center-in-parent">
                <date-dropdown default="1995-01-10" min="1950" max="2019" v-model="selectedDate" />
            </div>
        </div>
        <div v-on:click="search" class="btn btn-primary">Search</div>
    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'

    //https://www.npmjs.com/package/vue-date-dropdown
    import DateDropdown from 'vue-date-dropdown'

    export default {
        name: 'search-by-personal-info',
        data: function () {
            return {
                selectedDate: '',
                firstName: null,
                lastName: null
            }

        },
        computed: {
            ...mapGetters({
                currentStepNumber: 'progress/currentStepNumber',
                terminalId: 'progress/terminalId'
            }),
            searchData: function () {
                //create a date object from the selected date value (mm.dd.yyyy)
                var dobArray = this.selectedDate.split('.')

                return {
                    firstName: this.firstName,
                    lastName: this.lastName,
                    dateOfBirth: dobArray[2] + '-' + dobArray[1] + '-' + dobArray[0],
                    terminalId: this.terminalId
                }
            }
        },
        methods: {
            search: function () {
                this.$store.dispatch('contact/searchByPersonalInfo', this.searchData)
                this.$router.push('contact-list')
            }
        },
        components: {
            DateDropdown
        }
    }
</script>