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
      path: '/profile',
      name: 'user-profile',
      component: () => import(/* webpackChunkName: "about" */ './views/Users/UserProfile.vue')
    },
    {
      path: '/quizzes',
      name: 'quizzes',
      component: () => import(/* webpackChunkName: "about" */ './views/Quizzes/QuizzesList.vue')
    },
    {
      path: '/quiz/:id',
      name: 'quiz',
      component: () => import(/* webpackChunkName: "about" */ './views/Quizzes/Quiz.vue')
    }
  ]
});

Vue.router = router;

export default router;