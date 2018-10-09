<template>
    <div>
        <!--Confirm Restart-->
        <aside id="progress-map-container" class="left-nav full-height">
            <progressmap></progressmap>
        </aside>
        <div id="group-screen" class="full-screen full-height">
            <div class="outer-panel">
                <h3 class="page-title">Confirm Restart</h3>
                <div id="group-management-panel" class="inner-panel center-in-parent">
                    <div class="tip">Are you sure you want to start over?<br />All progress will be lost</div>
                    <a href="./" class="btn btn-primary">Yes</a>
                    <a class="btn btn-warning" @click="$router.go(-1)">No</a>
                </div>
            </div>
            <memberbar></memberbar>
        </div>
    </div>

</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'
    import progressmap from '../components/progress-map'
    import navigation from '../components/navigation'
    import memberbar from '../components/member-bar'

    export default {
        name: 'manage-group',
        data: function () {
            return {
                selectedMember: null
            }
        },
        computed: {
            ...mapGetters({
                thisContact: 'contact/selectedContact',
                members: 'group/members',
                selectedMembers: 'group/selectedMembers',
                currentStepNumber: 'progress/currentStepNumber'
            })
        },
        components: {
            progressmap,
            navigation,
            memberbar
        },

    }

</script>