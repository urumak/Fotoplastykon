import {ForumElement} from "@/interfaces/shared";

export interface FilmPersonPage {
    id: number;
    firstName: string;
    surname: string;
    profession: string;
    rank: number;
    photoUrl: string;
    roles: RoleInFilm[];
    filmMakings: FilmMaking[];
    forumThreads: ForumElement[];
}

export interface RoleInFilm {
    filmId: number;
    filmName: string;
    characterName: string;
    yearOfProduction: number;
    photoUrl: string;
}

export interface FilmMaking {
    filmId: number;
    filmName: string;
    role: string;
    yearOfProduction: number;
    photoUrl: string;
}
