<template>
    <v-container class="flex flex-center">
        <v-form ref="form">
            <v-text-field v-model="form.userName" label="Nazwa użytkownika"></v-text-field>
            <v-text-field v-model="form.password" type="password" label="Hasło"></v-text-field>
            <v-text-field v-model="form.repeatPassword" type="password" label="Powtórz hasło"></v-text-field>
            <v-text-field v-model="form.firstName" label="Imię"></v-text-field>
            <v-text-field v-model="form.surname" label="Nazwisko"></v-text-field>
            <v-text-field v-model="form.email" label="Adres email"></v-text-field>
            <v-btn v-on:click="register()">Zarejestruj się</v-btn>
        </v-form>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import AuthService from "@/services/AuthService.ts";
    import Form from 'form-backend-validation';

    @Component({name: 'register-component'})
    export default class RegisterComponent extends Vue {
        private form = new Form({
            surname: '',
            userName: '',
            firstName: '',
            password: '',
            repeatPassword: '',
            email: ''
        });

        async register(): Promise<void> {

            let response: any = {};
            try {
                this.form.errors = null;
                response = await AuthService.register(this.form);
            }  catch (ex) {
                if (ex.code === 400) {
                    this.form.errors = ex.data.errors;
                    console.log(this.form.errors['Email']);
                }
            }
        }
    }
</script>
