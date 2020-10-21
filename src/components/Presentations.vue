<template>
    <div>
        <h3>2019 Presentations</h3>
        <ul v-if="!dataLoading && status" class="bx--button-wrapper">
            <li class="bx--button" :class="{active: selectedDate === agendaDay.date}" @click="selectedDate = agendaDay.date" v-for="(agendaDay, aIndex) in agendaBookData" :key="aIndex">
                {{ agendaDay.date }}
            </li>
            <router-link :to="{ name: 'toolshome' }" tag="li" class="bx--button">
                <svg width="100pt" height="100pt" version="1.1" viewBox="0 0 100 100" xmlns="http://www.w3.org/2000/svg">
                    <path d="m30.555 25.777h-7.7773l-8.332 9.668c-0.33203 0.33203-0.55469 0.66797-0.89062 1-0.10938 0.10938-0.22266 0.22266-0.33203 0.44531-0.89062 1-1.668 1.8906-2.5547 2.8906 0.89062 1 1.7773 2 2.5547 3.1094 0.33203 0.44531 0.66797 0.89062 1.1094 1.2227h-0.10938l5.668 6.5547c0.89062 1 1.668 2 2.5547 3l0.33203 0.33203h7.7773l-9.5547-11.109-2.2227-2.5547 2.2227 2.5547c17.891 0.10938 35.891 0.10938 53.891 0.22266 2.5547 0.10938 4.668 1.2227 6.2227 2.7773 1.5547 1.5547 2.4453 3.668 2.4453 5.7773v0.44531c-0.10938 1.7773-0.89062 3.5547-2.2227 5-1.2227 1.332-3 2.4453-5.1094 2.7773h-29.109v5.8906h29.555 0.44531c3.4453-0.66797 6.4453-2.332 8.5547-4.668 2.2227-2.332 3.5547-5.4453 3.7773-8.668v-0.77734c0-3.7773-1.5547-7.2227-4.1094-9.8906-2.5547-2.668-6.1094-4.4453-10.223-4.5547h-0.21875c-18-0.10938-35.891-0.10938-53.891-0.22266l9.5469-11.223"/>
                </svg>
                Back to Tools
            </router-link>
        </ul>
        <div id="book-wrapper" :class="{loading: dataLoading}">
            <div v-if="agendaBookData" id="agenda-book">
                <div class="agenda-day" v-if="agendaDay.date === selectedDate" v-for="(agendaDay, aIndex) in agendaBookData" :key="aIndex">
                    <div class="section" v-if="agendaDay.orals && agendaDay.orals.length > 0" v-for="(section, sIndex) in daySections(agendaDay)" :key="sIndex">
                        <div class="section-title" style="text-align: center;font-size: 12pt;">
                            <div class="section__name" style="text-transform: uppercase;page-break-before: always;page-break-after: avoid;">
                                <strong>{{ agendaDay.date }}</strong>
                                <br/>
                                <br/>
                            </div>
                            <div class="section__subheading" style="page-break-before: avoid;">
                                <strong>{{ section }}</strong>
                                <br/>
                                <br/>
                            </div>
                        </div>
                        <table class="agenda-entry" style="font-size: 10pt;border:none !important;" v-if="agendaDay.orals">
                            <tr v-for="(oral, oIndex) in agendaDay.orals" :key="oIndex" :class="{ 'no-presentation': !oral.paper_presentation_file }">
                                <template v-if="oral.paper_section === section">
                                    <td style="text-align: left; padding-right: 1em; vertical-align: top;border-top: none !important;">{{ oral.paper_time }}</td>
                                    <td style="text-align: left; vertical-align: top; padding-bottom: 1em;border-top: none !important;">
                                        <div class="agenda-oral" style="page-break-inside: avoid;">
                                            <div class="agenda-oral--title"><strong v-html="formatTitle(oral.title)"></strong></div>
                                            <div class="agenda-oral--authors" v-html="oral.paper_authors"></div>
                                            <div class="agenda-oral--presentation">
                                                <div class="agenda-oral--presentation-link" v-if="oral.paper_presentation_file">
                                                    <span><strong>Presentation: </strong></span>
                                                    <a :href="oral.paper_presentation_file.url" target="_blank">{{ oral.paper_presentation_file.filename }}</a>
                                                </div>
                                                <div v-else><strong>No Presentation File</strong></div>
                                            </div>
                                        </div>
                                    </td>   
                                </template>
                            </tr>
                        </table>
                    </div>
                    <div class="no-orals-message" v-else>
                        <h3>No Oral Presentations On This Date</h3>
                    </div>
                </div>
            </div>
            <div id="no-book-data" v-else>
                <loader :loading="dataLoading && !agendaBookData"></loader>
                <div class="message" v-if="dataLoading">Loading book data...</div>
            </div>
        </div>
    </div>
