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
                <v-text-field v-model="model.firstName" label="Imię" :error-messages="errors['FirstName']"></v-text-field>
                <v-text-field v-model="model.surname" label="Nazwisko" :error-messages="errors['Surname']"></v-text-field>
                <v-btn class="primary" @click="update()">Zapisz</v-btn>
            </v-form>
        </v-card>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import Form from 'form-backend-validation';
    import FilmPeopleService from "../../services/FilmPeopleService";
    import {FilmPersonFormModel} from "@/interfaces/filmPeople";

    @Component({})
    export default class UserFormComponent extends Vue {
        private errors:any = {};
        private newPhoto = [];
        private model: FilmPersonFormModel = {
            id: 0,
            firstName: '',
            surname: '',
            photoUrl: ''
        };

        private get id() : number {
            return Number(this.$route.params.id || 0);
        }

        async created() {
            if(this.id !== 0) this.model = await FilmPeopleService.get(this.id);
        }

        async update() {
            try {
                if(this.id !== 0) {
                    await FilmPeopleService.update(this.id, this.model);
                    if(this.newPhoto && this.newPhoto.length !== 0) await FilmPeopleService.changePhoto(this.id, this.newPhoto)
                }
                else {
                    let id = await FilmPeopleService.add(this.model);
                    if(this.newPhoto && this.newPhoto.length !== 0) await FilmPeopleService.changePhoto(id, this.newPhoto)
                }
                this.$store.state.alert = {
                    show: true,
                    type: 'success',
                    message: 'Zmiany zostały zapisane'
                };
                await this.$router.push({name: 'film-people'});
            } catch(ex) {
                if (ex.code === 400) this.errors = ex.data.errors;
            }
        }
    }
</script>
