<template>
    <div>
        <v-card class="chat-card">
            <v-toolbar class="primary chat-header" @click="toggle()">
                <v-avatar class="chat-window-avatar" min-width="40px" width="40px" min-height="30px" height="30px">
                    <v-img v-if="model.photoUrl != null && model.photoUrl.length != 0" :src='model.photoUrl'></v-img>
                    <v-img v-else src="@/assets/subPhoto.png"></v-img>
                </v-avatar>
                {{ model.nameAndSurname }}
                <v-spacer></v-spacer>
                <v-icon @click="close()" aria-label="asdasd">mdi-close</v-icon></v-toolbar>
            <div v-if="expanded" class="chat-messages">
                <div class="content" ref="window" @scroll="pullMoreMessages">
                    <div v-for="item in messages" :key="'cw' + item.id" class="col-12 message-row">
                        <v-card :class="'message-card' + (item.isSender ? ' float-right' : ' float-left primary')">{{ item.messageText }}</v-card>
                    </div>
                </div>
            </div>
            <v-text-field
                    v-if="expanded"
                    v-model="currentMessage"
                    label="Napisz wiadomość"
                    @keyup="onKeyUp"
                    @focus="onFocus()"
                    @blur="hasFocus = false"
                    hide-details solo>
            </v-text-field>
        </v-card>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import { mapGetters } from "vuex";
    import { Watch, Prop } from 'vue-property-decorator';
    import { ChatWindowModel, Message } from '@/interfaces/chat';
    import ChatService from '@/services/ChatService';
    import { InfiniteScroll } from '@/interfaces/infiniteScroll';

    @Component({})
    export default class ChatWindow extends Vue {
        $refs!: {
            window: any;
        };

        private expanded = true;
        private currentMessage = '';
        private hasFocus = true;
        private oldScrollHeight = 0;
        private keepScrollPosition = false;

        @Prop({default: {
            id: 0,
            nameAndSurname: '',
            photoUrl: '',
            messages: {
                infiniteScroll: new InfiniteScroll(20),
                items: []
            }
        }}) private model!: ChatWindowModel;

        private get messages() : Message[]
        {
            return this.model.messages.items.sort((first: Message, second: Message) => {
                if (first.dateCreated < second.dateCreated) {
                    return -1;
                }
                if (first.dateCreated > second.dateCreated) {
                    return 1;
                }
                return 0;
            });
        }

        async pullMoreMessages(event: any) {
            this.keepScrollPosition = true;
            if(event.target.scrollTop >= 0) this.oldScrollHeight = this.$refs.window.scrollHeight;
            if(event.target.scrollTop === 0) {
                this.model.messages.scroll.rowsLoaded = this.model.messages.items.length;
                let response = await ChatService.getMessages(this.model.messages.scroll, this.model.id);
                if(this.model.messages.items) this.model.messages.items = this.model.messages.items.concat(response.items);
                else this.model.messages.items = response.items;
            }
        }

        async created () {
            (this as any).$chatHub.$on('chat-message-received', this.onMessageReceived);
            await this.readMessages();
        }

        mounted() {
            if(this.$refs.window) this.$refs.window.scrollTop = this.$refs.window.scrollHeight;
        }

        updated() {
            if(this.$refs.window) {
                if(this.keepScrollPosition) this.$refs.window.scrollTop = this.$refs.window.scrollHeight - this.oldScrollHeight;
                else this.$refs.window.scrollTop = this.$refs.window.scrollHeight;
            }
            this.keepScrollPosition = false;
        }

        beforeDestroy () {
            (this as any).$chatHub.$off('chat-message-received', this.onMessageReceived);
        }

        close() {
            if(this.$store.state.chat.activeWindows.length == 1) this.$store.state.chat.activeWindows = [];
            else this.$store.state.chat.activeWindows = this.$store.state.chat.activeWindows.filter((x: ChatWindowModel) => x.id !== this.model.id);

            let windowsTmp = JSON.parse(localStorage.chatWindows);
            windowsTmp = windowsTmp.filter((x: number) => x !== this.model.id);
            localStorage.chatWindows = JSON.stringify(windowsTmp);
        }

        toggle() {
            this.expanded = !this.expanded;
            this.hasFocus = false;
        }

        async onKeyUp(event: any) {
            this.keepScrollPosition = false;
            if(event.code === 'Enter'){
                this.sendMessage();
            }
        }

        async sendMessage() {
            await ChatService.sendMessage({
                receiverId: this.model.id,
                messageText: this.currentMessage,
            });

            this.currentMessage = '';
        }

        onMessageReceived(senderId: number, message: Message) {
            this.keepScrollPosition = false;
            if(!this.hasFocus && !this.expanded && senderId !== (this as any).$auth.user().id) this.$store.state.chat.unreadMessagesFromIds.push(senderId);
            if(senderId == this.model.id) this.model.messages.items.push(message);
        }

        async onFocus() {
            this.keepScrollPosition = false;
            this.hasFocus = true;
            this.readMessages();
        }

        async readMessages() {
            this.$store.state.chat.unreadMessagesFromIds = this.$store.state.chat.unreadMessagesFromIds.filter((x: number) => x !== this.model.id);
            await ChatService.updateLastReadingDate(this.model.id);
        }
    }
</script>
