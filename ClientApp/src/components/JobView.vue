<template>
  <table class="table border-start border-end">
    <tbody>
      <tr>
        <td>
          <div class="d-flex justify-content-end align-items-center">
            <span>State</span>
            <span class="bi bi-play-fill cursor-pointer text-success" @click="start"></span>
          </div>
          <div class="input-group mt-2" v-for="(param, idx) in job.parameters" :key="idx">
            <span class="input-group-text">{{ param.name }}</span>
            <input type="text" class="form-control">
            <span class="input-group-text">{{ param.value }}</span>
            <input type="text" class="form-control">
          </div>
        </td>
      </tr>
      <tr class="text-start">
        <td>
          <strong>Date:</strong> message<br />
          <strong>Date:</strong> message
        </td>
      </tr>
    </tbody>
  </table>
</template>

<script>  
export default {
  name: 'JobList',
  components: {},
  props: {
    job: Object
  },
  setup: function () {
    function start() {
      console.log('start');
    }
    return { start }
  }
}
</script>