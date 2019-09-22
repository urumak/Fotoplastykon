import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import vuetify from 'vuetify';
import 'vuetify/dist/vuetify.min.css'
import './css/app.styl'
import '@/bootstrap/axios';

Vue.use(vuetify);

Vue.config.productionTip = false

new Vue({
  router,
  store,
  vuetify: new vuetify({}),
  render: h => h(App)
}).$mount('#app')


