import Vue from 'vue';
import {Pager, PaginationResult} from "@/interfaces/pager";
import merge from "lodash/merge";
import {InformationFormModel} from "@/interfaces/information";
import {FilmPersonFormModel, FilmPersonListItem} from "@/interfaces/filmPeople";

export default class FilmPeopleService {
    public static async getList(pager: Pager): Promise<PaginationResult<FilmPersonListItem>> {
        return (await Vue.axios.get<PaginationResult<FilmPersonListItem>>(`admin/film-people`,{params: merge({}, pager)})).data;
    }

    public static async get(id: number): Promise<FilmPersonFormModel> {
        return (await Vue.axios.get<FilmPersonFormModel>(`admin/film-people/${id}`)).data;
    }

    public static async update(id: number, model: InformationFormModel): Promise<FilmPersonFormModel> {
        return (await Vue.axios.post<FilmPersonFormModel>(`admin/film-people/${id}`, model)).data;
    }

    public static async delete(id: number): Promise<any> {
        return (await Vue.axios.delete(`admin/film-people/${id}`));
    }

    public static async changePhoto(id: number, file: any): Promise<FilmPersonFormModel> {
        let form = new FormData();
        form.append('file', file);
        return (await Vue.axios.post(`admin/film-people/change-photo/${id}`, form)).data;
    }
}
