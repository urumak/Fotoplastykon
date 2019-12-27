import Vue from 'vue';
import {PasswordModel, RegisterModel, TokenModel} from "@/interfaces/auth";

export default class AuthService
{
    public static async refreshToken(): Promise<TokenModel>
    {
        return (await Vue.axios.get<TokenModel>('auth/token/refresh')).data;
    }

    public static async login(username: string, password:string): Promise<any>
    {
        let test = (await Vue.axios.post<TokenModel>('auth/login', {username: username, password: password})).data['token'];
        return (Vue as any).$auth.login({
            username: username,
            password: password,
        });
    }

    public static async recoverToken(refreshToken: string): Promise<TokenModel>
    {
        return (await Vue.axios.post<TokenModel>('auth/token/recover', {
            token: refreshToken
        })).data;
    }


    public static async resetPassword(email: string, callbackUrl: string): Promise<string>
    {
        return (await Vue.axios.post<string>('auth/reset-password', {
            email: email,
            callbackUrl: callbackUrl
        })).data;
    }

    public static async setPassword(token: string, email: string, newPassword: string, repeatPassword: string): Promise<string>
    {
        return (await Vue.axios.post<string>('auth/set-password', {
            token: token,
            email: email,
            newPassword: newPassword,
            repeatPassword: repeatPassword
        })).data;
    }

    public static async changePassword(model: PasswordModel): Promise<string>
    {
        return (await Vue.axios.post<string>('auth/change-password', model)).data;
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
