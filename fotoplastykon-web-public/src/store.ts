import Vue from 'vue'
import Vuex from 'vuex'
import information from "@/store/information";
import quizzes from "@/store/quizzes";
import forum from "@/store/forum";
import chat from "@/store/chat";
import notifications from "@/store/notifications";
import StoreHelper from "@/store/storeHelper";
import storeHelper from "@/store/storeHelper";
import user from "@/store/user";

Vue.use(Vuex);

const mutations = {
  resetState (state: any) {
    state.chat = StoreHelper.getDefaultChatState();
    state.information = StoreHelper.getDefaultInformationState();
    state.forum = StoreHelper.getDefaultForumState();
    state.quizzes = StoreHelper.getDefaultQuizzesState();
    state.notifications = storeHelper.getDefaultNotificationsState();
    state.user = storeHelper.getDefaultUserState();
  }
};

const store = new Vuex.Store({
  strict: false,
  modules: {
    information: information,
    forum: forum,
    quizzes: quizzes,
    chat: chat,
    notifications: notifications,
    user: user
  },
  mutations: mutations
});

(Vue as any).store = store;

export default store;
