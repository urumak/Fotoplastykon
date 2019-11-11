import Vue from 'vue';
import {InformationCommentModel, InformationListModel, InformationModel} from "@/interfaces/information";
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

    public static async addComment(model: InformationCommentModel): Promise<number>
    {
        return (await Vue.axios.post<number>(`information/comment`, model)).data;
    }

    public static async updateComment(model: InformationCommentModel)
    {
        (await Vue.axios.post(`information/comment/${model.id}`, model));
    }

    public static async removeComment(id: number)
    {
        (await Vue.axios.delete(`information/comment/${id}`));
    }
}
