<template>
  <div class="text-center mt-5">
    <div class="" v-if="testData != null">
      <br />Performanse <br />Test ID: {{ testData.id }} <br />Date:
      {{ testData.date }} <br />URL: {{ testData.siteUrl }} <br />Total links:
      {{ testData.urlTestResults ? testData.urlTestResults.length : 0 }}

      <table class="table table-striped">
        <thead>
          <tr>
            <th scope="col">URL</th>
            <th scope="col">inSitemap</th>
            <th scope="col">inSitePage</th>
            <th scope="col">responseTime</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item of SortedTime" :key="item.id">
            <th scope="row">{{ item.url }}</th>
            <td>{{ item.inSitemap }}</td>
            <td>{{ item.inSitePage }}</td>
            <td>{{ item.responseTime }}</td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="" v-else>
      Test not found
    </div>
  </div>
</template>
<script>
export default {
  name: 'TestResultTable',
  props: {
    testData: {
      id: Number,
      date: Date,
      siteUrl: URL,
      urlTestResults: []
    }
  },
  computed: {
    SortedTime () {
      return this.testData.urlTestResults.sort((a,b) => {
        return a.responseTime - b.responseTime
      })
    },
    SitemapOnly () {
      return this.testData.urlTestResults.filter(url => {
        return url.inSitemap&&!url.inSitePage
      })
    },
    SitePageOnly () {
     return this.testData.urlTestResults.filter(url => {
        return !url.inSitemap&&url.inSitePage
      })
    }
  }
}
</script>
