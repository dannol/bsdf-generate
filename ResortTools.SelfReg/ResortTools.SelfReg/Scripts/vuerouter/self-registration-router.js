import Vue from 'vue/dist/vue.js';
import VueRouter from 'vue-router'

import start from '../components/start'
import ordersearch from '../components/order-search'
import scancard from '../components/scan-card'
import accountsearch from '../components/account-search'
import group from '../components/group'
//import addMember from '../components/add-member'
import selectMembers from '../components/select-members'
import waiver from '../components/waiver'
import complete from '../components/complete'

var routesFromConfig = [];

const routes = [
    {
        path: '/ordersearch',
        name: 'OrderSearch',
        component: ordersearch
    },
    {
        path: '/accountsearch',
        name: 'AccountSearch',
        component: accountsearch
    },
    {
        path: '/scancard',
        name: 'Scan Card',
        component: scancard
    },
    {
        path: '/group',
        name: 'Group',
        component: group
    },
    {
        path: '/waivers',
        name: 'Waivers',
        component: waiver
    },
    //{
    //    path: '/addMember',
    //    name: 'AddMember',
    //    component: addMember
    //},
    {
        path: '/selectMembers',
        name: 'SelectMembers',
        component: selectMembers
    },
    {
        path: '/complete',
        name: 'Complete',
        component: complete
    },
    {
        path: '/',
        name: "Start",
        component: start
    }
]

export default new VueRouter({
    routes: routes
})