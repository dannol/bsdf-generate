<template>
    <div>
        <h3> Emergency Contact Info</h3>
        <div>
            <forminput v-model="primaryContactName" placeholder="Primary Contact Name"></forminput>
        </div>
        <div>
            <input v-model="primaryContactPhone" name="primary-contact-phone" type="text" placeholder="Primary Contact Phone" class="wide" />
        </div>
        <div>
            <input v-model="alternateContactName" name="secondary-contact-name" type="text" placeholder="Secondary Contact Name" class="wide" />
        </div>
        <div>
            <input v-model="alternateContactPhone" name="secondary-contact-phone" type="text" placeholder="Secondary Contact Phone" class="wide" />
        </div>
        <div v-on:click="saveContacts()" class="btn btn-primary">Save</div>

    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'
    import forminput from '../components/form/form-input'

    export default {
        name: 'emergency-contacts',
        data: function () {
            return {
                primaryContactName: null,
                primaryContactPhone: null,
                alternateContactName: null,
                alternateContactPhone: null,
            }
        },
        computed: {
            ...mapGetters({
                currentStep: 'progress/currentStep',
                nextStep: 'progress/nextStep',
                selectedMembers: 'group/selectedMembers',
                selectedContact: 'contact/selectedContact',
                stepChanged: 'progress/stepChanged',
                terminalId: 'progress/terminalId'
            }),
            registrants: function () {
                return this.selectedMembers.filter(thismember => {
                    return (thismember.age < 18)
                })
            }
        },
        methods: {
            saveContacts: function () {

                var i
                for (i = 0; i < this.registrants.length; i++) {

                    //Load the entered contact data into each contact
                    this.registrants[i].primaryContactName = this.primaryContactName
                    this.registrants[i].primaryContactPhone = this.primaryContactPhone
                    this.registrants[i].alternateContactName = this.alternateContactName
                    this.registrants[i].alternateContactPhone = this.alternateContactPhone

                    var contactData = {
                        contact: this.registrants[i],
                        terminalId: this.terminalId
                    }

                    this.$store.dispatch('contact/updateContact', contactData).then(data => {
                        if (this.activeRegistrantIndex < this.registrants.length - 1) {
                            this.activeRegistrantIndex++
                        }
                    })
                }

                if (this.activeRegistrantIndex >= this.registrants.length - 1) {
                    this.$store.commit('progress/completeStep', this.currentStep.stepNumber)
                    if (this.currentStep.nextStepOnComplete) {
                        this.$router.push({ name: this.nextStep.routeName })
                    }
                }

            }
        },
        components: {
            forminput
        }
    }

</script>