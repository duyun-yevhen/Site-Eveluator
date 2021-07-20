<template>
  <div class="TestResult">
    <div class="text-center">
      <h1 class="display-4">Site Performance Result</h1>
      <br />
      <form @submit.prevent="$router.push('/')">
        <input type="submit" value="Back to Tests" />
      </form>
    </div>
    <test-result-table v-if="testId > 0" :testData="testData" />
  </div>
</template>
<script>
export default {
  name: 'TestResult',
  beforeMount () {
    this.getData()
  },
  methods: {
    getData () {
      if (this.testId > 0) {
        this.$http
          .get('https://webcrawler.me.com/api/CrawlerTests/' + this.testId)
          .then(response => {
            this.testData = response.data
          })
          .catch(() => (this.testData = null))
      }
    }
  },
  data () {
    return {
      testId: this.$route.params['testId'],
      testData: null
    }
  }
}
</script>
