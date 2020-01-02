import Vue from 'vue';
import { RegisterModel } from "@/interfaces/auth";

export default class AuthService
{
    public static async login(username: string, password:string): Promise<any>
    {
        return (Vue as any).$auth.login({
            username: username,
            password: password,
        });
    }

    public static async register(model: RegisterModel): Promise<any>
    {
        await Vue.axios.post('auth/register', model);
    }

    public static async profile(): Promise<any>
    {
        return (await Vue.axios.get('auth/user')).data;
    }
}
