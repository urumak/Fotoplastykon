<template>
    <v-container class="flex flex-center">
        <v-row>
            <div class="col-8">
                <v-card class="my-card col-8">
                    <v-textarea v-if="model.editMode" label="Temat" v-model="model.subject" auto-grow outlined rows="1" row-height="15"></v-textarea>
                    <v-textarea v-if="model.editMode" label="Treść" v-model="model.content" auto-grow outlined rows="20" row-height="15"></v-textarea>
                    <v-btn v-if="showEditButton()" @click="enableEditMode()">Edytuj</v-btn>
                    <div v-if="!model.editMode">{{model.subject}}</div>
                    <div v-if="!model.editMode">{{model.content}}</div>
                    <v-btn v-if="showCancelAndSaveButton()" @click="disableEditMode()">Anuluj</v-btn>
                    <v-btn v-if="showCancelAndSaveButton()" @click="saveChanges()">Zapisz</v-btn>
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
            if(this.id !== 0) {
                await ForumService.update(this.model);
                await this.loadData(this.id);
            } else {
                let id = await ForumService.add(this.model);
                await this.loadData(id);
                console.log(id);
                await this.$router.push({ name: 'forum-thread', params: { id: id } });
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
            await this.loadData(this.id);
        }
    }
</script>
