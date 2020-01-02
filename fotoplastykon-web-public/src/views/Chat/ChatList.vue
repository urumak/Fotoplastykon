<template>
    <div @scroll="pullMoreFriends" class="chat-container v-list custom-scroll">
        <v-list>
            <v-list-item-group class="chat-list">
                <v-list-item v-for="item in friends" :key="'ci' + item.id" @click="addChatWindow(item)">
                    <v-list-item-avatar>
                        <v-img v-if="item.photoUrl != null && item.photoUrl.length != 0" :src='item.photoUrl'></v-img>
                        <v-img v-else src="@/assets/subPhoto.png"></v-img>
                    </v-list-item-avatar>
                    <v-list-item-content>
                        <v-list-item-title>{{ item.nameAndSurname }}</v-list-item-title>
                    </v-list-item-content>
                </v-list-item>
            </v-list-item-group>
        </v-list>
        <v-text-field label="Szukaj" v-model="searchInput" hide-details solo class="chat-search"></v-text-field>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import ChatService from '@/services/ChatService.ts'
    import {ChatWindowModel} from '@/interfaces/chat';
    import {InfiniteScroll} from '@/interfaces/infiniteScroll';
    import { Watch } from 'vue-property-decorator';
    import { FriendListItem } from '@/interfaces/shared';

    @Component({})
    export default class ChatList extends Vue {
        private searchInput : string = "";

        async created() {
            if(!this.$store.state.chat.friends || this.$store.state.chat.friends.length === 0) await this.loadFriends();
            (this as any).$chatHub.$on('refresh-chat-list', this.reloadChatList);
        }

        beforeDestroy () {
            (this as any).$chatHub.$off('refresh-chat-list', this.reloadChatList);
        }

        private get scroll(): InfiniteScroll
        {
            return this.$store.state.chat.infiniteScroll;
        }

        private get friends(): FriendListItem[]
        {
            return this.$store.state.chat.friends;
        }

        async loadFriends() {
            let response = await ChatService.getFriends(this.scroll);
            if(this.$store.state.chat.friends) this.$store.state.chat.friends = this.$store.state.chat.friends.concat(response.items);
            else this.$store.state.chat.friends = response.items;

            this.scroll.setRowsLoaded(this.friends.length);
        }

        async reloadChatList() {
            this.$store.state.chat.infiniteScroll = new InfiniteScroll(20);
            this.$store.state.chat.friends = [];

            let response = await ChatService.getFriends(this.$store.state.chat.infiniteScroll);
            this.$store.state.chat.friends = response.items;
            this.$store.state.chat.infiniteScroll.rowsLoaded = response.items.length;
        }

        async pullMoreFriends(event: any) {
            if(event.target.scrollTop + event.target.clientHeight >= event.target.scrollHeight) {
                await this.loadFriends();
            }
        }

        @Watch('searchInput')
        async search() {
            if (this.searchInput){
                this.$store.state.chat.friends = [];
                this.scroll.setRowsLoaded(0);
                this.$store.state.chat.friends = await ChatService.searchFriends(this.searchInput);
            } else {
                this.scroll.restore();
                this.$store.state.chat.friends = [];
                await this.loadFriends();
            }
        }

        async addChatWindow(item: FriendListItem)
        {
            let chatWindow = null;
            if(this.$store.state.chat.activeWindows && this.$store.state.chat.activeWindows.length !== 0) chatWindow = this.$store.state.chat.activeWindows.find((x: ChatWindowModel) => x.id == item.id);

            if(!chatWindow)
            {
                this.$store.state.chat.activeWindows.push((await ChatService.getForWindows([item.id]))[0]);

                let windowsTmp = JSON.parse(localStorage.chatWindows);
                windowsTmp.push(item.id);
                localStorage.chatWindows = JSON.stringify(windowsTmp);
            }
        }
    }
</script>
