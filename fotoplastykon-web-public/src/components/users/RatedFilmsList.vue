<template>
    <div>
        <v-list>
            <v-list-item
                    v-if="watchedFilms.length === 0"
            >
                <v-list-item-content>
                    <v-list-item-title>Brak ocenionych filmów</v-list-item-title>
                </v-list-item-content>
            </v-list-item>
            <v-list-item
                    v-for="(item, index) in watchedFilms"
                    :key="index"
                    @click="goToDetails(item.id)"
            >
                <v-list-item-avatar>
                    <v-img v-if="item.photoUrl != null && item.photoUrl.length != 0" :src='item.photoUrl'></v-img>
                    <v-img v-else src="@/assets/subPhoto.png"></v-img>
                </v-list-item-avatar>
                <v-list-item-content>
                    <v-list-item-title>{{ item.itemName }}</v-list-item-title>
                    <v-list-item-subtitle>
                        <v-rating
                            v-model="item.mark"
                            :length="10"
                            color="purple"
                            background-color="grey lighten-1"
                            half-increments
                            readonly
                            small
                        ></v-rating>
                    </v-list-item-subtitle>
                </v-list-item-content>
            </v-list-item>
        </v-list>
        <custom-pagination :pager="pager" class="pa-5"></custom-pagination>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import { Prop, Watch } from 'vue-property-decorator';
    import {Pager} from '@/interfaces/pager';
    import { RankModel } from '@/interfaces/shared';
    import FilmsService from '@/services/FilmsService';
    import CustomPagination from '@/components/CustomPagination.vue';

    @Component({components: { 'custom-pagination': CustomPagination}})
    export default class RatedFilmsList extends Vue {

        private watchedFilms: RankModel[] = [];
        private pager : Pager = new Pager(1, 2);
        private pageSizeOptions = [2,5,10,20];
        private get id() {
            return this.userId;
        }

        @Prop({default: 0})
        public userId!: number;

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
            let response = await FilmsService.getWatchedFilms(this.pager, this.id);
            this.watchedFilms = response.items;
            this.pager.setTotalRows(response.pager.totalRows);
        }

        @Watch('pager.pageSize')
        @Watch('pager.pageIndex')
        async paginate(index: number) {
            this.pager.setPageIndex(index);
            await this.loadData();
        }

        async goToDetails(id: number) {
            await this.$router.push({ name: 'film-page', params: { id: id.toString() }});
        }
    }
</script>
