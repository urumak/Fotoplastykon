<template>
    <div style="padding-left: 10%;padding-right: 10%;">
        <div v-for="(item, i) in Items" :key="item.id" class="row">
            <v-card v-if="i % 2 !== 0" class="col-xl-8 col-md-8 col-sm-12 news-card" :to="{ name: 'information-details', params: { id: item.id}}">
                <div class="card-title">
                    {{ item.title }}
                </div>
                <div class="card-title-line"></div>
                <div class="news-card-content col-sm-12">
                    {{ item.introduction }}
                </div>
            </v-card>
            <v-img v-if="i % 2 !== 0" class="news-img" :src="item.photoUrl" contain>
            </v-img>
            <v-img v-if="i % 2 === 0" class="news-img-reflection" :src="item.photoUrl" contain></v-img>
            <v-card v-if="i % 2 === 0" class="col-xl-8 col-md-8 col-sm-12 news-card" :to="{ name: 'information-details', params: { id: item.id}}">
                <div class="card-title">
                    {{ item.title }}
                </div>
                <div class="card-title-line"></div>
                <div class="news-card-content-reflection col-sm-12">
                    {{ item.introduction }}
                </div>
            </v-card>
        </div>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import InformationService from "@/services/InformationService.ts";
    import { InformationListModel } from '@/interfaces/information';

    @Component({})
    export default class HomeComponent extends Vue {
        private Items : InformationListModel[] = [];

        async created() {
            await this.loadData();
        }

        async loadData() {
            this.Items = await InformationService.getListForMainPage();
        }
    }
</script>
