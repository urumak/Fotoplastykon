<template>
    <div>
        <v-card class="my-card">
            <v-textarea v-if="model.editMode" label="Temat" v-model="model.subject" auto-grow outlined rows="1" row-height="15"></v-textarea>
            <v-textarea v-if="model.editMode" label="Treść" v-model="model.content" auto-grow outlined rows="20" row-height="15"></v-textarea>
            <v-btn v-if="showEditButton()" @click="enableEditMode()">Edytuj</v-btn>
            <div v-if="!model.editMode">{{model.subject}}</div>
            <div v-if="!model.editMode">{{model.content}}</div>
            <v-btn v-if="showCancelAndSaveButton()" @click="disableEditMode()">Anuluj</v-btn>
            <v-btn v-if="showCancelAndSaveButton()" @click="saveChanges()">Zapisz</v-btn>
        </v-card>
        <v-btn v-if="!isCommentAdding" @click="newComment()">Nowy komentarz</v-btn>
        <v-btn v-else @click="cancelComment()">Anuluj</v-btn>
        <v-card v-for="item in model.comments" :key="item.id">
            <v-avatar>
                <v-img :src="item.photoUrl" contain>
                </v-img>
            </v-avatar>
            <router-link :to="{ name: 'user-page', params: { id: item.createdById }}" class="font-weight-light home-link">{{ item.creatorFullName }}</router-link>
            <div>{{item.dateCreated}}</div>
            <v-btn v-if="!item.editMode && isCommentCreator(item.createdById) && !item.isDeleted" @click="edit(item)">Edytuj</v-btn>
            <v-btn v-if="!item.editMode && isCommentCreator(item.createdById) && !item.isDeleted" @click="removeComment(item.id)">Usuń</v-btn>
            <v-btn v-if="item.editMode && isCommentCreator(item.createdById) && !item.isDeleted" @click="cancelEdit(item)">Anuluj</v-btn>
            <v-btn v-if="!item.editMode && !item.isDeleted"  @click="newCommentReply(item.id)">Odpowiedz</v-btn>
            <div v-if="!item.editMode">{{item.content}}</div>
            <v-textarea v-if="item.editMode" label="Komentarz" v-model="item.content" auto-grow outlined rows="5" row-height="15"></v-textarea>
            <v-btn v-if="item.editMode" @click="submitComment(item)">Zapisz</v-btn>
            <v-card style="margin-left: 20px" v-for="reply in item.replies" :key="'r' + reply.id">
                <v-avatar>
                    <v-img :src="reply.photoUrl" contain>
                    </v-img>
                </v-avatar>
                <router-link :to="{ name: 'user-page', params: { id: reply.createdById }}" class="font-weight-light home-link">{{ reply.creatorFullName }}</router-link>
                <div>{{reply.dateCreated}}</div>
                <v-btn v-if="!reply.editMode && isCommentCreator(reply.createdById)" @click="edit(reply)">Edytuj</v-btn>
                <v-btn v-if="!reply.editMode && isCommentCreator(reply.createdById)" @click="removeComment(reply.id)">Usuń</v-btn>
                <v-btn v-if="reply.editMode && isCommentCreator(reply.createdById)"  @click="cancelEdit(reply)">Anuluj</v-btn>
                <div v-if="!reply.editMode">{{reply.content}}</div>
                <v-textarea v-if="reply.editMode" label="Komentarz" v-model="reply.content" auto-grow outlined rows="5" row-height="15"></v-textarea>
                <v-btn v-if="reply.editMode" @click="submitComment(reply)">Zapisz</v-btn>
            </v-card>
        </v-card>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import ForumService from "@/services/ForumService.ts";
    import { ForumThreadCommentModel, ForumThreadModel } from '@/interfaces/forum';

    @Component({})
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

        async created() {
            if(this.id !== 0) await this.loadData(this.id);
            else this.model.editMode = true;
        }

        async loadData(id: number) {
            this.model = await ForumService.get(id);
        }

        async saveChanges() {
            if (this.id === 0) {
                let id = await ForumService.add(this.model);
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

        async removeComment(id: number) {
            await ForumService.removeComment(id);
            this.model.comments = (await ForumService.get(this.id)).comments;
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
