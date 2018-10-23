<template>
    <div>
        <!-- Manage Registrations Step-->
        <aside id="progress-map-container" class="left-nav full-height">
            <progressmap></progressmap>
        </aside>
        <div id="group-screen" class="full-screen full-height">
            <div class="outer-panel">
                <a href="./#/confirm-restart" class="start-over-btn">Start Over</a>
                <h3 class="page-title">Complete Registrations</h3>
                <div id="registration-management-panel" class="inner-panel center-in-parent">
                    <router-view></router-view>
                    <navigation></navigation>
                </div>
            </div>
            <memberbar></memberbar>
        </div>
    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store';
    import progressmap from '../components/progress-map'
    import navigation from '../components/navigation'
    import memberbar from '../components/member-bar'

    export default {
        name: 'manage-regstrations',
        data: function () {
            return {
                //Data goes here
            }
        },
        computed: {
            ...mapGetters({
               //Getters here
            })
        },
        components: {
            progressmap,
            navigation,
            memberbar
        }
    }

</script>