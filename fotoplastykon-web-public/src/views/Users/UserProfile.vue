<template>
    <v-container class="flex flex-center">
        <v-card class="container-item main-card">
            <v-row>
                <v-col cols="6">
                    <v-avatar class="container-item-avatar ml-3">
                        <v-img v-if="photoUrl || $auth.user().photoUrl" :src="photoUrl ? photoUrl : $auth.user().photoUrl"></v-img>
                        <v-img v-else src="@/assets/subPhoto.png"></v-img>
                    </v-avatar>
                </v-col>
                <v-col>
                    <v-file-input
                            accept="image/png, image/jpeg, image/bmp"
                            placeholder="Wybierz zdjęcie profilowe"
                            prepend-icon="mdi-camera"
                            label="Zdjęcie profilowe"
                            v-model="newPhoto"
                    ></v-file-input>
                </v-col>
            </v-row>
            <v-row>
                <v-col cols="6">
                    <v-text-field v-model="model.userName" label="Nazwa użytkownika" :error-messages="errors['UserName']"></v-text-field>
                    <v-text-field v-model="model.firstName" label="Imię" :error-messages="errors['FirstName']"></v-text-field>
                    <v-text-field v-model="model.surname" label="Nazwisko" :error-messages="errors['Surname']"></v-text-field>
                    <v-text-field v-model="model.email" label="Adres email" :error-messages="errors['Email']"></v-text-field>
                    <v-btn class="primary mt-5" @click="update()">Zapisz</v-btn>
                </v-col>
                <v-col cols="6" class="float-right">
                    <v-text-field v-model="model.password" type="password" label="Hasło" :error-messages="errors['Password']"></v-text-field>
                    <v-text-field v-model="model.repeatPassword" type="password" label="Powtórz hasło" :error-messages="errors['RepeatPassword']"></v-text-field>
                    <v-subheader>Jeśli nie chcesz zmianiać hasła pozostaw powyższe pola puste</v-subheader>
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
    import {RegisterModel} from '@/interfaces/auth';

    @Component({components: {
            'friends-list': FriendsList,
            'rated-films-list': RatedFilmsList,
            'rated-people-list': RatedPeopleList
        }})
    export default class UserProfileComponent extends Vue {
        private errors = {};
        private model: RegisterModel = {
            surname: '',
            userName: '',
            firstName: '',
            password: '',
            repeatPassword: '',
            email: ''
        };

        private newPhoto = [];

        private get photoUrl() {
            return this.$store.state.user.photoUrl;
        }

        created() {
            this.model = {
                surname: (this as any).$auth.user().surname,
                userName: (this as any).$auth.user().userName,
                firstName: (this as any).$auth.user().firstName,
                password: '',
                repeatPassword: '',
                email: (this as any).$auth.user().email
            };
        }

        async update() {
            try {
                await AccountService.update(this.model);
                if(this.newPhoto && this.newPhoto.length !== 0) await AccountService.changeProfilePhoto(this.newPhoto)
                await (this as any).$auth.user(await AuthService.profile());

                this.$store.state.user.photoUrl = (this as any).$auth.user().photoUrl;
                this.newPhoto = [];
                this.$forceUpdate();
                this.$store.state.alert = {
                    show: true,
                    type: 'success',
                    message: 'Zmiany zostały zapisane'
                };
            } catch(ex) {
                if (ex.code === 400) this.errors = ex.data.errors;
            }
        }
    }
</script>
