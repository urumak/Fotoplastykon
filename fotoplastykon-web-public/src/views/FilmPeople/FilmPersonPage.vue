<template>
    <div>
        <v-card class="container-item main-card">
            <v-row>
                <v-col cols="7">
                    <div class="main-card-title">{{ filmPersonModel.firstName + " " + filmPersonModel.surname }}</div>
                    <v-avatar class="container-item-avatar">
                        <v-img v-if="filmPersonModel.photoUrl" :src="filmPersonModel.photoUrl"></v-img>
                        <v-img v-else src="@/assets/subPhoto.png"></v-img>
                    </v-avatar>
                </v-col>
                <v-col cols="5">
                    <div class="rating-description">Średnia ocena: {{ filmPersonModel.rating }}</div>
                    <v-rating
                            v-model="filmPersonModel.rating"
                            :length="10"
                            color="purple"
                            background-color="grey lighten-1"
                            half-increments
                            readonly
                    ></v-rating>
                    <div class="rating-description">Twoja ocena: {{ filmPersonModel.userRating }}</div>
                    <v-rating
                            v-model="filmPersonModel.userRating"
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
                <v-tab :href="`#tab-roles`">Filmy</v-tab>
                <v-tab :href="`#tab-film-forum`">Forum</v-tab>
                <v-tab-item :value="'tab-roles'">
                    <film-person-roles :personId="id"></film-person-roles>
                </v-tab-item>
                <v-tab-item :value="'tab-film-forum'">
                    <film-person-forum :personId="id"></film-person-forum>
                </v-tab-item>
            </v-tabs>
        </v-card>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import {FilmPersonPage} from '@/interfaces/filmPeople';
    import FilmPeopleService from '@/services/FilmPeopleService';
    import { Watch } from 'vue-property-decorator';
    import FilmPersonRoles from '@/components/filmPeople/FilmPersonRoles.vue';
    import FilmPersonForum from '@/components/filmPeople/FilmPersonForum.vue';

    @Component({components: {
            'film-person-roles': FilmPersonRoles,
            'film-person-forum': FilmPersonForum
        }})
    export default class FilmPersonPageComponent extends Vue {
        private filmPersonModel : FilmPersonPage = {
            id: 0,
            firstName: '',
            surname: '',
            profession: '',
            rating: 0,
            userRating: 0,
            photoUrl: ''
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
