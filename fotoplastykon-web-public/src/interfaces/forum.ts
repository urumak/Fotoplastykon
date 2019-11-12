export interface ForumThreadModel
{
    id: number;
    filmId?: number;
    personId?: number;
    createdByName: string;
    dateCreated: Date;
    subject: string;
    photoUrl: string;
    content: string;
    createdById: number;
    editMode: boolean;
    comments: ForumThreadCommentModel[];
}

export interface ForumThreadCommentModel
{
    id: number;
    createdByName: string;
    forumThreadId: number;
    parentId?: number;
    content: string;
    dateCreated: Date;
    editMode?: boolean;
    photoUrl: string;
    createdById: number;
    isDeleted: boolean;
    isReplyAdding: boolean;
    replies: ForumThreadCommentModel[];
}
