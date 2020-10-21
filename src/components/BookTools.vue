<template>
  <div>
    <router-view :agenda-book-data="agendaBookData" :abstract-book-data="abstractBookData"></router-view>
  </div>
</template>

<script>
import axios from 'axios'
import * as _ from 'lodash'
import Loader from './Loader.vue'

import { mapState } from 'vuex'

export default {
  name: 'BookTools',
  components: { Loader },
  data() {
    return {
      dataLoading: false,
      loadStatus: false,
      loadedPageNumber: null,
      oralSections: null,
      posterSections: null,
      sections: null,
      perPageCount: 25,
      totalAbstractCount: null,
      pageCount: null,
      pageData: [],
      paperCountURL: 'https://www.bsdf-assbt.org/wp-json/wp/v2/callforpapers?per_page=100',
      paperDataURL: 'https://www.bsdf-assbt.org/wp-json/wp/v2/callforpapers',
      status: null,
      meetingDates: null,
      orals: null,
      posters: null,
      oralsByDate: null,
      postersByDate: null,
      agenda: null
    }
  },
  mounted () {
    this.loadPapers()
  },
  methods: {
    createSections() {
      let sections = []
      this.oralSections.forEach((papSec) => {
        let sec = {}
        sec.sectionName = papSec.section
        sec.orals = papSec.orals
        let matchingPosterSection = this.posterSections.filter((posSec) => {
          return posSec.section === sec.sectionName
        })
        if (matchingPosterSection) {
          sec.posters = matchingPosterSection[0].posters
        }
        sections.push(sec)
      })

      this.sections = sections
      this.$set(this, 'dataLoading', false)
      this.$set(this, 'status', '2019 ABSTRACTS SUCCESSFULLY LOADED')
      this.$store.commit('setAbstractBookData', sections)
      this.$store.commit('setStatus', '2019 ABSTRACTS SUCCESSFULLY LOADED!')
    },

    createAgendaDays() {
      let days = []

      let orals = this.orals
      let posters = this.posters

      let oralsByDate = _.chain(orals)
        .sortBy([function (oral) { return oral.paper_date }])
        .groupBy("paper_date")
        .toPairs()
        .map(function(currentItem) {
            return _.fromPairs(_.zip(["date", "orals"], currentItem));
        })
        .value()

      let postersByDate = _.chain(posters)
        .sortBy([function (oral) { return oral.paper_date }])
        .groupBy("paper_date")
        .toPairs()
        .map(function(currentItem) {
            return _.fromPairs(_.zip(["date", "posters"], currentItem));
        })
        .value()

      this.oralsByDate = oralsByDate
      this.postersByDate = postersByDate

      let sortingAgenda = _.unionBy(oralsByDate, postersByDate, "date")
      _.forEach(sortingAgenda, (agendaDay) => {
        let sortedOrals = _.sortBy(agendaDay.orals, (oral) => {
          let time = oral.paper_time
          if (oral.paper_time.includes(':')) {
            let hour = parseInt(oral.paper_time.split(':')[0])
            let minute = parseInt(oral.paper_time.split(':')[1])
            if (hour < 8) {
              hour = hour + 12
            }
            time = hour.toString() + _.padStart(minute.toString(), 2, '0')
          }
          return parseInt(time)
        })
        agendaDay.orals = sortedOrals
        let sortedPosters = _.sortBy(agendaDay.posters, (poster) => {
          let time = poster.paper_time
          if (poster.paper_time.includes(':')) {
            let hour = parseInt(poster.paper_time.split(':')[0])
            let minute = parseInt(poster.paper_time.split(':')[1])
            if (hour < 8) {
              hour = hour + 12
            }
            time = hour.toString() + _.padStart(minute.toString(), 2, '0')
          }
          return parseInt(time)
        })
        agendaDay.posters = sortedPosters
      })

      this.agenda = _.sortBy(sortingAgenda, (agendaDay) => {
        return agendaDay.date.toLowerCase()
      })

      this.$store.commit('setAgendaBookData', this.agenda)
    },

    loadPapers() {
      this.$set(this, 'dataLoading', true)
      this.$store.commit('setDataLoading', true)
      this.$set(this, 'status', 'Loading Abstracts...')
      this.$store.commit('setStatus', 'LOADING ABSTRACTS...')
      axios
      .head(this.paperCountURL)
      .then((response) => {
        this.totalAbstractCount = response.headers['x-wp-total']
        this.$store.state.totalAbstractCount = this.totalAbstractCount
        this.pageCount = Math.ceil(this.totalAbstractCount / this.perPageCount)

        let dataPagePromises = []

        for (let i=1; i <= this.pageCount; i++) {
          dataPagePromises.push(this.fetchPaperDataPage(this.perPageCount, i))
        }

        Promise.all(dataPagePromises)
          .then(() => {
            this.processPaperData(this.pageData)
            this.$set(this, 'loadStatus', true)
            this.$store.commit('setLoadStatus', {
              totalAbstractCount: this.totalAbstractCount,
              sectionCount: this.sectionCount,
              oralsCount: this.oralsCount,
              postersCount: this.postersCount
            })
          })
          .error(() => {
            this.$set(this, 'dataLoading', false)
            this.$store.commit('setDataLoading', false)
          })

      })
      
    },

    fetchPaperDataPage(pageCount, pageNumber) {
      return new Promise((resolve) => {
        this.$set(this, 'dataLoading', true)
        axios.get(`${this.paperDataURL}?per_page=${pageCount}&page=${pageNumber}`)
        .then((response) => {
          let currentData = this.pageData
          let combinedData = Array.concat(this.pageData, response.data)
          this.pageData = combinedData
          this.$set(this, 'loadedPageNumber', pageNumber)
          resolve()
        })
        .finally(() => {
          this.$set(this, 'dataLoading', false)
          this.$store.commit('setDataLoading', false)
        })
      })
    },

    processPaperData(data) {
      let response = data

      let orals = response.filter((submission) => {
        return submission.acf.paper_submission_type === 'Paper'
      }).map(paper => ({ title: paper.title.rendered, ...paper.acf }))

      this.orals = orals

      let posters = response.filter((submission) => {
        return submission.acf.paper_submission_type === 'Poster'
      }).map(paper => ({ title: paper.title.rendered, ...paper.acf }))

      this.posters = posters

      orals.forEach((paper) => {
        paper.paper_authors = this.trimParagraphTags(paper.paper_authors)
        paper.paper_abstract = this.trimParagraphTags(paper.paper_abstract)
      })

      posters.forEach((poster) => {
        poster.paper_authors = this.trimParagraphTags(poster.paper_authors)
        poster.paper_abstract = this.trimParagraphTags(poster.paper_abstract)
      })

      let unsortedOralSections = _.chain(orals)
        .sortBy([function (oral) { return oral.paper_authors }])
        .groupBy("paper_section")
        .toPairs()
        .map(function(currentItem) {
            return _.fromPairs(_.zip(["section", "orals"], currentItem));
        })
        .value()

      this.oralSections = _.sortBy(unsortedOralSections, (sec) => {
        return sec.section.toLowerCase()
      })

      let unsortedPosterSections = _.chain(posters)
        .sortBy([function (poster) { return poster.paper_authors }])
        .groupBy("paper_section")
        .toPairs()
        .map(function(currentItem) {
            return _.fromPairs(_.zip(["section", "posters"], currentItem));
        })
        .value()

      this.posterSections = _.sortBy(unsortedPosterSections, (sec) => {
        return sec.section.toLowerCase()
      })

      this.createSections()
      this.createAgendaDays()
    },

    trimParagraphTags(text) {
      text = text.trim()
      if (text.startsWith('<p>')) {
        text = text.slice(3)
      }
      if (text.endsWith('</p>')) {
        let length = text.length - 4
        text = text.slice(0, length)
      }
      return text
    }
  },
  computed: {
    abstractCount () {
      return this.totalAbstractCount
    },

    oralsCount () {
      let count = 0

      this.oralSections.forEach((section) => {
        count += section.orals.length
      })

      return count
    },

    postersCount () {
      let count = 0

      this.posterSections.forEach((section) => {
        count += section.posters.length
      })

      return count
    },

    sectionCount () {
      return this.sections.length
    },

    ...mapState([
      'agendaBookData',
      'abstractBookData'
    ])
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style lang="scss" scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
  li {
    display: inline-block;
    margin: 0 10px;
  }
}
a {
  color: $blue;
}
</style>
