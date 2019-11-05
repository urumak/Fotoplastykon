<template>
    <v-container class="flex flex-center">
        <div>film person page</div>
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
            rank: 0,
            photoUrl: '',
            roles: [],
            filmMakings: [],
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
    }
</script>
