<template>
    <v-container class="flex flex-center">
        <v-card>
            <v-row>
                <v-avatar height="300" width="230" :tile="true" class="ml-3">
                    <v-img v-if="form.photoUrl" :src="form.photoUrl"></v-img>
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
                <v-text-field v-model="form.title" label="Tytuł"></v-text-field>
                <v-textarea label="Wstęp" v-model="form.introduction" auto-grow row-height="15"></v-textarea>
                <v-textarea label="Treść" v-model="form.content" auto-grow row-height="25"></v-textarea>
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

    @Component({})
    export default class InformationFormComponent extends Vue {
        private newPhoto = [];
        private form = new Form({
            id: 0,
            title: '',
            introduction: '',
            photoUrl: '',
            content: ''
        });

        private get id() : number {
            return Number(this.$route.params.id || 0);
        }

        async created() {
            if(this.id !== 0) this.form = new Form(await InformationService.get(this.id));
        }

        async update() {
            if(this.id !== 0) {
                await InformationService.update(this.id, this.form);
                if(this.newPhoto && this.newPhoto.length !== 0) await InformationService.changePhoto(this.id, this.newPhoto)
            }
            else {
                let id = await InformationService.add(this.form);
                if(this.newPhoto && this.newPhoto.length !== 0) await InformationService.changePhoto(id, this.newPhoto)
            }

            await this.$router.push({name: 'information'});
        }
    }
</script>
