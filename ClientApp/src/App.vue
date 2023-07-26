<template>
  <ProjectList />
</template>

<script>
import ProjectList from './components/ProjectList.vue'

export default {
  name: 'App',
  components: {
    ProjectList
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
  margin: 20px;
}
</style>
