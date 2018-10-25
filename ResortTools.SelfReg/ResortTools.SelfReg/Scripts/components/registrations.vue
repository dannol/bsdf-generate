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
            <div>
                <b>Primary Contact:</b> {{registrant.primaryContactName}}
            </div>
            <div>
                <b>Primary Phone:</b> {{registrant.primaryContactPhone}}
            </div>
            <div>
                <b>Secondary Contact:</b> {{registrant.alternateContactName}}
            </div>
            <div>
                <b>Secondary Phone:</b> {{registrant.alternateContactPhone}}
            </div>
            <div v-on:click="saveRegistration(registrant, index)" class="btn btn-primary">Next</div>
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
                selectedMembers: 'group/selectedMembers'
            }),
            registrants: function () {
                return this.selectedMembers.filter(thismember => {
                    return (thismember.age < 18)
                })
            }           
        },
        methods: {
            saveRegistration: function (registrant, registrationIndex) {
                if (this.activeRegistrantIndex < this.registrants.length - 1) {
                    this.activeRegistrantIndex++
                }
                else {
                    //Go to the emergency contact page if all registrants are processed
                    this.$router.push({ name: 'emergencyContacts' })
                }
            }
        },
        components: {
            //Components Here
        }
    }

</script>