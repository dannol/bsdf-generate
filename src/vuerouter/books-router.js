import VueRouter from 'vue-router'
import BookGenerator from '../components/BookGenerator'
import AgendaBook from '../components/AgendaBook'
import AbstractBook from '../components/AbstractBook'
import Presentations from '../components/Presentations'

const routes = [{
    path: '/',
    component: BookGenerator,
    name: 'toolshome',
    props: true
  }, {
    path: '/agendabook',
    name: 'agendabook',
    component: AgendaBook
  }, {
    path: '/abstractbook',
    name: 'abstractbook',
    component: AbstractBook
  }, {
    path: '/presentations',
    name: 'presentations',
    component: Presentations
  }]
  
export default new VueRouter
({
    routes
})