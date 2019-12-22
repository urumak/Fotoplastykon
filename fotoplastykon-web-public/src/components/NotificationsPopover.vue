<template>
    <div>
        <v-menu
                offset-y
                transition="slide-y-transition"
                :close-on-content-click="true"
                :nudge-width="300">
            <template v-slot:activator="{ on }">
                <v-badge class="mr-4" color="red" overlap>
                    <template v-if="notificationsCount > 0"  v-slot:badge>
                        <span>{{ notificationsCount }}</span>
                    </template>
                    <v-icon class="nav-icon" v-on="on">{{ icon }}</v-icon>
                </v-badge>
            </template>
            <div>
                <v-list>
                    <v-list-item
                            v-for="(item, index) in messages"
                            :key="index"
                    >
                        <v-list-item-title>{{ item }}</v-list-item-title>
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

    @Component({})
    export default class NotificationsPopover extends Vue {
        private messages = ["sdddfs", 'daswdas', 'dsadasd'];

        @Prop({default: 0}) private notificationsCount!: number;
        @Prop({default: ''}) private icon!: string;
    }
</script>
