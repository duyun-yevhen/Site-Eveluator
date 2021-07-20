<template>
  <div class="AllTests">
    <div class="text-center">
      <h1 class="display-4">Site Performance Test</h1>
      <form @submit.prevent="startTest">
        <p>Enter site URL:</p>
        <input size="60" v-model.lazy="url" type="url" required name="url" />
        <input type="submit" value="Test" />
      </form>
      <br />
      <form @submit.prevent="$router.push({ path: '/test/' + testId })">
        <p>Get Test Result by Id:</p>
        <input type="number" v-model.lazy="testId" required name="testId" />
        <input type="submit" value="View Reults" />
      </form>
    </div>
    <test-table :testResults="testResults" />
  </div>
</template>

<script>
export default {
  name: 'AllTests',
  beforeMount () {
    this.getData()
  },

  data () {
    return {
      url: '',
      testId: 0,
      testResults: []
    }
  },
  methods: {
    startTest () {
      let url = new URL(this.url)
      if (url) {
        this.$http
          .get(
            'https://webcrawler.me.com/api/Crawler/GetPerformance?url=' + url
          )
          .then(response => {
            console.log(response.data)
            this.getData()
          })
      }
    },
    getData () {
      this.$http
        .get('https://webcrawler.me.com/api/Crawler/GetAllTestsResult')
        .then(response => (this.testResults = response.data.reverse()))
    }
  }
}
</script>
