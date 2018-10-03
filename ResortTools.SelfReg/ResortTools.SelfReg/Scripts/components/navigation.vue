<template>
    <div class="col-xs-12 navigation">
        <a :href="'./#' + previousStep.route" class="btn btn-primary" v-if="showPreviousButton"> << {{previousStep.name}}</a>
        <a :href="'./#' + nextStep.route" class="btn btn-primary" :disabled="!currentStep.stepComplete" v-if="showNextButton">{{nextStep.name}} >></a>
    </div>
</template>

<script>

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
                    if (this.processSteps[i].stepNumber == this.currentStepNumber - 1) {
                        return this.processSteps[i]
                    }
                }

                return null
            },
            nextStep: function () {
                var i;
                for (i = 0; i < this.processSteps.length; i++) {
                    if (this.processSteps[i].stepNumber == this.currentStepNumber + 1) {
                        return this.processSteps[i]
                    }
                }

                return null
            },
            ...mapGetters({
                currentStepNumber: 'progress/currentStepNumber',
                currentStep: 'progress/currentStep'
            }),
            isStepComplete: function () {
                return null
            },
            showPreviousButton: function () {
                if (this.previousStep != null && this.previousStep.stepNumber != 0) {
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
        }
    }

</script>