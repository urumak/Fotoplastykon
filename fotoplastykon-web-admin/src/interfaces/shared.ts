export interface ForumElement {
     id: number;
     subject: string;
     content: string;
     createdByName: string;
     photoUrl: string;
     createdById: number;
}

export interface FriendListItem {
     id: number;
     nameAndSurname: string;
     photoUrl: string;
}

export interface RankModel {
     id: number;
     itemName: string;
     mark: number;
     photoUrl: string;
}

