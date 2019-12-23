export interface NotificationModel
{
    id: number;
    friendId: number;
    unread: boolean;
    canAccept: boolean;
    photoUrl: string;
    nameAndSurname: string;
    type: number;
    dateCreated: Date;
}
