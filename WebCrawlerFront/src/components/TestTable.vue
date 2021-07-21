<template>
  <div class="text-center mt-5">
    <div class="display-4">Tests:</div>
    <table class="table table-striped">
      <thead>
        <tr>
          <th scope="col">Date</th>
          <th scope="col">Site URL</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in testResults" :key="item.id">
          <td>{{ item.siteUrl }}</td>
          <td>{{ item.date }}</td>
          <td>
            <router-link :to="{ path: '/test/' + item.id }"
              ><button type="button" class="btn btn-primary text-nowrap">
                Go to Test Results
              </button>
            </router-link>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import PageTable from './PageTable.vue'
export default {
  components: { PageTable },
  props: {
    testResults: []
  },

  name: 'TestsTable'
}
</script>
