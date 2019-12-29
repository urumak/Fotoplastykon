import Vue from 'vue';
import {FilmFormModel, FilmListItem, PersonDropDownModel, RoleTypeDictionary} from "@/interfaces/films";
import {Pager, PaginationResult} from "@/interfaces/pager";
import merge from "lodash/merge";

export default class FilmsService {
    public static async getList(pager: Pager): Promise<PaginationResult<FilmListItem>> {
        return (await Vue.axios.get<PaginationResult<FilmFormModel>>(`admin/films`,{params: merge({}, pager)})).data;
    }

    public static async get(id: number): Promise<FilmFormModel> {
        return (await Vue.axios.get<FilmFormModel>(`admin/films/${id}`)).data;
    }

    public static async update(id: number, model: FilmFormModel): Promise<FilmFormModel> {
        return (await Vue.axios.post<FilmFormModel>(`admin/films/${id}`, model)).data;
    }

    public static async delete(id: number): Promise<any> {
        return (await Vue.axios.delete(`admin/films/${id}`));
    }

    public static async changePhoto(id: number, file: any): Promise<FilmFormModel> {
        let form = new FormData();
        form.append('file', file);
        return (await Vue.axios.post(`admin/films/change-photo/${id}`, form)).data;
    }

    public static async getRoleTypes(): Promise<RoleTypeDictionary[]> {
        return (await Vue.axios.get<RoleTypeDictionary[]>(`admin/films/role-types`)).data;
    }

    public static async getFilmPeople(search: string): Promise<PersonDropDownModel[]> {
        return (await Vue.axios.get<PersonDropDownModel[]>(`admin/films/people/${search}`)).data;
    }

    public static async getFilmPerson(id: number): Promise<PersonDropDownModel> {
        return (await Vue.axios.get<PersonDropDownModel>(`admin/films/person/${id}`)).data;
    }
}
