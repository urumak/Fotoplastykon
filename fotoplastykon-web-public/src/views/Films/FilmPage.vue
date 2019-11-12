<template>
    <v-container class="flex flex-center">
        <v-card>
            <div>{{ filmModel.title }}</div>
            <div>{{ filmModel.yearOfProduction }}</div>
            <div class="col-3">
                <v-img v-if="filmModel.photoUrl" :src="filmModel.photoUrl"></v-img>
                <v-img v-else src="@/assets/subPhoto.png"></v-img>
            </div>
            <v-rating
                    v-model="filmModel.rating"
                    :length="10"
                    color="purple"
                    background-color="grey lighten-1"
                    half-increments
                    readonly
            ></v-rating>
            <div>Średnia ocena {{ filmModel.rating }}</div>
            <v-rating
                    v-model="filmModel.userRating"
                    :length="10"
                    color="purple"
                    background-color="grey lighten-1"
                    small
                    hover
                    @input="rate()"
            ></v-rating>
            <div>Twoja ocena {{ filmModel.userRating }}</div>
            <p>Obsada</p>
            <div v-for="item in filmModel.cast" :key="'c' + item.personId">
                <v-avatar>
                    <v-img :src="item.photoUrl"></v-img>
                </v-avatar>
                <router-link :to="{ name: 'film-person-page', params: { id: item.personId }}" class="font-weight-light home-link">{{ item.fullName }} - {{ item.characterName }}</router-link>
            </div>
            <p>Twórcy</p>
            <div v-for="item in filmModel.filmmakers" :key="'m' + item.personId">
                <v-avatar>
                    <v-img :src="item.photoUrl"></v-img>
                </v-avatar>
                <router-link :to="{ name: 'film-person-page', params: { id: item.personId }}" class="font-weight-light home-link">{{ item.fullName }} - {{ item.profession }}</router-link>
            </div>
            <p>Dyskusje</p>
            <div v-for="item in filmModel.forumThreads" :key="'d' + item.id">
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
    import { FilmPage } from '@/interfaces/films';
    import FilmsService from '@/services/FilmsService';
    import {Watch} from 'vue-property-decorator';

    @Component({})
    export default class FilmPersonPageComponent extends Vue {
        private filmModel : FilmPage = {
            id: 0,
            title: '',
            yearOfProduction: 0,
            rating: 0,
            userRating: 0,
            photoUrl: '',
            cast: [],
            filmmakers: [],
            forumThreads: [],
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
            this.filmModel = await FilmsService.getForPage(id);
        }

        async rate() {
            await FilmsService.rate(this.id, this.filmModel.userRating);
            this.filmModel.rating = await FilmsService.getRating(this.id);
        }
    }
</script>
