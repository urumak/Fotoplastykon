import Vue from 'vue';
import {FilmPersonPage} from "@/interfaces/filmPeople";

export default class FilmPeopleService {
    public static async getForPage(id: number): Promise<FilmPersonPage> {
        return (await Vue.axios.get<FilmPersonPage>(`film-people/${id}`)).data;
    }

    public static async getRating(id: number): Promise<number> {
        return (await Vue.axios.get<number>(`film-people/rating/${id}`)).data;
    }

    public static async rate(personId: number, rating: number){
        await Vue.axios.post(`film-people/rate`, {personId: personId, mark: rating});
    }
}
