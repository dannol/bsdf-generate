﻿import Vue from 'vue/dist/vue.js';
import Vuelidate from 'vuelidate'
import VueRouter from 'vue-router'
import Vuex from 'vuex'
import store from './vuex/self-registration-store'
import router from './vuerouter/self-registration-router'
import progressMap from './components/progress-map'
import navigation from './components/navigation'
import { mapGetters } from 'vuex'

Vue.use(VueRouter)
Vue.use(Vuex)
Vue.use(Vuelidate)

new Vue({
    store,
    router,
    el: '#rental-office-app',
    components: { progressMap, navigation },
    created: function () {
        console.log('Create Rental Office app')
    },
    mounted: function () {
        console.log('Mounted Rental Office app')
    },
    computed: {
        //Just the active steps that should be included in the progress map
        processSteps: function () {

            return selfRegistrationConfig.steps.filter(thisStep => {
                return (thisStep.displayProgress)
            })
        },
        ...mapGetters({
            currentStepNumber: 'progress/currentStepNumber',
            navSteps: 'progress/processSteps'
        })

    },
    watch: {
        $route(to, from) {

            console.log('Route changed from ' + from.path + ' to ' + to.path)

            //Find the Step from the app config based on the current path
            var index = this.navSteps.findIndex(step => step.route == to.path)

            console.log('index is  ' + index)

            //The current route represents new step in the process
            if (index > -1) {

                var thisProcessStep = this.navSteps[index];

                this.$store.commit('progress/setCurrentStepNumber', thisProcessStep.stepNumber)

            }

        }
    }
})
