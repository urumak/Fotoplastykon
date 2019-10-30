import Vue from 'vue';

/**
 * AuthService
 */
export default class SearchService
{
    /**
     * @returns Promise<number>
     */
    public static async getOptions(text: string): Promise<LinkedItem[]>
    {
        return (await Vue.axios.get<LinkedItem[]>(`search/${text}`)).data;
    }
}

export interface LinkedItem
{
    key: string;
    value: string;
    photoUrl: string;
}
