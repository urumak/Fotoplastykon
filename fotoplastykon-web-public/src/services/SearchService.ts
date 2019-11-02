import Vue from 'vue';
import {LinkedItem} from "@/interfaces/shared";

export default class SearchService
{
    public static async getOptions(text: string): Promise<LinkedItem[]>
    {
        return (await Vue.axios.get<LinkedItem[]>(`search/${text}`)).data;
    }
}
