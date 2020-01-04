<template>
    <v-container class="flex flex-center">
        <v-form @keyup.enter="login()" ref="form">
            <v-text-field v-model="model.username" label="Nazwa użytkownika" :error-messages="errors['UserName']" required></v-text-field>
            <v-text-field type='password' v-model="model.password" label="Hasło" :error-messages="errors['Password']" required></v-text-field>
            <v-btn v-on:click="login()">Zaloguj się</v-btn>
        </v-form>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import { LoginModel } from '@/interfaces/auth';

    @Component({ name: 'login-component'})
    export default class LoginComponent extends Vue {
        private errors = {};
        private model: LoginModel = {
            username: '',
            password: ''
        };

        async login() {
            (await (this as any).$auth.login)({
                headers: {
                    'Content-Type': 'application/json'
                },
                data: this.model
            }).then((response: any) => {
                Vue.prototype.startSignalR((this as any).$auth.token());
            }, (response: any) => {
                if(response.data.status === 400)this.errors = response.data.errors;
            });
        }
    }
</script>
