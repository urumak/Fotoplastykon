import Vue from 'vue'
import Vuex from 'vuex'
import pager from "@/store/pager";
import chat from "@/store/chat";

Vue.use(Vuex);

const store = new Vuex.Store({
  strict: false,
  modules: {
    information: pager,
    forum: pager,
    chat: chat
  }
});

(Vue as any).store = store;

export default store;
