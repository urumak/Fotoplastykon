<template>
    <div>
        <v-list>
            <v-list-item
                    v-if="items.length === 0"
            >
                <v-list-item-content>
                    <v-list-item-title>Brak filmów</v-list-item-title>
                </v-list-item-content>
            </v-list-item>
            <v-list-item
                    v-for="(item, index) in items"
                    :key="index"
                    @click="goToDetails(item.filmId)"
            >
                <v-list-item-avatar>
                    <v-img v-if="item.photoUrl != null && item.photoUrl.length != 0" :src='item.photoUrl'></v-img>
                    <v-img v-else src="@/assets/subPhoto.png"></v-img>
                </v-list-item-avatar>
                <v-list-item-content>
                    <v-list-item-title>{{ item.filmName + ' (' + item.yearOfProduction + ')' }}</v-list-item-title>
                    <v-list-item-subtitle>{{ item.roleDescription }}</v-list-item-subtitle>
                </v-list-item-content>
            </v-list-item>
        </v-list>
        <v-btn v-for="i in pager.totalPages" :key="'p' + i" @click="paginate(i)">{{i}}</v-btn>
        <v-select :items="pageSizeOptions" v-model="pager.pageSize" solo @change="loadData()"></v-select>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import { Prop, Watch } from 'vue-property-decorator';
    import {Pager} from '@/interfaces/pager';
    import FilmPeopleService from '@/services/FilmPeopleService';
    import {RoleInFilm} from "../../interfaces/filmPeople";

    @Component({})
    export default class FilmPersonRolesComponent extends Vue {

        private items : RoleInFilm[] = [];
        private pager : Pager = new Pager(1, 2);
        private pageSizeOptions = [2,5,10,20];
        private get id() {
            return this.personId;
        }

        @Prop({default: 0})
        public personId!: number;

        @Watch('$route')
        async reload() {
            this.pager = new Pager(1, 2);
            await this.loadData();
        }

        async created() {
            this.loadData();
        }

        async loadData() {
            if(this.pager.pageIndex > this.pager.totalPages) this.pager.setPageIndex(this.pager.totalPages);
            let response = await FilmPeopleService.getPersonRoles(this.pager, this.id);
            this.items = response.items;
            this.pager.setTotalRows(response.pager.totalRows);
        }

        async paginate(index: number) {
            this.pager.setPageIndex(index);
            await this.loadData();
        }

        async goToDetails(id: number) {
            console.log(id);
            await this.$router.push({ name: 'film-page', params: { id: id.toString() }});
        }
    }
</script>
