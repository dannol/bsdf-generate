<template>
    <div>
        <div class="col-xs-9">
            <h4>Update {{thisContact.firstName}} {{thisContact.lastName}}</h4>
            <div>
                <img :src="thisContact.photoUrl" />
            </div>
            <div>
                <label for="first-name">First Name:</label>
                <input v-model="thisContact.firstName" name="first-name" type="text" />
            </div>
            <div>
                <label for="last-name">Last Name:</label>
                <input v-model="thisContact.lastName" name="last-name" type="text" />
            </div>

            <div>
                <a v-on:click="updateContact" class=" btn btn-warning">Update</a>
                <router-link :to="{ name: 'contactList'}" tag="a" class="btn btn-warning">Cancel</router-link>
            </div>
        </div>
    </div>
</template>

<script>
    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'

    export default {
        name: 'update-contact',
        data: function () {
            return {
               // Data Here
            }
        },
        methods: {
            updateContact: function () {
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
        }
    }
</script>