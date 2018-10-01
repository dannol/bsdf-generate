<template>
    <div>
        <div>
            <h4>Complete Your Information</h4>
            <div>Tips: If registering for a child, continue entering a parent's or guardian's info.</div>
            <div>
                <input v-model="firstName" name="first-name" type="text" placeholder="First Name" />
            </div>
            <div>
                <input v-model="lastName" name="last-name" type="text" placeholder="Last Name" />
            </div>
            <div>
                <div class="center-in-parent">
                    <date-dropdown default="1995-01-10" min="1950" max="2019" v-model="selectedDate" />
                </div>
            </div>
            <div>
                <input v-model="email" name="email" type="text" placeholder="Email" />
            </div>
            <div>
                <input v-model="phone" name="phone" type="text" placeholder="Phone" />
            </div>
            <div>
                <input v-model="address1" name="address-street" type="text" placeholder="Street Address" />
                <input v-model="apartment" name="address-apt" type="text" placeholder="Apartment" />
            </div>
            <div>
                <input v-model="city" name="address-city" type="text" placeholder="City" />
                <input v-model="state" name="address-state" type="text" placeholder="State" />

                <input v-model="postalCode" name="address-postal-code" type="text" placeholder="Postal Code" />
            </div>
            <div>
                <a v-on:click="addContact" class="btn btn-warning" :disabled="newContact.firstName == null">Add</a>
                <router-link :to="{ name: 'contactList' }" tag="a" class="btn btn-warning">Cancel</router-link>
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
        name: 'add-contact',
        data: function () {
            return {
                selectedDate: '',
                accountId: null,
                firstName: null,
                lastName: null,
                email: null,
                phone: null,
                address1: null,
                apartment: null,
                city: null,
                state: null,
                postalCode: null
            }
        },
        computed: {
            ...mapGetters({
                // mapGetters from store here
            }),
            newContact: function () {
                //create a date object from the selected date value (mm.dd.yyyy)
                var dobArray = this.selectedDate.split('.')

                return {
                    accountId: this.accountId,
                    firstName: this.firstName,
                    lastName: this.lastName,
                    email: this.email,
                    phone: this.phone,
                    hometown: this.city + ', ' + this.state,
                    dateOfBirth: dobArray[2] + '-' + dobArray[1] + '-' + dobArray[0],
                    cardNumber: this.cardNumber,
                    photoUrl: this.photoUrl,
                    orderArrivalDate: this.orderArrivalDate
                }
            }
        },
        methods: {
            addContact: function () {
                this.$store.dispatch('contact/addContact', this.newContact).then(data => {
                    if (data.status == 'OK') {
                        this.$router.push('contact-list')
                    }
                }).catch(err => console.log('add-contact.vue - Error adding contact: ' + err));
            }
        },
        components: {
            DateDropdown
        }
    }
</script>