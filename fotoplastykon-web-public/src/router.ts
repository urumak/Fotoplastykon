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
      path: '/home',
      name: 'home',
      component: () => import(/* webpackChunkName: "about" */ './views/Information/Home.vue')
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
      path: '/forum',
      name: 'forum',
      component: () => import(/* webpackChunkName: "about" */ './views/Forum/ForumList.vue')
    },
    {
      path: '/forum/:id',
      name: 'forum-thread',
      component: () => import(/* webpackChunkName: "about" */ './views/Forum/ForumThread.vue')
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
