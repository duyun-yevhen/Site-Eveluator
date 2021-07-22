// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import VueResource from 'vue-resource'
import router from './router'
import Vuelidate from 'vuelidate'

import App from './App'
import TestResultTables from './components/TestResultTables'
import PageTable from './components/PageTable'

Vue.use(VueResource)
Vue.use(Vuelidate)
Vue.http.options.root = 'https://webcrawler.me.com/api/CrawlerTest/'
Vue.component('TestResultTable', TestResultTables)
Vue.component('PageTable', PageTable)
Vue.config.productionTip = false
    /* eslint-disable no-new */
new Vue({
    el: '#app',
    router,
    components: { App },
    template: '<App/>'
})