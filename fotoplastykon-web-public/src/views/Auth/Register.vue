<template>
    <v-container class="flex flex-center">
        <v-text-field v-model="user.userName" placeholder="Nazwa użytkownika"></v-text-field>
        <v-text-field v-model="user.password" placeholder="Hasło"></v-text-field>
        <v-text-field v-model="user.repeatPassword" placeholder="Powtórz hasło"></v-text-field>
        <v-text-field v-model="user.firstName" placeholder="Imię"></v-text-field>
        <v-text-field v-model="user.surname" placeholder="Nazwisko"></v-text-field>
        <v-text-field v-model="user.email" placeholder="Adres email"></v-text-field>
        <v-btn v-on:click="register()">Zarejestruj się</v-btn>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import AuthService, { RegisterModel }from "@/services/AuthService.ts";

    @Component({})
    export default class RegisterComponent extends Vue {
        private user: RegisterModel = {
            surname: '',
            userName: '',
            firstName: '',
            password: '',
            repeatPassword: '',
            email: ''
        };
        async register(): Promise<void> {
            await AuthService.register(this.user);
        }
    }
</script>
