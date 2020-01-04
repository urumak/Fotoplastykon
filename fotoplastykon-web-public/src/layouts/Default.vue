<template>
    <div>
        <app-alert></app-alert>
        <v-app-bar class="app-bar-standard" app>
            <v-toolbar-title class="headline text-uppercase">
                <router-link :to="{ name: 'home' }" class="font-weight-light custom-link">Fotoplastykon</router-link>
            </v-toolbar-title>
            <navbar-search></navbar-search>
            <v-btn class="mr-2" :to="{ name: 'information-list' }" text>
                <span>Aktualności</span>
            </v-btn>
            <v-btn class="mr-2" :to="{ name: 'films' }" text>
                <span>Filmy</span>
            </v-btn>
            <v-btn class="mr-2" :to="{ name: 'film-people' }" text>
                <span>Ludzie kina</span>
            </v-btn>
            <v-btn v-if="$auth.check()" :to="{ name: 'forum' }" class="mr-2" text>
                <span>Forum</span>
            </v-btn>
            <v-btn v-if="$auth.check()" :to="{ name: 'quizzes' }" class="mr-2" text>
                <span>Quizy</span>
            </v-btn>
            <v-spacer></v-spacer>
            <v-btn v-if="!$auth.check()" text class="mr-2" :to="{name:'landing'}">
                <span>Zaloguj się</span>
            </v-btn>
            <v-btn v-if="!$auth.check()" text  class="mr-2" :to="{name:'landing'}">
                <span>Zarejestruj się</span>
            </v-btn>
            <messages-popover v-if="$auth.check()"></messages-popover>
            <notifications-popover v-if="$auth.check()"></notifications-popover>
            <profile-popover v-if="$auth.check()"></profile-popover>
        </v-app-bar>
        <v-content style="margin-top:60px;">
            <v-container class="float-right flex flex-center">
                <div class="row  main-content">
                    <div class="col-lg-8">
                        <slot />
                    </div>
                </div>
                <chat-windows></chat-windows>
            </v-container>
            <div class="float-left film-tape">
            </div>
            <chat-list v-if="$auth.check()"></chat-list>
        </v-content>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import ChatWindows from '@/components/chat/ChatWindows.vue';
    import ChatList from '@/components/chat/ChatList.vue';
    import NotificationsPopover from '@/components/popovers/NotificationsPopover.vue';
    import ProfilePopover from '@/components/popovers/ProfilePopover.vue';
    import MessagesPopover from '@/components/popovers/MessagesPopover.vue';
    import NavbarSearchComponent from '@/components/search/NavbarSearch.vue';
    import AppAlertComponent from '@/components/AppAlert.vue';

    @Component({components: {
        'chat-windows': ChatWindows,
        'chat-list': ChatList,
        'notifications-popover': NotificationsPopover,
        'messages-popover': MessagesPopover,
        'profile-popover': ProfilePopover,
        'navbar-search': NavbarSearchComponent,
        'app-alert': AppAlertComponent
    }})
    export default class Default extends Vue {

    }
</script>
