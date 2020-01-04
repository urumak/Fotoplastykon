<template>
    <v-autocomplete
            class="flex ml-5 mt-8 mr-5"
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
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import SearchService from "@/services/SearchService.ts";
    import { SearchItem } from '@/interfaces/search';
    import { Watch } from 'vue-property-decorator';
    import ChatWindows from '@/components/chat/ChatWindows.vue';
    import ChatList from '@/components/chat/ChatList.vue';
    import NotificationsPopover from '@/components/popovers/NotificationsPopover.vue';
    import ProfilePopover from '@/components/popovers/ProfilePopover.vue';
    import ChatService from '@/services/ChatService';
    import { Message } from '@/interfaces/chat';
    import MessagesPopover from '@/components/popovers/MessagesPopover.vue';

    @Component({})
    export default class NavbarSearchComponent extends Vue {
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
                        if((this as any).$auth.user().id === this.selectedItem.id) await this.$router.push({ name: 'user-profile' });
                        else await this.$router.push({ name: 'user-page', params: { id: this.selectedItem.id }});
                        break;
                }
            }
        }
    }
</script>
