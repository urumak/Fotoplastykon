import Vue from 'vue';
import {CastMember, FilmListItem, Filmmaker, FilmPage} from "@/interfaces/films";
import {Pager, PaginationResult} from "@/interfaces/pager";
import {ForumElement, RankModel} from "@/interfaces/shared";
import merge from "lodash/merge";
import {ForumThreadModel} from "@/interfaces/forum";

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

    public static async getCast(pager: Pager, filmId: number): Promise<PaginationResult<CastMember>> {
        return (await Vue.axios.get<PaginationResult<CastMember>>(`films/cast/${filmId}`,{params: merge({}, pager)})).data;
    }

    public static async getForumThreads(pager: Pager, filmId: number): Promise<PaginationResult<ForumElement>> {
        return (await Vue.axios.get<PaginationResult<ForumElement>>(`films/forum/${filmId}`,{params: merge({}, pager)})).data;
    }

    public static async getFilmMakers(pager: Pager, filmId: number): Promise<PaginationResult<Filmmaker>> {
        return (await Vue.axios.get<PaginationResult<Filmmaker>>(`films/film-makers/${filmId}`,{params: merge({}, pager)})).data;
    }
}
