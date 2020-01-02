import Vue from 'vue';
import {FilmListItem, FilmPage} from "@/interfaces/films";
import {Pager, PaginationResult} from "@/interfaces/pager";
import {RankModel} from "@/interfaces/shared";
import merge from "lodash/merge";

export default class FilmsService {
    public static async getList(pager: Pager): Promise<PaginationResult<FilmListItem>> {
        return (await Vue.axios.get<PaginationResult<FilmListItem>>(`films`,{params: merge({}, pager)})).data;
    }

    public static async getForPage(id: number): Promise<FilmPage> {
        return (await Vue.axios.get<FilmPage>(`films/${id}`)).data;
    }

    public static async getRating(id: number): Promise<number> {
        return (await Vue.axios.get<number>(`films/rating/${id}`)).data;
    }

    public static async rate(filmId: number, rating: number){
        await Vue.axios.post(`films/rate`, {filmId: filmId, mark: rating});
    }

    public static async getWatchedFilms(pager: Pager, userId: number): Promise<PaginationResult<RankModel>> {
        return (await Vue.axios.get<PaginationResult<RankModel>>(`films/watched-films/${userId}`,{params: merge({}, pager)})).data;
    }
}
