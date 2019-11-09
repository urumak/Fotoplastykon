import Vue from 'vue';
import {InformationModel} from "@/interfaces/information";

export default class InformationService
{
    public static async getList(text: string): Promise<InformationModel[]>
    {
        return (await Vue.axios.get<InformationModel[]>(`information/${text}`)).data;
    }

    public static async getListForMainPage(): Promise<InformationModel[]>
    {
        return (await Vue.axios.get<InformationModel[]>(`information/main-page`)).data;
    }
}
