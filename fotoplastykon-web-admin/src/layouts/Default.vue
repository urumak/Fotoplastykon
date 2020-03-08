<template>
    <div>
        <app-alert></app-alert>
        <v-app-bar class="app-bar-standard" app>
            <v-toolbar-title class="headline text-uppercase">
                <router-link :to="{ name: 'home' }" class="font-weight-light custom-link">Fotoplastykon</router-link>
            </v-toolbar-title>
            <v-spacer></v-spacer>
            <profile-popover></profile-popover>
        </v-app-bar>
        <v-navigation-drawer
                fixed
                class="secondary"
                style="margin-top: 64px"
        >
            <v-divider></v-divider>
            <v-list dense>
                <v-list-item
                        v-for="(item, index) in items"
                        :key="index"
                        @click="navigate(item.routeName)"
                >
                    <v-list-item-icon>
                        <v-icon>{{ item.icon }}</v-icon>
                    </v-list-item-icon>
                    <v-list-item-content>
                        <v-list-item-title>{{ item.name }}</v-list-item-title>
                    </v-list-item-content>
                </v-list-item>
            </v-list>
        </v-navigation-drawer>
        <v-content style="margin-top:60px;">
            <v-container class="main-content" style="margin-right: 0px; margin-left: 350px;">
                <div class="row">
                    <div class="col-lg-12" style="padding-left: 60px; padding-right: 100px;">
                        <slot />
                    </div>
                </div>
            </v-container>
        </v-content>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import ProfilePopover from '@/components/popovers/ProfilePopover.vue';
    import menuItems from '@/menu.json'
    import { MenuListItem } from '@/interfaces/menu';
    import AppAlertComponent from '@/components/AppAlert.vue';

    @Component({components: {
        'profile-popover': ProfilePopover,
        'app-alert': AppAlertComponent
    }})
    export default class Default extends Vue {
        private items : MenuListItem[] = menuItems;

        async navigate(routeName: string) {
            await this.$router.push({ name: routeName });
        }
    }
</script>
