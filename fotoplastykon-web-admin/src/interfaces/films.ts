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
