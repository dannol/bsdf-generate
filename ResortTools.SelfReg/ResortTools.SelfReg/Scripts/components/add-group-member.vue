<template>
    <div class="inner-panel-content">
        <div class="col-xs-9">
            <h3>Add a family member participant.</h3>
            <div class="tip">
                Tip: Adults ({{adultAge}}+) must be present to sign their season-pass waiver.
            </div>
            <div>
                <input v-model="firstName" name="first-name" type="text" placeholder="First Name" />
            </div>
            <div>
                <input v-model="lastName" name="last-name" type="text" placeholder="Last Name" />
            </div>
            <div>
                <input v-model="email" name="email" type="text" placeholder="Email (optional)" class="wide" />
            </div>
            <div>
                <div class="center-in-parent">
                    <date-dropdown default="1995-01-10" min="1950" max="2019" v-model="selectedDate" />
                </div>
            </div>
            <div>
                <a v-on:click="addMember" class=" btn btn-warning">Add</a>
                <router-link :to="{ name: 'groupMemberList' }" tag="Span" class="btn btn-warning">Cancel</router-link>
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
        name: 'add-group-member',
        data: function () {
            return {
                firstName: null,
                lastName: null,
                email: null,
                selectedDate: ''
            }
        },
        mounted: function () {
            this.lastName = this.thisContact.lastName
        },
        computed: {
            ...mapGetters({
                thisContact: 'contact/selectedContact',
                selectedMembers: 'group/selectedMembers',
                members: 'group/memberList',
                terminalId: 'progress/terminalId'
            }),
            adultAge: function () {
                return selfRegistrationConfig.minAdultAge
            },
            newMemberData: function () {
                //create a date object from the selected date value (mm.dd.yyyy)
                var dobArray = this.selectedDate.split('.')

                return {
                    member: {
                        parentContactId: this.thisContact.contactId,
                        contactId: this.contactId,
                        firstName: this.firstName,
                        lastName: this.lastName,
                        email: '',
                        phone: '',
                        hometown: '',
                        //TODO: SRK-52 - Drive this from a configurable localized date format
                        dateOfBirth: dobArray[2] + '-' + dobArray[1] + '-' + dobArray[0],
                        cardNumber: '',
                        photoUrl: '',
                        orderArrivalDate: ''
                    },
                    terminalId: this.terminalId
                }
            }
        },
        methods: {
            addMember: function () {
                var contactId = this.thisContact.contactId
                this.$store.dispatch('group/addMember', this.newMemberData).then(data => {
                    if (data.status == 'OK') {
                        this.$router.push({ name: 'groupMemberList', params: { reloadMembers: false } })
                    }
                }).catch(err => console.log('add-contact.vue - Error adding contact: ' + err));
            }
        },
        components: {
            DateDropdown
        }
    }
</script>