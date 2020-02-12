<template>
    <div>
        <v-card class="container-item main-card">
            <div class="main-card-title">
                {{ model.title }}
            </div>
            <div>
                {{ "Autor: " + model.createdByName }}, Data dodania: {{ model.dateCreated | moment() }}
            </div>
            <div class="card-title-line mb-8"></div>
            <div class="news-text">
                {{ model.introduction }}
            </div>
            <v-img :src="model.photoUrl" class="mt-12 mb-12" max-height="600" max-width="700" style="left: 210px">
            </v-img>
            <div class="news-text">
                {{ model.content }}
            </div>
        </v-card>
        <v-card class="container-item pa-8">
            <div style="font-size: 20px">Komentarze</div>
            <v-btn class="mt-2 mb-2" color="primary" v-if="!isCommentAdding" @click="newComment()"><v-icon left>mdi-plus</v-icon> Dodaj</v-btn>
            <v-btn class="mt-2 mb-2" color="secondary" v-else @click="cancelComment()"><v-icon left>mdi-close</v-icon> Anuluj</v-btn>
            <div class="comment" v-for="item in model.comments" :key="item.id">
                <v-card class="container-item col-12 mb-5">
                    <v-row class="ml-5">
                        <v-col cols="8">
                            <v-row>
                                <v-avatar>
                                    <v-img v-if="item.photoUrl" :src="item.photoUrl"></v-img>
                                    <v-img v-else src="@/assets/subPhoto.png"></v-img>
                                </v-avatar>
                                <div>
                                    <router-link :to="{ name: 'user-page', params: { id: item.createdById }}" class="font-weight-light custom-link">{{ item.creatorFullName }}</router-link>
                                    <div style="font-size: small">{{ item.dateCreated | moment() }}</div>
                                </div>
                            </v-row>
                        </v-col>
                        <v-col cols="4" right>
                            <v-btn class="float-right" small v-if="!item.editMode && isCommentCreator(item.createdById)" @click="edit(item)"><v-icon left>mdi-pencil</v-icon> Edytuj</v-btn>
                            <v-btn class="float-right" small v-if="!item.editMode && isCommentCreator(item.createdById)" @click="removeComment(item.id)"><v-icon left>mdi-delete</v-icon> Usuń</v-btn>
                            <v-btn class="float-right" small v-if="item.editMode && isCommentCreator(item.createdById)" @click="cancelEdit(item)"><v-icon left>mdi-close</v-icon> Anuluj</v-btn>
                            <v-btn class="float-right" small v-if="!item.editMode"  @click="newCommentReply(item.id)"><v-icon left>mdi-replay</v-icon> Odpowiedz</v-btn>
                        </v-col>
                        <div v-if="!item.editMode">{{item.content}}</div>
                        <v-textarea v-if="item.editMode" label="Komentarz" v-model="item.content" auto-grow outlined rows="5" row-height="15" class="mr-5"></v-textarea>
                    </v-row>
                    <v-btn small v-if="item.editMode && item.content" @click="submitComment(item)">Zapisz</v-btn>
                </v-card>
                <v-card class="container-item col-11 float-right mb-5" v-for="reply in item.replies" :key="'r' + reply.id">
                    <v-row class="ml-5">
                        <v-col cols="8">
                            <v-row>
                                <v-avatar>
                                    <v-img v-if="reply.photoUrl" :src="reply.photoUrl"></v-img>
                                    <v-img v-else src="@/assets/subPhoto.png"></v-img>
                                </v-avatar>
                                <div>
                                    <router-link :to="{ name: 'user-page', params: { id: reply.createdById }}" class="font-weight-light custom-link">{{ reply.creatorFullName }}</router-link>
                                    <div style="font-size: small">{{ reply.dateCreated | moment() }}</div>
                                </div>
                            </v-row>
                        </v-col>
                        <v-col cols="4" right>
                            <v-btn class="float-right" small v-if="!reply.editMode && isCommentCreator(reply.createdById)" @click="edit(reply)"><v-icon left>mdi-pencil</v-icon> Edytuj</v-btn>
                            <v-btn class="float-right" small v-if="!reply.editMode && isCommentCreator(reply.createdById)" @click="removeComment(reply.id)"><v-icon left>mdi-delete</v-icon> Usuń</v-btn>
                            <v-btn class="float-right" small v-if="reply.editMode && isCommentCreator(reply.createdById)" @click="cancelEdit(reply)"><v-icon left>mdi-close</v-icon> Anuluj</v-btn>
                        </v-col>
                        <div v-if="!reply.editMode">{{reply.content}}</div>
                        <v-textarea v-if="reply.editMode" label="Komentarz" v-model="reply.content" auto-grow outlined rows="5" row-height="15" class="mr-5"></v-textarea>
                    </v-row>
                    <v-btn small v-if="reply.editMode && reply.content" @click="submitComment(reply)">Zapisz</v-btn>
                </v-card>
            </div>
        </v-card>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import InformationService from "@/services/InformationService.ts";
    import { InformationModel, InformationCommentModel } from '@/interfaces/information';
    import moment from 'moment';

    @Component({filters: {
            moment (date: Date) {
                return moment(date).format('YYYY-MM-DD');
            }}})
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
        private isCommentAdding: boolean = false;

        private get id() : number {
            return Number(this.$route.params.id || 0);
        }

        async created() {
            await this.loadData(this.id);
        }

        async loadData(id: number) {
            this.model = await InformationService.get(id);
        }

        async reloadComments() {
            this.model.comments = (await InformationService.get(this.id)).comments;
        }

        async submitComment(item: InformationCommentModel) {
            if(item.id === 0) item.id = await InformationService.addComment(item);
            else await InformationService.updateComment(item);
            item.editMode = false;
            this.isCommentAdding = false;
            await this.reloadComments();
        }

        async removeComment(id: number) {
            await InformationService.removeComment(id);
            this.model.comments = (await InformationService.get(this.id)).comments;
        }

        newComment() {
            this.model.comments.splice(0, 0, this.getNewComment() );
            this.isCommentAdding = true;
        }

        newCommentReply(parentId: number) {
            let item = this.model.comments.find(x => x.id === parentId);
            if(item) {
                item.replies.splice(0, 0, this.getNewComment(parentId));
                item.isReplyAdding = true;
            }
        }

        cancelComment(){
            this.model.comments.splice(0, 1);
            this.isCommentAdding = false;
        }

        edit(item: InformationCommentModel){
            item.editMode = true;
            this.$forceUpdate();
        }

        async cancelEdit(item: InformationCommentModel){
            item.editMode = false;
            await this.reloadComments();
        }

        isCommentCreator(commentCreatorId: number) : boolean {
            return (this as any).$auth.user().id === commentCreatorId;
        }

        getNewComment(parentId?: number) : InformationCommentModel {
            return {
                id: 0,
                creatorFullName: (this as any).$auth.user().firstName + ' ' + (this as any).$auth.user().surname,
                informationId: this.id,
                parentId: parentId,
                content: '',
                dateCreated: new Date(),
                replies: [],
                photoUrl: (this as any).$auth.user().photoUrl,
                editMode: true,
                createdById: (this as any).$auth.user().id,
                isDeleted: false,
                isReplyAdding: false
            }
        }
    }
</script>
