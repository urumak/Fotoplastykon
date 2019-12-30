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

    public static async changePhoto(id: number, file: any): Promise<QuizForm> {
        let form = new FormData();
        form.append('file', file);
        return (await Vue.axios.post(`admin/quizzes/change-photo/${id}`, form)).data;
    }

    public static async delete(id: number): Promise<any> {
        return (await Vue.axios.delete(`admin/quizzes/${id}`));
    }

    public static async update(id: number, model: QuizForm): Promise<QuizForm> {
        return (await Vue.axios.post<QuizForm>(`admin/quizzes/${id}`, model)).data;
    }
}
