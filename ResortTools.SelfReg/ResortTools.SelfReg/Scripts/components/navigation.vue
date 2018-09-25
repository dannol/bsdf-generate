<template>
    <div class="col-xs-12 navigation">
        <a :href="'./#' + previousStep.route" class="btn btn-primary" v-if="previousStep !== null && previousStep.stepNumber !== 0"> << {{previousStep.name}}</a>
        <a :href="'./#' + nextStep.route" class="btn btn-primary" :disabled="!currentStep.stepComplete" v-if="nextStep !== null && nextStep.stepNumber > 1">{{nextStep.name}} >></a>
        <a href="./" class="btn btn-primary">Start Over</a>
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
            }
        }
    }

</script>