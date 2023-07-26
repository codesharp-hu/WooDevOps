<template>
  <div class="accordion" id="projectAccordion">
    <div class="accordion-item" v-for="(project, idx) in projects" :key="idx">
      <h2 class="accordion-header" :id="'project_heading'+idx">
        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" :data-bs-target="'#project_collapse'+idx" aria-expanded="true" :aria-controls="'project_collapse'+idx">
          {{ project.name }}
        </button>
      </h2>
      <div :id="'project_collapse'+idx" class="accordion-collapse collapse" :aria-labelledby="'project_heading'+idx">
        <div class="accordion-body">
          <PipeLineList :parentIdx=idx :pipelines="project.pipelines" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue';
import PipeLineList from './PipeLineList.vue';

export default {
  name: 'ProjectList',
  components: { PipeLineList },
  props: {},
  setup: function () {
    const jobs = ref([
      {name: 'Job 1', parameters: [{name: 'Param 1', value: 'Value 1'}]},
      {name: 'Job 2', parameters: [{name: 'Param 1', value: 'Value 1'}]},
      {name: 'Job 3', parameters: [{name: 'Param 1', value: 'Value 1'}]},
    ]);
    const pipelines = ref([
      {name: 'Pipe Line 1', jobs: jobs},
      {name: 'Pipe Line 2', jobs: jobs},
      {name: 'Pipe Line 3', jobs: jobs},
    ]);
    const projects = ref([
      {name: 'Project 1', production: {}, staging: {}, pipelines: pipelines},
      {name: 'Project 2', production: {}, staging: {}, pipelines: pipelines},
      {name: 'Project 3', production: {}, staging: {}, pipelines: pipelines},
    ]);

    return { projects }
  }
}
</script>