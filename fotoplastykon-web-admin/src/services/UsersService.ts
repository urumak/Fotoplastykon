import Vue from 'vue';
import {Pager, PaginationResult} from "@/interfaces/pager";
import merge from "lodash/merge";
import {UserFromModel, UserListItem} from "@/interfaces/users";

export default class UsersService {
    public static async getList(pager: Pager): Promise<PaginationResult<UserListItem>> {
        return (await Vue.axios.get<PaginationResult<UserListItem>>(`admin/users`,{params: merge({}, pager)})).data;
    }

    public static async get(id: number): Promise<UserFromModel> {
        return (await Vue.axios.get<UserFromModel>(`admin/users/${id}`)).data;
    }

    public static async update(id: number, model: UserFromModel): Promise<UserFromModel> {
        return (await Vue.axios.post<UserFromModel>(`admin/users/${id}`, model)).data;
    }

    public static async delete(id: number): Promise<any> {
        return (await Vue.axios.delete(`admin/users/${id}`));
    }

    public static async changeProfilePhoto(id: number, file: any): Promise<UserFromModel> {
        let form = new FormData();
        form.append('file', file);
        return (await Vue.axios.post(`admin/users/change-profile-photo/${id}`, form)).data;
    }
}
