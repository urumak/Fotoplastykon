import Vue from 'vue';
import {FilmPage} from "@/interfaces/films";

export default class FilmsService {
    public static async getForPage(id: number): Promise<FilmPage> {
        return (await Vue.axios.get<FilmPage>(`films/${id}`)).data;
    }
}
