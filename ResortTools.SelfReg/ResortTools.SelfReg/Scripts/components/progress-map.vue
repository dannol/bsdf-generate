<template>
    <div class="progress-map">
        <img src="/images/logo.png" class="aside-logo" />
        <div v-if="displaySteps" v-for="(step, index) in progressSteps" class="col-xs-12 progress-step" v-bind:class="{'active-step': isActive(step.stepNumber)}">
            <div>
                <div class="progress-step-indicator">
                    {{index +1}}
                </div>
                <div class="progress-step-name">{{step.name}}</div>
            </div>
        </div>
    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store';

    export default {
        data() {
            return {

                //Determines if a certain step is active as defined in the store
                isActive: function (stepNumber) {

                    if (stepNumber == this.currentStep.stepNumber) {
                        return true
                    }
                    else {
                        return false
                    }

                }
            }

        },
        name: 'progress-map',
        computed: {
            //Just the active steps that should be included in the progress map
            progressSteps: function () {

                return selfRegistrationConfig.steps.filter(thisStep => {
                    return (thisStep.displayProgress)
                })
            },
            ...mapGetters({
                currentStep: 'progress/currentStep'
            }),
            displaySteps: function() {

                if (this.currentStep != null && this.currentStep.displayProgress) {
                    return true
                }
                else{
                    return false
                }

            }
            
        }
    }

</script>