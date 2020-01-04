import Vue from 'vue'
import Vuex from 'vuex'
import information from "@/store/information";
import quizzes from "@/store/quizzes";
import storeHelper from "@/store/storeHelper";
import user from "@/store/user";
import alert from "@/store/alert";

Vue.use(Vuex);

const mutations = {
  resetState (state: any) {
    state.information = storeHelper.getDefaultInformationState();
    state.quizzes = storeHelper.getDefaultQuizzesState();
    state.user = storeHelper.getDefaultUserState();
    state.alert = storeHelper.getDefaultAlertState()
  }
};

const store = new Vuex.Store({
  strict: false,
  modules: {
    information: information,
    quizzes: quizzes,
    user: user,
    alert: alert
  },
  mutations: mutations
});

(Vue as any).store = store;

export default store;
