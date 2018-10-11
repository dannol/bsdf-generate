//* Module: progress
//* Data and functionality necesssary for tracking progress through self reg process

import Vue from 'vue/dist/vue.js';

const state = {
    processSteps: selfRegistrationConfig.steps,
    terminalId: terminalId,
    currentStep: null,
    previousStep: null,
    nextStep: null,
    stepChanged: false,
    printOnComplete: selfRegistrationConfig.printOnComplete
}

const getters = {
    processSteps: state => state.processSteps,
    terminalId: state => state.terminalId,
    currentStep: state => state.currentStep,
    previousStep: state => state.previousStep,
    nextStep: state => state.nextStep,
    stepChanged: state => state.stepChanged,
    printOnComplete: state => state.printOnComplete
}

const mutations = {
    setCurrentStepNumber(state, stepNumber) {
        if (stepNumber != null) {
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
