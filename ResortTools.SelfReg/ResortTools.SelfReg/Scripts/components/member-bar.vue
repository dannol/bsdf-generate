
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
            <div v-for="member in members" class="group-member">
                <img :src="member.photoUrl" class="group-member-image">
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
                thisContact: 'contact/thisContact',
                members: 'group/members'
            })
        }
    }

</script>