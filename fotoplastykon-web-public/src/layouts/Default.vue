<template>
    <div>
        <v-app-bar class="app-bar-standard" app>
            <v-toolbar-title class="headline text-uppercase">
                <span class="font-weight-light">Fotoplastykon</span>
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
                    item-value="key"
                    dense
                    solo
                    clearable
                    @keyup.enter="submit()"
                    @keyup="search()"
                    @change="onChange()"
                    no-data-text="Brak wyników"
                    autocomplete="off"
            >
                <template slot="selection" slot-scope="data">
                    <v-flex xs2>
                        <v-avatar>
                            <v-img v-if="data.item.photoUrl != null && data.item.photoUrl.length != 0" :src='data.item.photoUrl'></v-img>
                            <v-img v-else src="@/assets/bird.jpg"></v-img>
                        </v-avatar>
                    </v-flex>
                    <v-flex class='ml-1'>
                        {{ data.item.value }}
                    </v-flex>
                </template>
                <template slot="item" slot-scope="data">
                    <v-avatar>
                        <v-img v-if="data.item.photoUrl != null && data.item.photoUrl.length != 0" :src='data.item.photoUrl'></v-img>
                        <v-img v-else src="@/assets/bird.jpg"></v-img>
                    </v-avatar>
                        <v-flex v-html="data.item.value"></v-flex>
                </template>
            </v-autocomplete>
            <v-spacer></v-spacer>
            <v-btn v-if="$auth.check()" class="mr-2" text>
                <span>Filmy</span>
            </v-btn>
            <v-btn v-if="$auth.check()" class="mr-2" text>
                <span>Ludzie kina</span>
            </v-btn>
            <v-btn v-if="$auth.check()" class="mr-2" text>
                <span>Forum</span>
            </v-btn>
            <v-btn v-if="$auth.check()" class="mr-2" text>
                <span>Quizy</span>
            </v-btn>
            <v-spacer></v-spacer>
            <v-btn v-if="!$auth.check()" text class="mr-2" :to="{name:'home'}">
                <span>Zaloguj się</span>
            </v-btn>
            <v-btn v-if="!$auth.check()" text  class="mr-2" :to="{name:'home'}">
                <span>Zarejestruj się</span>
            </v-btn>
            <v-badge v-if="$auth.check()" class="mr-4" color="red" overlap>
                <template v-slot:badge>
                <span>1</span>
                </template>
                <v-icon class="nav-icon">mdi-chat</v-icon>
            </v-badge>
            <v-badge v-if="$auth.check()" color="red" class="mr-4" overlap>
                <template v-slot:badge>
                <span>1</span>
                </template>
                <v-icon class="nav-icon">mdi-bell</v-icon>
            </v-badge>
            <v-avatar v-if="$auth.check()" class="nav-avatar mr-2" @click="$auth.logout()">
                <v-img src="@/assets/bird.jpg"></v-img>
            </v-avatar>
        </v-app-bar>
        <v-content style="margin-top:60px;">
            <v-container class="float-right flex flex-center">
                <slot />
            </v-container>
            <div class="float-left film-tape">
            </div>
        </v-content>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import SearchService, {LinkedItem} from "@/services/SearchService.ts";

    @Component({})
    export default class Default extends Vue {
        private items : LinkedItem[] = [];
        private selectedItem : any = {};
        private searchInput : string = "";
        private filesEndpoint = process.env.PUBLIC_FILES_ENDPOINT;
        async search() {
            console.log(this.selectedItem);
            if(this.searchInput.length > 0) this.items = await SearchService.getOptions(this.searchInput);
            else this.items = [];
        }

        async onChange() {
            console.log("change");
        }

        async submit() {
            console.log("submit");
        }
    }
</script>
