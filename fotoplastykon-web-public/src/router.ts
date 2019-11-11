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
      path: '/information',
      name: 'information',
      component: () => import(/* webpackChunkName: "about" */ './views/Information/MainPage.vue')
    },
    {
      path: '/information/:id',
      name: 'information-details',
      component: () => import(/* webpackChunkName: "about" */ './views/Information/InformationDetails.vue')
    },
    {
      path: '/information/list',
      name: 'information-list',
      component: () => import(/* webpackChunkName: "about" */ './views/Information/List.vue')
    },
    {
      path: '/login',
      name: 'login',
      component: () => import(/* webpackChunkName: "about" */ './views/Auth/Login.vue')
    },
    {
      path: '/register',
      name: 'register',
      component: () => import(/* webpackChunkName: "about" */ './views/Auth/Register.vue')
    },
    {
      path: '/films/:id',
      name: 'film-page',
      component: () => import(/* webpackChunkName: "about" */ './views/Films/FilmPage.vue')
    },
    {
      path: '/film-people/:id',
      name: 'film-person-page',
      component: () => import(/* webpackChunkName: "about" */ './views/FilmPeople/FilmPersonPage.vue')
    },
    {
      path: '/users/:id',
      name: 'user-page',
      component: () => import(/* webpackChunkName: "about" */ './views/Users/UserPage.vue')
    },
    {
      path: '/forum',
      name: 'forum',
      component: () => import(/* webpackChunkName: "about" */ './views/Forum/ForumList.vue')
    },
    {
      path: '/forum/:id',
      name: 'forum-thread',
      component: () => import(/* webpackChunkName: "about" */ './views/Forum/ForumThread.vue')
    },
  ]
});

Vue.router = router;

export default router;
