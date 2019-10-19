<template>
    <div>
        <v-app-bar class="app-bar-standard" app>
            <v-toolbar-title class="headline text-uppercase mr-6">
                <span class="font-weight-light">Fotoplastykon</span>
            </v-toolbar-title>
            <v-text-field label="Szukaj" class="mt-7 mr-6" outlined dense single-line></v-text-field>
            <v-btn v-if="$auth.check()" class="mr-2" text>
                <span>Filmy</span>
            </v-btn>
            <v-btn v-if="$auth.check()" class="mr-2" text>
                <span>Ludzie kina</span>
            </v-btn>
            <v-btn v-if="$auth.check()" class="mr-2" text>
                <span>Forum</span>
            </v-btn>
            <v-btn v-if="$auth.check()" class="mr-2" text>
                <span>Quizy</span>
            </v-btn>
            <v-spacer></v-spacer>
            <v-btn v-if="!$auth.check()" text class="mr-2" :to="{name:'home'}">
                <span>Zaloguj się</span>
            </v-btn>
            <v-btn v-if="!$auth.check()" text  class="mr-2" :to="{name:'home'}">
                <span>Zarejestruj się</span>
            </v-btn>
            <v-badge v-if="$auth.check()" class="mr-4" color="red" overlap>
                <template v-slot:badge>
                <span>1</span>
                </template>
                <v-icon class="nav-icon">mdi-chat</v-icon>
            </v-badge>
            <v-badge v-if="$auth.check()" color="red" class="mr-4" overlap>
                <template v-slot:badge>
                <span>1</span>
                </template>
                <v-icon class="nav-icon">mdi-bell</v-icon>
            </v-badge>
            <v-avatar v-if="$auth.check()" class="nav-avatar mr-2" @click="$auth.logout()">
                <v-img src="@/assets/bird.jpg"></v-img>
            </v-avatar>
        </v-app-bar>
        <v-content style="margin-top:60px;">
            <v-container class="float-right flex flex-center">
                <slot />
            </v-container>
            <div class="float-left film-tape">
            </div>
        </v-content>
    </div>
</template>
