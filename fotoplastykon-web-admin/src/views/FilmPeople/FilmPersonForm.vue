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
                <v-text-field v-model="form.firstName" label="Imię"></v-text-field>
                <v-text-field v-model="form.surname" label="Nazwisko"></v-text-field>
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

    @Component({})
    export default class UserFormComponent extends Vue {
        private newPhoto = [];
        private form = new Form({
            id: 0,
            firstName: '',
            surname: '',
            photoUrl: ''
        });

        private get id() : number {
            return Number(this.$route.params.id || 0);
        }

        async created() {
            if(this.id !== 0) this.form = new Form(await FilmPeopleService.get(this.id));
        }

        async changePhoto(){
            this.form = new Form(await FilmPeopleService.changePhoto(this.id, this.newPhoto));
        }

        async update(){
            this.form = new Form(await FilmPeopleService.update(this.id, this.form));
        }
    }
</script>
