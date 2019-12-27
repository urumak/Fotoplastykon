<template>
    <v-menu
            offset-y
            transition="slide-y-transition"
            :close-on-content-click="true"
            :nudge-width="200">
        <template v-slot:activator="{ on }">
            <v-avatar v-if="$auth.check()" class="nav-avatar mr-2" v-on="on">
                <v-img v-if="photoUrl || $auth.user().photoUrl" :src="photoUrl ? photoUrl : $auth.user().photoUrl"></v-img>
                <v-img v-else src="@/assets/subPhoto.png"></v-img>
            </v-avatar>
        </template>
        <div>
            <v-list>
                <v-list-item @click="profile()">
                    <v-list-item-title>Profil</v-list-item-title>
                </v-list-item>
                <v-list-item @click="logout()">
                    <v-list-item-title>Wyloguj się</v-list-item-title>
                </v-list-item>
            </v-list>
        </div>
    </v-menu>
</template>
<style>
    .v-menu__content{
        box-shadow: 0px 5px 5px -3px rgba(0, 0, 0, 0.2), 0px 8px 10px 1px rgba(0, 0, 0, 0.14), 0px 15px 30px -15px rgba(0, 0, 0, 0.12) !important
    }
</style>
<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";

    @Component({})
    export default class ProfilePopover extends Vue {

        private get photoUrl() {
            return this.$store.state.user.photoUrl;
        }

        logout() {
            Vue.prototype.stopSignalR();
            this.$store.commit('resetState', this.$store.state);
            localStorage.clear();
            (this as any).$auth.logout();
        }

        async profile() {
            await this.$router.push({ name: 'user-profile' });
        }
    }
</script>
