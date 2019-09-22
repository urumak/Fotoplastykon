import Vue from 'vue';

/**
 * AuthService
 */
export default class AuthService
{
    /**
     * @param refreshToken string
     *
     * @returns Promise<TokenModel>
     */
    public static async refreshToken(): Promise<TokenModel>
    {
        return (await Vue.axios.get<TokenModel>('auth/token/refresh')).data;
    }

    /**
     * @param refreshToken string
     *
     * @returns Promise<TokenModel>
     */
    public static async login(username: string, password:string): Promise<any>
    {
        let test = (await Vue.axios.post<TokenModel>('auth/login', {username: username, password: password})).data['token'];
        console.log(test);
        return (Vue as any).$auth.login({
            username: username,
            password: password,
        });
    }

    /**
     * @param refreshToken string
     *
     * @returns Promise<TokenModel>
     */
    public static async recoverToken(refreshToken: string): Promise<TokenModel>
    {
        return (await Vue.axios.post<TokenModel>('auth/token/recover', {
            token: refreshToken
        })).data;
    }


    /**
     * @returns Promise<AuthModel>
     */
    public static async getIdentity(): Promise<AuthModel>
    {
        return (await Vue.axios.get<AuthModel>('auth/profile')).data;
    }

    /**
     * @param email string
     * @param callbackUrl string
     *
     * @returns Promise<string>
     */
    public static async resetPassword(email: string, callbackUrl: string): Promise<string>
    {
        return (await Vue.axios.post<string>('auth/reset-password', {
            email: email,
            callbackUrl: callbackUrl
        })).data;
    }

    /**
     * @param token string
     * @param email string
     * @param newPassword string
     * @param repeatPassword string
     *
     * @returns Promise<string>
     */
    public static async setPassword(token: string, email: string, newPassword: string, repeatPassword: string): Promise<string>
    {
        return (await Vue.axios.post<string>('auth/set-password', {
            token: token,
            email: email,
            newPassword: newPassword,
            repeatPassword: repeatPassword
        })).data;
    }

    /**
     * @param model PasswordModel
     *
     * @returns Promise<string>
     */
    public static async changePassword(model: PasswordModel): Promise<string>
    {
        return (await Vue.axios.post<string>('auth/change-password', model)).data;
    }

    /**
     * @returns Promise<number>
     */
    public static async passwordExpires(): Promise<number>
    {
        return (await Vue.axios.get<number>('auth/password-expires')).data;
    }
}

export interface TokenModel
{
    token: string;
    refresh: string;
    expires: string;
}

export interface AuthModel
{
    id: number;
    userName: string;
    email: string;
    givenName: string;
    surname: string;
    permissions: string[];
    forcePasswordChange: boolean;
    systemVersion: string;
}

export interface ProfileModel
{
    userName: string;
    email: string;
    givenName: string;
    surname: string;
}

export interface LoginModel
{
    username: string;
    password: string;
    rememberMe: boolean;
}

export interface PasswordModel
{
    currentPassword: string;
    newPassword: string;
    repeatPassword: string;
}

export interface ResetModel
{
    email: string;
    newPassword: string;
    repeatPassword: string;
}
