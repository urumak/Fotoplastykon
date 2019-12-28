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
    isInvitationSender: boolean;
}

export interface UserListItem {
    id: number;
    userName: string;
    firstName: string;
    surname: string;
    photoUrl: string;
}

export interface UserFromModel {
    userName: string;
    email: string;
    firstName: string;
    surname: string;
    password: string;
    repeatPassword: string;
    photoUrl: string;
    isAdmin: boolean;
}

