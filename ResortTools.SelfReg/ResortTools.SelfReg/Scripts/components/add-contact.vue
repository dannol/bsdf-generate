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
                <label class="form-label"  for="address-street-1">Hometown</label>
                <input v-model="newContact.hometown" name="address-street-1" type="text" />
            </div>
            <div>
                <label class="form-label"  for="address-street-2">Photo</label>
                <input v-model="newContact.photoUrl" name="address-street-2" type="text" />
            </div>
            <div>
                <label class="form-label"  for="address-city">Card Number:</label>
                <input v-model="newContact.cardNumber" name="address-city" type="text" />
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

    export default {
        name: 'add-contact',
        data: function () {
            return {
                newContact: {
                    accountId: null,
                    firstName: null,
                    lastName: null,
                    hometown: null,
                    dateOfBirth: null,
                    cardNumber: null,
                    photoUrl: null,
                    orderArrivalDate: null
                }
            }
        },
        computed: {
            ...mapGetters({
                // mapGetters from store here
            })
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
            //Child Components Here
        }
    }
</script>