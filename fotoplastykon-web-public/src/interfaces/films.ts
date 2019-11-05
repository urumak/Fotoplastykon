import {ForumElement} from "@/interfaces/shared";

export interface FilmPage {
    id: number;
    title: string;
    yearOfProduction: number;
    rating: number;
    userRating: number;
    ratingsCount: number;
    photoUrl: string;
    cast: CastMember[];
    filmmakers: Filmmaker[];
    forumThreads: ForumElement[];
}

export interface CastMember {
    personId: number;
    fullName: string;
    characterName: string;
    photoUrl: string;
}

export interface Filmmaker {
    personId: number;
    fullName: string;
    profession: string;
    photoUrl: string;
}
