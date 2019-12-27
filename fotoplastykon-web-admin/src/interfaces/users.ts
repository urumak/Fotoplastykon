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

