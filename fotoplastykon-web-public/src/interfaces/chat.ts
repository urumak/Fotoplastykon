import {InfiniteScrollResult} from "@/interfaces/infiniteScroll";

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

export interface LastMessage
{
    id: number;
    messageText: string;
    senderId: number;
    friendId: number;
    unread: boolean;
    photoUrl: string;
    nameAndSurname: string;
    dateCreated: Date;
}


