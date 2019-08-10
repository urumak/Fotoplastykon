<template>
  <q-layout view="hHh lpR fFf">

    <q-header elevated class="bg-primary text-purple">
      <q-toolbar>
        <q-toolbar-title>
          <q-avatar>
            <img src="assets/icon.png">
          </q-avatar>
          Fotoplastykon
        </q-toolbar-title>
      </q-toolbar>
    </q-header>

    <q-drawer v-model="left" side="left">
      <div class="absolute-left film-tape-left">
      </div>
      <!-- drawer content -->
    </q-drawer>
    
    <q-drawer v-model="right" side="right">
      <!-- drawer content -->
    </q-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>

  </q-layout>
</template>

<script>
import { openURL } from "quasar";

export default {
  name: "MyLayout",
  data() {
    return {
      leftDrawerOpen: this.$q.platform.is.desktop,
      left: true,
      right: true
    };
  },
  methods: {
    openURL
  }
};
</script>

<style></style>