</template>

<script>
    import { mapState } from 'vuex'
    import Loader from './Loader.vue'

    export default {
        components: { Loader },
        data() {
            return {
                selectedDate: null
            }
        },
        mounted () {
            if (this.agendaBookData) {
                this.selectedDate = this.agendaBookData[0].date
            }
        },
        watch: {
            agendaBookData (toValue) {
                if (toValue) {
                    this.selectedDate = this.agendaBookData[0].date
                }
            }
        },
        methods: {
            daySections (agendaDay) {
                const key = 'paper_section'

                let oralSections = agendaDay.orals.reduce(function(carry, item) {
                    if (item[key] && !~carry.indexOf(item[key])) carry.push(item[key])
                    return carry
                }, [])

                let posterSections = agendaDay.posters.reduce(function(carry, item) {
                    if (item[key] && !~carry.indexOf(item[key])) carry.push(item[key])
                    return carry
                }, [])

                return _.union(oralSections, posterSections)
            },
            formatTitle (titleHtml) {
                let title = titleHtml
                if (!titleHtml.endsWith('.')) {
                    title += '.'
                }
                return title
            },
            formatHTML (html) {
                let newHTML = html
                return newHTML
            },
        },
        computed: {
            ...mapState([
                'dataLoading',
                'totalAbstractCount',
                'sectionCount',
                'oralsCount',
                'postersCount',
                'status'
            ])
        },
        props: {
            agendaBookData: {
                type: Object,
                default: null
            },
        },
    }
</script>

<style lang="scss" scoped>
    #book-wrapper {
        position: relative;
        width: 60%;
        margin: 3rem auto;
        border: 2px solid #ccc;
        padding: 4rem;
        box-shadow: 10px 10px 0px 0px rgba(0,0,0,0.1);
        max-height: 600px;
        overflow-y: scroll;
    }
    .agenda-entry {
        text-align: left;
        vertical-align: top;
        border: none;

        td {
            border-top: none !important;
        }
    }
    .abstracts-wrapper {
        text-align: justify;
    }
    .section {
        font-family: inherit;
        font-size: 10pt;
    }
    .section-title {
        font-weight: bold;
    }
    .abstract__title {
        font-weight: bold;
    }
    .abstract__authors:after {
        content: '. ';
    }
    #no-book-data {
        border: 1px solid rgba(0,0,0,0.1);
        background: #f2f2f2;
        position: relative;
        display: flex;
        justify-content: center;
        align-items: center;
        height: 300px;
        max-width: 700px;
        margin: 50px auto;
        .loading & {
            opacity: 0.35;
        }
        .message {
            font-size: 1.5rem;
            font-weight: bold;
        }
    }
    .bx--button svg {
        height: 45px;
        width: auto;
        position: relative;
        left: -0.5rem;
        fill: #295699;
    }
    .no-orals-message {
        display: flex;
        margin: 0 auto;
        background: #f2f2f2;
        border: 1px solid #ccc;
        width: 80%;
        height: 250px;
        justify-content: center;
        align-items: center;
        display: none;
        &:first-child {
            display: flex;
        }
    }
    .no-presentation {
        opacity: 0.5;
    }
    .agenda-oral--presentation-link {
        font-size: 1.25em;
        padding-top: 0.5em;
    }
</style>