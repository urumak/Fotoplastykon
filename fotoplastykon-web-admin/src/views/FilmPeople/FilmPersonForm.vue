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

        async update() {
            if(this.id !== 0) {
                await FilmPeopleService.update(this.id, this.form);
                if(this.newPhoto && this.newPhoto.length !== 0) await FilmPeopleService.changePhoto(this.id, this.newPhoto)
            }
            else {
                let id = await FilmPeopleService.add(this.form);
                if(this.newPhoto && this.newPhoto.length !== 0) await FilmPeopleService.changePhoto(id, this.newPhoto)
            }

            await this.$router.push({name: 'film-people'});
        }
    }
</script>
