<template>
    <v-container class="flex flex-center">
        <v-card>
            <div>film page</div>
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

        private get id() : number {
            return Number(this.$route.params.id || 0);
        }

        async created() {
            await this.loadData(this.id);
        }

        async loadData(id: number) {
            this.filmModel = await FilmsService.getForPage(id);
            console.log(this.filmModel);
        }
    }
</script>
