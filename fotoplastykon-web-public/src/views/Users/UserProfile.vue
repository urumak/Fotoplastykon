<template>
    <v-container class="flex flex-center">
        <v-card>
            <div>{{ $auth.user().firstName }}</div>
            <div>{{ $auth.user().surname }}</div>
            <div>{{ $auth.user().userName }}</div>
            <div class="col-3">
                <v-img v-if="photoUrl || $auth.user().photoUrl" contain :src="photoUrl ? photoUrl : $auth.user().photoUrl"></v-img>
                <v-img v-else src="@/assets/subPhoto.png"></v-img>
            </div>
            <v-file-input
                    accept="image/png, image/jpeg, image/bmp"
                    placeholder="Wybierz zdjęcie profilowe"
                    prepend-icon="mdi-camera"
                    label="Zdjęcie profilowe"
                    outlined
                    v-model="newPhoto"
            ></v-file-input>
            <v-btn class="primary" @click="changeProfilePhoto()">Zmień zdjęcie</v-btn>
        </v-card>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import AccountService from '@/services/AccountService';
    import AuthService from '@/services/AuthService';

    @Component({})
    export default class UserProfileComponent extends Vue {
        private newPhoto = [];

        private get photoUrl() {
            return this.$store.state.user.photoUrl;
        }

        async changeProfilePhoto() {
            await AccountService.changeProfilePhoto(this.newPhoto);
            await (this as any).$auth.user(await AuthService.profile())

            this.$store.state.user.photoUrl = (this as any).$auth.user().photoUrl;
            (this as any).$forceUpdate();
        }
    }
</script>
