<template>
    <v-container class="flex flex-center">
        <v-form @keyup.enter="login()" ref="form">
            <v-text-field v-model="form.username" label="Nazwa użytkownika" required></v-text-field>
            <v-text-field type='password' v-model="form.password" label="Hasło" required></v-text-field>
            <v-checkbox
                    v-model="rememberMe"
                    label="Zapamiętaj mnie"
                    required
            ></v-checkbox>
            <v-btn v-on:click="login()">Zaloguj się</v-btn>
        </v-form>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import UsersService from "@/services/UsersService.ts";
    import AuthService from "@/services/AuthService.ts";
    import Form from 'form-backend-validation';

    @Component({ name: 'login-component'})
    export default class LoginComponent extends Vue {
        private valid = true;
        private rememberMe = false;

        private form = new Form({
            username: '',
            password: '',
        });

        async login(): Promise<void> {
            (await (this as any).$auth.login)({
                headers: {
                    'Content-Type': 'application/json'
                },
                data: this.form,
                rememberMe: this.rememberMe
            });
        }
    }
</script>
