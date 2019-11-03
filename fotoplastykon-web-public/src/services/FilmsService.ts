import Vue from 'vue';
import {FilmPage} from "@/interfaces/films";

export default class FilmsService {
    public static async getForPage(id: number): Promise<FilmPage> {
        return (await Vue.axios.get<FilmPage>(`films/${id}`)).data;
    }

    public static async getRate(id: number): Promise<number> {
        return (await Vue.axios.get<number>(`films/get-rate/${id}`)).data;
    }

    public static async rate(filmId: number, rating: number){
        await Vue.axios.post(`films/rate`, {filmId: filmId, mark: rating});
    }
}
