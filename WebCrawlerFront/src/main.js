// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import VueResource from 'vue-resource'
import router from './router'
import Vuelidate from 'vuelidate'
import TestTable from './components/TestTable'
import TestResultTable from './components/TestResultTable'

Vue.use(VueResource)
Vue.use(Vuelidate)
Vue.http.options.root = 'https://webcrawler.me.com/api/Crawler'
Vue.component('TestTable', TestTable)
Vue.component('TestResultTable', TestResultTable)
Vue.config.productionTip = false
    /* eslint-disable no-new */
new Vue({
    el: '#app',
    router,
    components: { App },
    template: '<App/>'
})