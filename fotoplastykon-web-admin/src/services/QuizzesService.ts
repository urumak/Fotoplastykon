import Vue from 'vue';
import {QuizListItem, Quiz, QuizResult} from "@/interfaces/quizes"
import {Pager, PaginationResult} from "@/interfaces/pager";
import merge from "lodash/merge";

export default class QuizzesService {
    public static async getList(pager: Pager): Promise<PaginationResult<QuizListItem>> {
        return (await Vue.axios.get<PaginationResult<QuizListItem>>(`quizzes`,{params: merge({}, pager)})).data;
    }

    public static async get(id: number): Promise<Quiz> {
        return (await Vue.axios.get<Quiz>(`quizzes/${id}`)).data;
    }

    public static async submit(id: number, quiz: Quiz): Promise<QuizResult> {
        return (await Vue.axios.post<QuizResult>(`quizzes/submit/${id}`, quiz)).data;
    }
}
