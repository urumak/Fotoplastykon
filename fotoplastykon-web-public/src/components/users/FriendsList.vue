<template>
    <div>
        <v-list>
            <v-list-item
                    v-if="friends.length === 0"
            >
                <v-list-item-content>
                    <v-list-item-title>Brak znajomych</v-list-item-title>
                </v-list-item-content>
            </v-list-item>
            <v-list-item
                    v-for="(item, index) in friends"
                    :key="index"
                    @click="goToDetails(item.id)"
            >
                <v-list-item-avatar>
                    <v-img v-if="item.photoUrl != null && item.photoUrl.length != 0" :src='item.photoUrl'></v-img>
                    <v-img v-else src="@/assets/subPhoto.png"></v-img>
                </v-list-item-avatar>
                <v-list-item-content>
                    <v-list-item-title>{{ item.nameAndSurname }}</v-list-item-title>
                </v-list-item-content>
            </v-list-item>
        </v-list>
        <v-btn v-for="i in pager.totalPages" :key="'p' + i" @click="paginate(i)">{{i}}</v-btn>
        <v-select :items="pageSizeOptions" v-model="pager.pageSize" solo @change="loadData()"></v-select>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import { Prop, Watch } from 'vue-property-decorator';
    import {FriendListItem} from '@/interfaces/shared';
    import {Pager} from '@/interfaces/pager';
    import UsersService from '@/services/UsersService';

    @Component({})
    export default class FriendsList extends Vue {

        private friends : FriendListItem[] = [];
        private pager : Pager = new Pager(1, 2);
        private pageSizeOptions = [2,5,10,20];
        private get id() {
            return this.userId;
        }

        @Prop({default: 0})
        public userId!: number;

        @Watch('$route')
        async reload() {
            this.pager = new Pager(1, 2);
            await this.loadData();
        }

        async created() {
            this.loadData();
        }

        async loadData() {
            if(this.pager.pageIndex > this.pager.totalPages) this.pager.setPageIndex(this.pager.totalPages);
            let response = await UsersService.getList(this.pager, this.id);
            this.friends = response.items;
            this.pager.setTotalRows(response.pager.totalRows);
        }

        async paginate(index: number) {
            this.pager.setPageIndex(index);
            await this.loadData();
        }

        async goToDetails(id: number) {
            await this.$router.push({ name: 'user-page', params: { id: id.toString() }});
        }
    }
</script>
