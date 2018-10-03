<template>
    <div>
        <div class="inner-panel-content">
            <h4>Update Member</h4>
            <div>
                <img :src="thisMember.photoUrl" />
            </div>
            <div>
                <input v-model="thisMember.firstName" name="first-name" type="text" placeholder="First Name" />
            </div>
            <div>
                <input v-model="thisMember.lastName" name="last-name" type="text" placeholder="Last Name" />
            </div>
            <div>
                <input v-model="thisMember.email" name="email" type="text" placeholder="Email (optional)" class="wide" />
            </div>
            <div>
                <div>Date of Birth</div>
                <div class="center-in-parent">
                    <date-dropdown min="1950" max="2019" :default="updateDob" v-model="selectedDate" />
                </div>
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

    //https://www.npmjs.com/package/vue-date-dropdown
    import DateDropdown from 'vue-date-dropdown'

    export default {
        name: 'update-group-member',
        data: function () {
            return {
                selectedDate: ''
            }
        },
        computed: {
            updateDob: {
                get: function () {
                    return this.formatDate(this.thisMember.dateOfBirth)
                }
            }
        },
        methods: {
            updateMember: function () {

                var dobArray = this.selectedDate.split('.')
                this.thisMember.dateOfBirth =  dobArray[2] + '-' + dobArray[1] + '-' + dobArray[0]

                this.$store.dispatch('group/updateMember', this.thisMember)
                this.$router.push({ name: 'groupMemberList', params: { reloadMembers: false } })
            },
            formatDate: function (date) {
                var d = new Date(date),
                    month = '' + (d.getMonth() + 1),
                    day = '' + d.getDate(),
                    year = d.getFullYear();

                if (month.length < 2) month = '0' + month;
                if (day.length < 2) day = '0' + day;

                return [month, day, year].join('.');
            }
        },
        props: {
            thisMember: {
                type: Object,
                default: null
            }
        },
        components: {
            DateDropdown
        }
    }
</script>