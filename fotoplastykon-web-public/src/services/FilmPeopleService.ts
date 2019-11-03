import Vue from 'vue';
import {FilmPersonPage} from "@/interfaces/filmPeople";

export default class FilmPeopleService {
    public static async getForPage(id: number): Promise<FilmPersonPage> {
        return (await Vue.axios.get<FilmPersonPage>(`film-people/${id}`)).data;
    }
}
