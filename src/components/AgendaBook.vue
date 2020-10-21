<template>
    <div>
        <h3>2019 Agenda Book</h3>
        <ul v-if="!dataLoading && status" class="bx--button-wrapper">
            <li class="bx--button" @click="export2Doc">
                <svg width="100pt" height="100pt" version="1.1" viewBox="0 0 100 100" xmlns="http://www.w3.org/2000/svg">
                    <g>
                        <path d="m59.922 19.137v13.332c0 0.29297 0.11328 0.57422 0.32031 0.78125 0.20703 0.20703 0.48828 0.32422 0.78125 0.32422h13.477c0.49219 0.054687 0.95703-0.22266 1.1445-0.67969s0.050781-0.98047-0.33984-1.2852l-13.434-13.277c-0.30469-0.37891-0.82422-0.50781-1.2734-0.32422-0.44922 0.18359-0.72656 0.64453-0.67578 1.1289z"/>
                        <path d="m75.891 37.168h-18.043c-0.81641 0-1.4766-0.66016-1.4766-1.4766v-17.824c0-0.81641-0.66406-1.4766-1.4805-1.4766h-30.781c-0.81641 0-1.4766 0.66016-1.4766 1.4766v64.266c0 0.81641 0.66016 1.4766 1.4766 1.4766h51.836c0.81641 0 1.4766-0.66016 1.4766-1.4766v-43.531c-0.007813-0.39453-0.17969-0.76562-0.46484-1.0352-0.28906-0.26953-0.67188-0.41406-1.0664-0.39844zm-16.059 32.203h-5.8789l-3.6719-15.328c-0.042969-0.19922-0.089844-0.40625-0.13281-0.62891-0.042969 0.23438-0.089844 0.44531-0.13281 0.62891l-3.7695 15.328h-6.1016v-0.21875l-5.8789-23.934h5.957l3.1094 15.949c0 0.19922 0.078125 0.41797 0.10938 0.67188 0-0.25391 0.10938-0.49609 0.17578-0.75l4.0078-15.871h5.7031l3.6289 16.07c0 0.14453 0 0.29688 0.089844 0.46484l0.097656-0.55078 3.0508-15.984h5.5156z"/>
                    </g>
                </svg>
                Download Document
            </li>
            <li class="bx--button clipboard-btn" data-clipboard-target="#abstract-book">
                <svg width="100pt" height="100pt" version="1.1" viewBox="0 0 100 100" xmlns="http://www.w3.org/2000/svg">
                    <path d="m46 4c-2.4688 0-4.832 0.80469-6.5 2.4688-0.96484 0.96484-1.5625 2.2109-1.9688 3.5312h-3.5312c-2.1641 0-4 1.8359-4 4v2h-4c-4.9336 0-9 4.0664-9 9v62c0 4.9336 4.0664 9 9 9h48c4.9336 0 9-4.0664 9-9v-62c0-4.9336-4.0664-9-9-9h-4v-2c0-2.1641-1.8359-4-4-4h-3.5312c-0.40625-1.3164-1.0039-2.5352-1.9688-3.5-1.6641-1.6641-4.0312-2.5-6.5-2.5zm0 6h8c1.2109 0 1.8359 0.30469 2.25 0.71875s0.74609 1.0664 0.75 2.2812c0 1.6562 1.3438 3 3 3h4v6h-28v-6h4c0.80078 0 1.5703-0.32031 2.1328-0.89062s0.875-1.3398 0.86719-2.1406c0-1.2031 0.33594-1.8359 0.75-2.25s1.0391-0.71875 2.25-0.71875zm-20 12h4v2c0 2.1641 1.8359 4 4 4h32c2.1641 0 4-1.8359 4-4v-2h4c1.7148 0 3 1.2852 3 3v62c0 1.7148-1.2852 3-3 3h-48c-1.7148 0-3-1.2852-3-3v-62c0-1.7148 1.2852-3 3-3zm20 24c-0.76562 0-1.5234 0.29297-2.125 0.875l-9.875 9.875c-0.61719 0.55078-1 1.3594-1 2.25s0.38281 1.6992 1 2.25l9.875 9.875c1.2227 1.1641 3.0781 1.1719 4.25 0s1.1719-3.0781 0-4.25l-4.875-4.875h20.75c1.6562 0 3-1.3438 3-3s-1.3438-3-3-3h-20.75l4.875-4.875c1.1719-1.1719 1.1719-3.0781 0-4.25-0.58594-0.58594-1.3594-0.875-2.125-0.875z"/>
                </svg>
                Copy to Clipboard
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
                <div class="agenda-day" v-for="(agendaDay, aIndex) in agendaBookData" :key="aIndex">
                    <div class="section" v-for="(section, sIndex) in daySections(agendaDay)" :key="sIndex">
                        <div class="section-title" style="text-align: center;font-size: 12pt;">
                            <div class="section__name" style="text-transform: uppercase;page-break-before: always;page-break-after: avoid;">
                                <strong>{{ agendaDay.date }}</strong>
                                <br/>
                                <br/>
                            </div>
                            <div v-if="agendaDay.posters && agendaDay.posters.length > 0" class="section__subheading" style="text-transform: uppercase;page-break-before: avoid;">
                                <strong>Poster Session</strong>
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
                            <tr v-for="(oral, oIndex) in agendaDay.orals" :key="oIndex">
                                <template v-if="oral.paper_section === section">
                                    <td style="text-align: left; padding-right: 1em; vertical-align: top;border-top: none !important;">{{ oral.paper_time }}</td>
                                    <td style="text-align: left; vertical-align: top; padding-bottom: 1em;border-top: none !important;">
                                        <div class="agenda-oral" style="page-break-inside: avoid;">
                                            <div class="agenda-oral--title"><strong v-html="formatTitle(oral.title)"></strong></div>
                                            <div class="agenda-oral--authors" v-html="oral.paper_authors"></div>
                                        </div>
                                    </td>
                                </template>
                            </tr>
                        </table>
                        <table class="agenda-entry" style="font-size: 10pt;border: none !important;" v-if="agendaDay.posters">
                            <tr v-for="(poster, pIndex) in agendaDay.posters" :key="pIndex">
                                <template v-if="poster.paper_section === section">
                                    <td style="text-align: left; padding-right: 1em; vertical-align: top;border-top: none !important;">{{ poster.paper_time }}</td>
                                    <td style="text-align: left; vertical-align: top; padding-bottom: 1em;border-top: none !important;">
                                        <div class="agenda-oral" style="page-break-inside: avoid;">
                                            <div class="agenda-oral--title"><strong v-html="formatTitle(poster.title)"></strong></div>
                                            <div class="agenda-oral--authors" v-html="poster.paper_authors"></div>
                                        </div>
                                    </td>
                                </template>
                            </tr>
                        </table>
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
    import ClipboardJS from 'clipboard'

    export default {
        components: { Loader },
        data() {
            return {
                elementId: 'agenda-book',
                filename: 'agenda',
            }
        },
        mounted () {
            var clipboard = new ClipboardJS('.clipboard-btn');
        },
        methods: {
            copyToClipboard () {
                new ClipboardJS('.btn', {
                    text: function() {
                        var htmlBlock = document.querySelector('#abstract-book');
                        return htmlBlock.text;
                    }
                });
                console.log('COPIED AGENDA BOOK TO CLIPBOARD')
            },
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
            export2Doc () {
                var element = this.elementId
                var filename = this.filename
                var preHtml = "<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:word' xmlns='http://www.w3.org/TR/REC-html40'><head><meta charset='utf-8'><title>Export HTML To Doc</title></head><body>";
                var postHtml = "</body></html>";
                var html = preHtml+document.getElementById(element).innerHTML+postHtml;

                var blob = new Blob(['\ufeff', html], {
                    type: 'application/msword'
                });
                
                // Specify link url
                var url = 'data:application/vnd.ms-word;charset=utf-8,' + encodeURIComponent(html);
                
                // Specify file name
                filename = filename?filename+'.doc':'document.doc';
                
                // Create download link element
                var downloadLink = document.createElement("a");

                document.body.appendChild(downloadLink);
                
                if(navigator.msSaveOrOpenBlob ){
                    navigator.msSaveOrOpenBlob(blob, filename);
                }else{
                    // Create a link to the file
                    downloadLink.href = url;
                    
                    // Setting the file name
                    downloadLink.download = filename;
                    
                    //triggering the function
                    downloadLink.click();
                }
                
                document.body.removeChild(downloadLink);
            }
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
        font-family: 'Times New Roman';
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
</style>