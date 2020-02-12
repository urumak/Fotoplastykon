<template>
    <div>
        <v-list>
            <v-list-item
                    v-if="cast.length === 0"
            >
                <v-list-item-content>
                    <v-list-item-title>Brak obsady</v-list-item-title>
                </v-list-item-content>
            </v-list-item>
            <v-list-item
                    v-for="(item, index) in cast"
                    :key="index"
                    @click="goToDetails(item.personId)"
            >
                <v-list-item-avatar>
                    <v-img v-if="item.photoUrl != null && item.photoUrl.length != 0" :src='item.photoUrl'></v-img>
                    <v-img v-else src="@/assets/subPhoto.png"></v-img>
                </v-list-item-avatar>
                <v-list-item-content>
                    <v-list-item-title>{{ item.fullName }}</v-list-item-title>
                    <v-list-item-subtitle>{{ item.characterName }}</v-list-item-subtitle>
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
    import UsersService from '@/services/UsersService';
    import { CastMember } from '@/interfaces/films';
    import FilmsService from '@/services/FilmsService';
    import CustomPagination from '@/components/CustomPagination.vue';

    @Component({components: { 'custom-pagination': CustomPagination}})
    export default class FilmCast extends Vue {

        private cast : CastMember[] = [];
        private pager : Pager = new Pager(1, 2);
        private pageSizeOptions = [2,5,10,20];
        private get id() {
            return this.filmId;
        }

        @Prop({default: 0})
        public filmId!: number;

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
            let response = await FilmsService.getCast(this.pager, this.id);
            this.cast = response.items;
            this.pager.setTotalRows(response.pager.totalRows);
        }

        @Watch('pager.pageSize')
        @Watch('pager.pageIndex')
        async paginate(index: number) {
            this.pager.setPageIndex(index);
            await this.loadData();
        }

        async goToDetails(id: number) {
            await this.$router.push({ name: 'film-person-page', params: { id: id.toString() }});
        }
    }
</script>
