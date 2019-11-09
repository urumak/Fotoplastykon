import Vue from 'vue'
import Vuex from 'vuex'
import information from "@/store/information";

Vue.use(Vuex);

const store = new Vuex.Store({
  strict: false,
  modules: {
    information: information
  }
});

(Vue as any).store = store;

export default store;
