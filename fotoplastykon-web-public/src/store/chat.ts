import { InfiniteScroll } from "@/interfaces/infiniteScroll";

export default {
    namespaced: true,
    state: {
        infiniteScroll: new InfiniteScroll(20),
        friends: [],
        activeWindows: [],
        unreadMessagesFromIds: []
    }
}
