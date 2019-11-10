import DateTimeFormat = Intl.DateTimeFormat;

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
    content: string;
    dateCreated: Date;
    replies: InformationCommentModel[];
}
