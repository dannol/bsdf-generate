//* Module: progress
//* Data and functionality necesssary for tracking progress through self reg process

import Vue from 'vue/dist/vue.js';

const state = {
    currentStepNumber: 0,
    processSteps: selfRegistrationConfig.steps,
    currentStep: null,
    previousStep: null,
    nextStep: null,
    stepChanged: false
}

const getters = {
    currentStepNumber: state => state.currentStepNumber,
    processSteps: state => state.processSteps,
    currentStep: state => state.currentStep,
    previousStep: state => state.previousStep,
    nextStep: state => state.nextStep,
    stepChanged: state => state.stepChanged
}

const mutations = {
    setCurrentStepNumber(state, stepNumber) {
        if (stepNumber != null) {
            state.currentStepNumber = stepNumber
            var i;
            for (i = 0; i < state.processSteps.length; i++) {
                if (state.processSteps[i].stepNumber == stepNumber) {
                    state.currentStep = state.processSteps[i]
                }
            }
        }

    },
    setCurrentStep(state, step) {
        if (step != null) {
            state.currentStep = step
        }
    },
    setPreviousStep(state, step) {
        if (step != null) {
            state.previousStep = step
        }
    },
    setNextStep(state, step) {
        if (step != null) {
            state.nextStep = step
        }
    },
    setStepChanged(state, changed) {
        if (changed != null) {
            state.stepChanged = changed
        }
    },
    completeStep(state, stepNumber) {
        if (stepNumber != null) {
            var i;
            for (i = 0; i < state.processSteps.length; i++) {
                if (state.processSteps[i].stepNumber == stepNumber) {
                    state.processSteps[i].stepComplete = true
                }
            }
        }
    }
}

const actions = {
    
}

export default {
    namespaced: true,
    state,
    getters,
    mutations,
    actions
}
