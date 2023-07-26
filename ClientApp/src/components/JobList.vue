<template>
  <table class="table table-hover table-bordered">
    <tbody>
      <tr class="text-start" v-for="(job, idx) in jobs" :key="idx">
        <td @click="$emit('selectJob', job)">{{ job.name }}</td>
      </tr>
    </tbody>
  </table>
</template>

<script>  
export default {
  name: 'JobList',
  components: {},
  props: {
    jobs: Array
  },
  setup: function () {
    return {}
  }
}
</script>