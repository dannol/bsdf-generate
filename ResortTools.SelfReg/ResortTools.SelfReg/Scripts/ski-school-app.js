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
    el: '#ski-school-app',
    components: { progressMap, navigation },
    created: function () {
        console.log('Create  Ski School app')
    },
    mounted: function () {
        console.log('Mounted  Ski School app')
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

            //Find the Step from the app config based on the current path
            var index = this.navSteps.findIndex(step => step.route == to.path)

            console.log('Route changed from ' + from.path + ' to ' + to.path + ' with index ' + index)

            //The current route represents new step in the process
            if (index > -1) {
                console.log('Found a step ' + this.navSteps[index].stepName + 'with index ' + index)
                var thisProcessStep = this.navSteps[index]

                this.$store.commit('progress/setCurrentStepNumber', thisProcessStep.stepNumber)
                this.$store.commit('progress/setCurrentStep', thisProcessStep)

                if (index < this.navSteps.length - 1) {
                    this.$store.commit('progress/setNextStep', this.navSteps[index + 1])
                }

                if (index > 0) {
                    this.$store.commit('progress/setPreviousStep', this.navSteps[index - 1])
                }

                this.$store.commit('progress/setStepChanged', true)

            }

        }
    }
})

