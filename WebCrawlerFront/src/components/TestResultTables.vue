<template>
  <div class="text-center mt-5">
    <div class="" v-if="testData != null">
      <br />Performanse <br />Test ID: {{ testData.id }} <br />Date:
      {{ testData.date }} <br />URL: {{ testData.siteUrl }} <br />Total links:
      {{ testData.urlTestResults ? testData.urlTestResults.length : 0 }}
      <page-table :tableData="SitemapOnly"></page-table>
      <page-table :tableData="SitePageOnly"></page-table>
      <page-table :tableData="SortedTime"></page-table>
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
      return {
        colomnHeaders: ['URL', 'inSitemap', 'inSitePage', 'Response Time'],
        items: this.testData.urlTestResults
          .sort((a, b) => {
            return a.responseTime - b.responseTime
          })
          .map(value => {
            return {
              url: value.url,
              inSitemap: value.inSitemap,
              inSitePage: value.inSitePage,
              responseTime: value.responseTime
            }
          })
      }
    },
    SitemapOnly () {
      return {
        colomnHeaders: ['URL'],
        items: this.SortedTime.items.filter(
          value => !value.inSitemap && value.inSitePage
        ).map(value => {
          return {
            url: value.url
          }
        })
      }
    },
    SitePageOnly () {
      return {
        colomnHeaders: ['URL'],
        items: this.SortedTime.items.filter(
          value => value.inSitemap && !value.inSitePage
        ).map(value => {
          return {
            url: value.url
          }
        })
      }
    }
  }
}
</script>
