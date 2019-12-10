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
    isPrincipalSender: boolean;
    dateCreated: Date;
}

