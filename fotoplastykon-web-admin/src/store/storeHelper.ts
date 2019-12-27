import {Pager} from "@/interfaces/pager";
import {InfiniteScroll} from "@/interfaces/infiniteScroll";

export default class StoreHelper {
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

    public static getDefaultUserState(): any {
        return {
            photoUrl: ''
        }
    }
}
