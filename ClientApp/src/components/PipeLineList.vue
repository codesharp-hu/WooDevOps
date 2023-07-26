<template>
  <div class="accordion" id="pipeLineAccordion">
    <div class="accordion-item" v-for="(pipeline, idx) in pipelines" :key="idx">
      <h2 class="accordion-header" :id="'piple_line_heading'+parentIdx+idx">
        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" :data-bs-target="'#piple_line_collapse'+parentIdx+idx" aria-expanded="true" :aria-controls="'piple_line_collapse'+parentIdx+idx">
          {{ pipeline.name }}
        </button>
      </h2>
      <div :id="'piple_line_collapse'+parentIdx+idx" class="accordion-collapse collapse" :aria-labelledby="'piple_line_heading'+parentIdx+idx">
        <div class="accordion-body">
          <JobList :parentIdx="parentIdx + '' + idx" :jobs="pipeline.jobs" />
        </div>
      </div>
    </div>
  </div>
</template>

<script> 
import JobList from './JobList.vue';

export default {
  name: 'PipeLineList',
  components: { JobList },
  props: {
    parentIdx: Number,
    pipelines: Array
  },
  setup: function () {

    return {}
  }
}
</script>