<template>
  <table class="table table-hover border-start border-end">
    <tbody>
      <tr class="text-start" v-for="(project, idx) in projects" :key="idx">
        <td @click="$emit('selectProject', project)">{{ project.name }}</td>
      </tr>
    </tbody>
  </table>
</template>

<script>
export default {
  name: 'ProjectList',
  props: {
    projects: Array
  },
  setup: function () {
    return {}
  }
}
</script>