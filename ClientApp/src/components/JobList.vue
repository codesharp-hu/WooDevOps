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
export default {
  name: 'JobList',
  props: {
    jobs: Array
  },
  setup: function () {
    return {}
  }
}
</script>