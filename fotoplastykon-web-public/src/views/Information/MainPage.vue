<template>
    <v-container class="flex flex-center">
        <v-row>
            <div class="col-8">
                <div v-for="(item, i) in Items" :key="item.id" class="row">
                    <v-card v-if="i % 2 !== 0" class="my-card col-8">
                        <div class="card-title">
                            {{ item.title }}
                        </div>
                        <div class="card-title-line"></div>
                        <div class="news-card-content">
                            {{ item.introduction }}
                        </div>
                    </v-card>
                    <v-img v-if="i % 2 !== 0" class="news-img" :src="item.photoUrl" contain>
                    </v-img>
                    <v-img v-if="i % 2 === 0" class="news-img-reflection" :src="item.photoUrl" contain></v-img>
                    <v-card v-if="i % 2 === 0" class="my-card col-8">
                        <div class="card-title">
                            {{ item.title }}
                        </div>
                        <div class="card-title-line"></div>
                        <div class="news-card-content-reflection">
                            {{ item.introduction }}
                        </div>
                    </v-card>
                </div>
            </div>
            <v-col> </v-col>
            <div class="float-right col-3 right-slider">
                <v-card class="vertical-slider"></v-card>
            </div>
        </v-row>
        <router-link :to="{ name: 'information-list' }">Zobacz wszystkie aktualności</router-link>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import InformationService from "@/services/InformationService.ts";
    import { InformationModel } from '@/interfaces/information';

    @Component({})
    export default class MainPageComponent extends Vue {
        private Items : InformationModel[] = [];

        async created() {
            this.loadData();
        }

        async loadData() {
            this.Items = await InformationService.getListForMainPage();
        }
    }
</script>
