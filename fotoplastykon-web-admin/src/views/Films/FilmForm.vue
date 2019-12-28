<template>
    <v-container class="flex flex-center">
        <v-card>
            <v-row>
                <div class="col-3">
                    <v-img v-if="form.photoUrl" contain :src="form.photoUrl"></v-img>
                    <v-img v-else src="@/assets/subPhoto.png"></v-img>
                </div>
                <div class="col-9">
                    <v-file-input
                            accept="image/png, image/jpeg, image/bmp"
                            placeholder="Wybierz zdjęcie"
                            prepend-icon="mdi-camera"
                            label="Zdjęcie"
                            v-model="newPhoto"
                    ></v-file-input>
                    <v-btn class="primary" @click="changePhoto()">Zmień zdjęcie</v-btn>
                </div>
            </v-row>
            <v-form
                    ref="form"
            >
                <v-text-field v-model="form.title" label="Tytuł"></v-text-field>
                <v-text-field type="number" v-model="form.yaerOfProduction" label="Rok produkcji"></v-text-field>
                <v-btn class="secondary" @click="addRole()">Dodaj rolę</v-btn>
                <div v-for="(item, index) in form.people" :key="index">
                    <v-autocomplete
                            class="flex ml-1"
                            style="margin-top:30px;"
                            label="Szukaj"
                            :items="people"
                            v-model="selectedItem"
                            :search-input.sync="peopleSearchInput"
                            item-text="nameAndSurname"
                            item-value="id"
                            dense
                            solo
                            clearable
                            return-object
                            @change="onChange(index)"
                            no-data-text="Brak wyników"
                            autocomplete="off">
                        <template slot="selection" slot-scope="data">
                            <v-flex xs2>
                                <v-avatar>
                                    <v-img v-if="data.item.photoUrl != null && data.item.photoUrl.length != 0" :src='data.item.photoUrl'></v-img>
                                    <v-img v-else src="@/assets/subPhoto.png"></v-img>
                                </v-avatar>
                            </v-flex>
                            <v-flex class='ml-1'>
                                {{ data.item.nameAndSurname }}
                            </v-flex>
                        </template>
                        <template slot="item" slot-scope="data">
                            <v-avatar>
                                <v-img v-if="data.item.photoUrl != null && data.item.photoUrl.length != 0" :src='data.item.photoUrl'></v-img>
                                <v-img v-else src="@/assets/subPhoto.png"></v-img>
                            </v-avatar>
                            <v-flex v-html="data.item.nameAndSurname"></v-flex>
                        </template>
                    </v-autocomplete>
                </div>
                <v-btn class="primary" @click="update()">Zapisz</v-btn>
            </v-form>
        </v-card>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import Form from 'form-backend-validation';
    import FilmsService from "../../services/FilmsService";
    import {PersonDropDownModel, RolesDropDownDictionary, PersonInRoleForm} from "@/interfaces/films";
    import { Watch } from 'vue-property-decorator';

    @Component({})
    export default class FilmFormComponent extends Vue {
        private newPhoto = [];
        private people : PersonDropDownModel[] = [];
        private selectedItem : any = null;
        private peopleSearchInput : string = "";
        private roleTypes : RolesDropDownDictionary = {};

        private form = new Form({
            id: 0,
            title: '',
            yearOfProduction: 0,
            photoUrl: '',
            people: []
        });

        @Watch('peopleSearchInput')
        async search() {
            if (this.peopleSearchInput){
                this.people = await FilmsService.getFilmPeople(this.peopleSearchInput);
            } else {
                this.people = [];
                this.selectedItem = {};
            }
        }

        private get id() : number {
            return Number(this.$route.params.id || 0);
        }

        async created() {
            //if(this.id !== 0) this.form = new Form(await FilmsService.get(this.id));
        }

        async changePhoto() {
            this.form = new Form(await FilmsService.changePhoto(this.id, this.newPhoto));
        }

        async update() {
            this.form = new Form(await FilmsService.update(this.id, this.form));
        }

        onChange(index: number) {
            if(this.selectedItem) {
                this.form.people[index].personId = this.selectedItem.personId;
                this.selectedItem = {};
            }
            console.log(this.form.people);
        }

        addRole() {
            this.form.people.push({
                personId: 0,
                roleId: 0,
                roleName: ''
            })
        }
    }
</script>
