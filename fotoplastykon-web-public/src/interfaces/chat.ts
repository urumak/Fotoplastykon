export interface ChatListItem
{
    id: number;
    nameAndSurname: string;
    photoUrl: string;
}

export interface ChatWindowModel
{
    id: number;
    nameAndSurname: string;
    photoUrl: string;
    messages: Message[];
}

export interface Message
{
    id: number;
    messageText: string;
    isPrincipalSender: boolean;
    dateCreated: Date;
}


