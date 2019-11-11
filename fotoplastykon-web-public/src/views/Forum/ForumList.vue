<template>
    <v-container class="flex flex-center">
        <v-row>
            <div class="col-8">
                <div v-for="item in items" :key="item.id" class="row">
                    <v-card class="my-card col-8">
                        <v-avatar>
                            <v-img :src="item.photoUrl"></v-img>
                        </v-avatar>
                        <div>{{ item.subject }}</div>
                        <router-link :to="{ name: 'user-page', params: { id: item.createdById }}" class="font-weight-light home-link">{{ item.createdByFullName }}</router-link>
                        <div>{{ item.content }}</div>
                    </v-card>
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
    import ForumService from "@/services/ForumService.ts";
    import { ForumThreadCommentModel, ForumThreadModel } from '@/interfaces/forum';
    import {Pager} from '@/interfaces/pager';
    import merge from 'lodash/merge'

    @Component({})
    export default class InformationListComponent extends Vue {
        private items : ForumThreadModel[] = [];
        private pageSizeOptions = [2,5,10,20];

        private get pager(): Pager
        {
            return this.$store.state.forum.pager;
        }

        async created() {
            await this.loadData();
        }

        async loadData() {
            if(this.pager.pageIndex > this.pager.totalPages) this.pager.setPageIndex(this.pager.totalPages);
            let response = await ForumService.getList(this.pager);
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
