import Vue from 'vue';
import { UserModel } from "@/interfaces/users"

export default class AuthService
{
    public static async passwordExpires(): Promise<UserModel[]>
    {
        return (await Vue.axios.get<UserModel[]>('auth/password-expires')).data;
    }
}
