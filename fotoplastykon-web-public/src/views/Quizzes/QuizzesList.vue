<template>
    <div>
        <v-row><v-text-field v-model="pager.search" label="Szukaj" solo></v-text-field></v-row>
        <div v-for="item in items" :key="item.id" class="row">
            <v-card class="my-card">
                <v-row>
                    <v-col cols="2">
                        <v-avatar class="ml-6 list-avatar">
                            <v-img v-if="item.photoUrl != null && item.photoUrl.length != 0" :src="item.photoUrl"></v-img>
                            <v-img v-else src="@/assets/subPhoto.png"></v-img>
                        </v-avatar>
                    </v-col>
                    <router-link :to="{ name: 'quiz', params: { id: item.id }}" class="font-weight-light custom-link">{{ item.name }}</router-link>
                </v-row>
            </v-card>
        </div>
        <v-btn v-for="i in pager.totalPages" :key="'p' + i" @click="paginate(i)">{{i}}</v-btn>
        <v-select :items="pageSizeOptions" v-model="pager.pageSize" solo @change="changePageSize()"></v-select>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import QuizzesService from "@/services/QuizzesService.ts";
    import { QuizListItem } from '@/interfaces/quizes';
    import {Pager} from '@/interfaces/pager';
    import { Watch } from 'vue-property-decorator';

    @Component({})
    export default class QuizzesListComponent extends Vue {
        private items : QuizListItem[] = [];
        private pageSizeOptions = [2,5,10,20];

        private get pager(): Pager
        {
            return this.$store.state.quizzes.pager;
        }

        async created() {
            await this.loadData();
        }

        @Watch('pager.search')
        async filter() {
            this.pager.pageIndex = 1;
            await this.loadData();
        }

        async loadData() {
            if(this.pager.pageIndex > this.pager.totalPages) this.pager.setPageIndex(this.pager.totalPages);
            let response = await QuizzesService.getList(this.pager);
            this.items = response.items;
            this.pager.setTotalRows(response.pager.totalRows);
        }

        async paginate(index: number) {
            this.pager.setPageIndex(index);
            await this.loadData();
            window.scrollTo(0,0);
        }

        async changePageSize() {
            await this.loadData();
            window.scrollTo(0,0);
        }
    }
</script>
