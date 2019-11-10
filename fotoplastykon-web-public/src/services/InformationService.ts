import Vue from 'vue';
import {InformationListModel, InformationModel} from "@/interfaces/information";
import {Pager, PaginationResult} from "@/interfaces/pager";
import merge from 'lodash/merge'

export default class InformationService
{
    public static async getList(pager: Pager): Promise<PaginationResult<InformationListModel>>
    {
        return (await Vue.axios.get<PaginationResult<InformationListModel>>(`information`,{params: merge({}, pager)})).data;
    }

    public static async getListForMainPage(): Promise<InformationListModel[]>
    {
        return (await Vue.axios.get<InformationListModel[]>(`information/main-page`)).data;
    }

    public static async get(id: number): Promise<InformationModel>
    {
        return (await Vue.axios.get<InformationModel>(`information/${id}`)).data;
    }
}
