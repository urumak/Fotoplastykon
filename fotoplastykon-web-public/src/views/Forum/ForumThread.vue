<template>
    <div>
        <v-card class="container-item main-card">
            <v-textarea v-if="model.editMode" label="Temat" v-model="model.subject" auto-grow outlined rows="1" row-height="15"></v-textarea>
            <v-textarea v-if="model.editMode" label="Treść" v-model="model.content" auto-grow outlined rows="20" row-height="15"></v-textarea>
            <v-row>
               <v-col cols="6">
                   <div v-if="!model.editMode" style="font-size: 20px">{{model.subject}}</div>
               </v-col>
                <v-col cols="6">
                    <v-tooltip top>
                        <template v-slot:activator="{ on }">
                            <v-btn small class="float-right" v-if="showEditButton()" @click="enableEditMode()" v-on="on"><v-icon left>mdi-pencil</v-icon></v-btn>
                        </template>
                        <span>Edytuj</span>
                    </v-tooltip>
                    <v-tooltip top>
                        <template v-slot:activator="{ on }">
                            <v-btn class="float-right" small v-if="canDelete()" v-on="on" @click="remove()"><v-icon left>mdi-delete</v-icon></v-btn>
                        </template>
                        <span>Usuń</span>
                    </v-tooltip>
                </v-col>
            </v-row>
            <div v-if="!model.editMode">{{model.content}}</div>
            <v-btn v-if="showCancelAndSaveButton() && model.content && model.subject" class="mr-2 primary" @click="saveChanges()"><v-icon left>mdi-check</v-icon> Zapisz</v-btn>
            <v-btn v-if="showCancelAndSaveButton()" @click="disableEditMode()"><v-icon left>mdi-close</v-icon> Anuluj</v-btn>
        </v-card>
        <v-card class="container-item pa-8">
            <div style="font-size: 20px">Komentarze</div>
            <v-btn class="mt-2 mb-2 primary" v-if="!isCommentAdding" @click="newComment()"><v-icon left>mdi-plus</v-icon> Dodaj</v-btn>
            <v-btn class="mt-2 mb-2 secondary" v-else @click="cancelComment()"><v-icon left>mdi-close</v-icon> Anuluj</v-btn>
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
                                    <router-link :to="{ name: 'user-page', params: { id: item.createdById }}" class="font-weight-light custom-link">{{ item.createdByName }}</router-link>
                                    <div style="font-size: small">{{ item.dateCreated | moment() }}</div>
                                </div>
                            </v-row>
                        </v-col>
                        <v-col cols="4" right>
                            <v-tooltip top>
                                <template v-slot:activator="{ on }">
                                    <v-btn class="float-right" small v-if="!item.editMode"  v-on="on" @click="newCommentReply(item.id)"><v-icon left>mdi-replay</v-icon></v-btn>
                                </template>
                                <span>Odpowiedz</span>
                            </v-tooltip>
                            <v-tooltip top>
                                <template v-slot:activator="{ on }">
                                    <v-btn class="float-right" small v-if="!item.editMode && isCommentCreator(item.createdById)" v-on="on" @click="edit(item)"><v-icon left>mdi-pencil</v-icon></v-btn>
                                </template>
                                <span>Edytuj</span>
                            </v-tooltip>
                            <v-tooltip top>
                                <template v-slot:activator="{ on }">
                                    <v-btn class="float-right" small v-if="!item.editMode && isCommentCreator(item.createdById)" v-on="on" @click="removeComment(item.id)"><v-icon left>mdi-delete</v-icon></v-btn>
                                </template>
                                <span>Usuń</span>
                            </v-tooltip>
                            <v-tooltip top>
                                <template v-slot:activator="{ on }">
                                    <v-btn class="float-right" small v-if="item.editMode && isCommentCreator(item.createdById) && item.id" v-on="on" @click="cancelEdit(item)"><v-icon left>mdi-close</v-icon></v-btn>
                                </template>
                                <span>Anuluj</span>
                            </v-tooltip>
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
                                    <router-link :to="{ name: 'user-page', params: { id: reply.createdById }}" class="font-weight-light custom-link">{{ reply.createdByName }}</router-link>
                                    <div style="font-size: small">{{ reply.dateCreated | moment() }}</div>
                                </div>
                            </v-row>
                        </v-col>
                        <v-col cols="4" right>
                            <v-tooltip top>
                                <template v-slot:activator="{ on }">
                                    <v-btn class="float-right" small v-if="!reply.editMode && isCommentCreator(reply.createdById)" v-on="on" @click="edit(reply)"><v-icon left>mdi-pencil</v-icon></v-btn>
                                </template>
                                <span>Edytuj</span>
                            </v-tooltip>
                            <v-tooltip top>
                                <template v-slot:activator="{ on }">
                                    <v-btn class="float-right" small v-if="!reply.editMode && isCommentCreator(reply.createdById)" v-on="on" @click="removeComment(reply.id)"><v-icon left>mdi-delete</v-icon></v-btn>
                                </template>
                                <span>Usuń</span>
                            </v-tooltip>
                            <v-tooltip top>
                                <template v-slot:activator="{ on }">
                                    <v-btn class="float-right" small v-if="reply.editMode && isCommentCreator(reply.createdById)" v-on="on" @click="cancelEdit(reply)"><v-icon left>mdi-close</v-icon></v-btn>
                                </template>
                                <span>Anuluj</span>
                            </v-tooltip>
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

