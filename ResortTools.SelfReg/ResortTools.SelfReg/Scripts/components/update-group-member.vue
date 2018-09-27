<template>
    <div>
        <div class="col-xs-9">
            <h4>Update Member</h4>
            <div>
                <img :src="thisMember.photoUrl" />
            </div>
            <div>
                <label for="first-name">First Name:</label>
                <input v-model="thisMember.firstName" name="first-name" type="text" />
            </div>
            <div>
                <label for="last-name">Last Name:</label>
                <input v-model="thisMember.lastName" name="last-name" type="text" />
            </div>

            <div>
                <a v-on:click="updateMember" class=" btn btn-warning">Update</a>
                <router-link :to="{ name: 'groupMemberList', params: {reloadMembers: true} }" tag="a" class="btn btn-warning">Cancel</router-link>
            </div>
        </div>
    </div>
</template>

<script>
    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'

    export default {
        name: 'update-group-member',
        data: function () {
            return {
                //Data Here
            }
        },
        methods: {
            updateMember: function () {
                this.$store.dispatch('group/updateMember', this.thisMember)
                this.$router.push({ name: 'groupMemberList', params: {reloadMembers: false} })
            }
        },
        props: {
            thisMember: {
                type: Object,
                default: null
            }
        }
    }
</script>