<template>
    <div>
        <v-card class="chat-card">
            <v-toolbar class="primary chat-header" @click="toggle()">
                <v-avatar class="chat-window-avatar" min-width="40px" width="40px" min-height="30px" height="30px">
                    <v-img v-if="model.photoUrl != null && model.photoUrl.length != 0" :src='model.photoUrl'></v-img>
                    <v-img v-else src="@/assets/subPhoto.png"></v-img>
                </v-avatar>
                {{ model.nameAndSurname }}
                <v-icon class="nav-icon" @click="close()">mdi-close</v-icon></v-toolbar>
            <div v-if="expanded" class="chat-messages">
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
                <div>test</div>
            </div>
            <v-text-field v-if="expanded" label="Napisz wiadomość" hide-details solo></v-text-field>
        </v-card>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import { mapGetters } from "vuex";
    import { Watch, Prop } from 'vue-property-decorator';
    import { ChatWindowModel } from '@/interfaces/chat';

    @Component({})
    export default class ChatWindow extends Vue {
        private expanded = true;

        @Prop({default: {
            id: 0,
            nameAndSurname: '',
            photoUrl: '',
            messages: []
        }}) private model!: ChatWindowModel;

        created() {

        }

        close() {
            this.$store.state.chat.activeWindows = this.$store.state.chat.activeWindows.filter((x: ChatWindowModel) => x.id !== this.model.id);
        }

        toggle() {
            this.expanded = !this.expanded;
        }
    }
</script>
