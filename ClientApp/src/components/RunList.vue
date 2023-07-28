<template>
  <div class="accordion text-start" id="accordionPanelsStayOpenExample">
    <div class="accordion-item" v-for="(run, idx) in runs" :key="idx">
      <h2 class="accordion-header" :id="'run-heading'+idx">
        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" :data-bs-target="'#run-collapse'+idx" aria-expanded="false" aria-controls="panelsStayOpen-collapseThree">
          {{ run.state }} - {{ $filters.dateTimeFormat(run.date) }}
        </button>
      </h2>
      <div :id="'run-collapse'+idx" class="accordion-collapse collapse" :aria-labelledby="'run-heading'+idx">
        <div class="accordion-body">
          <div v-for="(jobState, jIdx) in run.jobStates" :key="jIdx">
            <strong>{{ jobState.state }} - {{ $filters.dateTimeFormat(jobState.date) }}</strong><br/>
            <span v-for="(message, mIdx) in jobState.messages" :key="mIdx">{{ message }}<br/></span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>  
export default {
  name: 'RunList',
  props: {
    runs: Array
  },
  setup: function () {
    return {}
  }
}
</script>