import Vue from 'vue';
import { ForumThreadCommentModel, ForumThreadModel } from '@/interfaces/forum';
import {Pager, PaginationResult} from "@/interfaces/pager";
import merge from 'lodash/merge'

export default class ForumService
{
    public static async getList(pager: Pager): Promise<PaginationResult<ForumThreadModel>>
    {
        return (await Vue.axios.get<PaginationResult<ForumThreadModel>>(`forum`,{params: merge({}, pager)})).data;
    }

    public static async get(id: number): Promise<ForumThreadModel>
    {
        return (await Vue.axios.get<ForumThreadModel>(`forum/${id}`)).data;
    }

    public static async add(model: ForumThreadModel): Promise<number>
    {
        return (await Vue.axios.post<number>(`forum`, model)).data;
    }

    public static async addForFilm(model: ForumThreadModel, id: number): Promise<number>
    {
        return (await Vue.axios.post<number>(`forum/film/${id}`, model)).data;
    }

    public static async addForFilmPerson(model: ForumThreadModel, id: number): Promise<number>
    {
        return (await Vue.axios.post<number>(`forum/film-person/${id}`, model)).data;
    }

    public static async update(model: ForumThreadModel)
    {
        (await Vue.axios.post(`forum/${model.id}`, model));
    }

    public static async remove(id: number)
    {
        (await Vue.axios.delete(`forum/${id}`));
    }

    public static async addComment(model: ForumThreadCommentModel): Promise<number>
    {
        return (await Vue.axios.post<number>(`forum/comment`, model)).data;
    }

    public static async updateComment(model: ForumThreadCommentModel)
    {
        (await Vue.axios.post(`forum/comment/${model.id}`, model));
    }

    public static async removeComment(id: number)
    {
        (await Vue.axios.delete(`forum/comment/${id}`));
    }
}
