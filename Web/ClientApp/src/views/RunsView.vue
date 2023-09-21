<template>
  <section>
    <div class="accordion text-start" id="accordionPanelsStayOpenExample">    
      <div class="accordion-item" v-for="(run, idx) in runs" :key="idx">
        <h2 class="accordion-header" :id="'run-heading'+idx">
          <button class="accordion-button" type="button" data-bs-toggle="collapse" :data-bs-target="'#run-collapse'+idx" aria-expanded="true" aria-controls="panelsStayOpen-collapseThree">
            {{ run.state }} - {{ $filters.dateTimeFormat(run.date) }}
          </button>
        </h2>
        <div :id="'run-collapse'+idx" class="accordion-collapse collapse show" :aria-labelledby="'run-heading'+idx">
          <div class="accordion-body">
            <div v-for="(jobState, jIdx) in run.jobStates" :key="jIdx">
              <strong>{{ jobState.state }} - {{ $filters.dateTimeFormat(jobState.date) }}</strong><br/>
              <span v-for="(message, mIdx) in jobState.messages" :key="mIdx">{{ message }}<br/></span>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import { ref } from 'vue';
import { useRoute } from 'vue-router';
import { fetchGet } from '../web.js';

export default {
  name: 'RunsView',
  components: {},
  props: {},
  setup: function () {
    const route = useRoute();
    const runs = ref([]);

    init();

    async function init() {
      fetchGet(`/api/projects/${route.params.projectId}/runs`).then(resp => {
        resp.json().then(data => {
          runs.value = data;
        });
      });
    }

    return { runs }
  }
}
</script>