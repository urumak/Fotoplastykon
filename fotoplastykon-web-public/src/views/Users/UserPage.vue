<script src="../../interfaces/shared.ts"></script>
<template>
    <v-container class="flex flex-center">
        <v-card>
            <div>{{ userModel.firstName }}</div>
            <div>{{ userModel.surname }}</div>
            <div>{{ userModel.userName }}</div>
            <div class="col-3">
                <v-img v-if="userModel.photoUrl" contain :src="userModel.photoUrl"></v-img>
                <v-img v-else src="@/assets/bird.jpg"></v-img>
            </div>
            <p>Ocenione filmy</p>
            <div v-for="item in userModel.watchedFilms" :key="'c' + item.id">
                <v-avatar>
                    <v-img :src="item.photoUrl"></v-img>
                </v-avatar>
                <router-link :to="{ name: 'film-page', params: { id: item.id }}" class="font-weight-light home-link">{{ item.itemName }}</router-link>
                <v-rating
                        v-model="item.mark"
                        :length="10"
                        color="purple"
                        background-color="grey lighten-1"
                        half-increments
                        readonly
                ></v-rating>
                <div>{{ item.mark }}</div>
            </div>
            <p>Ocenione osoby</p>
            <div v-for="item in userModel.ratedPeople" :key="'p' + item.id">
                <v-avatar>
                    <v-img :src="item.photoUrl"></v-img>
                </v-avatar>
                <router-link :to="{ name: 'film-person-page', params: { id: item.id }}" class="font-weight-light home-link">{{ item.itemName }}</router-link>
                <v-rating
                        v-model="item.mark"
                        :length="10"
                        color="purple"
                        background-color="grey lighten-1"
                        half-increments
                        readonly
                ></v-rating>
                <div>{{ item.mark }}</div>
            </div>
        </v-card>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import { UserPage } from '@/interfaces/users';
    import UsersService from '@/services/UsersService';
    import { Watch } from 'vue-property-decorator';

    @Component({})
    export default class FilmPersonPageComponent extends Vue {
        private userModel : UserPage = {
            id: 0,
            userName: '',
            firstName: '',
            surname: '',
            photoUrl: '',
            watchedFilms: [],
            ratedPeople: []
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
            console.log(this.userModel);
        }
    }
</script>
