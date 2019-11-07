<template>
    <v-container class="flex flex-center">
        <v-card>
            <div>{{ filmPersonModel.firstName }}</div>
            <div>{{ filmPersonModel.surname }}</div>
            <v-img v-if="filmPersonModel.photoUrl" :src="filmPersonModel.photoUrl" class="profile-photo"></v-img>
            <v-img v-else src="@/assets/bird.jpg"></v-img>
            <v-rating
                    v-model="filmPersonModel.rating"
                    :length="10"
                    color="purple"
                    background-color="grey lighten-1"
                    half-increments
                    readonly
            ></v-rating>
            <div>Średnia ocena {{ filmPersonModel.rating }}</div>
            <v-rating
                    v-model="filmPersonModel.userRating"
                    :length="10"
                    color="purple"
                    background-color="grey lighten-1"
                    small
                    hover
                    @input="rate()"
            ></v-rating>
            <div>Twoja ocena {{ filmPersonModel.userRating }}</div>
            <p>Filmy</p>
            <div v-for="item in filmPersonModel.roles" :key="'f' + item.filmId">
                <v-avatar>
                    <v-img :src="item.photoUrl"></v-img>
                </v-avatar>
                <router-link :to="{ name: 'film-page', params: { id: item.filmId }}" class="font-weight-light home-link">{{ item.filmName }} - {{ item.roleDescription }}</router-link>
            </div>
            <p>Dyskusje</p>
            <div v-for="item in filmPersonModel.forumThreads" :key="'d' + item.id">
                <v-avatar>
                    <v-img :src="item.photoUrl"></v-img>
                </v-avatar>
                <div>{{ item.subject }}</div>
                <router-link :to="{ name: 'user-page', params: { id: item.createdById }}" class="font-weight-light home-link">{{ item.createdByName }}</router-link>
                <div>{{ item.content }}</div>
            </div>
        </v-card>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import {FilmPersonPage} from '@/interfaces/filmPeople';
    import FilmPeopleService from '@/services/FilmPeopleService';
    import { Watch } from 'vue-property-decorator';

    @Component({})
    export default class FilmPersonPageComponent extends Vue {
        private filmPersonModel : FilmPersonPage = {
            id: 0,
            firstName: '',
            surname: '',
            profession: '',
            rating: 0,
            userRating: 0,
            photoUrl: '',
            roles: [],
            forumThreads: []
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
            this.filmPersonModel = await FilmPeopleService.getForPage(id);
        }

        async rate() {
            await FilmPeopleService.rate(this.id, this.filmPersonModel.userRating);
            this.filmPersonModel.rating = await FilmPeopleService.getRating(this.id);
        }
    }
</script>
