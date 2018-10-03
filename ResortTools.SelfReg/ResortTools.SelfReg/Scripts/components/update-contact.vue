<template>
    <div>
        <div class="col-xs-9">
            <h4>Update {{thisContact.firstName}} {{thisContact.lastName}}</h4>
            <div>
                <img :src="thisContact.photoUrl" />
            </div>
            <div>
                <input v-model="thisContact.firstName" name="first-name" type="text" placeholder="First Name" disabled class="wide" />
            </div>
            <div>
                <input v-model="thisContact.lastName" name="last-name" type="text" placeholder="Last Name" disabled class="wide" />
            </div>
            <div>
                <div class="center-in-parent">
                    <date-dropdown default="1995-01-10" min="1950" max="2019" v-model="selectedDate" />
                </div>
            </div>
            <div>
                <input v-model="thisContact.email" name="email" type="text" placeholder="Email" class="wide" />
            </div>
            <div>
                <input v-model="thisContact.phone" name="phone" type="text" placeholder="Phone" class="wide" />
            </div>
            <div>
                <input v-model="thisContact.address1" name="address-street" type="text" placeholder="Street Address" class="wide" />
                <input v-model="thisContact.apartment" name="address-apt" type="text" placeholder="Apartment" class="thin" />
            </div>
            <div>
                <input v-model="thisContact.city" name="address-city" type="text" placeholder="City" class="wide" />
                <input v-model="thisContact.state" name="address-state" type="text" placeholder="State" class="thin" />

                <input v-model="thisContact.postalCode" name="address-postal-code" type="text" placeholder="Postal Code" class="medium" />
            </div>
            <div>
                <a v-on:click="updateContact" class=" btn btn-warning">Update</a>
                <router-link :to="{ name: 'contactList', params: {reloadMembers: true} }" tag="a" class="btn btn-warning">Cancel</router-link>
            </div>
        </div>
    </div>
</template>

<script>
    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'

    //https://www.npmjs.com/package/vue-date-dropdown
    import DateDropdown from 'vue-date-dropdown'
    export default {
        name: 'update-contact',
        data: function () {
            return {
                selectedDate: ''
            }
        },
        methods: {
            updateContact: function () {
                var dobArray = this.selectedDate.split('.')
                this.thisContact.dateOfBirth = dobArray[2] + '-' + dobArray[1] + '-' + dobArray[0]

                this.$store.dispatch('contact/updateContact', this.thisContact).then(data => {
                    //Go back to the contact list once all processing is complete
                    this.$router.push({ name: 'contactList', params: { reloadMembers: true } })
                })
                
            }
        },
        props: {
            thisContact: {
                type: Object,
                default: null
            }
        },
        components: {
            DateDropdown
        }
    }
</script>