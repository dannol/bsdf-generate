
<template>
    <div>
        <aside class="member-list">
            <div class="contact-name">
                <div class="contact-first-name">
                    {{thisContact.firstName}}
                </div>
                <div class="contact-last-name">
                    {{thisContact.lastName}}
                </div>
            </div>
            <div v-for="member in selectedMembers" class="group-member">
                <img v-if="member.photoUrl != null && member.photoUrl.length" :src="member.photoUrl" class="group-member-image">
                <div v-else class="group-member-image" >{{getInitials(member)}}</div>
                <div class="member-name">{{member.firstName}}</div>
            </div>
        </aside>
    </div>

</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'

    export default {
        name: 'member-bar',
        computed: {
            ...mapGetters({
                thisContact: 'contact/selectedContact',
                members: 'group/members',
                selectedMembers: 'group/selectedMembers'
            })
        },
        methods: {
            getInitials: function (member) {
                return member.firstName.charAt(0) + member.lastName.charAt(0) 
            }
        }
    }

</script>