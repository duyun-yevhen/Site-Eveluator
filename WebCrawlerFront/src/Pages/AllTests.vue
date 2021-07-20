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
            'https://webcrawler.me.com/api/CrawlerTests/NewTest/' + url
          )
          .then(response => {
            console.log(response.data)
            this.getData()
          })
      }
    },
    getData () {
      this.$http
        .get('https://webcrawler.me.com/api/CrawlerTests/')
        .then(response => (this.testResults = response.data.reverse()))
    }
  }
}
</script>
