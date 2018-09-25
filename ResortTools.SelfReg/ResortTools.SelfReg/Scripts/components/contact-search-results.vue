<template>
    <div>
        <form id="select-contact" v-if="!addingContact">
            <fieldset class="col-xs-12">
                <div class="page-header">
                    <h2>{{results.length}} Matches Found</h2>
                    Looks like you may have been here before.<br />
                    Select name below.
                </div>
                <div v-for="(result, index) in results" class="row contact-search-result">
                    <div class="col-xs-1">
                        <input type="radio" name="search-result" v-on:click="selectContact(result)" />
                    </div>
                    <div class="col-xs-5">
                        <h4 class="search-result-name">{{result.firstName}} {{result.lastName}}</h4>
                        <div class="search-result-hometown">
                            {{result.hometown}}
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div v-if="result.orderArrivalDate != null" class="search-result-order-date">Upcoming Order<br />Arrival Date:{{result.orderArrivalDate}}</div>
                        <div v-else class="search-result-order-date">No Upcoming Orders</div>
                    </div>
                    <div class="col-xs-2">
                        <div v-if="result.jCardNumber != null" class="search-result-card"><img src="/images/card-small-black.png"><div>{{result.jCardNumber}}</div></div>
                        <div v-else class="search-result-order-date"><img src="/images/card-small-grey.png"><div>No JCard</div></div>
                    </div>
                </div>
                <div>
                    <a class="btn btn-warning" v-on:click="addContact">I'm Not Listed</a>
                </div>
            </fieldset>
        </form>
        <addcontact v-if="addingContact"></addcontact>
    </div>
</template>

<script>

    import { mapGetters } from 'vuex'
    import store from '../vuex/self-registration-store'
    import addcontact from '../components/add-contact'


    export default {
        name: 'contact-search-results',
        data: function () {
            return {
                addingContact: false
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
            }
        },
        components: {
            addcontact
        }
    }
</script>