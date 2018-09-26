<template>
    <div>
        <form id="select-contact"  v-if="!addingContact && !updatingContact">
            <fieldset class="col-xs-12">
                <div>
                    <h2>{{results.length}} Matches Found</h2>
                    Looks like you may have been here before.<br />
                    Select name below.
                </div>
                <div v-for="(contact, index) in results" class="row contact-search-result">
                    <div class="col-xs-1">
                        <input type="radio" name="search-result" v-on:click="selectContact(contact)" />
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
                    <div class="col-xs-2">
                        <div v-if="contact.CardNumber != null" class="search-result-card"><img src="/images/card-small-black.png"><div>{{contact.jCardNumber}}</div></div>
                        <div v-else class="search-result-order-date"><img src="/images/card-small-grey.png"><div>No Card</div></div>
                    </div>
                    <div class="col-xs-2"><span class="btn btn-warning" v-on:click="updateContact(contact)">Update</span></div>
                </div>
                <div>
                    <a class="btn btn-warning" v-on:click="addContact">I'm Not Listed</a>
                </div>
            </fieldset>
        </form>
        <addcontact v-if="addingContact"></addcontact>
        <updatecontact v-if="updatingContact"></updatecontact>
    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'
    import addcontact from '../components/add-contact'
    import updatecontact from '../components/update-contact'


    export default {
        name: 'contact-list',
        data: function () {
            return {
                addingContact: false,
                updatingContact: false,
                selectedContact: null
            }
        },
        computed: {
            ...mapGetters({
                results: 'contact/results',
                currentStepNumber: 'progress/currentStepNumber'
            })
        },
        methods: {
            completeStep: function () {
                this.$store.commit('progress/completeStep', this.currentStepNumber)
            },
            addContact: function () {
                this.addingContact = true
            },
            selectContact: function (contact) {
                this.$store.commit('contact/setContact', contact)
                this.$store.commit('progress/completeStep', this.currentStepNumber)
            },
            updateContact: function (contact) {
                this.selectedContact = contact
                this.updatingContact = true
            },
            loadContacts: function () {
                this.$parent.loadContacts()
            }
        },
        components: {
            addcontact,
            updatecontact
        }
    }
</script>