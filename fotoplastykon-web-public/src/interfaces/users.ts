export interface UserModel {
    userName: string;
    name: string;
    surname: string;
    id: number;
}

export interface UserPage {
    id: number;
    userName: string;
    firstName: string;
    surname: string;
    photoUrl: string;
    isFriend: boolean;
    invitationSent: boolean;
    watchedFilms: RankModel[];
    ratedPeople: RankModel[];
}

export interface RankModel {
    id: number;
    itemName: string;
    mark: number;
    photoUrl: string;
}
