<template>
  <section>
    <nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <li class="breadcrumb-item cursor-pointer text-primary" :class="{'active text-secondary': idx == routes.length-1}" v-for="(route, idx) in routes" :key="idx">
          <a @click="navigate(idx)">{{ route }}</a>
        </li>
      </ol>
    </nav>
    <ProjectList v-if="routes.length == 1 && projects.length > 0" :projects="projects" @selectProject="selectProject" @run="runProject" />
    <PipelineList v-if="routes.length == 2" :pipelines="selectedProject.pipelines" @selectJobs="selectJobs" />
    <JobList v-if="routes.length == 3 && routes[2].includes('Jobs')" :jobs="selectedPipeline.jobs" />
  </section>
</template>

<script>
import { ref } from 'vue';
import ProjectList from '../components/ProjectList.vue';
import PipelineList from '../components/PipelineList.vue';
import router from '@/router';
import JobList from '../components/JobList.vue';
import { fetchGet, fetchPost } from '../web.js';

export default {
  name: 'HomeView',
  components: { ProjectList, PipelineList, JobList },
  props: {},
  setup: function () {
    const routes = ref(['Projects']);
    const selectedProject = ref(null);
    const selectedPipeline = ref(null);
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
    function selectJobs(pipeline) {
      selectedPipeline.value = pipeline;
      routes.value.push(`${pipeline.name} Jobs`)
    }
    function runProject(project) {
      fetchPost(`/api/projects/${project.id}/start`);
      router.push(`${project.id}/runs`);
    }

    return { 
      projects, routes, selectedProject, selectedPipeline, runProject,
      navigate, selectProject, selectJobs
    }
  }
}
</script>