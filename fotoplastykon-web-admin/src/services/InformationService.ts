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

    public static async add(model: InformationFormModel): Promise<number> {
        return (await Vue.axios.post<number>(`admin/information/add`, model)).data;
    }

    public static async update(id: number, model: InformationFormModel): Promise<any> {
        return (await Vue.axios.post<any>(`admin/information/${id}`, model)).data;
    }

    public static async delete(id: number): Promise<any> {
        return (await Vue.axios.delete(`admin/information/${id}`));
    }

    public static async changePhoto(id: number, file: any): Promise<any> {
        let form = new FormData();
        form.append('file', file);
        return (await Vue.axios.post<any>(`admin/information/change-photo/${id}`, form)).data;
    }
}
