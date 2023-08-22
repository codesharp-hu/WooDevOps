<template>
  <div class="row">
    <div class="col-md-4 col-sm-6" v-for="(pipeline, idx) in pipelines" :key="idx">
      <div class="card text-start">
        <div class="card-header">
          {{ pipeline.name }}
        </div>
        <div class="card-body">
          <p class="card-text">Jobs: {{ pipeline.jobs.length }}</p>
          <p class="card-text">Runs: {{ pipeline.runs.length }}</p>
          <a class="btn btn-primary me-2" @click="$emit('selectJobs', pipeline)">Jobs</a>
          <a class="btn btn-primary me-2" @click="$emit('selectRuns', pipeline)">Runs</a>
          <a class="btn btn-primary" @click="start(pipeline)">Run</a>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { fetchPost } from '../web.js';

export default {
  name: 'PipelineList',
  emits: ['selectJobs', 'selectRuns'],
  props: {
    pipelines: Array
  },
  setup: function () {
    function start(pipeline) {
      console.log('pipeline: ', pipeline);
      fetchPost('/api/pipelines/start', pipeline);
    }

    return { start }
  }
}
</script>