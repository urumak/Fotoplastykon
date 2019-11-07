import Vue from 'vue';
import {UserModel, UserPage} from "@/interfaces/users"

export default class UsersService {
    public static async passwordExpires(): Promise<UserModel[]> {
        return (await Vue.axios.get<UserModel[]>('auth/password-expires')).data;
    }

    public static async getForPage(id: number): Promise<UserPage> {
        return (await Vue.axios.get<UserPage>(`users/${id}`)).data;
    }

    public static async inviteFriend(friendId: number){
        await Vue.axios.post(`friendships/invite`, { friendId: friendId });
    }

    public static async removeFriend(friendId: number){
        await Vue.axios.post(`friendships/remove-friend`, { friendId: friendId });
    }
}
