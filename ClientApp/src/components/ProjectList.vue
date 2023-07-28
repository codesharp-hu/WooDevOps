<template>
  <div class="row">
    <div class="col-md-4 col-sm-6" v-for="(project, idx) in projects" :key="idx">
      <div class="card text-start">
        <div class="card-header">
          {{ project.name }}
        </div>
        <div class="card-body">
          <p class="card-text">Pipelines: {{ project.pipelines.length }}</p>
          <p class="card-text">Jobs: {{ jobLenght }}</p>
          <p class="card-text">Runs: {{ runsLenght }}</p>
          <a class="btn btn-primary" @click="$emit('selectProject', project)">Pipelines</a>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue';

export default {
  name: 'ProjectList',
  emits: ['selectProject'],
  props: {
    projects: Array
  },
  setup(props) {
    const jobLenght = ref(0);
    const runsLenght = ref(0);

    props.projects.forEach(project => {
      project.pipelines.forEach(pipeline => {
        jobLenght.value += pipeline.jobs.length;
        runsLenght.value += pipeline.runs.length;
      });
    });
    
    return { jobLenght, runsLenght }
  }
}
</script>