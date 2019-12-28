import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/views/Home.vue'

Vue.use(Router)

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      meta: { noLayout: true },
      component: Home
    },
    {
      path: '/users',
      name: 'users',
      component: () => import(/* webpackChunkName: "about" */ './views/Users/UsersList.vue')
    },
    {
      path: '/users/add',
      name: 'user-add',
      component: () => import(/* webpackChunkName: "about" */ './views/Users/UserForm.vue')
    },
    {
      path: '/users/:id',
      name: 'user-edit',
      component: () => import(/* webpackChunkName: "about" */ './views/Users/UserForm.vue')
    },
    {
      path: '/information',
      name: 'information',
      component: () => import(/* webpackChunkName: "about" */ './views/Information/MainPage.vue')
    },
    {
      path: '/information/add',
      name: 'information-add',
      component: () => import(/* webpackChunkName: "about" */ './views/Information/InformationForm.vue')
    },
    {
      path: '/information/:id',
      name: 'information-edit',
      component: () => import(/* webpackChunkName: "about" */ './views/Information/InformationForm.vue')
    },
    {
      path: '/information/list',
      name: 'information-list',
      component: () => import(/* webpackChunkName: "about" */ './views/Information/InformationList.vue')
    },
    {
      path: '/films',
      name: 'films',
      component: () => import(/* webpackChunkName: "about" */ './views/Films/FilmsList.vue')
    },
    {
      path: '/films/add',
      name: 'film-add',
      component: () => import(/* webpackChunkName: "about" */ './views/Films/FilmForm.vue')
    },
    {
      path: '/films/:id',
      name: 'film-edit',
      component: () => import(/* webpackChunkName: "about" */ './views/Films/FilmForm.vue')
    },
    {
      path: '/film-people',
      name: 'film-people',
      component: () => import(/* webpackChunkName: "about" */ './views/FilmPeople/FilmPeopleList.vue')
    },
    {
      path: '/film-person/add',
      name: 'film-person-add',
      component: () => import(/* webpackChunkName: "about" */ './views/FilmPeople/FilmPersonForm.vue')
    },
    {
      path: '/film-person/:id',
      name: 'film-person-edit',
      component: () => import(/* webpackChunkName: "about" */ './views/FilmPeople/FilmPersonForm.vue')
    }
  ]
});

Vue.router = router;

export default router;
