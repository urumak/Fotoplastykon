export interface InformationListModel
{
    id: number;
    title: string;
    introduction: string;
    photoUrl: string;
}

export interface InformationModel
{
    id: number;
    createdByName: string;
    dateCreated: Date;
    title: string;
    introduction: string;
    photoUrl: string;
    content: string;
    comments: InformationCommentModel[];
}

export interface InformationCommentModel
{
    id: number;
    creatorFullName: string;
    informationId: number;
    parentId?: number;
    content: string;
    dateCreated: Date;
    editMode?: boolean,
    replies: InformationCommentModel[];
    photoUrl: string;
    tempContent: string;
    createdById: number;
    isDeleted: boolean;
    isReplyAdding: boolean;
}
