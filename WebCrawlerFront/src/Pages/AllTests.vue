<template>
  <div class="AllTests">
    <div class="text-center">
      <h1 class="display-4">Site Performance Test</h1>
      <form @submit.prevent="startTest">
        <p>Enter site URL:</p>
        <input size="60" v-model.lazy="url" type="url" required name="url" />
        <input type="submit" value="Test" />
      </form>
    </div>
    <test-table :testResults="testResults" />
  </div>
</template>

<script>
export default {
  name: 'AllTests',
  created () {
    this.testsResource = this.$resource('CrawlerTests')
    this.newTest = this.$resource('CrawlerTests/NewTest')
    this.getData()
  },

  data () {
    return {
      url: '',
      testId: 0,
      testResults: [],
      testsResource: null,
      newTest: null
    }
  },
  methods: {
    startTest () {
      let url = new URL(this.url)
      console.log(url)
      if (url && (url.protocol == 'http'|| url.protocol =='https') ) {
        this.newTest.save(url).then(response => {
          console.log(response.data)
          this.getData()
        })
      }
    },
    getData () {
      this.testsResource
        .get()
        .then(response => (this.testResults = response.data.reverse()))
    }
  }
}
</script>
