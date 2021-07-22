<template>
  <div class="TestResult">
    <div class="text-center">
      <h1 class="display-4">Site Performance Result</h1>
      <br />
      <form @submit.prevent="$router.push('/')">
        <input class="btn btn-primary" type="submit" value="Back to Tests" />
      </form>
    </div>
    <test-result-table v-if="testId > 0" :testData="testData" />
  </div>
</template>
<script>
export default {
  name: 'TestResult',
   data () {
    return {
      resource: null,
      testId: this.$route.params['testId'],
      testData: null
    }
  },
  created () {
     this.resource = this.$resource('CrawlerTests{/id}')
     this.getData()
  },
  methods: {
    getData () {
      if (this.testId > 0) {
        this.resource
          .get({id:this.testId})
          .then(response => {
            this.testData = response.data
          })
          .catch(() => (this.testData = null))
      }
    }
  }
}
</script>
