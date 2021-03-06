import Vue from 'vue';
import {Pager, PaginationResult} from "@/interfaces/pager";
import merge from "lodash/merge";
import {ChatWindowModel, LastMessage, Message, SendMessage} from "@/interfaces/chat";
import {InfiniteScrollResult, InfiniteScroll} from "@/interfaces/infiniteScroll";
import {Quiz, QuizResult} from "@/interfaces/quizes";
import {FriendListItem} from "@/interfaces/shared";

export default class ChatService {
    public static async getFriends(scroll: InfiniteScroll): Promise<InfiniteScrollResult<FriendListItem>> {
        return (await Vue.axios.get<InfiniteScrollResult<FriendListItem>>(`chat/friends`,{params: merge({}, scroll)})).data;
    }

    public static async getMessages(scroll: InfiniteScroll, friendId: number): Promise<InfiniteScrollResult<Message>> {
        return (await Vue.axios.get<InfiniteScrollResult<Message>>(`chat/messages/${friendId}`,{params: merge({}, scroll)})).data;
    }

    public static async searchFriends(searchInput: string): Promise<FriendListItem[]> {
        return (await Vue.axios.get<FriendListItem[]>(`chat/search-friends/${searchInput}`)).data;
    }

    public static async getForWindows(friendsIds: number[]): Promise<ChatWindowModel[]> {
        let qs = require('qs');

        return (await Vue.axios.get<ChatWindowModel[]>(`chat/chat-windows`,{params: {friendsIds: friendsIds}, paramsSerializer:function(params) {
                return qs.stringify(params, { arrayFormat: 'repeat' })
            }})).data;
    }

    public static async sendMessage(message: SendMessage) {
        return (await Vue.axios.post(`chat/send-message`, message)).data;
    }

    public static async getUnreadMessagesUsersIds(): Promise<number[]> {
        return (await Vue.axios.get<number[]>(`chat/get-unread-messages-users-ids`)).data;
    }

    public static async updateLastReadingDate(senderId: number) {
        return (await Vue.axios.post(`chat/update-last-reading-date/${senderId}`)).data;
    }

    public static async getLastMessagesForEachFriend(scroll: InfiniteScroll): Promise<InfiniteScrollResult<LastMessage>> {
        return (await Vue.axios.get<InfiniteScrollResult<LastMessage>>(`chat/get-last-messages`,{params: merge({}, scroll)})).data;
    }
}
