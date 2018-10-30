<template>
    <div>
        <div v-for="(profile, index ) in rentalProfiles" v-if="index == activeProfileIndex">
            <h3> Complete Rental profile for {{profile.renterFirstName}} {{profile.renterLastName}}</h3>
            <div>
                <select v-model="profile.ability">
                    <option disabled value="">Select Abiility</option>
                    <option>A</option>
                    <option>B</option>
                    <option>C</option>
                </select>           
            </div>
            <div>
                <input v-model="profile.shoeSize" name="shoe-size" type="text" placeholder="Shoe Size" class="wide" />
            </div>
            <div v-on:click="saveProfile(profile, index)" class="btn btn-primary">Next</div>
        </div>

    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store';

    export default {
        name: 'rental-profiles',
        data: function () {
            return {
                rentalProfiles: [],
                activeProfileIndex: 0,
                numberOfProfilesProcessed: 0
            }
        },
        mounted: function() {
            if (!this.selectedMembers || this.selectedMembers.length == 0) {
                this.$store.commit('progress/completeStep', this.currentStep.stepNumber)
                if (this.currentStep.nextStepOnComplete) {
                    this.$router.push(this.nextStep.route)
                }
            }
            else {
                this.loadProfiles()
            }
        },
        computed: {
            ...mapGetters({
                currentStep: 'progress/currentStep',
                nextStep: 'progress/nextStep',
                selectedMembers: 'group/selectedMembers',
                stepChanged: 'progress/stepChanged',
                terminalId: 'progress/terminalId'
            })
        },
        methods: {
            saveProfile: function (renter, renterIndex) {

                var profileData = {
                    profile: this.registrants[i],
                    terminalId: this.terminalId
                }

                this.$store.dispatch('profile/updateProfile', profileData).then(data => {
                    if (this.activeProfileIndex < this.activeProfileIndex.length - 1) {
                        this.activeProfileIndex++
                    }
                    //For each asynchrnous call that comes back, keep track of how many regs are processed.
                    this.numberOfProfilesProcessed++
                    //If all the regs are processed, complete the step
                    if (this.numberOfProfilesProcessed == this.selectedMembers.length) {
                        this.$store.commit('progress/completeStep', this.currentStep.stepNumber)
                        if (this.currentStep.nextStepOnComplete) {
                            this.$router.push(this.nextStep.route)
                        }
                    }
                })
            },
            loadProfiles: function () {
                debugger
                var profiles = []
                var i
                for (i = 0; i < this.selectedMembers.length; i++) {
                    var postData = {
                        contactId: this.selectedMembers[i].contactId,
                        firstName: this.selectedMembers[i].firstName,
                        lastName: this.selectedMembers[i].lastName
                    }

                    var getProfileUrl = '/api/rental/getcontactprofile/terminalId/' + this.terminalId
                    var dataType = 'application/json; charset=utf-8'

                    this.$store.dispatch('api/post',
                        { url: getProfileUrl, data: postData, config: { headers: { 'Content-Type': 'application/json' } } },
                        { root: true }
                    ).then(data => {
                        this.rentalProfiles.push(data.results[0])
                    }).catch(err => console.log('Error getting rental profile: ' + err));
                }
            }
        },
        components: {
            //Components Here
        }
    }

</script>