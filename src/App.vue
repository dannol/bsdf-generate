<template>
  <div id="app">
    <h1>ASSBT Meeting Tools</h1>
    <book-tools></book-tools>
  </div>
</template>

<script>

import BookTools from './components/BookTools.vue'

export default {
  name: 'app',
  components: {
    BookTools
  },
}
</script>

<style lang="scss">
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;

  h1 {
    margin-bottom: 1rem;
  }
}
</style>
