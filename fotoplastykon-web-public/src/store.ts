import Vue from 'vue'
import Vuex from 'vuex'
import information from "@/store/information";
import quizzes from "@/store/quizzes";
import forum from "@/store/forum";
import chat from "@/store/chat";
import StoreHelper from "@/store/storeHelper";

Vue.use(Vuex);

const mutations = {
  resetState (state: any) {
    state.chat = StoreHelper.getDefaultChatState();
    state.information = StoreHelper.getDefaultInformationState();
    state.forum = StoreHelper.getDefaultForumState();
    state.quizzes = StoreHelper.getDefaultQuizzesState();
  }
};

const store = new Vuex.Store({
  strict: false,
  modules: {
    information: information,
    forum: forum,
    quizzes: quizzes,
    chat: chat
  },
  mutations: mutations
});

(Vue as any).store = store;

export default store;
