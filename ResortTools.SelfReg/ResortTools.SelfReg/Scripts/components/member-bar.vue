
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
                <img v-if="member.photo != null && member.photo.length" :src="getPhotoUrl(member)" class="group-member-image">
                <div v-else class="group-member-image">{{getInitials(member)}}</div>
                <div class="member-name">{{member.firstName}}</div>
            </div>
            <div class="terminal-id" v-if="testMode">Terminal: {{terminalId}}</div>
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
                selectedMembers: 'group/selectedMembers',
                terminalId: 'progress/terminalId',
                testMode: 'progress/testMode'
            })
        },
        methods: {
            getInitials: function (member) {
                return member.firstName.charAt(0) + member.lastName.charAt(0)
            },
            getPhotoUrl: function (member) {
                return 'data:image/jpeg;base64,' + this.hexToBase64(member.photo)
            },
            hexToBase64: function(str) {
                return btoa(String.fromCharCode.apply(null, str.replace(/\r|\n/g, "").replace(/([\da-fA-F]{2}) ?/g, "0x$1 ").replace(/ +$/, "").split(" ")));
            }
        }
    }

</script>