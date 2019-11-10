<template>
    <v-container class="flex flex-center">
        <v-row>
            <div class="col-8">
                <v-card class="my-card">
                    <div>
                        {{ model.title }}
                    </div>
                    <div>
                        {{ model.createdByName }}
                    </div>
                    <div>
                        {{ model.dateCreated }}
                    </div>
                    <div class="card-title-line"></div>
                    <div>
                        {{ model.introduction }}
                    </div>
                    <v-img :src="model.photoUrl" contain>
                    </v-img>
                    <div>
                        {{ model.content }}
                    </div>
                    <v-textarea label="Dodaj komentarz" auto-grow outlined rows="5" row-height="15"></v-textarea>
                    <v-card v-for="item in model.comments" :key="item.id">
                        <v-avatar>
                            <v-img :src="item.photoUrl" contain>
                            </v-img>
                        </v-avatar>
                        <router-link :to="{ name: 'user-page', params: { id: item.createdById }}" class="font-weight-light home-link">{{ item.creatorFullName }}</router-link>
                        <div>{{item.dateCreated}}</div>
                        <div>{{item.content}}</div>
                        <v-card v-for="reply in item.replies" :key="reply.id">
                            <v-avatar>
                                <v-img :src="reply.photoUrl" contain>
                                </v-img>
                            </v-avatar>
                            <router-link :to="{ name: 'user-page', params: { id: reply.createdById }}" class="font-weight-light home-link">{{ reply.creatorFullName }}</router-link>
                            <div>{{reply.dateCreated}}</div>
                            <div>{{reply.content}}</div>
                        </v-card>
                    </v-card>
                </v-card>
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

    @Component({})
    export default class InformationDetailsComponent extends Vue {
        private model : InformationModel = {
            id: 0,
            createdByName: '',
            dateCreated: new Date(),
            title: '',
            introduction: '',
            photoUrl: '',
            content: '',
            comments: []
        };

        private get id() : number {
            return Number(this.$route.params.id || 0);
        }

        async created() {
            await this.loadData(this.id);
        }

        async loadData(id: number) {
            this.model = await InformationService.get(id);
        }
    }
</script>
