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
                <v-text-field type="number" v-model="form.yearOfProduction" label="Rok produkcji"></v-text-field>
                <v-divider></v-divider>
                <v-subheader>Twórcy</v-subheader>
                <person-in-role v-for="(item, index) in form.people" :key="index" :model="item" @delete-click="deleteItem(index)"></person-in-role>
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

    @Component({components:{
            'person-in-role': PersonInRole
        }
    })
    export default class FilmFormComponent extends Vue {
        private newPhoto = [];

        private form = new Form({
            id: 0,
            title: '',
            yearOfProduction: 0,
            photoUrl: '',
            people: []
        });

        private get id() : number {
            return Number(this.$route.params.id || 0);
        }

        async created() {
            if(this.id !== 0) this.form = new Form(await FilmsService.get(this.id));
        }

        async update() {
            if(this.id !== 0) {
                await FilmsService.update(this.id, this.form);
                if(this.newPhoto && this.newPhoto.length !== 0) await FilmsService.changePhoto(this.id, this.newPhoto)
            }
            else {
                let id = await FilmsService.add(this.form);
                if(this.newPhoto && this.newPhoto.length !== 0) await FilmsService.changePhoto(id, this.newPhoto)
            }

            await this.$router.push({name: 'films'});
        }

        addRole() {
            this.form.people.push({
                id: 0,
                personId: 0,
                role: 0,
                characterName: ''
            })
        }

        deleteItem(index: number) {
            this.form.people.splice(index, 1);
        }
    }
</script>
