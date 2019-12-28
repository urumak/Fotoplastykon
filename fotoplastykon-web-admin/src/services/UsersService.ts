import Vue from 'vue';
import {Pager, PaginationResult} from "@/interfaces/pager";
import merge from "lodash/merge";
import {UserListItem} from "@/interfaces/users";

export default class UsersService {
    public static async getList(pager: Pager): Promise<PaginationResult<UserListItem>> {
        return (await Vue.axios.get<PaginationResult<UserListItem>>(`admin/users`,{params: merge({}, pager)})).data;
    }
}
