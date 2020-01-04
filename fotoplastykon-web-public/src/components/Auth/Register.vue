<template>
    <v-container class="flex flex-center">
        <v-form @keyup.enter="register()" ref="form">
            <v-text-field v-model="model.userName" label="Nazwa użytkownika" :error-messages="errors['UserName']"></v-text-field>
            <v-text-field v-model="model.password" type="password" label="Hasło" :error-messages="errors['Password']"></v-text-field>
            <v-text-field v-model="model.repeatPassword" type="password" label="Powtórz hasło" :error-messages="errors['RepeatPassword']"></v-text-field>
            <v-text-field v-model="model.firstName" label="Imię" :error-messages="errors['FirstName']"></v-text-field>
            <v-text-field v-model="model.surname" label="Nazwisko" :error-messages="errors['Surname']"></v-text-field>
            <v-text-field v-model="model.email" label="Adres email" :error-messages="errors['Email']"></v-text-field>
            <v-btn v-on:click="register()">Zarejestruj się</v-btn>
        </v-form>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import AuthService from "@/services/AuthService";
    import {RegisterModel} from '@/interfaces/auth';

    @Component({name: 'register-component'})
    export default class RegisterComponent extends Vue {
        private errors = {};
        private model: RegisterModel = {
            surname: '',
            userName: '',
            firstName: '',
            password: '',
            repeatPassword: '',
            email: ''
        };

        async register() {
            try {
                await AuthService.register(this.model);
                this.errors = {};
                this.model = {
                    surname: '',
                    userName: '',
                    firstName: '',
                    password: '',
                    repeatPassword: '',
                    email: ''
                };

                this.$store.state.alert = {
                    show: true,
                    type: 'success',
                    message: 'Konto zostało dodane, teraz możesz się zalogować'
                };
            }  catch (ex) {
                if (ex.code === 400) this.errors = ex.data.errors;
            }
        }
    }
</script>
