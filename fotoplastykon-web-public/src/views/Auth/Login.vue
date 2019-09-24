<template>
    <v-container class="flex flex-center">
        <v-text-field v-model="username" placeholder="Nazwa użytkownika"></v-text-field>
        <v-text-field v-model="password" placeholder="Hasło"></v-text-field>
        <v-btn v-on:click="login()">Zaloguj się</v-btn>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import UsersService from "@/services/UsersService.ts";
    import AuthService from "@/services/AuthService.ts";

    @Component({})
    export default class LoginComponent extends Vue {
        private username: string = '';
        private password: string = '';
        async login(): Promise<void> {
            (await (this as any).$auth.login)({
                headers: {
                    'Content-Type': 'application/json'
                },
                data: {
                    userName: this.username,
                    password: this.password
                }
            });
        }
    }
</script>
