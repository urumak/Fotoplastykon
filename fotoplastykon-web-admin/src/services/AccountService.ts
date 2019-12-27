import Vue from 'vue';

export default class AccountService
{
    public static async changeProfilePhoto(file: any): Promise<any>
    {
        let form = new FormData();
        form.append('file', file);
        await Vue.axios.post('account/change-profile-photo', form);
    }

    public static async removeProfilePhoto(): Promise<any>
    {
        await Vue.axios.delete('account/remove-profile-photo');
    }
}
