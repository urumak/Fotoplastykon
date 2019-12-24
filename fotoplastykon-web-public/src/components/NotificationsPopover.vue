<template>
    <div>
        <v-menu
                offset-y
                transition="slide-y-transition"
                :close-on-content-click="true"
                :nudge-width="300">
            <template v-slot:activator="{ on }">
                <v-badge class="mr-4" color="red" overlap>
                    <template v-if="$store.state.notifications.count > 0"  v-slot:badge>
                        <span>{{ $store.state.notifications.count }}</span>
                    </template>
                    <v-icon class="nav-icon" v-on="on" @click="loadData()">mdi-bell</v-icon>
                </v-badge>
            </template>
            <div>
                <v-list>
                    <v-list-item
                            v-for="(item, index) in notifications"
                            :key="index"
                    >
                        <v-list-item-avatar>
                            <v-img v-if="item.photoUrl != null && item.photoUrl.length != 0" :src='item.photoUrl'></v-img>
                            <v-img v-else src="@/assets/subPhoto.png"></v-img>
                        </v-list-item-avatar>
                        <v-list-item-content>
                            <v-list-item-title>
                                <router-link
                                        class="font-weight-light notification-link"
                                        :to="{ name: 'user-page', params: { id: item.friendId.toString() }}">
                                            {{ item.nameAndSurname }}
                                </router-link></v-list-item-title>
                            <v-list-item-subtitle v-if="isInvitationToAccept(item)">
                                {{ item.nameAndSurname + ' wysłał/a Ci zaproszenie do grona znajomych.' }}
                                <v-row>
                                    <v-btn class="primary" @click="acceptInvitation(item)">Zaakceptuj</v-btn>
                                    <v-btn class="secondary" @click="refuseInvitation(item)">Odrzuć</v-btn>
                                </v-row>
                            </v-list-item-subtitle>
                            <v-list-item-subtitle v-if="isAcceptedInvitation(item)">
                                {{ item.nameAndSurname + ' zaakceptował/a Twoje zaproszenie do grona znajomych.' }}
                            </v-list-item-subtitle>
                            <v-list-item-subtitle v-if="isOldInvitation(item)">
                                {{ item.nameAndSurname + ' wysłał/a Ci zaproszenie do grona znajomych.' }}
                            </v-list-item-subtitle>
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
    import { NotificationModel } from '@/interfaces/notifications';
    import { InfiniteScroll } from '@/interfaces/infiniteScroll';
    import NotificationsService from '@/services/NotificationsService';
    import UsersService from '@/services/UsersService';
    import storeHelper from '@/store/storeHelper';
    import ChatService from '@/services/ChatService';

    @Component({})
    export default class NotificationsPopover extends Vue {
        $refs!: {
            lasNotifications: any;
        };

        private notifications : NotificationModel[] = [];
        private infiniteScroll : InfiniteScroll = new InfiniteScroll(20);

        async created() {
            (this as any).$chatHub.$on('notification-received', this.onNotificationReceived);
            (this as any).$chatHub.$on('refresh-notifications', this.onNotificationsRefreshing);
            await this.refreshNotificationCount();
        }

        beforeDestroy () {
            (this as any).$chatHub.$off('chat-message-received', this.onNotificationReceived);
            (this as any).$chatHub.$off('refresh-notifications', this.onNotificationsRefreshing);
        }

        async loadData() {
            this.scrollTop();

            this.infiniteScroll.setRowsLoaded(0);
            this.notifications = (await NotificationsService.getNotifications(this.infiniteScroll)).items;
            this.infiniteScroll.setRowsLoaded(this.notifications.length);

            await this.readNotifications();
        }

        scrollTop () {
            if(this.$refs.lasNotifications) this.$refs.lasNotifications.scrollTop = 0;
        }

        async acceptInvitation(item: NotificationModel) {
            await UsersService.acceptInvitation(item.friendId);
            item.canAccept = false;
        }

        async refuseInvitation(item: NotificationModel) {
            await UsersService.refuseInvitation(item.friendId);
            item.canAccept = false;
        }

        async readNotifications() {
            await NotificationsService.updateLastReadingDate();
            await this.refreshNotificationCount();
        }

        async onNotificationReceived(notification: NotificationModel) {
            this.notifications.unshift(notification);
            await this.refreshNotificationCount();
        }

        async onNotificationsRefreshing(notificationId: number) {
            this.notifications = this.notifications.filter((x: NotificationModel) => x.id === notificationId);
            await this.refreshNotificationCount();
        }

        async refreshNotificationCount() {
            this.$store.state.notifications.count = await NotificationsService.getNotificationsCount();
        }

        isInvitationToAccept(item: NotificationModel) {
            return item.canAccept;
        }

        isAcceptedInvitation(item: NotificationModel) {
            return item.type === 1;
        }

        isOldInvitation(item: NotificationModel) {
            return item.type === 0 && !item.canAccept;
        }
    }
</script>
