import Vue from 'vue';

export default class InformationService
{
    public static async getList(text: string): Promise<InformationModel[]>
    {
        return (await Vue.axios.get<InformationModel[]>(`informations/${text}`)).data;
    }
}

export interface InformationModel
{
    id: number;
    title: string;
    introduction: string;
    photoUrl: string;
}
