import {Pager} from "@/interfaces/pager";

export default {
    namespaced: true,
    state: {
        pager: new Pager(1, 2)
    }
}
