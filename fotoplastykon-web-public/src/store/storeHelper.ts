import {Pager} from "@/interfaces/pager";
import {InfiniteScroll} from "@/interfaces/infiniteScroll";

export default class StoreHelper {
    public static getDefaultChatState(): any {
        return {
            infiniteScroll: new InfiniteScroll(20),
            friends: [],
            activeWindows: [],
            unreadMessagesFromIds: [],
            notificationsInfiniteScroll: new InfiniteScroll(20)
        }
    }

    public static getDefaultForumState(): any {
        return {
            pager: new Pager(1, 2)
        }
    }

    public static getDefaultInformationState(): any {
        return {
            pager: new Pager(1, 2)
        }
    }

    public static getDefaultQuizzesState(): any {
        return {
            pager: new Pager(1, 2)
        }
    }

    public static getDefaultFilmsState(): any {
        return {
            pager: new Pager(1, 2)
        }
    }

    public static getDefaultFilmPeopleState(): any {
        return {
            pager: new Pager(1, 2)
        }
    }

    public static getDefaultNotificationsState(): any {
        return {
            count: 0
        }
    }

    public static getDefaultUserState(): any {
        return {
            photoUrl: ''
        }
    }

    public static getDefaultAlertState(): any {
        return {
            show: false,
            type: '',
            message: ''
        }
    }
}
