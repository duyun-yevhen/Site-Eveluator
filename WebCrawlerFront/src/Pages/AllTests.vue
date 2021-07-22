<template>
  <div class="AllTests">
    <div class="text-center">
      <h1 class="display-4">Site Performance Test</h1>
      <form @submit.prevent="startTest">
        <div class="form-group">
          <p>Enter site URL:</p>
          <input
            type="url"
            placeholder="https://www.example.com/"
            :class="{ 'is-invalid': $v.url.$error }"
            v-model.lazy="url"
            @blur="$v.url.$touch()"
            name="url"
          />
          <input class="btn btn-primary" :class="{ 'disabled': $v.url.$error }" type="submit" value="Test" />
          <div class="invalid-feedback" v-if="!$v.url.url">Wrong URL format</div>
            <div class="invalid-feedback" v-if="!$v.url.required">URL is required</div>
        </div>
      </form>
    </div>
    <page-table
      :tableData="testsData"
      :raws="50"
      @tableClicked="testSelected"
    ></page-table>
  </div>
</template>

<script>
import { required, url } from 'vuelidate/lib/validators'
export default {
  name: 'AllTests',
  created () {
    this.testsResource = this.$resource('CrawlerTests')
    this.newTest = this.$resource('CrawlerTests/NewTest')
    this.getData()
  },
  validations: {
    url: {
      required,
      url
    }
  },
  data () {
    return {
      url: '',
      testResults: [],
      testsResource: null,
      newTest: null
    }
  },
  computed: {
    testsData () {
      return {
        colomnHeaders: ['URL', 'Date'],
        items: this.testResults.map(value => {
          return {
            siteUrl: value.siteUrl,
            date: value.date
          }
        })
      }
    }
  },
  methods: {
    testSelected (value) {
      if (document.getSelection().toString().length == 0) {
        this.$router.push({ path: '/test/' + this.testResults[value].id })
      }
    },
    startTest () {
      this.$v.$touch()
      if (this.$v.$invalid) {
        console.log('ERROR')
      } else {
        console.log(this.url)
        this.newTest.save(new URL(this.url)).then(response => {
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
