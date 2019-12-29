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

export interface RoleTypeDictionary {
    key: number;
    value: string;
}

export interface PersonDropDownModel {
    id: number;
    nameAndSurname: string;
    photoUrl: string;
}

export interface PersonInRoleForm {
    id: number;
    personId: number;
    role: number;
    characterName: string;
}
