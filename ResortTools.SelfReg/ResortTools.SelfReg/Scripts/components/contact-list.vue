<template>
    <div>
        <form id="select-contact">
            <fieldset class="col-xs-12">
                <div>
                    <h2>{{contacts.length}} Matches Found</h2>
                    <span v-if="contacts.length > 0">
                        Looks like you may have been here before.<br />
                        Select name below.
                    </span>
                </div>
                <div v-for="(contact, index) in contacts" >
                    <div v-if="contact.dateOfBirth !== null" class="row contact-search-result" v-bind:class="{'contact-selected': contact.selected}" v-on:click="selectContact(contact)">
                        <div class="col-xs-1">
                            <input type="checkbox" name="search-result" v-model="contact.selected" disabled />
                        </div>
                        <div class="col-xs-4">
                            <h4 class="search-result-name">{{contact.firstName}} {{contact.lastName}}</h4>
                            <div class="search-result-hometown">
                                {{contact.hometown}}
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <div v-if="contact.orderArrivalDate != null" class="search-result-order-date">Upcoming Order<br />Arrival Date:{{contact.orderArrivalDate}}</div>
                            <div v-else class="search-result-order-date">No Upcoming Orders</div>
                        </div>
                        <div class="col-xs-4">
                            <div v-if="contact.cardNumber != null" class="search-result-card"><img src="/images/card-small-black.png"><div>{{contact.cardNumber}}</div></div>
                            <div v-else class="search-result-order-date"><img src="/images/card-small-grey.png"><div>No Card</div></div>
                        </div>
                    </div>
                    <div v-else class="member-ineligible row contact-search-result">
                        <div>
                            {{contact.firstName}} {{contact.lastName}}
                        </div>
                        <div>Ineligible (Missing Date of Birth)</div>
                    </div>
                </div>
                <div>
                    <router-link :to="{ name: 'addContact' }" tag="a" class="btn btn-warning">I'm not listed</router-link>
                </div>
            </fieldset>
        </form>
    </div>
</template>

<script>

    import Vue from 'vue/dist/vue.js';
    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'


    export default {
        name: 'contact-list',
        data: function () {
            return {
                selectedContact: null
            }
        },
        computed: {
            ...mapGetters({
                contacts: 'contact/contacts',
                currentStep: 'progress/currentStep'
            })
        },
        methods: {
            selectContact: function (contact) {
                this.$store.commit('contact/selectContact', contact)
                this.$store.commit('progress/completeStep', this.currentStep.stepNumber)
                //If this step is configured to automatically go to the next step, go there.
                if (this.currentStep.nextStepOnComplete) {
                    this.$router.push({ name: this.nextStep.routeName })
                }
            },
            updateContact: function (contact) {
                //We need a copy of the contact object to send the update in case the update is cancelled
                this.selectedContact = Object.assign({}, contact)
                this.$router.push({ name: 'updateContact', params: { thisContact: this.selectedContact } })
            }
        }
    }
</script>