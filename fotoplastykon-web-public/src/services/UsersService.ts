import Vue from 'vue';

/**
 * AuthService
 */
export default class AuthService
{
    /**
     * @returns Promise<number>
     */
    public static async passwordExpires(): Promise<UserModel[]>
    {
        return (await Vue.axios.get<UserModel[]>('auth/password-expires')).data;
    }
}

export interface UserModel
{
    userName: string;
    name: string;
    surname: string;
    id: number;
}
