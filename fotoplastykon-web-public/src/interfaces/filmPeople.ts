import {ForumElement} from "@/interfaces/shared";

export interface FilmPersonPage {
    id: number;
    firstName: string;
    surname: string;
    profession: string;
    rating: number;
    userRating: number;
    photoUrl: string;
    roles: RoleInFilm[];
    forumThreads: ForumElement[];
}

export interface RoleInFilm {
    filmId: number;
    filmName: string;
    roleDescription: string;
    yearOfProduction: number;
    photoUrl: string;
}

export interface FilmPersonListItem {
    id: number,
    firstName: string;
    surname: string;
    photoUrl: string;
}
