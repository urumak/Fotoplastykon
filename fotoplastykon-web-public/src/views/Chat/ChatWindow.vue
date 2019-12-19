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
                <v-icon @click="close()">mdi-close</v-icon></v-toolbar>
            <div v-if="expanded" class="chat-messages" ref="window">
                <div v-for="item in messages" :key="'cw' + item.id" class="col-12 message-row">
                    <v-card :class="'message-card' + (item.isSender ? ' float-right' : ' primary')">{{ item.messageText }}</v-card>
                </div>
            </div>
            <v-text-field v-if="expanded" v-model="currentMessage" label="Napisz wiadomość" @keyup.enter.native="sendMessage()" hide-details solo></v-text-field>
        </v-card>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import { mapGetters } from "vuex";
    import { Watch, Prop } from 'vue-property-decorator';
    import { ChatWindowModel, Message, MessageReceived } from '@/interfaces/chat';
    import ChatService from '@/services/ChatService';

    @Component({})
    export default class ChatWindow extends Vue {
        $refs!: {
            window: any;
        };

        private expanded = true;
        private currentMessage = '';

        @Prop({default: {
            id: 0,
            nameAndSurname: '',
            photoUrl: '',
            messages: []
        }}) private model!: ChatWindowModel;

        private get messages() : Message[]
        {
            return this.model.messages.items.sort((first: Message, second: Message) => {
                if (first.dateCreated < second.dateCreated ){
                    return -1;
                }
                if (first.dateCreated > second.dateCreated ){
                    return 1;
                }
                return 0;
            });
        }

        async created () {
            (this as any).$chatHub.$on('chat-message-received', this.onMessageReceived);
        }

        mounted() {
            this.$refs.window.scrollTop = this.$refs.window.scrollHeight;
        }

        updated() {
            this.$refs.window.scrollTop = this.$refs.window.scrollHeight;
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
        }

        async sendMessage() {
            let message = await ChatService.sendMessage({
                receiverId: this.model.id,
                messageText: this.currentMessage,
            });

            this.model.messages.items.push(message);
            this.currentMessage = '';
        }

        onMessageReceived(senderId: number, message: Message) {
            if(senderId == this.model.id) this.model.messages.items.push(message);
        }
    }
</script>
