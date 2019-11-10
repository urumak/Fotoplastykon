import Vue from 'vue';
import {InformationModel} from "@/interfaces/information";
import {Pager, PaginationResult} from "@/interfaces/pager";
import merge from 'lodash/merge'

export default class InformationService
{
    public static async getList(pager: Pager): Promise<PaginationResult<InformationModel>>
    {
        return (await Vue.axios.get<PaginationResult<InformationModel>>(`information`,{params: merge({}, pager)})).data;
    }

    public static async getListForMainPage(): Promise<InformationModel[]>
    {
        return (await Vue.axios.get<InformationModel[]>(`information/main-page`)).data;
    }
}
