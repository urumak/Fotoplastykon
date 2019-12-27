<template>
    <v-container class="flex flex-center">
        <v-card>
            <div>{{ userModel.firstName }}</div>
            <div>{{ userModel.surname }}</div>
            <div>{{ userModel.userName }}</div>
            <div class="col-3">
                <v-img v-if="userModel.photoUrl" contain :src="userModel.photoUrl"></v-img>
                <v-img v-else src="@/assets/subPhoto.png"></v-img>
            </div>
        </v-card>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import { UserPage } from '@/interfaces/users';
    import UsersService from '@/services/UsersService';
    import { Watch } from 'vue-property-decorator';

    @Component({})
    export default class FilmPersonPageComponent extends Vue {
        private userModel : UserPage = {
            id: 0,
            userName: '',
            firstName: '',
            surname: '',
            photoUrl: '',
            isFriend: false,
            invitationSent: false,
            isInvitationSender: false,
        };

        private get id() : number {
            return Number(this.$route.params.id || 0);
        }

        async created() {
            await this.loadData(this.id);
        }

        @Watch('$route')
        async reload() {
            await this.loadData(this.id);
        }

        async loadData(id: number) {
            this.userModel = await UsersService.getForPage(id);
        }

        async inviteFriend() {
            await UsersService.inviteFriend(this.userModel.id);
            this.userModel.invitationSent = true;
            this.userModel.isInvitationSender = true;
        }

        async cancelInvitation() {
            await UsersService.cancelInvitation(this.userModel.id);
            this.userModel.invitationSent = false;
        }

        async refuseInvitation() {
            await UsersService.refuseInvitation(this.userModel.id);
            this.userModel.invitationSent = false;
            this.userModel.isFriend = false;
        }

        async acceptInvitation() {
            await UsersService.acceptInvitation(this.userModel.id);
            this.userModel.invitationSent = false;
            this.userModel.isFriend = true;
        }


        async removeFriend() {
            await UsersService.removeFriend(this.userModel.id);
            this.userModel.isFriend = false;
        }

        canInvite() : boolean {
            return (this as any).$auth.user().id != this.userModel.id
                && !this.userModel.isFriend
                && !this.userModel.invitationSent;
        }

        isAlreadyAFriend() : boolean {
            return (this as any).$auth.user().id!= this.userModel.id
                && this.userModel.isFriend;
        }

        canCancelInvitation() : boolean {
            return (this as any).$auth.user().id != this.userModel.id
                && !this.userModel.isFriend
                && this.userModel.invitationSent
                && this.userModel.isInvitationSender;
        }

        canAcceptInvitation() : boolean {
            return (this as any).$auth.user().id != this.userModel.id
                && !this.userModel.isFriend
                && this.userModel.invitationSent
                && !this.userModel.isInvitationSender;
        }
    }
</script>
