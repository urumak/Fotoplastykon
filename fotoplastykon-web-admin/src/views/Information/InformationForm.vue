<template>
    <v-container class="flex flex-center">
        <v-card>
            <v-row>
                <v-avatar height="300" width="230" :tile="true" class="ml-3">
                    <v-img v-if="model.photoUrl" :src="model.photoUrl"></v-img>
                    <v-img v-else src="@/assets/subPhoto.png"></v-img>
                </v-avatar>
                <div class="col-9">
                    <v-file-input
                            accept="image/png, image/jpeg, image/bmp"
                            placeholder="Wybierz zdjęcie"
                            prepend-icon="mdi-camera"
                            label="Zdjęcie"
                            v-model="newPhoto"
                    ></v-file-input>
                </div>
            </v-row>
            <v-form
                    ref="form"
            >
                <v-text-field v-model="model.title" label="Tytuł" :error-messages="errors['Title']"></v-text-field>
                <v-textarea label="Wstęp" v-model="model.introduction" auto-grow row-height="15" :error-messages="errors['Introduction']"></v-textarea>
                <v-textarea label="Treść" v-model="model.content" auto-grow row-height="25" :error-messages="errors['Content']"></v-textarea>
                <v-btn class="primary" @click="update()">Zapisz</v-btn>
            </v-form>
        </v-card>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import Form from 'form-backend-validation';
    import InformationService from "@/services/InformationService";
    import {InformationFormModel} from "@/interfaces/information";

    @Component({})
    export default class InformationFormComponent extends Vue {
        private errors:any = {};
        private newPhoto = [];
        private model: InformationFormModel = {
            id: 0,
            title: '',
            introduction: '',
            photoUrl: '',
            content: ''
        };

        private get id() : number {
            return Number(this.$route.params.id || 0);
        }

        async created() {
            if(this.id !== 0) this.model = new Form(await InformationService.get(this.id));
        }

        async update() {
            try {
                if(this.id !== 0) {
                    await InformationService.update(this.id, this.model);
                    if(this.newPhoto && this.newPhoto.length !== 0) await InformationService.changePhoto(this.id, this.newPhoto)
                }
                else {
                    let id = await InformationService.add(this.model);
                    if(this.newPhoto && this.newPhoto.length !== 0) await InformationService.changePhoto(id, this.newPhoto)
                }
                this.$store.state.alert = {
                    show: true,
                    type: 'success',
                    message: 'Zmiany zostały zapisane'
                };
                await this.$router.push({name: 'information-list'});
            } catch(ex) {
                if (ex.code === 400) this.errors = ex.data.errors;
            }
        }
    }
</script>
