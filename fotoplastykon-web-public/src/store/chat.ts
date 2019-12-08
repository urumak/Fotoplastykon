import { InfiniteScroll } from "@/interfaces/infiniteScroll";

export default {
    namespaced: true,
    state: {
        infiniteScroll: new InfiniteScroll(2),
        friends: [],
        activeWindows: []
    }
}
