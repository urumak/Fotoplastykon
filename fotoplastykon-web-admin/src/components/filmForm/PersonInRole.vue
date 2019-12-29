<template>
    <div>
        <v-row style="margin-top:30px;">
            <v-autocomplete
                    class="flex ml-1 col-4"
                    label="Osoba"
                    :items="people"
                    v-model="selectedItem"
                    :search-input.sync="peopleSearchInput"
                    item-text="nameAndSurname"
                    item-value="id"
                    chip
                    clearable
                    return-object
                    no-data-text="Brak wyników"
                    autocomplete="off">
                <template slot="selection" slot-scope="data">
                    <v-flex xs2>
                        <v-avatar class="small-avatar">
                            <v-img v-if="data.item.photoUrl != null && data.item.photoUrl.length != 0" :src='data.item.photoUrl'></v-img>
                            <v-img v-else src="@/assets/subPhoto.png"></v-img>
                        </v-avatar>
                    </v-flex>
                    <v-flex class='ml-1'>
                        {{ data.item.nameAndSurname }}
                    </v-flex>
                </template>
                <template slot="item" slot-scope="data">
                    <v-avatar class="small-avatar">
                        <v-img v-if="data.item.photoUrl != null && data.item.photoUrl.length != 0" :src='data.item.photoUrl'></v-img>
                        <v-img v-else src="@/assets/subPhoto.png"></v-img>
                    </v-avatar>
                    <v-flex v-html="data.item.nameAndSurname"></v-flex>
                </template>
            </v-autocomplete>
            <v-select :items="roleTypes" v-model="model.role" label="Rola" item-text="value" item-value="key" class="col-3">
            </v-select>
            <v-text-field label="Postać" v-model="model.characterName" class="col-3">
            </v-text-field>
            <v-btn class="secondary" small @click="deleteRole()"><v-icon>mdi-minus</v-icon></v-btn>
        </v-row>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import { Prop, Watch } from 'vue-property-decorator';
    import {PersonInRoleForm, RoleTypeDictionary, PersonDropDownModel} from "@/interfaces/films";
    import FilmsService from '@/services/FilmsService';

    @Component({})
    export default class PersonInRoleComponent extends Vue {
        @Prop({default: {
                id: 0,
                personId: 0,
                role: 0,
                characterName: ''
            }
        }) private model!: PersonInRoleForm;

        private people : PersonDropDownModel[] = [];
        private selectedItem : any = null;
        private peopleSearchInput : string = "";
        private roleTypes : RoleTypeDictionary[] = [];

        async created() {
            if(this.model.id !== 0) {
                this.people = [await FilmsService.getFilmPerson(this.model.personId)]
                this.selectedItem = this.people[0];
            }
            this.roleTypes = await FilmsService.getRoleTypes();
        }

        @Watch('peopleSearchInput')
        async search() {
            if (this.peopleSearchInput){
                this.people = await FilmsService.getFilmPeople(this.peopleSearchInput);
            }
        }

        deleteRole() {
            this.$emit('delete-click');
        }
    }
</script>
