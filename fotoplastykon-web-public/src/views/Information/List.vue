<template>
    <v-container class="flex flex-center">
        <v-row>
            <div class="col-8">
                <div v-for="item in items" :key="item.id" class="row">
                    <v-card class="my-card col-8">
                        <div class="card-title">
                            {{ item.title }}
                        </div>
                        <div class="card-title-line"></div>
                        <div class="news-card-content">
                            {{ item.introduction }}
                        </div>
                    </v-card>
                    <v-img class="news-img" :src="item.photoUrl" contain>
                    </v-img>
                </div>
                <v-btn v-for="i in pager.totalPages" :key="'p' + i" @click="paginate(i)">{{i}}</v-btn>
                <v-select :items="pageSizeOptions" v-model="pager.pageSize" solo @change="changePageSize()"></v-select>
            </div>
            <v-col></v-col>
            <div class="float-right col-3 right-slider">
                <v-card class="vertical-slider"></v-card>
            </div>
        </v-row>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import InformationService from "@/services/InformationService.ts";
    import { InformationModel } from '@/interfaces/information';
    import {Pager} from '@/interfaces/pager';
    import merge from 'lodash/merge'

    @Component({})
    export default class MainPageComponent extends Vue {
        private items : InformationModel[] = [];
        private pageSizeOptions = [2,5,10,20];

        private get pager(): Pager
        {
            return this.$store.state.information.pager;
        }

        async created() {
            this.loadData();
        }

        async loadData() {
            if(this.pager.pageIndex > this.pager.totalPages) this.pager.setPageIndex(this.pager.totalPages);
            let response = await InformationService.getList(this.pager);
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
