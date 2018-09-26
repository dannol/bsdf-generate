import Vue from 'vue/dist/vue.js';
import VueRouter from 'vue-router'

import start from '../components/start'
import ordersearch from '../components/order-search'
import scancard from '../components/scan-card'
import accountsearch from '../components/account-search'
import manageGroup from '../components/manage-group'
import groupMemberList from '../components/group-member-list'
import addGroupMember from '../components/add-group-member'
import updateGroupMember from '../components/update-group-member'
import selectMembers from '../components/select-members'
import waiver from '../components/waiver'
import complete from '../components/complete'

var routesFromConfig = [];

const routes = [
    {
        path: '/ordersearch',
        name: 'orderSearch',
        component: ordersearch
    },
    {
        path: '/accountsearch',
        name: 'accountSearch',
        component: accountsearch
    },
    {
        path: '/scancard',
        name: 'scanCard',
        component: scancard
    },
    {
        path: '/manage-group',
        name: 'manageGroup',
        component: manageGroup,
        children: [{
            path: '',
            name: 'groupMemberList',
            component: groupMemberList,
            props: true
        },
        {
            path: 'add',
            name: 'addGroupMember',
            component: addGroupMember
        },
        {
            path: 'update',
            name: 'updateGroupMember',
            component: updateGroupMember,
            props: true
        }
        ]
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
        name: 'selectMembers',
        component: selectMembers
    },
    {
        path: '/complete',
        name: 'complete',
        component: complete
    },
    {
        path: '/',
        name: "start",
        component: start
    }
]

export default new VueRouter({
    routes: routes
})