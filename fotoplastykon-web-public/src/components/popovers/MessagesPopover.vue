<template>
    <div>
        <v-menu
                offset-y
                transition="slide-y-transition"
                :close-on-content-click="true"
                :nudge-width="300">
            <template v-slot:activator="{ on }">
                <v-badge class="mr-4" color="red" overlap>
                    <template v-if="$store.state.chat.unreadMessagesFromIds.length > 0"  v-slot:badge>
                        <span>{{ $store.state.chat.unreadMessagesFromIds.length }}</span>
                    </template>
                    <v-icon class="nav-icon" v-on="on" @click="loadData()">mdi-chat</v-icon>
                </v-badge>
            </template>
            <div @scroll="pullMoreMessages" class="custom-scroll" ref="lastMessages">
                <v-list>
                    <v-list-item
                            v-if="messages.length === 0"
                    >
                        <v-list-item-content>
                            <v-list-item-title>Brak wiadomości</v-list-item-title>
                            <v-list-item-subtitle>Napisz wiadomość wybirając znajmych z listy po prawej stronie ekranu.</v-list-item-subtitle>
                        </v-list-item-content>
                    </v-list-item>
                    <v-list-item
                            v-for="(item, index) in messages"
                            :key="index"
                            :class="item.unread ? 'secondary' : ''"
                            @click="addChatWindow(item)"
                    >
                        <v-list-item-avatar>
                            <v-img v-if="item.photoUrl != null && item.photoUrl.length != 0" :src='item.photoUrl'></v-img>
                            <v-img v-else src="@/assets/subPhoto.png"></v-img>
                        </v-list-item-avatar>
                        <v-list-item-content>
                            <v-list-item-title>{{ item.nameAndSurname }}</v-list-item-title>
                            <v-list-item-subtitle>{{ getListItemSubtitlePrefix(item) + item.messageText }}</v-list-item-subtitle>
                        </v-list-item-content>
                    </v-list-item>
                </v-list>
            </div>
        </v-menu>
    </div>
</template>
<style>
    .v-menu__content{
        box-shadow: 0px 5px 5px -3px rgba(0, 0, 0, 0.2), 0px 8px 10px 1px rgba(0, 0, 0, 0.14), 0px 15px 30px -15px rgba(0, 0, 0, 0.12) !important
    }
</style>
<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import { Prop } from 'vue-property-decorator';
    import ChatService from '@/services/ChatService';
    import { Message, LastMessage } from '@/interfaces/chat';
    import { InfiniteScroll } from '@/interfaces/infiniteScroll';

    @Component({})
    export default class MessagesPopover extends Vue {
        $refs!: {
            lastMessages: any;
        };

        private messages : LastMessage[] = [];
        private infiniteScroll : InfiniteScroll = new InfiniteScroll(2);

        async created() {
            (this as any).$chatHub.$on('chat-message-received', this.onMessageReceived);
            this.$store.state.chat.unreadMessagesFromIds = await ChatService.getUnreadMessagesUsersIds();
        }

        beforeDestroy () {
            (this as any).$chatHub.$off('chat-message-received', this.onMessageReceived);
        }

        onMessageReceived(senderId: number, message: Message) {
            let windows = JSON.parse(localStorage.chatWindows);
            if(!this.$store.state.chat.unreadMessagesFromIds.includes(senderId)
                && !windows.includes(senderId)
                && senderId !== (this as any).$auth.user().id) this.$store.state.chat.unreadMessagesFromIds.push(senderId);
        }

        async loadData() {
            this.scrollTop();
            this.infiniteScroll.setRowsLoaded(0);
            this.messages = (await ChatService.getLastMessagesForEachFriend(this.infiniteScroll)).items;
            this.infiniteScroll.setRowsLoaded(this.messages.length);
        }

        scrollTop() {
            if(this.$refs.lastMessages) this.$refs.lastMessages.scrollTop = 0;
        }

        async pullMoreMessages(event: any) {
            if(event.target.scrollTop + event.target.clientHeight >= event.target.scrollHeight) {
                let response = await ChatService.getLastMessagesForEachFriend(this.infiniteScroll);
                if(this.messages) this.messages = this.messages.concat(response.items);
                else this.messages = response.items;

                this.infiniteScroll.setRowsLoaded(this.messages.length);
            }
        }

        async addChatWindow(item: LastMessage)
        {
            item.unread = false;
            let chatWindow = null;
            if(this.$store.state.chat.activeWindows && this.$store.state.chat.activeWindows.length !== 0)
                chatWindow = this.$store.state.chat.activeWindows.find((x: LastMessage) => x.id == item.friendId);

            if(!chatWindow)
            {
                this.$store.state.chat.activeWindows.push((await ChatService.getForWindows([item.friendId]))[0]);

                let windowsTmp = JSON.parse(localStorage.chatWindows);
                windowsTmp.push(item.friendId);
                localStorage.chatWindows = JSON.stringify(windowsTmp);
            }
        }

        getListItemSubtitlePrefix(item: LastMessage) {
            return item.senderId !== (this as any).$auth.user().id ? (item.nameAndSurname + ': ') : 'Ty: ';
        }
    }
</script>
