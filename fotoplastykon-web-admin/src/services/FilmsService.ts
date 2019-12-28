import Vue from 'vue';
import {FilmListItem, PersonDropDownModel, RolesDropDownDictionary} from "@/interfaces/films";
import {Pager, PaginationResult} from "@/interfaces/pager";
import merge from "lodash/merge";
import {InformationFormModel, InformationListItem} from "@/interfaces/information";

export default class FilmsService {
    public static async getList(pager: Pager): Promise<PaginationResult<FilmListItem>> {
        return (await Vue.axios.get<PaginationResult<InformationListItem>>(`admin/films`,{params: merge({}, pager)})).data;
    }

    public static async get(id: number): Promise<InformationFormModel> {
        return (await Vue.axios.get<InformationFormModel>(`admin/films/${id}`)).data;
    }

    public static async update(id: number, model: InformationFormModel): Promise<InformationFormModel> {
        return (await Vue.axios.post<InformationFormModel>(`admin/films/${id}`, model)).data;
    }

    public static async delete(id: number): Promise<any> {
        return (await Vue.axios.delete(`admin/films/${id}`));
    }

    public static async changePhoto(id: number, file: any): Promise<InformationFormModel> {
        let form = new FormData();
        form.append('file', file);
        return (await Vue.axios.post(`admin/films/change-photo/${id}`, form)).data;
    }

    public static async getRoleTypes(): Promise<RolesDropDownDictionary> {
        return (await Vue.axios.get<RolesDropDownDictionary>(`admin/films/role-types`)).data;
    }

    public static async getFilmPeople(search: string): Promise<PersonDropDownModel[]> {
        return (await Vue.axios.get<PersonDropDownModel[]>(`admin/films/people-drop-down/${search}`)).data;
    }
}
