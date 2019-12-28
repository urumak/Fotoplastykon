import {ForumElement} from "@/interfaces/shared";

export interface FilmPage {
    id: number;
    title: string;
    yearOfProduction: number;
    rating: number;
    userRating: number;
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


export interface FilmListItem {
    id: number;
    title: string;
    photoUrl: string;
}

export interface FilmFormModel {
    id: number;
    title: string;
    yearOfProduction: number;
    photoUrl: string;
    people: PersonInRoleForm[];
}

export interface RolesDropDownDictionary {
    [key: number]: string
}

export interface PersonDropDownModel {
    id: number;
    nameAndSurname: string;
    photoUrl: string;
}

export interface PersonInRoleForm {
    personId: number;
    roleId: number;
    roleName: string;
}
