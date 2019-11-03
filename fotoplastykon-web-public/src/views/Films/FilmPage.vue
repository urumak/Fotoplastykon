<template>
    <v-container class="flex flex-center">
        <v-card>
            <div>film page</div>
            <v-rating
                    v-model="rating"
                    :length="10"
                    color="purple"
                    background-color="grey lighten-1"
            ></v-rating>
            <v-btn @click="rate()">Oceń</v-btn>
        </v-card>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import { FilmPage } from '@/interfaces/films';
    import FilmsService from '@/services/FilmsService';

    @Component({})
    export default class FilmPersonPageComponent extends Vue {
        private filmModel : FilmPage = {
            id: 0,
            title: '',
            yearOfProduction: 0,
            rank: 0,
            photoUrl: '',
            cast: [],
            filmmakers: [],
            forumThreads: [],
        };

        private rating: number = 0;

        private get id() : number {
            return Number(this.$route.params.id || 0);
        }

        async created() {
            await this.loadData(this.id);
        }

        async loadData(id: number) {
            this.filmModel = await FilmsService.getForPage(id);
            let response = await FilmsService.getRate(id);
            if(response) this.rating = response;
        }

        async rate() {
            await FilmsService.rate(this.id, this.rating);
        }
    }
</script>
