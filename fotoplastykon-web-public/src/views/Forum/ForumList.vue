<template>
    <div>
        <v-row>
            <v-col cols="10">
                <v-text-field class="outlined-input" v-model="pager.search" label="Szukaj" solo></v-text-field>
            </v-col>
            <v-col cols="2">
                <v-btn :to="{ name: 'forum-thread', params: { id: 0 }}" class="float-right primary mt-1"><v-icon left>mdi-plus</v-icon>&nbsp Dodaj</v-btn>
            </v-col>
        </v-row>
        <div v-for="(item, index) in items" :key="index" class="row">
            <v-card class="list-item" :to="{ name: 'forum-thread', params: { id: item.id }}">
                <v-row class="ml-5">
                    <v-col cols="1">
                        <v-avatar>
                            <v-img v-if="item.photoUrl != null && item.photoUrl.length != 0" :src="item.photoUrl"></v-img>
                            <v-img v-else src="@/assets/subPhoto.png"></v-img>
                        </v-avatar>
                        <div style="margin:auto" class="font-weight-light row">{{ item.createdByName }}</div>
                    </v-col>
                    <v-col cols="10">
                        <v-row>
                            <v-col cols="8">
                                <div class="col-10 font-weight-light">{{ item.subject }}</div>
                            </v-col>
                        </v-row>
                        <div>
                            {{ item.content }}
                        </div>
                    </v-col>
                </v-row>
            </v-card>
        </div>
        <custom-pagination :pager="pager"></custom-pagination>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import ForumService from "@/services/ForumService.ts";
    import { ForumThreadCommentModel, ForumThreadModel } from '@/interfaces/forum';
    import {Pager} from '@/interfaces/pager';
    import merge from 'lodash/merge'
    import { Watch } from 'vue-property-decorator';
    import CustomPagination from '@/components/CustomPagination.vue';

    @Component({components: { 'custom-pagination': CustomPagination}})
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

        async deleteItem(id: number) {
            await ForumService.remove(id);
            await this.loadData();
        }

        @Watch('pager.pageSize')
        @Watch('pager.pageIndex')
        async paginate() {
            await this.loadData();
            window.scrollTo(0,0);
        }

        canDelete(item: ForumThreadModel): boolean {
            return (this as any).$auth.user().id === item.createdById;
        }
    }
</script>
