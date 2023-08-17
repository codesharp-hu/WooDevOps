<template>
  <div class="accordion" id="accordionPanelsStayOpenExample">
    <div class="accordion-item" v-for="(job, idx) in jobs" :key="idx">
      <h2 class="accordion-header" :id="'job-heading'+idx">
        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" :data-bs-target="'#job-collapse'+idx" aria-expanded="false" aria-controls="panelsStayOpen-collapseThree">
          {{ job.name }}
        </button>
      </h2>
      <div :id="'job-collapse'+idx" class="accordion-collapse collapse" :aria-labelledby="'job-heading'+idx">
        <div class="accordion-body">
          <div class="d-flex justify-content-end align-items-center">
            <span class="bi bi-play-fill cursor-pointer text-success" @click="start(job)"></span>
          </div>
          <div class="input-group mt-2" v-for="(param, idx) in job.parameters" :key="idx">
            <span class="input-group-text">{{ param.name }}</span>
            <input type="text" class="form-control" :value="param.value" readonly>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>  
import { fetchPost } from '../web.js';

export default {
  name: 'JobList',
  props: {
    jobs: Array
  },
  setup: function () {
    function start(job) {
      console.log('job: ', job)
      fetchPost('/api/jobs/start', { job: job })
    }
    
    return { start }
  }
}
</script>