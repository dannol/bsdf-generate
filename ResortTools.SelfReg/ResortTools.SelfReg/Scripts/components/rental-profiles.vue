<template>
    <div>
        <div v-for="(profile, index ) in rentalProfiles" v-if="index == activeProfileIndex">
            <h3> Complete Rental profile for {{profile.renterFirstName}} {{profile.renterLastName}}</h3>
            <div>
                <select v-model="profile.ability" class="wide">
                    <option v-for="ability in abilities" :value="ability.listKey">{{ability.listDescription}}</option>
                </select>
            </div>
            <div>
                <select v-model="profile.age" class="wide">
                    <option v-for="age in ages" :value="age.listKey">{{age.listDescription}}</option>
                </select>
            </div>
            <div>
                <select v-model="profile.height" class="wide">
                    <option v-for="height in heights" :value="height.listKey">{{height.listDescription}}</option>
                </select>
            </div>
            <div>
                <select v-model="profile.weight" class="wide">
                    <option v-for="weight in weights" :value="weight.listKey">{{weight.listDescription}}</option>
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
        mounted: function () {
            if (!this.selectedMembers || this.selectedMembers.length == 0) {
                this.$store.commit('progress/completeStep', this.currentStep.stepNumber)
                if (this.currentStep.nextStepOnComplete) {
                    this.$router.push(this.nextStep.route)
                }
            }
            else {
                this.loadProfileData()
            }
        },
        computed: {
            ...mapGetters({
                currentStep: 'progress/currentStep',
                nextStep: 'progress/nextStep',
                selectedMembers: 'group/selectedMembers',
                stepChanged: 'progress/stepChanged',
                terminalId: 'progress/terminalId'
            }),
            //TODO: SRK-71 - Since every record returned has lists of all the possible values
            //for abiliteis, ages, heights and weights used in drop-downs, we will just use the first one.  
            //This should be replaced with a separate call to get the values.
            abilities: function () {
                return this.rentalProfiles[0].abilities
            },
            ages: function () {
                return this.rentalProfiles[0].ages
            },
            heights: function () {
                return this.rentalProfiles[0].heights
            },
            weights: function () {
                return this.rentalProfiles[0].weights
            }
        },
        methods: {
            saveProfile: function (profile, profileIndex) {

                //Create a smaller object of data matching the Rental Pofile model
                var profileData = {
                    contactId: profile.contactId,
                    ability: profile.ability,
                    age: profile.age,
                    height: profile.height,
                    weight: profile.weight,
                    shoeSize: profile.shoeSize
                }

                var saveProfileUrl = '/api/rental/saveprofile'
                var dataType = 'application/json; charset=utf-8'

                this.$store.dispatch('api/post',
                    { url: saveProfileUrl, data: profileData, config: { headers: { 'Content-Type': 'application/json' } } },
                    { root: true }
                ).then(data => {
                    if (data.status == 'OK') {
                        if (this.activeProfileIndex < this.activeProfileIndex.length - 1) {
                            this.activeProfileIndex++
                        }
                        //For each asynchrnous call that comes back, keep track of how many profiles are processed.
                        this.numberOfProfilesProcessed++
                        //If all the regs are processed, complete the step
                        if (this.numberOfProfilesProcessed == this.rentalProfiles.length) {
                            this.$store.commit('progress/completeStep', this.currentStep.stepNumber)
                            if (this.currentStep.nextStepOnComplete) {
                                this.$router.push(this.nextStep.route)
                            }
                        }
                    }
                }).catch(err => console.log('Error getting rental profile: ' + err));

            },
            loadProfileData: function () {
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