import {InfiniteScrollResult} from "@/interfaces/infiniteScroll";

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
    messages: InfiniteScrollResult<Message>;
}

export interface Message
{
    id: number;
    messageText: string;
    isSender: boolean;
    dateCreated: Date;
}

export interface SendMessage
{
    receiverId: number;
    messageText: string;
}


