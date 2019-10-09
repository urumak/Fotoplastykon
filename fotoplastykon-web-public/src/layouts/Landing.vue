<template>
    <div>
        <v-app-bar class="app-bar-standard" app>
            <v-toolbar-title class="headline text-uppercase">
                <span class="font-weight-light">Fotoplastykon</span>
            </v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn v-if="$auth.check()" text @click="$auth.logout()">
                <span class="mr-2">Wyloguj się</span>
            </v-btn>
            <v-btn v-else text :to="{ name: 'login' }">
                <span class="mr-2">Zaloguj się</span>
            </v-btn>
            <v-btn text :to="{ name: 'register' }">
                <span class="mr-2">Zarejestruj się</span>
            </v-btn>
        </v-app-bar>
        <v-content>
            <div class="float-left film-tape">
            </div>
        </v-content>
    </div>
</template>
