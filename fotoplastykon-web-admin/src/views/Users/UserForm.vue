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
                            placeholder="Wybierz zdjęcie profilowe"
                            prepend-icon="mdi-camera"
                            label="Zdjęcie profilowe"
                            v-model="newPhoto"
                    ></v-file-input>
                    <v-btn class="primary" @click="changeProfilePhoto()">Zmień zdjęcie</v-btn>
                </div>
            </v-row>
            <v-form
                ref="form"
            >
                <v-text-field v-model="form.userName" label="Nazwa użytkownika"></v-text-field>
                <v-text-field v-model="form.firstName" label="Imię"></v-text-field>
                <v-text-field v-model="form.surname" label="Nazwisko"></v-text-field>
                <v-text-field v-model="form.email" label="Adres email"></v-text-field>
                <v-text-field v-model="form.password" type="password" label="Hasło"></v-text-field>
                <v-text-field v-model="form.repeatPassword" type="password" label="Powtórz hasło"></v-text-field>
                <v-btn class="primary" @click="update()">Zapisz</v-btn>
            </v-form>
        </v-card>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import Form from 'form-backend-validation';
    import UsersService from "@/services/UsersService";

    @Component({})
    export default class UserFormComponent extends Vue {
        private newPhoto = [];
        private form = new Form({
            surname: '',
            userName: '',
            firstName: '',
            password: '',
            repeatPassword: '',
            email: '',
            photoUrl: '',
            isAdmin: false
        });

        private get id() : number {
            return Number(this.$route.params.id || 0);
        }

        async created() {
            if(this.id !== 0) this.form = new Form(await UsersService.get(this.id));
        }

        async changeProfilePhoto(){

        }

        async update(){

        }
    }
</script>
