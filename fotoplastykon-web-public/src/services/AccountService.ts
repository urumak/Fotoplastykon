import Vue from 'vue';
import {RegisterModel} from "@/interfaces/auth";

export default class AccountService
{
    public static async changeProfilePhoto(file: any): Promise<any>
    {
        let form = new FormData();
        form.append('file', file);
        await Vue.axios.post('account/change-profile-photo', form);
    }

    public static async update(model: RegisterModel): Promise<any>
    {
        await Vue.axios.post<any>('account/update', model);
    }
}
