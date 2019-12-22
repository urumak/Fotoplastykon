import {Pager} from "@/interfaces/pager";
import {InfiniteScroll} from "@/interfaces/infiniteScroll";

export default class StoreHelper {
    public static getDefaultChatState(): any {
        return {
            infiniteScroll: new InfiniteScroll(20),
            friends: [],
            activeWindows: [],
            unreadMessagesFromIds: []
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
}
