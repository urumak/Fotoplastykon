import Vue from 'vue';
import {UserPage} from "@/interfaces/users"
import {Pager, PaginationResult} from "@/interfaces/pager";
import {InformationListModel} from "@/interfaces/information";
import merge from "lodash/merge";
import {FriendListItem} from "@/interfaces/shared";

export default class UsersService {
    public static async getForPage(id: number): Promise<UserPage> {
        return (await Vue.axios.get<UserPage>(`users/${id}`)).data;
    }

    public static async inviteFriend(friendId: number){
        await Vue.axios.post(`friendships/invite`, { friendId: friendId });
    }

    public static async removeFriend(friendId: number){
        await Vue.axios.delete(`friendships/remove-friend`, { data: {friendId: friendId }});
    }

    public static async cancelInvitation(friendId: number){
        await Vue.axios.delete(`friendships/cancel-invitation`, { data: {friendId: friendId }});
    }

    public static async acceptInvitation(friendId: number){
        await Vue.axios.post(`friendships/accept-invitation`, { friendId: friendId });
    }

    public static async refuseInvitation(friendId: number){
        await Vue.axios.delete(`friendships/refuse-invitation`, { data: {friendId: friendId }});
    }

    public static async getList(pager: Pager, userId: number): Promise<PaginationResult<FriendListItem>> {
        return (await Vue.axios.get<PaginationResult<FriendListItem>>(`friendships/paginated-list/${userId}`,{params: merge({}, pager)})).data;
    }
}
