<template>
    <v-container class="flex flex-center">
        <v-card>
            <div>{{ $auth.user().firstName }}</div>
            <div>{{ $auth.user().surname }}</div>
            <div>{{ $auth.user().userName }}</div>
            <div class="col-3">
                <v-img v-if="photoUrl || $auth.user().photoUrl" contain :src="photoUrl ? photoUrl : $auth.user().photoUrl"></v-img>
                <v-img v-else src="@/assets/subPhoto.png"></v-img>
            </div>
            <v-file-input
                    accept="image/png, image/jpeg, image/bmp"
                    placeholder="Wybierz zdjęcie profilowe"
                    prepend-icon="mdi-camera"
                    label="Zdjęcie profilowe"
                    outlined
                    v-model="newPhoto"
            ></v-file-input>
            <v-btn class="primary" @click="changeProfilePhoto()">Zmień zdjęcie</v-btn>
            <v-tabs>
                <v-tabs-slider></v-tabs-slider>
                <v-tab :href="`#tab-films`">Ocenione filmy</v-tab>
                <v-tab :href="`#tab-people`">Ocenione osoby</v-tab>
                <v-tab :href="`#tab-friends`">Znajomi</v-tab>
                <v-tab-item :value="'tab-films'">
                    <rated-films-list :userId="$auth.user().id"></rated-films-list>
                </v-tab-item>
                <v-tab-item :value="'tab-people'">
                    <rated-people-list :userId="$auth.user().id"></rated-people-list>
                </v-tab-item>
                <v-tab-item :value="'tab-friends'">
                    <friends-list :userId="$auth.user().id"></friends-list>
                </v-tab-item>
            </v-tabs>
        </v-card>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import AccountService from '@/services/AccountService';
    import AuthService from '@/services/AuthService';
    import FriendsList from '@/components/users/FriendsList.vue';
    import RatedFilmsList from '@/components/users/RatedFilmsList.vue';
    import RatedPeopleList from '@/components/users/RatedPeopleList.vue';

    @Component({components: {
            'friends-list': FriendsList,
            'rated-films-list': RatedFilmsList,
            'rated-people-list': RatedPeopleList
        }})
    export default class UserProfileComponent extends Vue {
        private newPhoto = [];

        private get photoUrl() {
            return this.$store.state.user.photoUrl;
        }

        async changeProfilePhoto() {
            await AccountService.changeProfilePhoto(this.newPhoto);
            await (this as any).$auth.user(await AuthService.profile())

            this.$store.state.user.photoUrl = (this as any).$auth.user().photoUrl;
            (this as any).$forceUpdate();
        }
    }
</script>
