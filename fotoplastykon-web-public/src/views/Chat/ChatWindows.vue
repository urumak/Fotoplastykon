<template>
    <div class="row chat-area">
        <chat-window class="chat-window" v-for="item in windows" :key="'cw' + item.id" :model="item"></chat-window>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import ChatWindow from '@/views/Chat/ChatWindow.vue';
    import {ChatWindowModel} from '@/interfaces/chat';
    import ChatService from '@/services/ChatService';

    @Component({components: { 'chat-window': ChatWindow}})
    export default class ChatWindows extends Vue {
        private get windows() : ChatWindowModel[]
        {
            return this.$store.state.chat.activeWindows;
        }

        async created () {
            if(!localStorage.chatWindows) localStorage.chatWindows = '[]';
            console.log()
            this.$store.state.chat.activeWindows = await ChatService.getForWindows(JSON.parse(localStorage.chatWindows)) || [];
            (this as any).$chatHub.$on('chat-message-received', this.onMessageReceived);
        }

        beforeDestroy () {
            (this as any).$chatHub.$off('chat-message-received', this.onMessageReceived);
        }

        onMessageReceived() {
            console.log('halko here');
        }
    }
</script>
