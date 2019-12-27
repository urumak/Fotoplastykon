import Vue from 'vue';
import {SearchItem} from "@/interfaces/search";

export default class SearchService
{
    public static async getOptions(text: string): Promise<SearchItem[]>
    {
        return (await Vue.axios.get<SearchItem[]>(`search/${text}`)).data;
    }
}
