<template>
    <div>
        <div v-for="(registrant, index ) in registrants" v-if="index == activeRegistrantIndex">
            <h3> Complete Registration for {{registrant.firstName}} {{registrant.lastName}}</h3>
            <div>
                <input v-model="registrant.medication" name="medications" type="text" placeholder="Medications" class="wide" />
            </div>
            <div>
                <input v-model="registrant.foodAllergy" name="food-allergies" type="text" placeholder="Food Allergies" class="wide" />
            </div>
            <div>
                <input v-model="registrant.drugAllergy" name="drug-allergies" type="text" placeholder="Drug Allergies" class="wide" />
            </div>
            <div>
                <input v-model="registrant.specialCondition" name="special-conditions" type="text" placeholder="Special Conditions" class="wide" />
            </div>
            <div v-on:click="saveRegistration(registrant, index)" class="btn btn-primary">Save</div>
        </div>

    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store';

    export default {
        name: 'registrations',
        data: function () {
            return {
                activeRegistrantIndex: 0
            }
        },
        computed: {
            ...mapGetters({
                currentStep: 'progress/currentStep',
                nextStep: 'progress/nextStep',
                selectedMembers: 'group/selectedMembers',
                selectedContact: 'contact/selectedContact',
                registrations: 'registration/registrations',
                activeRegistration: 'registration/activeRegistration',
                activeRegistrationIndex: 'registration/activeRegistrationIndex',
                stepChanged: 'progress/stepChanged',
                terminalId: 'progress/terminalId'
            }),
            registrants: function () {
                return this.selectedMembers.filter(thismember => {
                    return (thismember.age < 18)
                })
            },
            registrantsData: function () {
                return {
                    registrants: this.registrants,
                    terminalId: this.terminalId
                }
            },
            builtRegistrations: function () {
                //We need this extra computed value so we can get the registrations in case the
                //participants changed
                debugger
                if (this.stepChanged) {
                    this.$store.dispatch('registration/getRegistrations', this.registrantsData)
                    this.$store.commit('progress/setStepChanged', false)
                }
                return this.registrations
            },
        },
        methods: {
            saveRegistration: function (registrant, registrationIndex) {
                if (this.activeRegistrantIndex < this.registrants.length - 1) {
                    this.activeRegistrantIndex++
                }
                else {
                    //Go to the emergency contact page if all registrants are processed
                    this.$router.push({ name: 'emergencyContacts'})
                }
            }
        },
        components: {
            //Components Here
        }
    }

</script>