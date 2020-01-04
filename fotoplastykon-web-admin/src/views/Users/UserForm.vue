<template>
    <v-container class="flex flex-center">
        <v-card>
            <v-row>
                <v-avatar height="300" width="230" :tile="true" class="ml-3">
                    <v-img v-if="model.photoUrl" :src="model.photoUrl"></v-img>
                    <v-img v-else src="@/assets/subPhoto.png"></v-img>
                </v-avatar>
                <di class="col-9">
                    <v-file-input
                            accept="image/png, image/jpeg, image/bmp"
                            placeholder="Wybierz zdjęcie profilowe"
                            prepend-icon="mdi-camera"
                            label="Zdjęcie profilowe"
                            v-model="newPhoto"
                    ></v-file-input>
                </di>
            </v-row>
            <v-form
                ref="form"
            >
                <v-text-field v-model="model.userName" label="Nazwa użytkownika" :error-messages="errors['UserName']"></v-text-field>
                <v-text-field v-model="model.firstName" label="Imię" :error-messages="errors['FirstName']"></v-text-field>
                <v-text-field v-model="model.surname" label="Nazwisko" :error-messages="errors['Surname']"></v-text-field>
                <v-text-field v-model="model.email" label="Adres email" :error-messages="errors['Email']"></v-text-field>
                <v-text-field v-model="model.password" type="password" label="Hasło" :error-messages="errors['Password']"></v-text-field>
                <v-text-field v-model="model.repeatPassword" type="password" label="Powtórz hasło" :error-messages="errors['RepeatPassword']"></v-text-field>
                <v-checkbox v-model="model.isAdmin" label="Administrator" :error-messages="errors['IsAdmin']"></v-checkbox>
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
        private errors:any = {};
        private newPhoto = [];
        private model = {
            id: 0,
            surname: '',
            userName: '',
            firstName: '',
            password: '',
            repeatPassword: '',
            email: '',
            photoUrl: '',
            isAdmin: false
        };

        private get id() : number {
            return Number(this.$route.params.id || 0);
        }

        async created() {
            if(this.id !== 0) this.model = new Form(await UsersService.get(this.id));
        }

        async update() {
            try {
                if(this.id !== 0) {
                    await UsersService.update(this.id, this.model);
                    if(this.newPhoto && this.newPhoto.length !== 0) await UsersService.changeProfilePhoto(this.id, this.newPhoto)
                }
                else {
                    let id = await UsersService.add(this.model);
                    if(this.newPhoto && this.newPhoto.length !== 0) await UsersService.changeProfilePhoto(id, this.newPhoto)
                }
                this.$store.state.alert = {
                    show: true,
                    type: 'success',
                    message: 'Zmiany zostały zapisane'
                };
                await this.$router.push({name: 'users'});
            } catch(ex) {
                if (ex.code === 400) this.errors = ex.data.errors;
            }
        }
    }
</script>
