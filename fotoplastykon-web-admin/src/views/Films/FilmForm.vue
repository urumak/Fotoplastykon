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
                <v-text-field type="number" v-model="model.yearOfProduction" label="Rok produkcji" :error-messages="errors['YearOfProduction']"></v-text-field>
                <v-divider></v-divider>
                <v-subheader>Twórcy</v-subheader>
                <person-in-role v-for="(item, index) in model.people"
                                :key="index" :model="item"
                                @delete-click="deleteItem(index)"
                                :errors="getItemErrors(index)"
                                :index="index"
                                :error-messages="errors['People']">
                </person-in-role>
                <v-btn class="secondary" small @click="addRole()"><v-icon>mdi-plus</v-icon></v-btn>
                <v-divider></v-divider>
                <v-btn class="primary mt-5" @click="update()">Zapisz</v-btn>
            </v-form>
        </v-card>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import Form from 'form-backend-validation';
    import FilmsService from "../../services/FilmsService";
    import PersonInRole from "@/components/filmForm/PersonInRole.vue";
    import { Watch } from 'vue-property-decorator';
    import {FilmFormModel} from "@/interfaces/films";

    @Component({components:{
            'person-in-role': PersonInRole
        }
    })
    export default class FilmFormComponent extends Vue {
        private errors: any = {};
        private newPhoto = [];

        private model: FilmFormModel = {
            id: 0,
            title: '',
            yearOfProduction: 0,
            photoUrl: '',
            people: []
        };

        private get id() : number {
            return Number(this.$route.params.id || 0);
        }

        async created() {
            if(this.id !== 0) this.model = new Form(await FilmsService.get(this.id));
        }

        async update() {
            try {
                if(this.id !== 0) {
                    await FilmsService.update(this.id, this.model);
                    if(this.newPhoto && this.newPhoto.length !== 0) await FilmsService.changePhoto(this.id, this.newPhoto)
                }
                else {
                    let id = await FilmsService.add(this.model);
                    if(this.newPhoto && this.newPhoto.length !== 0) await FilmsService.changePhoto(id, this.newPhoto)
                }
                this.$store.state.alert = {
                    show: true,
                    type: 'success',
                    message: 'Zmiany zostały zapisane'
                };
                await this.$router.push({name: 'films'});
            } catch(ex) {
                if (ex.code === 400) this.errors = ex.data.errors;
            }
        }

        addRole() {
            this.model.people.push({
                id: 0,
                personId: 0,
                role: 0,
                characterName: ''
            })
        }

        deleteItem(index: number) {
            this.model.people.splice(index, 1);
        }

        getItemErrors(index: number) : any {
            return {
                PersonId: this.errors['People[' + index.toString() + '].PersonId'],
                Role: this.errors['People[' + index.toString() + '].Role'],
                CharacterName: this.errors['People[' + index.toString() + '].CharacterName'],
            }
        }
    }
</script>
