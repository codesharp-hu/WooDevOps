<template>
  <div class="accordion" id="pipeLineAccordion">
    <div class="accordion-item" v-for="(job, idx) in jobs" :key="idx">
      <h2 class="accordion-header" :id="'piple_line_heading'+parentIdx+idx">
        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" :data-bs-target="'#piple_line_collapse'+parentIdx+idx" aria-expanded="true" :aria-controls="'piple_line_collapse'+parentIdx+idx">
          {{ job.name }}
        </button>
      </h2>
      <div :id="'piple_line_collapse'+parentIdx+idx" class="accordion-collapse collapse" :aria-labelledby="'piple_line_heading'+parentIdx+idx">
        <div class="accordion-body">
          <div class="input-group mb-3" v-for="param in job.parameters" :key="param.name">
            <span class="input-group-text" id="basic-addon3">{{ param.name }}</span>
            <input type="text" class="form-control" id="basic-url" aria-describedby="basic-addon3">
            <span class="input-group-text" id="basic-addon3">{{ param.value }}</span>
            <input type="text" class="form-control" id="basic-url" aria-describedby="basic-addon3">
          </div>
          <div class="text-start">
            <strong>Date:</strong> message<br />
            <strong>Date:</strong> message
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>  
export default {
  name: 'JobList',
  components: {},
  props: {
    parentIdx: String,
    jobs: Array
  },
  setup: function () {

    return {}
  }
}
</script>