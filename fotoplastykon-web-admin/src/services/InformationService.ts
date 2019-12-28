import Vue from 'vue';
import {InformationFormModel, InformationListItem, InformationListModel} from "@/interfaces/information";
import {Pager, PaginationResult} from "@/interfaces/pager";
import merge from 'lodash/merge'

export default class InformationService
{
    public static async getListForMainPage(): Promise<InformationListModel[]> {
        return (await Vue.axios.get<InformationListModel[]>(`information/main-page`)).data;
    }
    public static async getList(pager: Pager): Promise<PaginationResult<InformationListItem>> {
        return (await Vue.axios.get<PaginationResult<InformationListItem>>(`admin/information`,{params: merge({}, pager)})).data;
    }

    public static async get(id: number): Promise<InformationFormModel> {
        return (await Vue.axios.get<InformationFormModel>(`admin/information/${id}`)).data;
    }

    public static async update(id: number, model: InformationFormModel): Promise<InformationFormModel> {
        return (await Vue.axios.post<InformationFormModel>(`admin/information/${id}`, model)).data;
    }

    public static async delete(id: number): Promise<any> {
        return (await Vue.axios.delete(`admin/information/${id}`));
    }

    public static async changePhoto(id: number, file: any): Promise<InformationFormModel> {
        let form = new FormData();
        form.append('file', file);
        return (await Vue.axios.post(`admin/information/change-photo/${id}`, form)).data;
    }
}
