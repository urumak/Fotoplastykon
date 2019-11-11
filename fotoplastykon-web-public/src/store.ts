import Vue from 'vue'
import Vuex from 'vuex'
import pager from "@/store/pager";

Vue.use(Vuex);

const store = new Vuex.Store({
  strict: false,
  modules: {
    information: pager,
    forum: pager
  }
});

(Vue as any).store = store;

export default store;
