import Vue from 'vue';
import {QuizForm, QuizListItem} from "@/interfaces/quizes"
import {Pager, PaginationResult} from "@/interfaces/pager";
import merge from "lodash/merge";

export default class QuizzesService {
    public static async getList(pager: Pager): Promise<PaginationResult<QuizListItem>> {
        return (await Vue.axios.get<PaginationResult<QuizListItem>>(`admin/quizzes`,{params: merge({}, pager)})).data;
    }

    public static async get(id: number): Promise<QuizForm> {
        return (await Vue.axios.get<QuizForm>(`admin/quizzes/${id}`)).data;
    }

    public static async changePhoto(id: number, file: any): Promise<any> {
        let form = new FormData();
        form.append('file', file);
        return (await Vue.axios.post<any>(`admin/quizzes/change-photo/${id}`, form)).data;
    }

    public static async add(model: QuizForm): Promise<number> {
        return (await Vue.axios.post<number>(`admin/quizzes/add`, model)).data;
    }

    public static async delete(id: number): Promise<any> {
        return (await Vue.axios.delete(`admin/quizzes/${id}`));
    }

    public static async update(id: number, model: QuizForm): Promise<any> {
        return (await Vue.axios.post<any>(`admin/quizzes/${id}`, model)).data;
    }
}
