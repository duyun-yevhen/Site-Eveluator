<template>
  <div class="TestResult">
    <div class="text-center">
      <h1 class="display-4">Site Performance Result</h1>
      <form @submit.prevent="getData">
        <p>Get Test Result by Id:</p>
        <input type="number" v-model.lazy="testId" required name="testId" />
        <input type="submit" value="View Reults" />
      </form>
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
          .get(
            'https://webcrawler.me.com/api/Crawler/GetTestResultById?testID=' +
              this.testId
          )
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
