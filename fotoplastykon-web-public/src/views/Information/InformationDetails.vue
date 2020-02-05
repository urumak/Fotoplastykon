<template>
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
        <v-img :src="model.photoUrl" max-height="600" max-width="700" style="left: 250px">
        </v-img>
        <div>
            {{ model.content }}
        </div>
        <v-btn v-if="!isCommentAdding" @click="newComment()">Nowy komentarz</v-btn>
        <v-btn v-else @click="cancelComment()">Anuluj</v-btn>
        <v-card v-for="item in model.comments" :key="item.id">
            <v-avatar>
                <v-img v-if="item.photoUrl" :src="item.photoUrl"></v-img>
                <v-img v-else src="@/assets/subPhoto.png"></v-img>
            </v-avatar>
            <router-link :to="{ name: 'user-page', params: { id: item.createdById }}" class="font-weight-light custom-link">{{ item.creatorFullName }}</router-link>
            <div>{{item.dateCreated}}</div>
            <v-btn v-if="!item.editMode && isCommentCreator(item.createdById)" @click="edit(item)">Edytuj</v-btn>
            <v-btn v-if="!item.editMode && isCommentCreator(item.createdById)" @click="removeComment(item.id)">Usuń</v-btn>
            <v-btn v-if="item.editMode && isCommentCreator(item.createdById)" @click="cancelEdit(item)">Anuluj</v-btn>
            <v-btn v-if="!item.editMode"  @click="newCommentReply(item.id)">Odpowiedz</v-btn>
            <div v-if="!item.editMode">{{item.content}}</div>
            <v-textarea v-if="item.editMode" label="Komentarz" v-model="item.content" auto-grow outlined rows="5" row-height="15"></v-textarea>
            <v-btn v-if="item.editMode && item.content" @click="submitComment(item)">Zapisz</v-btn>
            <v-card style="margin-left: 20px" v-for="reply in item.replies" :key="'r' + reply.id">
                <v-avatar>
                    <v-img v-if="reply.photoUrl" :src="reply.photoUrl"></v-img>
                    <v-img v-else src="@/assets/subPhoto.png"></v-img>
                </v-avatar>
                <router-link :to="{ name: 'user-page', params: { id: reply.createdById }}" class="font-weight-light custom-link">{{ reply.creatorFullName }}</router-link>
                <div>{{reply.dateCreated}}</div>
                <v-btn v-if="!reply.editMode && isCommentCreator(reply.createdById)" @click="edit(reply)">Edytuj</v-btn>
                <v-btn v-if="!reply.editMode && isCommentCreator(reply.createdById)" @click="removeComment(reply.id)">Usuń</v-btn>
                <v-btn v-if="reply.editMode && isCommentCreator(reply.createdById)"  @click="cancelEdit(reply)">Anuluj</v-btn>
                <div v-if="!reply.editMode">{{reply.content}}</div>
                <v-textarea v-if="reply.editMode" label="Komentarz" v-model="reply.content" auto-grow outlined rows="5" row-height="15"></v-textarea>
                <v-btn v-if="reply.editMode && reply.content" @click="submitComment(reply)">Zapisz</v-btn>
            </v-card>
        </v-card>
    </v-card>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import InformationService from "@/services/InformationService.ts";
    import { InformationModel, InformationCommentModel } from '@/interfaces/information';

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
