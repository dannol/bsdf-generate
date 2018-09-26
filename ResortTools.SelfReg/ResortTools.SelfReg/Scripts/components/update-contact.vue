<template>
    <div>
        <div class="col-xs-9">
            <h4>Update {{thisContact.firstName}} {{thisContact.LastName}}</h4>
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
                <a v-on:click="cancel" class=" btn btn-warning">Cancel</a>
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

            }
        },
        computed: {
            thisMember: function () { return this.$parent.selectedMember }
        },
        methods: {
            updateContact: function () {
                this.$store.dispatch('contact/updateContact', this.thisContact)
                this.$parent.updatingContact = false
            },
            cancel: function () {
                this.$parent.loadContacts()
                this.$parent.updatingContact = false
            }
        }
    }
</script>