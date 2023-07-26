<template>
  <table class="table table-hover border-start border-end">
    <tbody>
      <tr class="text-start" v-for="(pipeline, idx) in pipelines" :key="idx">
        <td @click="$emit('selectPipeline', pipeline)">{{ pipeline.name }}</td>
      </tr>
    </tbody>
  </table>
</template>

<script>
export default {
  name: 'PipeLineList',
  props: {
    pipelines: Array
  },
  setup: function () {
    return {}
  }
}
</script>