<style lang="stylus">
    .v-icon--left
        margin-right 0px !important
</style>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import ForumService from "@/services/ForumService.ts";
    import { ForumThreadCommentModel, ForumThreadModel } from '@/interfaces/forum';
    import moment from 'moment'

    @Component({filters: {
            moment (date: Date) {
                return moment(date).format('YYYY-MM-DD HH:mm');
            }}})
    export default class InformationListComponent extends Vue {
        private model : ForumThreadModel = {
            id: 0,
            filmId: undefined,
            personId: undefined,
            createdByName: '',
            dateCreated: new Date(),
            subject: '',
            photoUrl: '',
            content: '',
            createdById: 0,
            editMode: false,
            comments: []
        };

        private isCommentAdding: boolean = false;

        private get id() : number {
            return Number(this.$route.params.id || 0);
        }

        private get threadType() : string {
            return this.$route.params.type || '';
        }

        private get sourceId() : number {
            return Number(this.$route.params.sourceId || 0);
        }

        async created() {
            if(this.id !== 0) await this.loadData(this.id);
            else this.model.editMode = true;
        }

        async loadData(id: number) {
            this.model = await ForumService.get(id);
        }

        async saveChanges() {
            if (this.id === 0) {
                let id = 0;
                if(this.threadType)
                {
                    if(this.threadType == 'film') id = await ForumService.addForFilm(this.model, this.sourceId);
                    else if (this.threadType == 'film') id = await ForumService.addForFilmPerson(this.model, this.sourceId);
                }
                else
                {
                    id = await ForumService.add(this.model);
                }
                await this.loadData(id);

                // @ts-ignore
                await this.$router.push({name: 'forum-thread', params: {id: id}});
            } else {
                await ForumService.update(this.model);
                await this.loadData(this.id);
            }

        }

        showEditButton() : boolean {
            return (this as any).$auth.user().id === this.model.createdById && !this.model.editMode;
        }

        canDelete() : boolean {
            return (this as any).$auth.user().id === this.model.createdById;
        }

        showCancelAndSaveButton() : boolean {
            return ((this as any).$auth.user().id === this.model.createdById && this.model.editMode) || this.id === 0;
        }

        enableEditMode() {
            this.model.editMode = true;
            (this as any).$forceUpdate();
        }

        async disableEditMode() {
            this.model.editMode = false;
            if(this.id !== 0) await this.loadData(this.id);
            else await this.$router.push({ name: 'forum' });
        }

        async reloadComments() {
            this.model.comments = (await ForumService.get(this.id)).comments;
        }

        async submitComment(item: ForumThreadCommentModel) {
            if(item.id === 0) item.id = await ForumService.addComment(item);
            else await ForumService.updateComment(item);
            item.editMode = false;
            this.isCommentAdding = false;
            await this.reloadComments();
        }

        async remove(id: number) {
            await ForumService.remove(this.id);
            this.$router.push({ name: 'forum' });
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

        edit(item: ForumThreadCommentModel){
            item.editMode = true;
            (this as any).$forceUpdate();
        }

        async cancelEdit(item: ForumThreadCommentModel){
            item.editMode = false;
            await this.reloadComments();
        }


        isCommentCreator(commentCreatorId: number) : boolean {
            return (this as any).$auth.user().id === commentCreatorId;
        }

        getNewComment(parentId?: number) : ForumThreadCommentModel {
            return {
                id: 0,
                createdByName: (this as any).$auth.user().firstName + ' ' + (this as any).$auth.user().surname,
                threadId: this.id,
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
