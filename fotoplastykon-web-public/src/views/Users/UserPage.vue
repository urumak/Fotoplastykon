<template>
    <v-container class="flex flex-center">
        <v-card>
            <div>{{ userModel.firstName }}</div>
            <div>{{ userModel.surname }}</div>
            <div>{{ userModel.userName }}</div>
            <v-avatar height="300" width="230" :tile="true">
                <v-img v-if="userModel.photoUrl" :src="userModel.photoUrl"></v-img>
                <v-img v-else src="@/assets/subPhoto.png"></v-img>
            </v-avatar>
            <v-btn v-if="isAlreadyAFriend()" @click="removeFriend()">Usuń z listy znajomych</v-btn>
            <v-btn v-if="canCancelInvitation()" @click="cancelInvitation()">Anuluj zaproszenie</v-btn>
            <v-btn v-if="canAcceptInvitation()" @click="acceptInvitation()">Przyjmij zaproszenie</v-btn>
            <v-btn v-if="canAcceptInvitation()" @click="refuseInvitation()">Odrzuć zaproszenie</v-btn>
            <v-btn v-if="canInvite()" @click="inviteFriend()">Dodaj do znajomych</v-btn>
            <v-tabs>
                <v-tabs-slider></v-tabs-slider>
                <v-tab :href="`#tab-films`">Ocenione filmy</v-tab>
                <v-tab :href="`#tab-people`">Ocenione osoby</v-tab>
                <v-tab :href="`#tab-friends`">Znajomi</v-tab>
                <v-tab-item :value="'tab-films'">
                    <rated-films-list :userId="id"></rated-films-list>
                </v-tab-item>
                <v-tab-item :value="'tab-people'">
                    <rated-people-list :userId="id"></rated-people-list>
                </v-tab-item>
                <v-tab-item :value="'tab-friends'">
                    <friends-list :userId="id"></friends-list>
                </v-tab-item>
            </v-tabs>
        </v-card>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import { UserPage } from '@/interfaces/users';
    import UsersService from '@/services/UsersService';
    import { Watch } from 'vue-property-decorator';
    import FriendsList from '@/components/users/FriendsList.vue';
    import RatedPeopleList from '@/components/users/RatedPeopleList.vue';
    import RatedFilmsList from '@/components/users/RatedFilmsList.vue';

    @Component({components: {
            'friends-list': FriendsList,
            'rated-films-list': RatedFilmsList,
            'rated-people-list': RatedPeopleList
        }})
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
