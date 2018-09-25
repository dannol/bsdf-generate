<template>
    <div>
        <div class="col-xs-9">
            <h4>Add Member</h4>
            <div>
                <img src="/images/headshot-silhouette.jpg" />
            </div>
            <div>
                <label for="first-name">First Name:</label>
                <input v-model="newMember.firstName" name="first-name" type="text" />
            </div>
            <div>
                <label for="last-name">Last Name:</label>
                <input v-model="newMember.lastName" name="last-name" type="text" />
            </div>

            <div>
                <a v-on:click="addMember" class=" btn btn-warning">Add</a>
                <a v-on:click="cancel" class=" btn btn-warning">Cancel</a>
            </div>
        </div>
    </div>
</template>

<script>
    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'

    export default {
        name: 'add-member',
        data: function () {
            return {
                newMember: {
                    firstName: null,
                    lastName: null,
                    contactPhotoUrl: '/images/headshot-silhouette.jpg'
                }
            }
        },
        computed: {
            ...mapGetters({
                thisContact: 'contact/thisContact',
                members: 'group/memberList'
            })
        },
        methods: {
            addMember: function () {
                var member = this.newMember
                var accountId = this.thisContact.accountId
                this.$store.dispatch('group/addMember', {member, accountId} )
                this.$parent.addingMember = false
            },
            cancel: function () {
                this.$parent.addingMember = false
            }
        }
    }
</script>