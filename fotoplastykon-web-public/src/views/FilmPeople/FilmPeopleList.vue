<template>
    <div>
        <v-row><v-text-field class="outlined-input" v-model="pager.search" label="Szukaj" solo></v-text-field></v-row>
        <div v-for="(item, index) in items" :key="index" class="row">
            <v-card class="list-item" :to="{ name: 'film-person-page', params: { id: item.id }}">
                <v-row>
                    <v-col cols="2">
                        <v-avatar class="ml-6 list-avatar">
                            <v-img v-if="item.photoUrl != null && item.photoUrl.length != 0" :src="item.photoUrl"></v-img>
                            <v-img v-else src="@/assets/subPhoto.png"></v-img>
                        </v-avatar>
                    </v-col>
                    <div class="font-weight-light">
                        <div class="list-item-title">{{ item.firstName + ' ' + item.surname }}</div>
                    </div>
                </v-row>
            </v-card>
        </div>
        <custom-pagination :pager="pager"></custom-pagination>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import QuizzesService from "@/services/QuizzesService.ts";
    import { QuizListItem } from '@/interfaces/quizes';
    import {Pager} from '@/interfaces/pager';
    import {FilmPersonListItem} from '@/interfaces/filmPeople';
    import FilmPeopleService from '@/services/FilmPeopleService';
    import { Watch } from 'vue-property-decorator';
    import CustomPagination from '@/components/CustomPagination.vue';

    @Component({components: { 'custom-pagination': CustomPagination}})
    export default class FilmPeopleListComponent extends Vue {
        private items : FilmPersonListItem[] = [];

        private get pager(): Pager
        {
            return this.$store.state.filmPeople.pager;
        }

        async created() {
            await this.loadData();
        }

        @Watch('pager.search')
        async filter() {
            this.pager.pageIndex = 1;
            this.paginate()
        }

        async loadData() {
            if(this.pager.pageIndex > this.pager.totalPages) this.pager.setPageIndex(this.pager.totalPages);
            let response = await FilmPeopleService.getList(this.pager);
            this.items = response.items;
            this.pager.setTotalRows(response.pager.totalRows);
        }

        @Watch('pager.pageSize')
        @Watch('pager.pageIndex')
        async paginate() {
            await this.loadData();
            window.scrollTo(0,0);
        }
    }
</script>
