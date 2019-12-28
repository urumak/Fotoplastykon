import Vue from 'vue';
import {TokenModel} from "@/interfaces/auth";

export default class AuthService
{
    public static async login(username: string, password:string): Promise<any>
    {
        await Vue.axios.post<TokenModel>('auth/login', {username: username, password: password});
        return (Vue as any).$auth.login({
            username: username,
            password: password,
        });
    }

    public static async profile(): Promise<any>
    {
        return (await Vue.axios.get('admin/auth/user')).data;
    }
}
