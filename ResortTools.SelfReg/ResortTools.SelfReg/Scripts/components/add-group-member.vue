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
                <router-link :to="{ name: 'manageGroup' }" tag="Span" class="btn btn-warning">Cancel</router-link>
            </div>
        </div>
    </div>
</template>

<script>
    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'

    export default {
        name: 'add-group-member',
        data: function () {
            return {
                newMember: {
                    firstName: null,
                    lastName: null,
                    photoUrl: '/images/headshot-silhouette.jpg'
                }
            }
        },
        computed: {
            ...mapGetters({
                thisContact: 'contact/selectedContact',
                members: 'group/memberList'
            })
        },
        methods: {
            addMember: function () {
                var member = this.newMember
                var accountId = this.thisContact.accountId
                this.$store.dispatch('group/addMember', {member, accountId} )
                this.$router.push({ name: 'groupMemberList', params: {reloadMembers: false}})
            }
        }
    }
</script>