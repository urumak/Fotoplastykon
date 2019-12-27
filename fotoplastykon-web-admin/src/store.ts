import Vue from 'vue'
import Vuex from 'vuex'
import information from "@/store/information";
import quizzes from "@/store/quizzes";
import StoreHelper from "@/store/storeHelper";
import storeHelper from "@/store/storeHelper";
import user from "@/store/user";

Vue.use(Vuex);

const mutations = {
  resetState (state: any) {
    state.information = StoreHelper.getDefaultInformationState();
    state.quizzes = StoreHelper.getDefaultQuizzesState();
    state.user = storeHelper.getDefaultUserState();
  }
};

const store = new Vuex.Store({
  strict: false,
  modules: {
    information: information,
    quizzes: quizzes,
    user: user
  },
  mutations: mutations
});

(Vue as any).store = store;

export default store;
