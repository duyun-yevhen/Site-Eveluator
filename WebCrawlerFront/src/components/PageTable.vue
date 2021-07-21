<template>
  <div class="container">
    <table class="table table-striped">
      <thead>
        <tr>
          <td scope="col" v-for="i of tableData.colomnHeaders" :key="i">
            {{ i }}
          </td>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(item, index) of page" :key="index" @click="tableClick(index)">
          <td v-for="i in item" :key="i">{{ i }}</td>
        </tr>
      </tbody>
    </table>
    <button
      class="btn btn-primary"
      :class="{ disabled: currentPage < 1 }"
      v-on:click.prevent="previousPage"
    >
      Previous Page
    </button>
    <button
      class="btn btn-primary"
      :class="{ disabled: currentPage > maxPage - 1 }"
      v-on:click.prevent="nextPage"
    >
      Next Page
    </button>
  </div>
</template>

<script>
export default {
  props: {
    tableData: {
      items: Array,
      colomnHeaders: Array
    },
    raws: {
      default: 20,
      type: Number
    }
  },
  methods: {
    tableClick (index) {
      this.$emit('tableClicked',index + this.currentPage*this.raws)
    },

    previousPage () {
      if (this.currentPage > 0) this.currentPage--
    },
    nextPage () {
      if (this.currentPage < this.maxPage - 1) this.currentPage++
    }
  },
  data () {
    return {
      currentPage: 0
    }
  },
  computed: {
    maxPage () {
      return this.tableData.items.length / this.raws
    },
    page () {
      return this.tableData.items.slice(
        this.raws * this.currentPage,
        this.raws * (this.currentPage + 1)
      )
    }
  },
  name: 'PageTable'
}
</script>
