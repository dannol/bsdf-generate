<template>
    <div class="book-generator bx--container max-width" :class="{loading: dataLoading}">
      <loader :loading="dataLoading"></loader>
      <p>{{status}}</p>
      <ul v-if="status" class="data-stats">
        <li><strong>{{ totalAbstractCount }}</strong> Abstracts</li>
        <li><strong>{{ sectionCount }}</strong> Sections</li>
        <li><strong>{{ oralsCount }}</strong> Oral Presentations</li>
        <li><strong>{{ postersCount }}</strong> Posters</li>
      </ul>
      <ul v-if="status" class="bx--button-wrapper">
        <router-link :to="{ name: 'agendabook' }" tag="li" class="button"><span>View Agenda Book</span></router-link>
        <router-link :to="{ name: 'abstractbook' }" tag="li" class="button"><span>View Abstract Book</span></router-link>
        <router-link :to="{ name: 'presentations' }" tag="li" class="button"><span>View Presentations</span></router-link>
      </ul>
    </div>
</template>

<script>
import { mapGetters } from 'vuex'
import { mapState } from 'vuex'
import Loader from './Loader.vue'

    export default {
        components: { Loader },
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
            dataLoading: {
                type: Boolean,
                defaut: false
            },
        },
    }
</script>

<style lang="scss" scoped>
    .button{
        @extend .bx--button;
    }
    .book-generator {
        @extend .bx--container;
        @extend .add-padding;

        &.loading {
            p, ul {
                opacity: 0.25;
            }
        }
    }
    .data-stats {
        border-top: 1px solid #999;
        padding-top: 1.5rem;
        width: 100%;
        font-size: 1.25em;
    }
    ul {
        list-style-type: none;
        padding: 0;
        li {
            display: inline-block;
            margin: 0 10px;
        }
    }
</style>