﻿import Vue from 'vue/dist/vue.js';
import VueRouter from 'vue-router'

import start from '../components/start'

//Search Components
import searchByOrder from '../components/search-by-order'
import searchByPersonalInfo from '../components/search-by-personal-info'
import scanCard from '../components/scan-card'

//Contact management Components
import manageContact from '../components/manage-contact'
import contactList from '../components/contact-list'
import addContact from '../components/add-contact'
import updateContact from '../components/update-contact'

//Group Management Components
import manageGroup from '../components/manage-group'
import groupMemberList from '../components/group-member-list'
import addGroupMember from '../components/add-group-member'
import updateGroupMember from '../components/update-group-member'
import selectMembers from '../components/select-members'

//Waiver Management Components
import manageWaivers from '../components/manage-waivers'
import waivers from '../components/waivers'


import complete from '../components/complete'

var routesFromConfig = [];

const routes = [
    {
        //Start Page Route
        path: '/',
        name: "start",
        component: start
    },
    {
        //Contact Management Routes
        path: '/manage-contact',
        name: 'manageContact',
        component: manageContact,
        children: [{
            path: 'contact-list',
            name: 'contactList',
            component: contactList,
            props: true
        },
        {
            path: 'search-by-order',
            name: 'searchByOrder',
            component: searchByOrder,
            props: true
        },
        {
            path: 'scan-card',
            name: 'scanCard',
            component: scanCard,
            props: true
        },
        {
            path: 'search-by-personal-info',
            name: 'searchByPersonalInfo',
            component: searchByPersonalInfo,
            props: true
        },
        {
            path: 'add-contact',
            name: 'addContact',
            component: addContact,
            props: true
        },
        {
            path: 'update-contact',
            name: 'updateContact',
            component: updateContact,
            props: true
        }
        ]
    },
    {
        //Group Management Routes
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
        //Waiver management Routes
        path: '/manage-waivers',
        name: 'manageWaivers',
        component: manageWaivers,
        children: [{
            path: '',
            name: 'waivers',
            component: waivers,
            props: true
        }]
    },
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

]

export default new VueRouter({
    routes: routes
})