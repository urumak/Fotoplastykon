import Vue from 'vue';
import {FilmPersonListItem, FilmPersonPage} from "@/interfaces/filmPeople";
import {Pager, PaginationResult} from "@/interfaces/pager";
import {RankModel} from "@/interfaces/shared";
import merge from "lodash/merge";

export default class FilmPeopleService {
    public static async getList(pager: Pager): Promise<PaginationResult<FilmPersonListItem>> {
        return (await Vue.axios.get<PaginationResult<FilmPersonListItem>>(`film-people`,{params: merge({}, pager)})).data;
    }

    public static async getForPage(id: number): Promise<FilmPersonPage> {
        return (await Vue.axios.get<FilmPersonPage>(`film-people/${id}`)).data;
    }

    public static async getRating(id: number): Promise<number> {
        return (await Vue.axios.get<number>(`film-people/rating/${id}`)).data;
    }

    public static async rate(personId: number, rating: number){
        await Vue.axios.post(`film-people/rate`, {personId: personId, mark: rating});
    }

    public static async getRatedPeople(pager: Pager, userId: number): Promise<PaginationResult<RankModel>> {
        return (await Vue.axios.get<PaginationResult<RankModel>>(`film-people/rated-people/${userId}`,{params: merge({}, pager)})).data;
    }
}
