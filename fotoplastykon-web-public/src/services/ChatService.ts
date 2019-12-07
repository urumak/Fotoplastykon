import Vue from 'vue';
import {Pager, PaginationResult} from "@/interfaces/pager";
import merge from "lodash/merge";
import {ChatListItem} from "@/interfaces/chat";
import {InfiniteResult, InfiniteScroll} from "@/interfaces/infiniteScroll";
import {Quiz, QuizResult} from "@/interfaces/quizes";
import pager from "@/store/pager";

export default class ChatService {
    public static async getFriends(scroll: InfiniteScroll): Promise<InfiniteResult<ChatListItem>> {
        return (await Vue.axios.get<InfiniteResult<ChatListItem>>(`chat/friends`,{params: merge({}, scroll)})).data;
    }

    public static async searchFriends(searchInput: string): Promise<ChatListItem[]> {
        return (await Vue.axios.get<ChatListItem[]>(`chat/search-friends/${searchInput}`)).data;
    }
}
