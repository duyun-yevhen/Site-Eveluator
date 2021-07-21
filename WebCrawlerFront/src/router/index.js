import Vue from 'vue'
import Router from 'vue-router'

import AllTests from '@/Pages/AllTests'
import TestResult from '@/Pages/TestResult'

Vue.use(Router)

export default new Router({
    routes: [{
            path: '/',
            name: 'AllTests',
            component: AllTests
        },
        {
            path: '/test/',
            name: 'TestResult',
            component: TestResult
        },
        {
            path: '/test/:testId',
            name: 'TestResult',
            component: TestResult
        }
    ],
    mode: 'history'
})