<template>
    <div class="col-xs-12 navigation">
        <a v-on:click="goToPreviousStep(previousStep)"  class="btn btn-primary" v-if="showPreviousButton"> << {{previousStep.name}}</a>
        <a :href="'./#' + nextStep.route" class="btn btn-primary" :disabled="!currentStep.stepComplete" v-if="showNextButton">{{nextStep.name}} >></a>
    </div>
</template>

<script>

    import Vue from 'vue/dist/vue.js';
    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store';

    export default {
        data() {
            return {
                //All the steps defined in appsetting.json
                processSteps: selfRegistrationConfig.steps
            }

        },
        name: 'navigation',
        computed: {
            previousStep: function () {

                var i;
                for (i = 0; i < this.processSteps.length; i++) {
                    if (this.processSteps[i].stepNumber == this.currentStep.stepNumber - 1) {
                        return this.processSteps[i]
                    }
                }

                return null
            },
            nextStep: function () {
                var i;
                for (i = 0; i < this.processSteps.length; i++) {
                    if (this.processSteps[i].stepNumber == this.currentStep.stepNumber + 1) {
                        return this.processSteps[i]
                    }
                }

                return null
            },
            ...mapGetters({
                currentStep: 'progress/currentStep'
            }),
            isStepComplete: function () {
                return null
            },
            showPreviousButton: function () {
                if (this.previousStep != null && this.previousStep.allowReturnToStep) {
                    return true
                }
                else {
                    return false
                }
            },
            showNextButton: function () {
                if (this.nextStep !== null && this.nextStep.stepNumber > 1 && !this.currentStep.nextStepOnComplete) {
                    return true
                }
                else {
                    return false
                }
            }
        },
        methods: {
            goToPreviousStep: function (previousStep) {
                //If going to the previous step, set it to incomplete
                Vue.set(previousStep, 'stepComplete', false)
                this.$router.push(previousStep.route)
            }
        }
    }

</script>