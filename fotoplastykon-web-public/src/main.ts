import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import 'vuetify/dist/vuetify.min.css'
import './css/app.styl'
import '@/bootstrap/axios';
import '@/notifications-hub'
import vuetify from '@/plugins/vuetify';
import Default from './layouts/Default.vue';

Vue.component('default-layout', Default);

Vue.use(require('@websanova/vue-auth'), {
  http: require('@websanova/vue-auth/drivers/http/axios.1.x.js'),
  router: require('@websanova/vue-auth/drivers/router/vue-router.2.x.js'),
  authRedirect: {path: '/auth/login'},
  forbiddenRedirect: {path: '/error/403'},
  notFoundRedirect: {path: '/error/404'},
  loginData: {url: 'auth/login', method: 'POST', redirect: '/home', fetchUser: true},
  logoutData: {url: 'auth/logout', method: 'POST', redirect: '/', makeRequest: false},
  fetchData: {url: 'auth/user', method: 'GET', enabled: true},
  refreshData: {url: 'auth/refresh', method: 'GET', enabled: true, interval: 30},
  auth: {
    request: function (req: any, token: any)
    {
      (this as any).options.http._setHeaders.call(this, req, {Authorization: 'Bearer ' + token});
    },
    response: function (res: any)
    {
      if (res && res.request && res.request.responseURL && res.request.responseURL.indexOf('/auth/') > 0)
      {
        return (res.data || {}).token;
      }
      return null;
    }
  },
  parseUserData: function (data: any)
  {
    return data;
  }
});

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app');


