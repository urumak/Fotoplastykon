<template>
    <div>
        <v-card class="container-item main-card">
            <v-row>
                <v-col cols="7">
                    <div>{{ filmModel.title + "(" + filmModel.yearOfProduction + ")"}}</div>
                    <v-avatar class="container-item-avatar">
                        <v-img v-if="filmModel.photoUrl" :src="filmModel.photoUrl"></v-img>
                        <v-img v-else src="@/assets/subPhoto.png"></v-img>
                    </v-avatar>
                </v-col>
                <v-col cols="5">
                    <div class="rating-description">Średnia ocena: {{ filmModel.rating }}</div>
                        <v-rating
                            v-model="filmModel.rating"
                            :length="10"
                            color="purple"
                            background-color="grey lighten-1"
                            half-increments
                            readonly
                        ></v-rating>
                    <div class="rating-description">Twoja ocena: {{ filmModel.userRating }}</div>
                    <v-rating
                            v-model="filmModel.userRating"
                            :length="10"
                            color="purple"
                            background-color="grey lighten-1"
                            small
                            hover
                            @input="rate()"
                    ></v-rating>
                </v-col>
            </v-row>
        </v-card>
        <v-card class="container-item">
            <v-tabs>
                <v-tabs-slider></v-tabs-slider>
                <v-tab :href="`#tab-cast`">Obsada</v-tab>
                <v-tab :href="`#tab-film-makers`">Twórcy</v-tab>
                <v-tab :href="`#tab-film-forum`">Forum</v-tab>
                <v-tab-item :value="'tab-cast'">
                    <film-cast :filmId="id"></film-cast>
                </v-tab-item>
                <v-tab-item :value="'tab-film-makers'">
                    <film-makers :filmId="id"></film-makers>
                </v-tab-item>
                <v-tab-item :value="'tab-film-forum'">
                    <film-forum :filmId="id"></film-forum>
                </v-tab-item>
            </v-tabs>
        </v-card>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import { FilmPage } from '@/interfaces/films';
    import FilmsService from '@/services/FilmsService';
    import {Watch} from 'vue-property-decorator';
    import FilmCast from '@/components/films/FilmCast.vue';
    import FilmMakers from '@/components/films/FilmMakers.vue';
    import FilmForum from '@/components/films/FilmForum.vue';

    @Component({components: {
            'film-cast': FilmCast,
            'film-makers': FilmMakers,
            'film-forum': FilmForum
        }})
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
