import Vue from 'vue';
import {InformationModel} from "@/interfaces/information";

export default class InformationService
{
    public static async getList(text: string): Promise<InformationModel[]>
    {
        return (await Vue.axios.get<InformationModel[]>(`informations/${text}`)).data;
    }
}
