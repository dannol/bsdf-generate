<template>
    <div>
        <div class="col-xs-9">
            <h4>Complete Your Information</h4>
            <div>Tips: If registering for a child, continue entering a parent's or guardian's info.</div>
            <div>
                <label class="form-label"  for="first-name">First Name:</label>
                <input v-model="newContact.firstName" name="first-name" type="text" />
            </div>

            <div>
                <label class="form-label"  for="last-name">Last Name:</label>
                <input v-model="newContact.lastName" name="last-name" type="text" />
            </div>
            <div>
                <label class="form-label"  for="email">Email:</label>
                <input v-model="newContact.email" name="email" type="text" />
            </div>
            <div>
                <label class="form-label"  for="phone">Phone:</label>
                <input v-model="newContact.phone" name="phone" type="text" />
            </div>
            <div>
                <label class="form-label"  for="address-street-1">Street 1:</label>
                <input v-model="newContact.addressStreet1" name="address-street-1" type="text" />
            </div>
            <div>
                <label class="form-label"  for="address-street-2">Street 2:</label>
                <input v-model="newContact.addressStreet2" name="address-street-2" type="text" />
            </div>
            <div>
                <label class="form-label"  for="address-city">City:</label>
                <input v-model="newContact.addressCity" name="address-city" type="text" />
            </div>
            <div>
                <label class="form-label"  for="address-state">State:</label>
                <input v-model="newContact.addressState" name="address-state" type="text" />
            </div>
            <div>
                <label class="form-label"  for="address-postal-code">Postal Code:</label>
                <input v-model="newContact.addressPostalCode" name="address-postal-code" type="text" />
            </div>
            <div>
                <label class="form-label"  for="address-country">Country:</label>
                <input v-model="newContact.addressCountry" name="address-country" type="text" />
            </div>
            <div>
                <a v-on:click="addContact" class="btn btn-warning" :disabled="newContact.addressCountry == null">Add</a>
                <a v-on:click="cancel" class="btn btn-warning">Cancel</a>
            </div>
        </div>
    </div>
</template>

<script>
    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'
    import progressmap from '../components/progress-map'

    export default {
        name: 'add-contact',
        mounted: function () {

        },
        data: function () {
            return {
                newContact: {
                    firstName: null,
                    lastName: null,
                    email: null,
                    phone: null,
                    addressStreet1: null,
                    addressStreet2: null,
                    addressCity: null,
                    addressState: null,
                    addressPostalCode: null,
                    addressCountry: null
                }
            }
        },
        computed: {
            ...mapGetters({
                members: 'group/memberList',
            })
        },
        methods: {
            addContact: function () {
                this.$store.dispatch('contact/saveContact', this.newContact).then(data => {
                    if (data.status == 'OK') {

                    }
                }).catch(err => console.log('add-contact.vue - Error adding contact: ' + err));

                this.$parent.addingContact = false
            },
            cancel: function () {
                this.$parent.addingContact = false
            },
        },
        components: {
            progressmap
        }
    }
</script>