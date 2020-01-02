import Vue from 'vue'
import Router from 'vue-router'
import landing from '@/views/Landing.vue'

Vue.use(Router)

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'landing',
      meta: { noLayout: true },
      component: landing,
      beforeEnter: (to, from, next) => {
        if(!Vue.prototype.$auth.watch.authenticated) next();
        else next('/home');
      }
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
      path: '/home',
      name: 'home',
      component: () => import(/* webpackChunkName: "about" */ './views/Information/Home.vue')
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
    },
    {
      path: '/quizzes',
      name: 'quizzes',
      component: () => import(/* webpackChunkName: "about" */ './views/Quizzes/QuizzesList.vue')
    },
    {
      path: '/quizzes/:id',
      name: 'quizzes-edit',
      component: () => import(/* webpackChunkName: "about" */ './views/Quizzes/QuizForm.vue')
    },
    {
      path: '/quizzes/add',
      name: 'quizzes-add',
      component: () => import(/* webpackChunkName: "about" */ './views/Quizzes/QuizForm.vue')
    }
  ]
});

Vue.router = router;

export default router;
