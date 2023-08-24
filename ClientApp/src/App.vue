<template>
  <NavigationBar/>
  <router-view/>
</template>

<script>
import NavigationBar from './components/NavigationBar.vue';

export default {
  name: 'App',
  components: { NavigationBar },
  props: {},
  setup () {
    return {}
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
.cursor-pointer {
  cursor: pointer;
}
.bi {
  font-size: 1.75rem;
}
.card-header {
  font-weight: bold;
}
section {
  margin: 20px
}
</style>
