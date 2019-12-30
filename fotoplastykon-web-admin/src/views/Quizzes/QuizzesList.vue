<template>
    <v-container class="flex flex-center">
        <v-card>
            <v-data-table
                    :headers="headers"
                    :items="items"
                    :options.sync="options"
                    :server-items-length="pager.totalRows"
                    :loading="loading"
                    class="elevation-1"
                    loading-text="Ładowanie..."
                    :footer-props="footerProps"
            >
                <template v-slot:top>
                    <v-toolbar flat color="white">
                        <v-toolbar-title>Quizy</v-toolbar-title>
                        <v-divider
                                class="mx-4"
                                inset
                                vertical
                        ></v-divider>
                        <v-text-field v-model="pager.search" label="Szukaj" class="mt-8 mr-12"></v-text-field>
                        <v-btn color="primary" dark class="mb-2" @click="$router.push({ name: 'quizzes-add' })">Dodaj</v-btn>
                    </v-toolbar>
                </template>
                <template v-slot:item.photo="{ item }">
                    <v-avatar>
                        <v-img v-if="item.photoUrl" :src="item.photoUrl"></v-img>
                        <v-img v-else src="@/assets/subPhoto.png"></v-img>
                    </v-avatar>
                </template>
                <template v-slot:item.action="{ item }">
                    <v-icon
                            small
                            class="mr-2"
                            @click="editItem(item.id)"
                    >
                        mdi-note
                    </v-icon>
                    <v-menu
                            offset-y
                            transition="slide-y-transition"
                            :close-on-content-click="true"
                            :nudge-width="200">
                        <template v-slot:activator="{ on }">
                            <v-icon small v-on="on">mdi-close</v-icon>
                        </template>
                        <div>
                            Czy na pewno chcesz usunąć rekord?
                            <v-btn class="primary" @click="deleteItem(item.id)">Tak</v-btn>
                            <v-btn class="secondary">Nie</v-btn>
                        </div>
                    </v-menu>
                </template>
                <template v-slot:no-data>
                    Brak danych
                </template>
            </v-data-table>
        </v-card>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import { Pager } from '@/interfaces/pager';
    import {Watch} from "vue-property-decorator";
    import tableFooterConfiguration from "@/tableFooterConfiguration.json"
    import {QuizListItem} from "@/interfaces/quizes";
    import QuizzesService from "@/services/QuizzesService";

    @Component({})
    export default class QuizzesListComponent extends Vue {
        private items: QuizListItem[] = [];
        private pager = new Pager(1, 5);
        private loading = false;
        private options: any = {
            page: 1,
            itemsPerPage: 5
        };

        private footerProps = tableFooterConfiguration;

        private headers = [
            {
                text: 'Id',
                align: 'left',
                sortable: false,
                value: 'id',
            },
            { text: '', value: 'photo', sortable: false },
            { text: 'Nazwa', value: 'name', sortable: false },
            { text: '', value: 'action', sortable: false }
        ];

        async created() {
            await this.loadData();
        }

        @Watch('options')
        @Watch('pager.search')
        async loadData() {
            this.loading = true;
            this.pager.pageIndex = this.options.page;
            this.pager.pageSize = this.options.itemsPerPage;
            if(this.pager.pageIndex > this.pager.totalPages) this.pager.setPageIndex(this.pager.totalPages);
            let response = await QuizzesService.getList(this.pager);
            this.items = response.items;
            this.pager.setTotalRows(response.pager.totalRows);
            window.scrollTo(0,0);
            this.loading = false;
        }

        async editItem(id: number) {
            await this.$router.push({ name: 'quizzes-edit', params: {id: id.toString()} });
        }

        async deleteItem(id: number) {
            //await QuizzesService.delete(id);
            await this.loadData();
        }
    }
</script>
