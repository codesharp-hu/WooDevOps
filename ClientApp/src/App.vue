<template>
  <ul class="nav nav-tabs">
    <li class="nav-item" v-for="(route, idx) in routes" :key="idx">
      <a class="nav-link cursor-pointer" :class="{'active': idx == routes.length-1}" aria-current="page" @click="navigate(idx)">{{ route }}</a>
    </li>
  </ul>
  <ProjectList v-if="routes.length == 1" :projects="projects" @selectProject="selectProject" />
  <PipelineList v-if="routes.length == 2" :pipelines="selectedProject.pipelines" @selectPipeline="selectPipeline" />
  <JobList v-if="routes.length == 3" :jobs="selectedPipeline.jobs" @selectJob="selectJob" />
  <JobView v-if="routes.length == 4" :job="selectedJob" />
</template>

<script>
import { ref } from 'vue';
import ProjectList from './components/ProjectList.vue';
import PipelineList from './components/PipelineList.vue';
import JobList from './components/JobList.vue';
import JobView from './components/JobView.vue';
import { fetchGet } from './web.js';

export default {
  name: 'App',
  components: { ProjectList, PipelineList, JobList, JobView },
  props: {},
  setup: function () {
    const routes = ref(['Projects']);
    const selectedProject = ref(null);
    const selectedPipeline = ref(null);
    const selectedJob = ref(null);
    const projects = ref([]);

    init();

    async function init() {
      fetchGet('/api/projects').then(resp => {
        resp.json().then(data => {
          projects.value = data;
        });
      });
    }

    function navigate(idx) {
      routes.value = routes.value.slice(0, idx+1);
    }
    function selectProject(project) {
      selectedProject.value = project;
      routes.value.push(project.name)
    }
    function selectPipeline(pipeline) {
      selectedPipeline.value = pipeline;
      routes.value.push(pipeline.name)
    }
    function selectJob(job) {
      selectedJob.value = job;
      routes.value.push(job.name)
    }

    return { 
      projects, routes, selectedProject, selectedPipeline, selectedJob,
      navigate, selectProject, selectPipeline, selectJob
    }
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
.cursor-pointer {
  cursor: pointer;
}
.bi {
  font-size: 1.75rem;
}
</style>
