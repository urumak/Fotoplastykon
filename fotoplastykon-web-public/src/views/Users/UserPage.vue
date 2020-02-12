<template>
    <v-container class="flex flex-center">
        <v-card class="container-item main-card">
            <div class="main-card-title">{{ userModel.firstName + " " + userModel.surname }}</div>
            <v-row>
                <v-col cols="6">
                    <v-avatar class="container-item-avatar">
                        <v-img v-if="userModel.photoUrl" :src="userModel.photoUrl"></v-img>
                        <v-img v-else src="@/assets/subPhoto.png"></v-img>
                    </v-avatar>
                </v-col>
                <v-col cols="6">
                    <v-btn small class="float-right" v-if="isAlreadyAFriend()" @click="removeFriend()"><v-icon left>mdi-delete</v-icon> Usuń z listy znajomych</v-btn>
                    <v-btn small class="float-right" v-if="canCancelInvitation()" @click="cancelInvitation()"><v-icon left>mdi-close</v-icon> Anuluj zaproszenie</v-btn>
                    <v-btn small class="float-right" v-if="canAcceptInvitation()" @click="acceptInvitation()"><v-icon left>mdi-done</v-icon> Przyjmij zaproszenie</v-btn>
                    <v-btn small class="float-right" v-if="canAcceptInvitation()" @click="refuseInvitation()"><v-icon left>mdi-close</v-icon> Odrzuć zaproszenie</v-btn>
                    <v-btn small class="float-right" v-if="canInvite()" @click="inviteFriend()"><v-icon left>mdi-plus</v-icon> Dodaj do znajomych</v-btn>
                </v-col>
            </v-row>
        </v-card>
        <v-card class="container-item">
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
