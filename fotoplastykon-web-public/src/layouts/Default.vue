<template>
    <div>
        <v-app-bar class="app-bar-standard" app>
            <v-toolbar-title class="headline text-uppercase">
                <router-link :to="{ name: 'information' }" class="font-weight-light custom-link">Fotoplastykon</router-link>
            </v-toolbar-title>
            <v-spacer></v-spacer>
            <v-autocomplete
                    class="flex ml-1"
                    style="margin-top:30px;"
                    label="Szukaj"
                    :items="items"
                    v-model="selectedItem"
                    :search-input.sync="searchInput"
                    item-text="value"
                    item-value="id"
                    dense
                    solo
                    clearable
                    return-object
                    @change="onChange()"
                    no-data-text="Brak wyników"
                    autocomplete="off">
                <template slot="selection" slot-scope="data">
                    <v-flex xs2>
                        <v-avatar>
                            <v-img v-if="data.item.photoUrl != null && data.item.photoUrl.length != 0" :src='data.item.photoUrl'></v-img>
                            <v-img v-else src="@/assets/subPhoto.png"></v-img>
                        </v-avatar>
                    </v-flex>
                    <v-flex class='ml-1'>
                        {{ data.item.value }}
                    </v-flex>
                </template>
                <template slot="item" slot-scope="data">
                    <v-avatar>
                        <v-img v-if="data.item.photoUrl != null && data.item.photoUrl.length != 0" :src='data.item.photoUrl'></v-img>
                        <v-img v-else src="@/assets/subPhoto.png"></v-img>
                    </v-avatar>
                    <v-flex v-html="data.item.value"></v-flex>
                </template>
            </v-autocomplete>
            <v-spacer></v-spacer>
            <v-btn class="mr-2" text>
                <span>Filmy</span>
            </v-btn>
            <v-btn class="mr-2" text>
                <span>Ludzie kina</span>
            </v-btn>
            <v-btn v-if="$auth.check()" :to="{ name: 'forum' }" class="mr-2" text>
                <span>Forum</span>
            </v-btn>
            <v-btn v-if="$auth.check()" :to="{ name: 'quizzes' }" class="mr-2" text>
                <span>Quizy</span>
            </v-btn>
            <v-spacer></v-spacer>
            <v-btn v-if="!$auth.check()" text class="mr-2" :to="{name:'home'}">
                <span>Zaloguj się</span>
            </v-btn>
            <v-btn v-if="!$auth.check()" text  class="mr-2" :to="{name:'home'}">
                <span>Zarejestruj się</span>
            </v-btn>
            <messages-popover></messages-popover>
            <notifications-popover></notifications-popover>
            <profile-popover></profile-popover>
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
    import SearchService from "@/services/SearchService.ts";
    import { SearchItem } from '@/interfaces/search';
    import { Watch } from 'vue-property-decorator';
    import ChatWindows from '@/views/Chat/ChatWindows.vue';
    import ChatList from '@/views/Chat/ChatList.vue';
    import NotificationsPopover from '@/components/NotificationsPopover.vue';
    import ProfilePopover from '@/components/ProfilePopover.vue';
    import ChatService from '@/services/ChatService';
    import { Message } from '@/interfaces/chat';
    import MessagesPopover from '@/components/MessagesPopover.vue';

    @Component({components: {
        'chat-windows': ChatWindows,
        'chat-list': ChatList,
        'notifications-popover': NotificationsPopover,
        'messages-popover': MessagesPopover,
        'profile-popover': ProfilePopover
    }})
    export default class Default extends Vue {
        private items : SearchItem[] = [];
        private selectedItem : any = null;
        private searchInput : string = "";

        @Watch('searchInput')
        async search() {
            if (this.searchInput){
                this.items = await SearchService.getOptions(this.searchInput);
            } else {
                this.items = [];
                this.selectedItem = {};
            }
        }

        async onChange() {
            if(this.selectedItem) {
                switch(this.selectedItem.type) {
                    case 0:
                        await this.$router.push({ name: 'film-page', params: { id: this.selectedItem.id }});
                        break;
                    case 1:
                        await this.$router.push({ name: 'film-person-page', params: { id: this.selectedItem.id }});
                        break;
                    case 2:
                        await this.$router.push({ name: 'user-page', params: { id: this.selectedItem.id }});
                        break;
                }
            }
        }
    }
</script>
