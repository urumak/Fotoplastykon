import Vue from 'vue';
import {InfiniteScroll, InfiniteScrollResult} from "@/interfaces/infiniteScroll";
import merge from 'lodash/merge'
import {NotificationModel} from "@/interfaces/notifications";

export default class NotificationsService {
    public static async getNotifications(scroll: InfiniteScroll): Promise<InfiniteScrollResult<NotificationModel>> {
        return (await Vue.axios.get<InfiniteScrollResult<NotificationModel>>(`notifications`,{params: merge({}, scroll)})).data;
    }

    public static async getNotificationsCount(): Promise<number> {
        return (await Vue.axios.get<number>(`notifications/count`)).data;
    }

    public static async updateLastReadingDate() {
        return (await Vue.axios.post(`notifications/read`)).data;
    }
}
