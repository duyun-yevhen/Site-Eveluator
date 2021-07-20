<template>
  <div class="text-center mt-5">
    <div class="display-4">Tests:</div>
    <table class="table table-striped">
      <thead>
        <tr>
          <th scope="col">Id</th>
          <th scope="col">Date</th>
          <th scope="col">Site URL</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in testResults" :key="item.id">
          <th scope="row">{{ item.id }}</th>
          <td>{{ item.siteUrl }}</td>
          <td>{{ item.date }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
export default {
  props: {
    testResults: []
  },

  name: 'TestsTable'
}
</script>
