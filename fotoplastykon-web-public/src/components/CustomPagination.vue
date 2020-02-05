<template>
    <div>
        <v-pagination class="mt-5"
                      v-model="pager.pageIndex"
                      :length="pager.totalPages"
                      total-visible="5"
        ></v-pagination>
        <v-row>
            <v-col cols="10">{{ "Ilość wszystkich rekordów: " + pager.totalRows}}</v-col>
            <v-col cols="2">
                Rozmiar strony:
                <v-select class="outlined-input" :items="pageSizeOptions" v-model="pager.pageSize" solo></v-select>
            </v-col>
        </v-row>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import {Pager} from '@/interfaces/pager';
    import { Watch, Prop } from 'vue-property-decorator';
    import {FilmListItem} from '@/interfaces/films';
    import FilmsService from '@/services/FilmsService';

    @Component({})
    export default class FilmsComponent extends Vue {
        private pageSizeOptions = [2,5,10,20];

        @Prop({default: {
                pageIndex : 0,
                pageSize : 0,
                totalRows : 1,
                search:  ''
            }}) private pager!: Pager;

    }
</script>
