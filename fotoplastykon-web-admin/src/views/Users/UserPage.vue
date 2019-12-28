<template>
    <v-container class="flex flex-center">
        <v-card>
            <div>{{ userModel.firstName }}</div>
            <div>{{ userModel.surname }}</div>
            <div>{{ userModel.userName }}</div>
            <div class="col-3">
                <v-img v-if="userModel.photoUrl" contain :src="userModel.photoUrl"></v-img>
                <v-img v-else src="@/assets/subPhoto.png"></v-img>
            </div>
        </v-card>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import { UserPage } from '@/interfaces/users';
    import UsersService from '@/services/UsersService';
    import { Watch } from 'vue-property-decorator';

    @Component({})
    export default class FilmPersonPageComponent extends Vue {
        private userModel : UserPage = {
            id: 0,
            userName: '',
            firstName: '',
            surname: '',
            photoUrl: '',
            isFriend: false,
            invitationSent: false,
            isInvitationSender: false,
        };

        private get id() : number {
            return Number(this.$route.params.id || 0);
        }
    }
</script>
